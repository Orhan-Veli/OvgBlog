using OvgBlog.Business.Abstract;
using OvgBlog.Business.Constants;
using OvgBlog.DAL.Abstract;
using OvgBlog.DAL.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace OvgBlog.Business.Services
{
    public class CategoryService(
        IEntityRepository<Category> categoryRepository,
        IEntityRepository<ArticleCategoryRelation> articleRelationRepository)
        : ICategoryService
    {
        public async Task<IResult<Category>> CategoryBySeoUrl(string seoUrl, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(seoUrl))
            {
                return new Result<Category>(false, Message.CategoryNotFound);
            }
            var category = await categoryRepository.Get(cancellationToken, x => x.SeoUrl == seoUrl);
            if (category == null)
            {
                return new Result<Category>(false, Message.CategoryNotFound);
            }
            return new Result<Category>(true, category);
        }


        public async Task<IResult<Category>> Create(Category category, CancellationToken cancellationToken)
        {
            if (category == null || string.IsNullOrEmpty(category.Name) || string.IsNullOrEmpty(category.SeoUrl))
            {
                return new Result<Category>(false, Message.ModelNotValid);
            }
            var result = await CategoryBySeoUrl(category.SeoUrl, cancellationToken);
            if (result.IsSuccess)
            {
                return new Result<Category>(false, Message.SeoUrlAlreadyTaken);
            }
            category.Id = Guid.NewGuid();
            category.CreatedDate = DateTime.Now;
            await categoryRepository.Create(category, cancellationToken);
            return new Result<Category>(true, category);
        }

        public async Task<IResult<object>> Delete(Guid id, CancellationToken cancellationToken)
        {
            if (id == Guid.Empty)
            {
                return new Result<object>(false, Message.CategoryNotFound);
            }
            var categoryEntity = await categoryRepository.Get(cancellationToken, x => x.Id == id && !x.IsDeleted);
            if (categoryEntity == null)
            {
                return new Result<object>(false, Message.CategoryNotFound);
            }
            var articleEntities = await articleRelationRepository.GetAll(cancellationToken, x => x.CategoryId == categoryEntity.Id && !x.IsDeleted);
            foreach (var item in articleEntities)
            {
                item.IsDeleted = true;
                item.DeletedDate = DateTime.Now;
                await articleRelationRepository.Update(item, cancellationToken);
            }
            categoryEntity.IsDeleted = true;
            categoryEntity.DeletedDate = DateTime.Now;
            await categoryRepository.Update(categoryEntity, cancellationToken);
            return new Result<object>(true);
        }

        public async Task<IResult<Category>> FindCategoryIdByName(string name, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(name))
            {
                return new Result<Category>(false, Message.NameNotFound);
            }
            var category = await categoryRepository.Get(cancellationToken, x => x.Name == name && !x.IsDeleted);
            if (category == null)
            {
                return new Result<Category>(false, Message.CategoryNotFound);
            }
            return new Result<Category>(true, category);
        }

        public async Task<IResult<IEnumerable<Category>>> GetAll(CancellationToken cancellationToken)
        {
            var list = await categoryRepository.GetAll(cancellationToken, x => !x.IsDeleted);
            return new Result<IEnumerable<Category>>(true, list);
        }

        public async Task<IResult<Category>> GetById(Guid id, CancellationToken cancellationToken)
        {
            if (id == Guid.Empty)
            {
                return new Result<Category>(false, Message.IdIsNotValid);
            }
            var categoryEntity = await categoryRepository.Get(cancellationToken, x => x.Id == id && !x.IsDeleted);
            if (categoryEntity == null)
            {
                return new Result<Category>(false, Message.IdIsNotValid);
            }
            return new Result<Category>(true, categoryEntity);
        }

        public async Task<IResult<Category>> Update(Category category, CancellationToken cancellationToken)
        {
            if (category == null || string.IsNullOrEmpty(category.Name) || string.IsNullOrEmpty(category.SeoUrl))
            {
                return new Result<Category>(false, Message.ModelNotValid);
            }
            var categoryEntity = await categoryRepository.Get(cancellationToken, x => x.Id == category.Id && !x.IsDeleted);
            if (categoryEntity == null)
            {
                return new Result<Category>(false, Message.CategoryNotFound);
            }
            category.UpdatedDate = DateTime.Now;
            categoryEntity = await categoryRepository.Update(category, cancellationToken);
            return new Result<Category>(true, categoryEntity);
        }
    }
}
