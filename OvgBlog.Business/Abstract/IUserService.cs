using OvgBlog.Business.Constants;
using OvgBlog.DAL.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using OvgBlog.Business.Dto.User;

namespace OvgBlog.Business.Abstract
{
    public interface IUserService
    {
        Task<IResult<UserDto>> CreateAsync(CreateUserDto user, CancellationToken cancellationToken);
        Task<IResult<object>> DeleteAsync(Guid id, CancellationToken cancellationToken);
        Task<IResult<IEnumerable<UserDto>>> GetAllAsync(UserFilterDto filterDto, CancellationToken cancellationToken);
        Task<IResult<UserDto>> UpdateAsync(UpdateUserDto user, CancellationToken cancellationToken);
        
        Task<IResult<UserDto>> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<IResult<bool>> CheckUserAsync(string userName, string password, CancellationToken cancellationToken);
    }
}
