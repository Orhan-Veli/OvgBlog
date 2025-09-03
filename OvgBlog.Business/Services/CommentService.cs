using OvgBlog.Business.Abstract;
using OvgBlog.Business.Constants;
using OvgBlog.DAL.Abstract;
using OvgBlog.DAL.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

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
        public async Task<IResult<Comment>> CreateAsync(Comment comment, CancellationToken cancellationToken)
        {
            if (comment == null || string.IsNullOrEmpty(comment.Name) || string.IsNullOrEmpty(comment.Body) || comment.ArticleId == Guid.Empty)
            {
                return new Result<Comment>(false, ErrorMessages.ModelNotValid);
            }
            var articleEntity = _articleRepository.GetAsync(cancellationToken, x => x.Id == comment.ArticleId && !x.IsDeleted);
            if (articleEntity == null)
            {
                return new Result<Comment>(false, ErrorMessages.CommentNotFound);
            }
            comment.Id = Guid.NewGuid();
            comment.CreatedDate = DateTime.Now;
            await _commentRepository.CreateAsync(comment, cancellationToken);
            return new Result<Comment>(true, comment);
        }

        public async Task<IResult<object>> DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            if (id == Guid.Empty)
            {
                return new Result<object>(false, ErrorMessages.IdIsNotValid);
            }
            var commentEntity = await _commentRepository.GetAsync(cancellationToken, x => x.Id == id && !x.IsDeleted);
            if (commentEntity == null)
            {
                return new Result<object>(false, ErrorMessages.CommentNotFound);
            }
            var articleEntity = _articleRepository.GetAsync(cancellationToken, x => x.Id == id && !x.IsDeleted);
            if (articleEntity == null)
            {
                return new Result<object>(false, ErrorMessages.CommentNotFound);
            }
            commentEntity.IsDeleted = true;
            commentEntity.DeletedDate = DateTime.Now;
            await _commentRepository.UpdateAsync(commentEntity, cancellationToken);
            return new Result<object>(true);
        }

        public async Task<IResult<IEnumerable<Comment>>> GetAllAsync(CancellationToken cancellationToken)
        {
            var list = await _commentRepository.GetAllAsync(cancellationToken, x => !x.IsDeleted);
            return new Result<IEnumerable<Comment>>(true, list);
        }

        public async Task<IResult<Comment>> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            if (id == Guid.Empty)
            {
                return new Result<Comment>(false, ErrorMessages.IdIsNotValid);
            }
            var commentEntity = await _commentRepository.GetAsync(cancellationToken, x => x.Id == id && !x.IsDeleted);
            if (commentEntity == null)
            {
                return new Result<Comment>(false, ErrorMessages.CommentNotFound);
            }
            return new Result<Comment>(true, commentEntity);
        }

        public async Task<IResult<Comment>> UpdateAsync(Comment comment, CancellationToken cancellationToken)
        {
            if (comment == null || string.IsNullOrEmpty(comment.Name) || string.IsNullOrEmpty(comment.Body) || comment.ArticleId == Guid.Empty)
            {
                return new Result<Comment>(false, ErrorMessages.ModelNotValid);
            }
            var commentEntity = await _commentRepository.GetAsync(cancellationToken, x => x.Id == comment.Id && !x.IsDeleted);
            if (commentEntity == null)
            {
                return new Result<Comment>(false, ErrorMessages.CommentNotFound);
            }
            var articleEntity = await _articleRepository.GetAsync(cancellationToken, x => x.Id == comment.ArticleId && !x.IsDeleted);
            if (articleEntity == null)
            {
                return new Result<Comment>(false, ErrorMessages.CommentNotFound);
            }
            comment.UpdatedDate = DateTime.Now;
            commentEntity = await _commentRepository.UpdateAsync(comment, cancellationToken);
            return new Result<Comment>(true, commentEntity);

        }
    }
}
