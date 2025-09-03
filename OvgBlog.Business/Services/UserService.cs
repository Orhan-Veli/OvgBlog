using OvgBlog.Business.Abstract;
using OvgBlog.Business.Constants;
using OvgBlog.DAL.Abstract;
using OvgBlog.DAL.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace OvgBlog.Business.Services
{
    public class UserService : IUserService
    {
        readonly IEntityRepository<User> _userRepository;
        public UserService(IEntityRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<IResult<User>> CreateAsync(User user, CancellationToken cancellationToken)
        {
            if (user == null || string.IsNullOrEmpty(user.Name) || string.IsNullOrEmpty(user.Password))
            {
                return new Result<User>(false, ErrorMessages.ModelNotValid);
            }
            user.Id = Guid.NewGuid();
            user.CreatedDate = DateTime.Now;
            await _userRepository.CreateAsync(user, cancellationToken);
            return new Result<User>(true, user);

        }

        public async Task<IResult<object>> DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            if (id == Guid.Empty)
            {
                return new Result<object>(false, ErrorMessages.IdIsNotValid);
            }
            var userEntity = await _userRepository.GetAsync(cancellationToken, x => x.Id == id && !x.IsDeleted);
            if (userEntity == null)
            {
                return new Result<object>(false, ErrorMessages.UserNotFound);
            }
            userEntity.IsDeleted = true;
            userEntity.DeletedDate = DateTime.Now;
            await _userRepository.UpdateAsync(userEntity, cancellationToken);
            return new Result<object>(true);
        }

        public async Task<IResult<bool>> CheckUserAsync(string userName, string password, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
            {
                return new Result<bool>(false, ErrorMessages.UserNotFound);
            }
            var user = await _userRepository.GetAsync(cancellationToken, x => x.Name == userName || x.Email == userName);
            if (user == null || user.Name == null || user.Password == null)
            {
                return new Result<bool>(false, ErrorMessages.UserNotFound);
            }
            if (user.Password != password)
            {
                return new Result<bool>(false, ErrorMessages.PasswordIsWrong);
            }
            return new Result<bool>(true);
        }

        public async Task<IResult<User>> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            if (id == Guid.Empty)
            {
                return new Result<User>(false, ErrorMessages.IdIsNotValid);
            }
            var userEntity = await _userRepository.GetAsync(cancellationToken, x => x.Id == id && !x.IsDeleted);
            if (userEntity == null)
            {
                return new Result<User>(false, ErrorMessages.UserNotFound);
            }
            return new Result<User>(true, userEntity);
        }

        public async Task<IResult<User>> UpdateAsync(User user, CancellationToken cancellationToken)
        {
            if (user == null || string.IsNullOrEmpty(user.Name) || string.IsNullOrEmpty(user.Password))
            {
                return new Result<User>(false, ErrorMessages.ModelNotValid);
            }
            var userEntity = await _userRepository.GetAsync(cancellationToken, x => x.Id == user.Id && !x.IsDeleted);
            if (userEntity == null)
            {
                return new Result<User>(false, ErrorMessages.UserNotFound);
            }
            user.UpdatedDate = DateTime.Now;
            userEntity = await _userRepository.UpdateAsync(user, cancellationToken);
            return new Result<User>(true, userEntity);
        }
    }
}
