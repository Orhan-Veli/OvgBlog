﻿using OvgBlog.Business.Abstract;
using OvgBlog.Business.Constants;
using OvgBlog.DAL.Abstract;
using OvgBlog.DAL.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace OvgBlog.Business.Services
{
    public class ArticleService(
        IEntityRepository<Article> articleRepository,
        IEntityRepository<User> userRepository,
        IEntityRepository<ArticleTagRelation> tagRelationRepository,
        IEntityRepository<ArticleCategoryRelation> categoryRelationRepository)
        : IArticleService
    {
        public async Task<IResult<Article>> Create(Article article, CancellationToken cancellationToken)
        {
            List<ArticleTagRelation> tempArticleRelation = new List<ArticleTagRelation>();
            if (article == null || string.IsNullOrEmpty(article.Title) || string.IsNullOrEmpty(article.SeoUrl))
            {
                return new Result<Article>(false, Message.ModelNotValid);
            }
            article.UserId = (await userRepository.Get(cancellationToken)).Id;
            var userEntity = await userRepository.Get(cancellationToken, x => x.Id == article.UserId && !x.IsDeleted);
            if (userEntity == null)
            {
                return new Result<Article>(false, Message.UserNotFound);
            }
            article.Id = Guid.NewGuid();
            article.CreatedDate = DateTime.Now;
            await articleRepository.Create(article, cancellationToken);
            
            foreach (var item in article.ArticleTagRelations)
            {
                tempArticleRelation.Add(item);
            }
            foreach (var tagmodel in tempArticleRelation.Select(item => new ArticleTagRelation
                     {
                         Id = Guid.NewGuid(),
                         TagId = item.TagId,
                         ArticleId = article.Id,
                         CreatedDate = DateTime.Now,
                         IsActive = true
                     }))
            {
                await tagRelationRepository.Create(tagmodel, cancellationToken);
            }
            tempArticleRelation.Clear();
            await categoryRelationRepository.Create(new ArticleCategoryRelation { Id = Guid.NewGuid(), ArticleId = article.Id, CategoryId = article.ArticleCategoryRelations.FirstOrDefault().CategoryId, CreatedDate = DateTime.Now }, cancellationToken);

            return new Result<Article>(true, article);
        }

        public async Task<IResult<object>> Delete(Guid id, CancellationToken cancellationToken)
        {
            if (id == Guid.Empty)
            {
                return new Result<object>(false, Message.IdIsNotValid);
            }
            var articleEntity = await articleRepository.Get(cancellationToken, x => x.Id == id && !x.IsDeleted);
            if (articleEntity == null)
            {
                return new Result<object>(false, Message.ArticleIsNotFound);
            }
            var tagRelationEntities = await tagRelationRepository.GetAll(cancellationToken, x => x.ArticleId == articleEntity.Id && !x.IsDeleted);
            foreach (var item in tagRelationEntities)
            {
                item.IsDeleted = true;
                item.DeletedDate = DateTime.Now;
                await tagRelationRepository.Update(item, cancellationToken);
            }
            var categoryEntities = await categoryRelationRepository.GetAll(cancellationToken, x => x.ArticleId == articleEntity.Id && !x.IsDeleted);
            foreach (var item in categoryEntities)
            {
                item.IsDeleted = true;
                item.DeletedDate = DateTime.Now;
                await categoryRelationRepository.Update(item, cancellationToken);
            }
            articleEntity.IsDeleted = true;
            articleEntity.DeletedDate = DateTime.Now;
            await articleRepository.Update(articleEntity, cancellationToken);
            return new Result<object>(true);
        }

        public async Task<IResult<IEnumerable<Article>>> GetAll(CancellationToken cancellationToken)
        {
            var list = await articleRepository.GetAll(cancellationToken, x => !x.IsDeleted, x => x.User);
            return new Result<IEnumerable<Article>>(true, list);
        }

        public async Task<IResult<IEnumerable<Article>>> GetByCategoryId(Guid categoryId, CancellationToken cancellationToken)
        {
            if (categoryId == Guid.Empty)
            {
                return new Result<IEnumerable<Article>>(false, Message.IdIsNotValid);
            }
            
            var result = await articleRepository.GetAll(cancellationToken, x => x.ArticleCategoryRelations.Any(c => c.CategoryId == categoryId));
            return new Result<IEnumerable<Article>>(true, result);
        }

        public async Task<IResult<Article>> GetById(Guid id, CancellationToken cancellationToken)
        {
            if (id == Guid.Empty)
            {
                return new Result<Article>(false, Message.IdIsNotValid);
            }
            var expressions = new List<Expression<Func<Article, object>>>();
            expressions.Add(x => x.Comments);
            expressions.Add(x => x.ArticleCategoryRelations);
            var articleEntity = await articleRepository.Get(cancellationToken, x => x.Id == id && !x.IsDeleted, expressions);
            if (articleEntity == null)
            {
                return new Result<Article>(false, Message.ArticleIsNotFound);
            }
            return new Result<Article>(true, articleEntity);
        }

        public async Task<IResult<Article>> GetBySeoUrl(string seoUrl, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(seoUrl))
            {
                return new Result<Article>(false, Message.FieldIsNotValid);
            }
            var expressions = new List<Expression<Func<Article, object>>>();
            expressions.Add(x => x.Comments);
            expressions.Add(x => x.ArticleCategoryRelations);
            expressions.Add(x => x.ArticleTagRelations);          
            var articleEntity = await articleRepository.Get(cancellationToken, x => x.SeoUrl == seoUrl && !x.IsDeleted, expressions);
            if (articleEntity == null)
            {
                return new Result<Article>(false, Message.ArticleIsNotFound);
            }            
            return new Result<Article>(true, articleEntity);
        }
        public async Task<IResult<Article>> Update(Article article, CancellationToken cancellationToken)
        {
            if (article == null || string.IsNullOrEmpty(article.Title) || string.IsNullOrEmpty(article.SeoUrl))
            {
                return new Result<Article>(false, Message.ModelNotValid);
            }
            var articleEntity = await articleRepository.Get(cancellationToken, x => x.Id == article.Id && !x.IsDeleted);
            if (articleEntity == null)
            {
                return new Result<Article>(false, Message.ArticleIsNotFound);
            }
            var userEntity = await userRepository.Get(cancellationToken, x => x.Id == article.UserId && !x.IsDeleted);
            if (userEntity == null)
            {
                return new Result<Article>(false, Message.UserNotFound);
            }
            article.UpdatedDate = DateTime.Now;
            articleEntity = await articleRepository.Update(article, cancellationToken);
            return new Result<Article>(true, articleEntity);
        }
    }
}
