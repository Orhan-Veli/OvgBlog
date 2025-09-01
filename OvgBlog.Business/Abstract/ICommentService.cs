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
        Task<IResult<Comment>> Create(Comment comment, CancellationToken cancellationToken);
        Task<IResult<object>> Delete(Guid id, CancellationToken cancellationToken);
        Task<IResult<Comment>> Update(Comment comment, CancellationToken cancellationToken);
        Task<IResult<IEnumerable<Comment>>> GetAll(CancellationToken cancellationToken);
        Task<IResult<Comment>> GetById(Guid id, CancellationToken cancellationToken);
    }
}
