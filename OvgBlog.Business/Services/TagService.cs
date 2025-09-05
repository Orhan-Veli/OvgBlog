using OvgBlog.Business.Abstract;
using OvgBlog.Business.Constants;
using OvgBlog.DAL.Abstract;
using OvgBlog.DAL.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Mapster;
using OvgBlog.Business.Dto;
using OvgBlog.DAL.Helpers;

namespace OvgBlog.Business.Services
{
    public class TagService(IEntityRepository<Tag> tagRepository) : ITagService
    {
        public async Task<IResult<TagDto>> CreateAsync(CreateTagDto dto, CancellationToken cancellationToken)
        {
            if (dto == null)
            {
                throw new OvgBlogException(ErrorMessages.TagNotFound);
            }

            var entity = dto.Adapt<Tag>();
            entity.CreatedDate = DateTime.UtcNow;
            await tagRepository.CreateAsync(entity, cancellationToken);
            
            var result = entity.Adapt<TagDto>();
            return new Result<TagDto>(true, result);
        }

        public async Task<IResult<object>> DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            if (id == Guid.Empty)
            { 
                throw new OvgBlogException(ErrorMessages.IdIsNotValid);
            }
            
            var entity = await tagRepository.GetAsync(cancellationToken, x => x.Id == id && !x.IsDeleted);
            if (entity == null)
            {
                throw new OvgBlogException(ErrorMessages.TagNotFound);
            }
            
            
            entity.DeletedDate = DateTime.UtcNow;
            entity.IsDeleted = true;
            await tagRepository.UpdateAsync(entity, cancellationToken);
            return new Result<object>(true);
        }

        public async Task<IResult<TagDto>> FindIdByNameAsync(string name, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new OvgBlogException(ErrorMessages.NameNotFound);
            }
            
            var result = await tagRepository.GetAsync(cancellationToken, x => x.Name == name && !x.IsDeleted);
            if (result == null)
            {
                return new Result<TagDto>(false, ErrorMessages.TagNotFound);
            }
            
            var resultDto = result.Adapt<TagDto>();
            return new Result<TagDto>(true, resultDto);
        }

        public async Task<IResult<IEnumerable<TagDto>>> GetAllAsync(TagFilterDto filterDto, CancellationToken cancellationToken)
        {
            var entites = await tagRepository.GetAllAsync(cancellationToken, x => !x.IsDeleted);
            
            var result = entites.Adapt<IEnumerable<TagDto>>();
            return new Result<IEnumerable<TagDto>>(true, result);
        }

        public async Task<IResult<TagDto>> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            if (id == Guid.Empty)
            {
                throw new OvgBlogException(ErrorMessages.IdIsNotValid);
            }
            
            var entity = await tagRepository.GetAsync(cancellationToken, x => x.Id == id && !x.IsDeleted);
            if (entity == null)
            {
                throw new OvgBlogException(ErrorMessages.TagNotFound);
            }
            
            var resultDto = entity.Adapt<TagDto>();
            return new Result<TagDto>(true, resultDto);
        }

        public async Task<IResult<IEnumerable<TagDto>>> GetByIdsAsync(List<Guid> ids, CancellationToken cancellationToken)
        {
            if (ids == null ||ids.Count==0)
            {
                throw new OvgBlogException(ErrorMessages.IdIsNotValid);
            }
            
            var entities = await tagRepository.GetAllAsync(cancellationToken, x=> ids.Contains(x.Id));
            
            var resultDto = entities.Adapt<IEnumerable<TagDto>>();
            return new Result<IEnumerable<TagDto>>(true, resultDto);
        }

        public async Task<IResult<TagDto>> UpdateAsync(UpdateTagDto tag, CancellationToken cancellationToken)
        {
            if (tag == null)
            {
                throw new OvgBlogException(ErrorMessages.DtoCannotBeNull);
            }
            
            var entity = await tagRepository.GetAsync(cancellationToken, x => x.Id == tag.Id && !x.IsDeleted);
            if (entity == null)
            {
                throw new OvgBlogException(ErrorMessages.TagNotFound);
            }
            
            entity.UpdatedDate = DateTime.UtcNow;
            entity = await tagRepository.UpdateAsync(entity, cancellationToken);
            
            var result = entity.Adapt<TagDto>();
            return new Result<TagDto>(true, result);
        }
    }
}
