using OvgBlog.Business.Abstract;
using OvgBlog.Business.Constants;
using OvgBlog.DAL.Abstract;
using OvgBlog.DAL.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OvgBlog.Business.Services
{
    public class CategoryService : ICategoryService
    {
        readonly IEntityRepository<Category> _categoryRepository;
        readonly IEntityRepository<ArticleCategoryRelation> _articleRelationRepository;
        public CategoryService(IEntityRepository<Category> categoryRepository, IEntityRepository<ArticleCategoryRelation> articleRelationRepository)
        {
            _categoryRepository = categoryRepository;
            _articleRelationRepository = articleRelationRepository;
        }

        public async Task<IResult<Category>> CategoryBySeoUrl(string seoUrl)
        {
            if (string.IsNullOrEmpty(seoUrl))
            {
                return new Result<Category>(false,Message.CategoryNotFound);
            }
            var category = await _categoryRepository.Get(x=>x.SeoUrl==seoUrl);
            if (category==null)
            {
                return new Result<Category>(false,Message.CategoryNotFound);
            }
            return new Result<Category>(true, category);
        }


        public async Task<IResult<Category>> Create(Category category)
        {
            if (category==null || string.IsNullOrEmpty(category.Name) || string.IsNullOrEmpty(category.SeoUrl))
            {
                return new Result<Category>(false, Message.ModelNotValid);
            }
            var result = await CategoryBySeoUrl(category.SeoUrl);
            if (result.Success)
            {
                return new Result<Category>(false, Message.SeoUrlAlreadyTaken);
            }
            category.Id = Guid.NewGuid();
            await _categoryRepository.Create(category);
            return new Result<Category>(true,category);
            
        }

        public async Task<IResult<object>> Delete(Guid id)
        {
            if (id==Guid.Empty)
            {
                return new Result<object>(false,Message.CategoryNotFound);
            }
            var categoryEntity = await _categoryRepository.Get(x => x.Id == id && !x.IsDeleted);
            if (categoryEntity==null)
            {
                return new Result<object>(false,Message.CategoryNotFound);
            }         
            var articleEntities = await _articleRelationRepository.GetAll(x => x.CategoryId == categoryEntity.Id && !x.IsDeleted);
            foreach (var item in articleEntities)
            {
                item.IsDeleted = true;
                item.DeletedDate = DateTime.Now;
                await _articleRelationRepository.Update(item);
            }
            categoryEntity.IsDeleted = false;
            categoryEntity.DeletedDate = DateTime.Now;
           await _categoryRepository.Update(categoryEntity);
            return new Result<object>(true);
        }

        public async Task<IResult<IEnumerable<Category>>> GetAll()
        {
            var list = await _categoryRepository.GetAll();
            return new Result<IEnumerable<Category>>(true, list);
        }

        public async Task<IResult<Category>> GetById(Guid id)
        {
            if (id== Guid.Empty)
            {
                return new Result<Category>(false, Message.IdIsNotValid);
            }
            var categoryEntity = await _categoryRepository.Get(x => x.Id == id && !x.IsDeleted);
            if (categoryEntity==null)
            {
                return new Result<Category>(false,Message.IdIsNotValid);
            }
            return new Result<Category>(true,categoryEntity);
        }

        public async Task<IResult<Category>> Update(Category category)
        {
            if (category == null || string.IsNullOrEmpty(category.Name) || string.IsNullOrEmpty(category.SeoUrl))
            {
                return new Result<Category>(false, Message.ModelNotValid);
            }
            var categoryEntity = await _categoryRepository.Get(x => x.Id == category.Id && !x.IsDeleted);
            if (categoryEntity==null)
            {
                return new Result<Category>(false, Message.CategoryNotFound);
            }
            categoryEntity = await _categoryRepository.Update(category);
            return new Result<Category>(true, categoryEntity);
        }
    }
}
