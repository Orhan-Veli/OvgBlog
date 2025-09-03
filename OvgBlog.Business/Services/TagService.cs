using OvgBlog.Business.Abstract;
using OvgBlog.Business.Constants;
using OvgBlog.DAL.Abstract;
using OvgBlog.DAL.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OvgBlog.Business.Services
{
    public class TagService(IEntityRepository<Tag> tagRepository) : ITagService
    {
        public async Task<IResult<Tag>> CreateAsync(Tag tag, CancellationToken cancellationToken)
        {
            if (tag == null || string.IsNullOrEmpty(tag.Name) || string.IsNullOrEmpty(tag.SeoUrl))
            {
                return new Result<Tag>(false, ErrorMessages.ModelNotValid);
            }
            tag.Id = Guid.NewGuid();
            tag.CreatedDate = DateTime.Now;
            await tagRepository.CreateAsync(tag, cancellationToken);
            return new Result<Tag>(true, tag);
        }

        public async Task<IResult<object>> DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            if (id == Guid.Empty)
            {
                return new Result<object>(false, ErrorMessages.IdIsNotValid);
            }
            var tagEntity = await tagRepository.GetAsync(cancellationToken, x => x.Id == id && !x.IsDeleted);
            if (tagEntity == null)
            {
                return new Result<Object>(false, ErrorMessages.TagNotFound);
            }
            tagEntity.IsDeleted = true;
            tagEntity.DeletedDate = DateTime.Now;
            await tagRepository.UpdateAsync(tagEntity, cancellationToken);
            return new Result<object>(true);
        }

        public async Task<IResult<Tag>> FindIdByNameAsync(string name, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(name))
            {
                return new Result<Tag>(false, ErrorMessages.NameNotFound);
            }
            var result = await tagRepository.GetAsync(cancellationToken, x => x.Name == name && !x.IsDeleted);
            if (result == null)
            {
                return new Result<Tag>(false, ErrorMessages.TagNotFound);
            }
            return new Result<Tag>(true, result);
        }

        public async Task<IResult<IEnumerable<Tag>>> GetAllAsync(CancellationToken cancellationToken)
        {
            var list = await tagRepository.GetAllAsync(cancellationToken, x => !x.IsDeleted);
            return new Result<IEnumerable<Tag>>(true, list);
        }

        public async Task<IResult<Tag>> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            if (id == Guid.Empty)
            {
                return new Result<Tag>(false, ErrorMessages.IdIsNotValid);
            }
            var tagEntity = await tagRepository.GetAsync(cancellationToken, x => x.Id == id && !x.IsDeleted);
            if (tagEntity == null)
            {
                return new Result<Tag>(false, ErrorMessages.TagNotFound);
            }
            return new Result<Tag>(true, tagEntity);
        }

        public async Task<IResult<IEnumerable<Tag>>> GetByIdsAsync(List<Guid> ids, CancellationToken cancellationToken)
        {
            if (ids == null ||ids.Count==0)
            {
                return new Result<IEnumerable<Tag>>(false, ErrorMessages.IdIsNotValid);
            }
            var tagEntities = await tagRepository.GetAllAsync(cancellationToken, x=> ids.Contains(x.Id));
            return new Result<IEnumerable<Tag>>(true,tagEntities);
        }

        public async Task<IResult<Tag>> UpdateAsync(Tag tag, CancellationToken cancellationToken)
        {
            if (tag == null || string.IsNullOrEmpty(tag.Name) || string.IsNullOrEmpty(tag.SeoUrl))
            {
                return new Result<Tag>(false, ErrorMessages.ModelNotValid);
            }
            var tagEntity = await tagRepository.GetAsync(cancellationToken, x => x.Id == tag.Id && !x.IsDeleted);
            if (tagEntity == null)
            {
                return new Result<Tag>(false, ErrorMessages.TagNotFound);
            }
            tag.UpdatedDate = DateTime.Now;
            tagEntity = await tagRepository.UpdateAsync(tag, cancellationToken);
            return new Result<Tag>(true, tagEntity);
        }
    }
}
