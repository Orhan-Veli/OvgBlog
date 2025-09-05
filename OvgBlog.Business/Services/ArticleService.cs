using OvgBlog.Business.Abstract;
using OvgBlog.Business.Constants;
using OvgBlog.DAL.Abstract;
using OvgBlog.DAL.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Mapster;
using Microsoft.EntityFrameworkCore;
using OvgBlog.Business.Dto;
using OvgBlog.DAL;
using OvgBlog.DAL.Helpers;
using OvgBlog.UI.Extentions;

namespace OvgBlog.Business.Services
{
    public class ArticleService(
        IArticleRepository articleRepository,
        IArticleTagRelationRepository articleTagRelationRepository,
        IArticleCategoryRelationRepository articleCategoryRelationRepository,
        ITagRepository tagRepository,
        OvgBlogContext context)
        : IArticleService
    {
        public async Task<IResult<ArticleDto>> CreateAsync(CreateArticleDto dto, CancellationToken cancellationToken)
        {
            if (dto == null)
            {
                return new Result<ArticleDto>(false, ErrorMessages.DtoCannotBeNull, null);
            }
               
            var article = new Article
            {
                Id = Guid.NewGuid(),
                SeoUrl = dto.SeoUrl.ReplaceSeoUrl(),
                Title = dto.Title,
                IsDeleted = false,
                CreatedDate = DateTime.UtcNow,
                UserId = dto.UserId,
                Body = dto.Body,
                ArticleCategoryRelations = [],
                ArticleTagRelations = []
            };
            
            var strategy = context.Database.CreateExecutionStrategy();
            await strategy.ExecuteAsync(async () =>
            {
                await using var transaction = await context.Database.BeginTransactionAsync(cancellationToken);
                try
                {
                    await ValidateArticleCreateAsync(dto, cancellationToken);
                    await articleRepository.CreateAsync(article, cancellationToken);
                    await CreateTagRelationsAsync(dto, article, cancellationToken);
                    await CreateFileAsync(dto, cancellationToken);
                    await CreateCategoryRelationAsync(dto, article, cancellationToken);
                    
                    await transaction.CommitAsync(cancellationToken);
                }
                catch (Exception e)
                {
                    throw new OvgBlogException(ErrorMessages.ArticleCreateError, e.Message);
                }
            });
            
            var articleDto = article.Adapt<ArticleDto>();
            return new Result<ArticleDto>(true, articleDto);
        }
        
        public async Task<IResult<object>> DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            if (id == Guid.Empty)
            {
                return new Result<object>(false, ErrorMessages.IdIsNotValid);
            }
            
            var strategy = context.Database.CreateExecutionStrategy();
            await strategy.ExecuteAsync(async () =>
            {
                await using var transaction = await context.Database.BeginTransactionAsync(cancellationToken);
                try
                {
                    var articleEntity = await articleRepository.GetAsync(cancellationToken, x => x.Id == id && !x.IsDeleted);
                    if (articleEntity == null)
                    {
                        throw new OvgBlogException(ErrorMessages.ArticleIsNotFound);
                    }

                    var tagRelationEntities = await articleTagRelationRepository.GetAllAsync(cancellationToken, x => x.ArticleId == articleEntity.Id && !x.IsDeleted);
                    foreach (var item in tagRelationEntities)
                    {
                        item.IsDeleted = true;
                        item.DeletedDate = DateTime.UtcNow;
                    }
                    
                    var categoryEntities = await articleCategoryRelationRepository.GetAllAsync(cancellationToken, x => x.ArticleId == articleEntity.Id && !x.IsDeleted);
                    foreach (var item in categoryEntities)
                    {
                        item.IsDeleted = true;
                        item.DeletedDate = DateTime.UtcNow;
                    }

                    articleEntity.IsDeleted = true;
                    articleEntity.DeletedDate = DateTime.UtcNow;
                    
                    await articleRepository.UpdateAsync(articleEntity, cancellationToken);
                    await articleCategoryRelationRepository.UpdateBulkArticleCategoryRelationsAsync(categoryEntities, cancellationToken);
                    await articleTagRelationRepository.UpdateBulkArticleTagRelationsAsync(tagRelationEntities, cancellationToken);
                    
                    await transaction.CommitAsync(cancellationToken);
                    return new Result<object>(true);
                }
                catch (Exception e)
                {
                    throw new OvgBlogException(ErrorMessages.ArticleDeleteError, e.Message);
                }
            });

            return new Result<object>(true);
        }

        public async Task<IResult<List<ArticleDto>>> GetAllAsync(ArticleFilterDto filterDto, CancellationToken cancellationToken)
        {
            var entities = await articleRepository.GetAllAsync(cancellationToken, x => !x.IsDeleted && filterDto.Ids.Contains(x.Id) && filterDto.UserIds.Contains(x.UserId), x => x.User);
            var dto = entities.Adapt<List<ArticleDto>>();
            
            return new Result<List<ArticleDto>>(true, dto);
        }

        public async Task<IResult<List<ArticleDto>>> GetByCategoryIdAsync(Guid categoryId,
            CancellationToken cancellationToken)
        {
            if (categoryId == Guid.Empty)
            {
                throw new OvgBlogException(ErrorMessages.CategoryIsNotValid);
            }

            var result = await articleRepository.GetAllAsync(cancellationToken,
                x => x.ArticleCategoryRelations.Any(c => c.CategoryId == categoryId));
            
            var dto = result.Adapt<List<ArticleDto>>();
            return new Result<List<ArticleDto>>(true, dto);
        }

        public async Task<IResult<ArticleDto>> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            if (id == Guid.Empty)
            {
                throw new OvgBlogException(ErrorMessages.IdIsNotValid);
            }

            var expressions = new List<Expression<Func<Article, object>>>();
            expressions.Add(x => x.Comments);
            expressions.Add(x => x.ArticleCategoryRelations);
            var articleEntity =
                await articleRepository.GetAsync(cancellationToken, x => x.Id == id && !x.IsDeleted, expressions);
            
            var dto = articleEntity?.Adapt<ArticleDto>() ?? throw new OvgBlogException(ErrorMessages.ArticleIsNotFound);
            return new Result<ArticleDto>(true, dto);
        }

        public async Task<IResult<ArticleDto>> GetBySeoUrlAsync(string seoUrl, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(seoUrl))
            {
                throw new OvgBlogException(ErrorMessages.IdIsNotValid);
            }

            var expressions = new List<Expression<Func<Article, object>>>
            {
                x => x.Comments,
                x => x.ArticleCategoryRelations,
                x => x.ArticleTagRelations
            };

            var articleEntity = await articleRepository.GetAsync(cancellationToken,
                x => x.SeoUrl == seoUrl && !x.IsDeleted, expressions);
            
            var dto = articleEntity?.Adapt<ArticleDto>() ?? throw new OvgBlogException(ErrorMessages.ArticleIsNotFound);
            return new Result<ArticleDto>(true, dto);
        }

        public async Task<IResult<ArticleDto>> UpdateAsync(UpdateArticleDto dto, CancellationToken cancellationToken)
        {
            if(dto == null) throw new OvgBlogException(ErrorMessages.DtoCannotBeNull);
            
            var entity= await articleRepository.GetAsync(cancellationToken, x => x.Id == dto.Id && !x.IsDeleted);
            if (entity == null)
            {
                throw new OvgBlogException(ErrorMessages.ArticleIsNotFound);
            }

            entity = dto.Adapt<Article>();
            entity.UpdatedDate = DateTime.UtcNow;
            var result  = await articleRepository.UpdateAsync(entity, cancellationToken);
            
            var resultDto = result.Adapt<ArticleDto>();
            return new Result<ArticleDto>(true, resultDto);
        }

        #region Private methods

        private async Task CreateFileAsync(CreateArticleDto dto, CancellationToken cancellationToken)
        {
            var fileName = Path.GetFileNameWithoutExtension(dto.FileImageUrl.FileName);
            var extension = Path.GetExtension(dto.FileImageUrl.FileName);
            var path = Path.Combine(dto.RootPath + ImageUrlConstants.Uploads, fileName + extension);

            await using var fileStream = new FileStream(path, FileMode.Create);
            await dto.FileImageUrl.CopyToAsync(fileStream, cancellationToken);
        }

        private async Task CreateTagRelationsAsync(CreateArticleDto dto, Article article,
            CancellationToken cancellationToken)
        {
            var tagNames = dto.TagName.Split(",").ToList();
            var tags = await tagRepository.GetAllTagsByNamesAsync(tagNames, cancellationToken);
            var creatableTags = new List<Tag>();
            var creatableTagRelations = new List<ArticleTagRelation>();

            foreach (var tag in tagNames)
            {
                var tagRelation = new ArticleTagRelation
                {
                    Id = Guid.NewGuid(),
                    ArticleId = article.Id,
                    CreatedDate = DateTime.UtcNow,
                    IsActive = true
                };

                var entity = tags.FirstOrDefault(x => x.Name == tag);
                if (entity != null)
                {
                    tagRelation.TagId = entity.Id;
                }
                else
                {
                    var tagId = Guid.NewGuid();
                    tagRelation.TagId = tagId;
                    creatableTags.Add(new Tag()
                    {
                        Id = tagId,
                        Name = tag,
                        SeoUrl = tag.ReplaceSeoUrl(),
                        CreatedDate = DateTime.UtcNow,
                        IsActive = true
                    });
                    creatableTagRelations.Add(tagRelation);
                }
            }

            await tagRepository.CreateBulkAsync(creatableTags, cancellationToken);
            await articleTagRelationRepository.CreateBulkArticleTagRelationAsync(creatableTagRelations,
                cancellationToken);
        }

        private async Task CreateCategoryRelationAsync(CreateArticleDto dto, Article article,
            CancellationToken cancellationToken)
        {
            var entity = new ArticleCategoryRelation
            {
                Id = Guid.NewGuid(), 
                ArticleId = article.Id,
                CategoryId = dto.CategoryId,
                CreatedDate = DateTime.UtcNow,
                IsActive = true
            };
            
            await articleCategoryRelationRepository.CreateAsync(entity, cancellationToken);   
        }
        
        private async Task ValidateArticleCreateAsync(CreateArticleDto dto, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(dto.Title) || string.IsNullOrEmpty(dto.SeoUrl))
            {
                throw new OvgBlogException(ErrorMessages.ModelNotValid);
            }

            var seoUrl = dto.SeoUrl.ReplaceSeoUrl();
            var result = await GetBySeoUrlAsync(seoUrl, cancellationToken);
            if (result.IsSuccess && result.Data != null)
            {
                throw new OvgBlogException(ErrorMessages.SeoUrlAlreadyTaken);
            }
        }

        #endregion
    }
}