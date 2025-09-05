using OvgBlog.Business.Constants;
using OvgBlog.DAL.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using OvgBlog.Business.Dto.Comment;

namespace OvgBlog.Business.Abstract
{
    public interface ICommentService
    {
        Task<IResult<CommentDto>> CreateAsync(CreateCommentDto comment, CancellationToken cancellationToken);
        Task<IResult<object>> DeleteAsync(Guid id, CancellationToken cancellationToken);
        Task<IResult<CommentDto>> UpdateAsync(UpdateCommentDto comment, CancellationToken cancellationToken);
        Task<IResult<IEnumerable<CommentDto>>> GetAllAsync(CommentFilterDto filterDto, CancellationToken cancellationToken);
        Task<IResult<CommentDto>> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    }
}
