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
        public async Task<IResult<Category>> CategoryBySeoUrlAsync(string seoUrl, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(seoUrl))
            {
                return new Result<Category>(false, ErrorMessages.CategoryNotFound);
            }
            var category = await categoryRepository.GetAsync(cancellationToken, x => x.SeoUrl == seoUrl);
            if (category == null)
            {
                return new Result<Category>(false, ErrorMessages.CategoryNotFound);
            }
            return new Result<Category>(true, category);
        }


        public async Task<IResult<Category>> CreateAsync(Category category, CancellationToken cancellationToken)
        {
            if (category == null || string.IsNullOrEmpty(category.Name) || string.IsNullOrEmpty(category.SeoUrl))
            {
                return new Result<Category>(false, ErrorMessages.ModelNotValid);
            }
            var result = await CategoryBySeoUrlAsync(category.SeoUrl, cancellationToken);
            if (result.IsSuccess)
            {
                return new Result<Category>(false, ErrorMessages.SeoUrlAlreadyTaken);
            }
            category.Id = Guid.NewGuid();
            category.CreatedDate = DateTime.Now;
            await categoryRepository.CreateAsync(category, cancellationToken);
            return new Result<Category>(true, category);
        }

        public async Task<IResult<object>> DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            if (id == Guid.Empty)
            {
                return new Result<object>(false, ErrorMessages.CategoryNotFound);
            }
            var categoryEntity = await categoryRepository.GetAsync(cancellationToken, x => x.Id == id && !x.IsDeleted);
            if (categoryEntity == null)
            {
                return new Result<object>(false, ErrorMessages.CategoryNotFound);
            }
            var articleEntities = await articleRelationRepository.GetAllAsync(cancellationToken, x => x.CategoryId == categoryEntity.Id && !x.IsDeleted);
            foreach (var item in articleEntities)
            {
                item.IsDeleted = true;
                item.DeletedDate = DateTime.Now;
                await articleRelationRepository.UpdateAsync(item, cancellationToken);
            }
            categoryEntity.IsDeleted = true;
            categoryEntity.DeletedDate = DateTime.Now;
            await categoryRepository.UpdateAsync(categoryEntity, cancellationToken);
            return new Result<object>(true);
        }

        public async Task<IResult<Category>> FindCategoryIdByName(string name, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(name))
            {
                return new Result<Category>(false, ErrorMessages.NameNotFound);
            }
            var category = await categoryRepository.GetAsync(cancellationToken, x => x.Name == name && !x.IsDeleted);
            if (category == null)
            {
                return new Result<Category>(false, ErrorMessages.CategoryNotFound);
            }
            return new Result<Category>(true, category);
        }

        public async Task<IResult<IEnumerable<Category>>> GetAllAsync(CancellationToken cancellationToken)
        {
            var list = await categoryRepository.GetAllAsync(cancellationToken, x => !x.IsDeleted);
            return new Result<IEnumerable<Category>>(true, list);
        }

        public async Task<IResult<Category>> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            if (id == Guid.Empty)
            {
                return new Result<Category>(false, ErrorMessages.IdIsNotValid);
            }
            var categoryEntity = await categoryRepository.GetAsync(cancellationToken, x => x.Id == id && !x.IsDeleted);
            if (categoryEntity == null)
            {
                return new Result<Category>(false, ErrorMessages.IdIsNotValid);
            }
            return new Result<Category>(true, categoryEntity);
        }

        public async Task<IResult<Category>> UpdateAsync(Category category, CancellationToken cancellationToken)
        {
            if (category == null || string.IsNullOrEmpty(category.Name) || string.IsNullOrEmpty(category.SeoUrl))
            {
                return new Result<Category>(false, ErrorMessages.ModelNotValid);
            }
            var categoryEntity = await categoryRepository.GetAsync(cancellationToken, x => x.Id == category.Id && !x.IsDeleted);
            if (categoryEntity == null)
            {
                return new Result<Category>(false, ErrorMessages.CategoryNotFound);
            }
            category.UpdatedDate = DateTime.Now;
            categoryEntity = await categoryRepository.UpdateAsync(category, cancellationToken);
            return new Result<Category>(true, categoryEntity);
        }
    }
}
