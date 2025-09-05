using OvgBlog.Business.Abstract;
using OvgBlog.Business.Constants;
using OvgBlog.DAL.Abstract;
using OvgBlog.DAL.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Mapster;
using OvgBlog.Business.Dto;
using OvgBlog.DAL.Extensions;
using OvgBlog.DAL.Helpers;

namespace OvgBlog.Business.Services
{
    public class CategoryService(
        IEntityRepository<Category> categoryRepository,
        IEntityRepository<ArticleCategoryRelation> articleRelationRepository)
        : ICategoryService
    {
        public async Task<IResult<CategoryDto>> GetCategoryBySeoUrlAsync(string seoUrl, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(seoUrl))
            {
                return new Result<CategoryDto>(false, ErrorMessages.CategoryNotFound);
            }
            
            var category = await categoryRepository.GetAsync(cancellationToken, x => x.SeoUrl == seoUrl);
            if (category == null)
            {
               throw new OvgBlogException(ErrorMessages.CategoryNotFound);
            }
            
            var dto = category.Adapt<CategoryDto>();
            return new Result<CategoryDto>(true, dto);
        }


        public async Task<IResult<CategoryDto>> CreateAsync(CreateCategoryDto dto, CancellationToken cancellationToken)
        {
            if (dto == null)
            {
                throw new OvgBlogException(ErrorMessages.CategoryNotFound);
            }
            
            var result = await GetCategoryBySeoUrlAsync(dto.SeoUrl, cancellationToken);
            if (result.IsSuccess)
            {
                throw new OvgBlogException(ErrorMessages.SeoUrlAlreadyTaken);
            }

            var entity = dto.Adapt<Category>();
            entity.CreatedDate = DateTime.UtcNow;
            
            await categoryRepository.CreateAsync(entity, cancellationToken);
            
            var resultEntity = entity.Adapt<CategoryDto>();
            return new Result<CategoryDto>(true, resultEntity);
        }

        public async Task<IResult<object>> DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            if (id == Guid.Empty)
            {
                throw new OvgBlogException(ErrorMessages.IdIsNotValid);
            }
            
            var categoryEntity = await categoryRepository.GetAsync(cancellationToken, x => x.Id == id && !x.IsDeleted);
            if (categoryEntity == null)
            {
               throw new OvgBlogException(ErrorMessages.CategoryNotFound);
            }
            
            var articleEntities = await articleRelationRepository.GetAllAsync(cancellationToken, x => x.CategoryId == categoryEntity.Id && !x.IsDeleted);
            foreach (var item in articleEntities.IfNullOrEmpty())
            {
                item.IsDeleted = true;
                item.DeletedDate = DateTime.UtcNow;
                await articleRelationRepository.UpdateAsync(item, cancellationToken);
            }
            
            categoryEntity.IsDeleted = true;
            categoryEntity.DeletedDate = DateTime.UtcNow;
            
            await categoryRepository.UpdateAsync(categoryEntity, cancellationToken);
            return new Result<object>(true);
        }

        public async Task<IResult<CategoryDto>> GetCategoryIdByName(string name, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new OvgBlogException(ErrorMessages.NameNotFound);
            }
            
            var entity = await categoryRepository.GetAsync(cancellationToken, x => x.Name == name && !x.IsDeleted);
            if (entity == null)
            {
                throw new OvgBlogException(ErrorMessages.CategoryNotFound);
            }
            
            var result = entity.Adapt<CategoryDto>();
            return new Result<CategoryDto>(true, result);
        }

        public async Task<IResult<IEnumerable<CategoryDto>>> GetAllAsync(CategoryFilterDto filterDto, CancellationToken cancellationToken)
        {
            var entities = await categoryRepository.GetAllAsync(cancellationToken, x => !x.IsDeleted && filterDto.Ids.Contains(x.Id));
            
            var dto = entities.Adapt<IEnumerable<CategoryDto>>();
            
            return new Result<IEnumerable<CategoryDto>>(true, dto);
        }

        public async Task<IResult<CategoryDto>> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            if (id == Guid.Empty)
            {
               throw new OvgBlogException(ErrorMessages.IdIsNotValid);
            }
            
            var entity = await categoryRepository.GetAsync(cancellationToken, x => x.Id == id && !x.IsDeleted);
            if (entity == null)
            {
                throw new OvgBlogException(ErrorMessages.CategoryNotFound);
            }
            
            var dto = entity.Adapt<CategoryDto>();
            return new Result<CategoryDto>(true, dto);
        }

        public async Task<IResult<CategoryDto>> UpdateAsync(UpdateCategoryDto category, CancellationToken cancellationToken)
        {
            if (category == null || string.IsNullOrEmpty(category.Name) || string.IsNullOrEmpty(category.SeoUrl))
            {
                return new Result<CategoryDto>(false, ErrorMessages.ModelNotValid);
            }
            
            var entity = await categoryRepository.GetAsync(cancellationToken, x => x.Id == category.Id && !x.IsDeleted);
            if (entity == null)
            {
                return new Result<CategoryDto>(false, ErrorMessages.CategoryNotFound);
            }
            
            entity.UpdatedDate = DateTime.UtcNow;
            entity = await categoryRepository.UpdateAsync(entity, cancellationToken);
            
            var dto = entity.Adapt<CategoryDto>();
            return new Result<CategoryDto>(true, dto);
        }
    }
}
