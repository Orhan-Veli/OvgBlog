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
        public async Task<IResult<User>> Create(User user, CancellationToken cancellationToken)
        {
            if (user == null || string.IsNullOrEmpty(user.Name) || string.IsNullOrEmpty(user.Password))
            {
                return new Result<User>(false, Message.ModelNotValid);
            }
            user.Id = Guid.NewGuid();
            user.CreatedDate = DateTime.Now;
            await _userRepository.Create(user, cancellationToken);
            return new Result<User>(true, user);

        }

        public async Task<IResult<object>> Delete(Guid id, CancellationToken cancellationToken)
        {
            if (id == Guid.Empty)
            {
                return new Result<object>(false, Message.IdIsNotValid);
            }
            var userEntity = await _userRepository.Get(cancellationToken, x => x.Id == id && !x.IsDeleted);
            if (userEntity == null)
            {
                return new Result<object>(false, Message.UserNotFound);
            }
            userEntity.IsDeleted = true;
            userEntity.DeletedDate = DateTime.Now;
            await _userRepository.Update(userEntity, cancellationToken);
            return new Result<object>(true);
        }

        public async Task<IResult<bool>> CheckUser(string userName, string password, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
            {
                return new Result<bool>(false, Message.UserNotFound);
            }
            var user = await _userRepository.Get(cancellationToken, x => x.Name == userName || x.Email == userName);
            if (user == null || user.Name == null || user.Password == null)
            {
                return new Result<bool>(false, Message.UserNotFound);
            }
            if (user.Password != password)
            {
                return new Result<bool>(false, Message.PasswordIsWrong);
            }
            return new Result<bool>(true);
        }

        public async Task<IResult<User>> GetById(Guid id, CancellationToken cancellationToken)
        {
            if (id == Guid.Empty)
            {
                return new Result<User>(false, Message.IdIsNotValid);
            }
            var userEntity = await _userRepository.Get(cancellationToken, x => x.Id == id && !x.IsDeleted);
            if (userEntity == null)
            {
                return new Result<User>(false, Message.UserNotFound);
            }
            return new Result<User>(true, userEntity);
        }

        public async Task<IResult<User>> Update(User user, CancellationToken cancellationToken)
        {
            if (user == null || string.IsNullOrEmpty(user.Name) || string.IsNullOrEmpty(user.Password))
            {
                return new Result<User>(false, Message.ModelNotValid);
            }
            var userEntity = await _userRepository.Get(cancellationToken, x => x.Id == user.Id && !x.IsDeleted);
            if (userEntity == null)
            {
                return new Result<User>(false, Message.UserNotFound);
            }
            user.UpdatedDate = DateTime.Now;
            userEntity = await _userRepository.Update(user, cancellationToken);
            return new Result<User>(true, userEntity);
        }
    }
}
