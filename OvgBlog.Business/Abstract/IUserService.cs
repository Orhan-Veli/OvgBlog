using OvgBlog.Business.Constants;
using OvgBlog.DAL.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OvgBlog.Business.Abstract
{
    public interface IUserService
    {
        Task<IResult<User>> Create(User user);
        Task<IResult<object>> Delete(Guid id);
        Task<IResult<User>> Update(User user);
        Task<IResult<IEnumerable<User>>> GetAll();
        Task<IResult<User>> GetById(Guid id);
    }
}
