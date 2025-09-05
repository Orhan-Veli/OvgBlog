using OvgBlog.Business.Abstract;
using OvgBlog.Business.Constants;
using OvgBlog.DAL.Abstract;
using OvgBlog.DAL.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Mapster;
using OvgBlog.Business.Dto.Comment;
using OvgBlog.DAL.Helpers;

namespace OvgBlog.Business.Services
{
    public class CommentService : ICommentService
    {
        readonly IEntityRepository<Comment> _commentRepository;
        readonly IEntityRepository<Article> _articleRepository;
        public CommentService(IEntityRepository<Comment> commentRepository, IEntityRepository<Article> articleRepository)
        {
            _commentRepository = commentRepository;
            _articleRepository = articleRepository;
        }
        public async Task<IResult<CommentDto>> CreateAsync(CreateCommentDto dto, CancellationToken cancellationToken)
        {
            if (dto == null)
            {
                throw new OvgBlogException(ErrorMessages.DtoCannotBeNull);
            }
            
            var articleEntity = _articleRepository.GetAsync(cancellationToken, x => x.Id == dto.ArticleId && !x.IsDeleted);
            if (articleEntity == null)
            {
                throw new OvgBlogException(ErrorMessages.CommentNotFound);
            }

            var entity = dto.Adapt<Comment>();
            entity.CreatedDate = DateTime.UtcNow;
            
            await _commentRepository.CreateAsync(entity, cancellationToken);
            
            var result = entity.Adapt<CommentDto>();
            return new Result<CommentDto>(true, result);
        }

        public async Task<IResult<object>> DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            if (id == Guid.Empty)
            {
                throw new OvgBlogException(ErrorMessages.IdIsNotValid);
            }
            
            var entity = await _commentRepository.GetAsync(cancellationToken, x => x.Id == id && !x.IsDeleted);
            if (entity == null)
            {
                throw new OvgBlogException(ErrorMessages.CommentNotFound);
            }
            
            var articleEntity = _articleRepository.GetAsync(cancellationToken, x => x.Id == id && !x.IsDeleted);
            if (articleEntity == null)
            {
                throw new OvgBlogException(ErrorMessages.ArticleIsNotFound);
            }
             
            entity.IsDeleted = true;
            entity.DeletedDate = DateTime.UtcNow;
            await _commentRepository.UpdateAsync(entity, cancellationToken);
            return new Result<object>(true);
        }

        public async Task<IResult<IEnumerable<CommentDto>>> GetAllAsync(CommentFilterDto filterDto, CancellationToken cancellationToken)
        {
            var entities = await _commentRepository.GetAllAsync(cancellationToken, x => !x.IsDeleted && filterDto.Ids.Contains(x.Id) && filterDto.ArticleIds.Contains(x.ArticleId));
            
            var result = entities.Adapt<IEnumerable<CommentDto>>();
            
            return new Result<IEnumerable<CommentDto>>(true, result);
        }

        public async Task<IResult<CommentDto>> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            if (id == Guid.Empty)
            {
               throw new OvgBlogException(ErrorMessages.IdIsNotValid);
            }
            
            var entity = await _commentRepository.GetAsync(cancellationToken, x => x.Id == id && !x.IsDeleted);
            if (entity == null)
            {
                throw new OvgBlogException(ErrorMessages.CommentNotFound);
            }
            
            var result = entity.Adapt<CommentDto>();
            
            return new Result<CommentDto>(true, result);
        }

        public async Task<IResult<CommentDto>> UpdateAsync(UpdateCommentDto dto, CancellationToken cancellationToken)
        {
            if (dto == null)
            {
                throw new OvgBlogException(ErrorMessages.DtoCannotBeNull);
            }
            
            var entity = await _commentRepository.GetAsync(cancellationToken, x => x.Id == dto.Id && !x.IsDeleted);
            if (entity == null)
            {
                throw new OvgBlogException(ErrorMessages.CommentNotFound);
            }
            
            var articleEntity = await _articleRepository.GetAsync(cancellationToken, x => x.Id == entity.ArticleId && !x.IsDeleted);
            if (articleEntity == null)
            {
                throw new OvgBlogException(ErrorMessages.ArticleIsNotFound);
            }
            
            entity.UpdatedDate = DateTime.UtcNow;
            entity = await _commentRepository.UpdateAsync(entity, cancellationToken);
            
            var result = entity.Adapt<CommentDto>();
            return new Result<CommentDto>(true, result);

        }
    }
}
