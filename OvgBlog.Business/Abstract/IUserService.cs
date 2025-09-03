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
        Task<IResult<User>> CreateAsync(User user, CancellationToken cancellationToken);
        Task<IResult<object>> DeleteAsync(Guid id, CancellationToken cancellationToken);
        Task<IResult<User>> UpdateAsync(User user, CancellationToken cancellationToken);
        Task<IResult<User>> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<IResult<bool>> CheckUserAsync(string userName, string password, CancellationToken cancellationToken);
    }
}
