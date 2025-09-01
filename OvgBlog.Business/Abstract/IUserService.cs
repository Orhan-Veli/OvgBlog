using OvgBlog.Business.Constants;
using OvgBlog.DAL.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OvgBlog.Business.Abstract
{
    public interface IUserService
    {
        Task<IResult<User>> Create(User user, CancellationToken cancellationToken);
        Task<IResult<object>> Delete(Guid id, CancellationToken cancellationToken);
        Task<IResult<User>> Update(User user, CancellationToken cancellationToken);
        Task<IResult<User>> GetById(Guid id, CancellationToken cancellationToken);
        Task<IResult<bool>> CheckUser(string userName, string password, CancellationToken cancellationToken);
    }
}
