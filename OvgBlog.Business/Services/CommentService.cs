﻿using OvgBlog.Business.Abstract;
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
        public async Task<IResult<Comment>> Create(Comment comment, CancellationToken cancellationToken)
        {
            if (comment == null || string.IsNullOrEmpty(comment.Name) || string.IsNullOrEmpty(comment.Body) || comment.ArticleId == Guid.Empty)
            {
                return new Result<Comment>(false, Message.ModelNotValid);
            }
            var articleEntity = _articleRepository.Get(cancellationToken, x => x.Id == comment.ArticleId && !x.IsDeleted);
            if (articleEntity == null)
            {
                return new Result<Comment>(false, Message.CommentNotFound);
            }
            comment.Id = Guid.NewGuid();
            comment.CreatedDate = DateTime.Now;
            await _commentRepository.Create(comment, cancellationToken);
            return new Result<Comment>(true, comment);
        }

        public async Task<IResult<object>> Delete(Guid id, CancellationToken cancellationToken)
        {
            if (id == Guid.Empty)
            {
                return new Result<object>(false, Message.IdIsNotValid);
            }
            var commentEntity = await _commentRepository.Get(cancellationToken, x => x.Id == id && !x.IsDeleted);
            if (commentEntity == null)
            {
                return new Result<object>(false, Message.CommentNotFound);
            }
            var articleEntity = _articleRepository.Get(cancellationToken, x => x.Id == id && !x.IsDeleted);
            if (articleEntity == null)
            {
                return new Result<object>(false, Message.CommentNotFound);
            }
            commentEntity.IsDeleted = true;
            commentEntity.DeletedDate = DateTime.Now;
            await _commentRepository.Update(commentEntity, cancellationToken);
            return new Result<object>(true);
        }

        public async Task<IResult<IEnumerable<Comment>>> GetAll(CancellationToken cancellationToken)
        {
            var list = await _commentRepository.GetAll(cancellationToken, x => !x.IsDeleted);
            return new Result<IEnumerable<Comment>>(true, list);
        }

        public async Task<IResult<Comment>> GetById(Guid id, CancellationToken cancellationToken)
        {
            if (id == Guid.Empty)
            {
                return new Result<Comment>(false, Message.IdIsNotValid);
            }
            var commentEntity = await _commentRepository.Get(cancellationToken, x => x.Id == id && !x.IsDeleted);
            if (commentEntity == null)
            {
                return new Result<Comment>(false, Message.CommentNotFound);
            }
            return new Result<Comment>(true, commentEntity);
        }

        public async Task<IResult<Comment>> Update(Comment comment, CancellationToken cancellationToken)
        {
            if (comment == null || string.IsNullOrEmpty(comment.Name) || string.IsNullOrEmpty(comment.Body) || comment.ArticleId == Guid.Empty)
            {
                return new Result<Comment>(false, Message.ModelNotValid);
            }
            var commentEntity = await _commentRepository.Get(cancellationToken, x => x.Id == comment.Id && !x.IsDeleted);
            if (commentEntity == null)
            {
                return new Result<Comment>(false, Message.CommentNotFound);
            }
            var articleEntity = await _articleRepository.Get(cancellationToken, x => x.Id == comment.ArticleId && !x.IsDeleted);
            if (articleEntity == null)
            {
                return new Result<Comment>(false, Message.CommentNotFound);
            }
            comment.UpdatedDate = DateTime.Now;
            commentEntity = await _commentRepository.Update(comment, cancellationToken);
            return new Result<Comment>(true, commentEntity);

        }
    }
}
