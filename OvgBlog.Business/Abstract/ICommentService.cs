using OvgBlog.Business.Constants;
using OvgBlog.DAL.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OvgBlog.Business.Abstract
{
    public interface ICommentService
    {
        Task<IResult<Comment>> Create(Comment comment);
        Task<IResult<object>> Delete(Guid id);
        Task<IResult<Comment>> Update(Comment comment);
        Task<IResult<IEnumerable<Comment>>> GetAll();
        Task<IResult<Comment>> GetById(Guid id);
    }
}
