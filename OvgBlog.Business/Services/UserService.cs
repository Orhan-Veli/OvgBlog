using OvgBlog.Business.Abstract;
using OvgBlog.Business.Constants;
using OvgBlog.DAL.Abstract;
using OvgBlog.DAL.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;
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
        public async Task<IResult<User>> Create(User user)
        {
            if (user==null || string.IsNullOrEmpty(user.Name) || string.IsNullOrEmpty(user.Password))
            {
                return new Result<User>(false,Message.ModelNotValid);
            }
            await _userRepository.Create(user);
            return new Result<User>(true,user);
        }

        public async Task<IResult<object>> Delete(Guid id)
        {
            if (id== Guid.Empty)
            {
                return new Result<object>(false, Message.IdIsNotValid);
            }
            var userEntity = await _userRepository.Get(x => x.Id == id && !x.IsDeleted);
            if (userEntity==null)
            {
                return new Result<object>(false,Message.UserNotFound);
            }
            userEntity.IsDeleted = true;
            userEntity.DeletedDate = DateTime.Now;
            await _userRepository.Update(userEntity);
            return new Result<object>(true);
        }

        public async Task<IResult<IEnumerable<User>>> GetAll()
        {
            var list = await _userRepository.GetAll();
            return new Result<IEnumerable<User>>(true,list);
        }

        public async Task<IResult<User>> GetById(Guid id)
        {
            if (id==Guid.Empty)
            {
                return new Result<User>(false,Message.IdIsNotValid);
            }
            var userEntity = await _userRepository.Get(x => x.Id == id && !x.IsDeleted);
            if(userEntity==null)
            {
                return new Result<User>(false,Message.UserNotFound);
            }
            return new Result<User>(true,userEntity);
        }

        public async Task<IResult<User>> Update(User user)
        {
            if (user == null || string.IsNullOrEmpty(user.Name) || string.IsNullOrEmpty(user.Password))
            {
                return new Result<User>(false, Message.ModelNotValid);
            }
            var userEntity = await _userRepository.Get(x=>x.Id==user.Id && !x.IsDeleted);
            if (userEntity==null)
            {
                return new Result<User>(false,Message.UserNotFound);
            }
            userEntity = await _userRepository.Update(user);
            return new Result<User>(true, userEntity);
        }
    }
}
