using OvgBlog.Business.Constants;
using OvgBlog.DAL.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OvgBlog.Business.Abstract
{
    public interface ICommentService
    {
        Task<IResult<Comment>> CreateAsync(Comment comment, CancellationToken cancellationToken);
        Task<IResult<object>> DeleteAsync(Guid id, CancellationToken cancellationToken);
        Task<IResult<Comment>> UpdateAsync(Comment comment, CancellationToken cancellationToken);
        Task<IResult<IEnumerable<Comment>>> GetAllAsync(CancellationToken cancellationToken);
        Task<IResult<Comment>> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    }
}
