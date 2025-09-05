using OvgBlog.Business.Abstract;
using OvgBlog.Business.Constants;
using OvgBlog.DAL.Abstract;
using OvgBlog.DAL.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Mapster;
using OvgBlog.Business.Dto.User;
using OvgBlog.DAL.Helpers;

namespace OvgBlog.Business.Services
{
    public class UserService(IEntityRepository<User> userRepository) : IUserService
    {
        public async Task<IResult<UserDto>> CreateAsync(CreateUserDto dto, CancellationToken cancellationToken)
        {
            if (dto == null)
            {
                throw new OvgBlogException(ErrorMessages.DtoCannotBeNull);
            }

            var entity = dto.Adapt<User>();
            entity.CreatedDate = DateTime.UtcNow;
            await userRepository.CreateAsync(entity, cancellationToken);
            
            var result = entity.Adapt<UserDto>();
            return new Result<UserDto>(true, result);

        }

        public async Task<IResult<object>> DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            if (id == Guid.Empty)
            {
                throw new OvgBlogException(ErrorMessages.IdIsNotValid);
            }
            
            var userEntity = await userRepository.GetAsync(cancellationToken, x => x.Id == id && !x.IsDeleted);
            if (userEntity == null)
            {
                throw new OvgBlogException(ErrorMessages.UserNotFound);
            }
            
            userEntity.IsDeleted = true;
            userEntity.DeletedDate = DateTime.UtcNow;
            await userRepository.UpdateAsync(userEntity, cancellationToken);
            return new Result<object>(true);
        }

        public async Task<IResult<IEnumerable<UserDto>>> GetAllAsync(UserFilterDto filterDto, CancellationToken cancellationToken)
        {
            var entites = await userRepository.GetAllAsync(cancellationToken, x => !x.IsDeleted && filterDto.Ids.Contains(x.Id));
            
            var result = entites.Adapt<IEnumerable<UserDto>>();
            return new Result<IEnumerable<UserDto>>(true, result);
        }

        public async Task<IResult<bool>> CheckUserAsync(string userName, string password, CancellationToken cancellationToken)
        {
            var user = await userRepository.GetAsync(cancellationToken, x => x.Name == userName || x.Email == userName);
            if (user == null)
            {
                 throw new OvgBlogException(ErrorMessages.UserNotFound);
            }
            
            if (user.Password != password)
            {
                return new Result<bool>(false, ErrorMessages.PasswordIsWrong);
            }
            return new Result<bool>(true);
        }

        public async Task<IResult<UserDto>> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            if (id == Guid.Empty)
            {
                throw new OvgBlogException(ErrorMessages.IdIsNotValid);
            }
            var entity = await userRepository.GetAsync(cancellationToken, x => x.Id == id && !x.IsDeleted);
            if (entity == null)
            {
               throw new OvgBlogException(ErrorMessages.UserNotFound);
            }
            
            var result = entity.Adapt<UserDto>();
            return new Result<UserDto>(true, result);
        }

        public async Task<IResult<UserDto>> UpdateAsync(UpdateUserDto user, CancellationToken cancellationToken)
        {
            if (user == null)
            {
                return new Result<UserDto>(false, ErrorMessages.ModelNotValid);
            }
            
            var entity = await userRepository.GetAsync(cancellationToken, x => x.Id == user.Id && !x.IsDeleted);
            if (entity == null)
            {
                return new Result<UserDto>(false, ErrorMessages.UserNotFound);
            }
            
            entity.UpdatedDate = DateTime.UtcNow;
            entity = await userRepository.UpdateAsync(entity, cancellationToken);
            
            var result = entity.Adapt<UserDto>();
            return new Result<UserDto>(true, result);
        }
    }
}
