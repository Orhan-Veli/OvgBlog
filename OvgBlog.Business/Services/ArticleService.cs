using OvgBlog.Business.Abstract;
using OvgBlog.Business.Constants;
using OvgBlog.DAL.Abstract;
using OvgBlog.DAL.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Threading.Tasks;

namespace OvgBlog.Business.Services
{
    public class ArticleService : IArticleService
    {
        readonly IEntityRepository<Article> _articleRepository;
        readonly IEntityRepository<User> _userRepository;
        readonly IEntityRepository<ArticleTagRelation> _tagRelationRepository;
        readonly IEntityRepository<ArticleCategoryRelation> _categoryRelationRepository;
        public ArticleService(IEntityRepository<Article> articleRepository, IEntityRepository<User> userRepository,
            IEntityRepository<ArticleTagRelation> tagRelationRepository, IEntityRepository<ArticleCategoryRelation> categoryRelationRepository)
        {
            _articleRepository = articleRepository;
            _userRepository = userRepository;
            _tagRelationRepository = tagRelationRepository;
            _categoryRelationRepository = categoryRelationRepository;
        }
        public async Task<IResult<Article>> Create(Article article)
        {
            List<ArticleTagRelation> tempArticleRelation = new List<ArticleTagRelation>();
            if (article == null || string.IsNullOrEmpty(article.Title) || string.IsNullOrEmpty(article.SeoUrl))
            {
                return new Result<Article>(false, Message.ModelNotValid);
            }
            article.UserId = (await _userRepository.Get()).Id;
            var userEntity = await _userRepository.Get(x => x.Id == article.UserId && !x.IsDeleted);
            if (userEntity == null)
            {
                return new Result<Article>(false, Message.UserNotFound);
            }
            article.Id = Guid.NewGuid();
            article.CreatedDate = DateTime.Now;
            await _articleRepository.Create(article);
            
            foreach (var item in article.ArticleTagRelations)
            {
                tempArticleRelation.Add(item);
            }
            foreach (var item in tempArticleRelation)
            {
                var tagmodel = new ArticleTagRelation
                {
                    Id = Guid.NewGuid(),
                    TagId = item.TagId,
                    ArticleId = article.Id,
                    CreatedDate = DateTime.Now,
                    IsActive = true
                };
                await _tagRelationRepository.Create(tagmodel);
            }
            tempArticleRelation.Clear();
            await _categoryRelationRepository.Create(new ArticleCategoryRelation { Id = Guid.NewGuid(), ArticleId = article.Id, CategoryId = article.ArticleCategoryRelations.FirstOrDefault().CategoryId, CreatedDate = DateTime.Now });

            return new Result<Article>(true, article);
        }

        public async Task<IResult<object>> Delete(Guid id)
        {
            if (id == Guid.Empty)
            {
                return new Result<object>(false, Message.IdIsNotValid);
            }
            var articleEntity = await _articleRepository.Get(x => x.Id == id && !x.IsDeleted);
            if (articleEntity == null)
            {
                return new Result<object>(false, Message.ArticleIsNotFound);
            }
            var tagRelationEntities = await _tagRelationRepository.GetAll(x => x.ArticleId == articleEntity.Id && !x.IsDeleted);
            foreach (var item in tagRelationEntities)
            {
                item.IsDeleted = true;
                item.DeletedDate = DateTime.Now;
                await _tagRelationRepository.Update(item);
            }
            var categoryEntities = await _categoryRelationRepository.GetAll(x => x.ArticleId == articleEntity.Id && !x.IsDeleted);
            foreach (var item in categoryEntities)
            {
                item.IsDeleted = true;
                item.DeletedDate = DateTime.Now;
                await _categoryRelationRepository.Update(item);
            }
            articleEntity.IsDeleted = true;
            articleEntity.DeletedDate = DateTime.Now;
            await _articleRepository.Update(articleEntity);
            return new Result<object>(true);
        }

        public async Task<IResult<IEnumerable<Article>>> GetAll()
        {
            var list = await _articleRepository.GetAll(x => !x.IsDeleted, x => x.User);
            return new Result<IEnumerable<Article>>(true, list);
        }

        public async Task<IResult<IEnumerable<Article>>> GetByCategoryId(Guid categoryId)
        {
            if (categoryId == Guid.Empty)
            {
                return new Result<IEnumerable<Article>>(false, Message.IdIsNotValid);
            }
            var result = await _articleRepository.GetAll(x => x.ArticleCategoryRelations.Any(c => c.CategoryId == categoryId));
            return new Result<IEnumerable<Article>>(true, result);
        }

        public async Task<IResult<Article>> GetById(Guid id)
        {
            if (id == Guid.Empty)
            {
                return new Result<Article>(false, Message.IdIsNotValid);
            }
            var expressions = new List<Expression<Func<Article, object>>>();
            expressions.Add(x => x.Comments);
            expressions.Add(x => x.ArticleCategoryRelations);
            var articleEntity = await _articleRepository.Get((x => x.Id == id && !x.IsDeleted), expressions);
            if (articleEntity == null)
            {
                return new Result<Article>(false, Message.ArticleIsNotFound);
            }
            return new Result<Article>(true, articleEntity);
        }

        public async Task<IResult<Article>> GetBySeoUrl(string seoUrl)
        {
            if (string.IsNullOrEmpty(seoUrl))
            {
                return new Result<Article>(false, Message.FieldIsNotValid);
            }
            var expressions = new List<Expression<Func<Article, object>>>();
            expressions.Add(x => x.Comments);
            expressions.Add(x => x.ArticleCategoryRelations);
            expressions.Add(x => x.ArticleTagRelations);          
            var articleEntity = await _articleRepository.Get((x => x.SeoUrl == seoUrl && !x.IsDeleted), expressions);
            if (articleEntity == null)
            {
                return new Result<Article>(false, Message.ArticleIsNotFound);
            }            
            return new Result<Article>(true, articleEntity);
        }
        public async Task<IResult<Article>> Update(Article article)
        {
            if (article == null || string.IsNullOrEmpty(article.Title) || string.IsNullOrEmpty(article.SeoUrl) || article.UserId == Guid.Empty || article.Id == Guid.Empty)
            {
                return new Result<Article>(false, Message.ModelNotValid);
            }
            var articleEntity = await _articleRepository.Get(x => x.Id == article.Id && !x.IsDeleted);
            if (articleEntity == null)
            {
                return new Result<Article>(false, Message.ArticleIsNotFound);
            }
            var userEntity = await _userRepository.Get(x => x.Id == article.UserId && !x.IsDeleted);
            if (userEntity == null)
            {
                return new Result<Article>(false, Message.UserNotFound);
            }
            article.UpdatedDate = DateTime.Now;
            articleEntity = await _articleRepository.Update(article);
            return new Result<Article>(true, articleEntity);
        }
    }
}
