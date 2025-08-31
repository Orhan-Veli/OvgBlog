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
    public class TagService : ITagService
    {
        readonly IEntityRepository<Tag> _tagRepository;
        public TagService(IEntityRepository<Tag> tagRepository)
        {
            _tagRepository = tagRepository;
        }
        public async Task<IResult<Tag>> Create(Tag tag, CancellationToken cancellationToken)
        {
            if (tag == null || string.IsNullOrEmpty(tag.Name) || string.IsNullOrEmpty(tag.SeoUrl))
            {
                return new Result<Tag>(false, Message.ModelNotValid);
            }
            tag.Id = Guid.NewGuid();
            tag.CreatedDate = DateTime.Now;
            await _tagRepository.Create(tag, cancellationToken);
            return new Result<Tag>(true, tag);
        }

        public async Task<IResult<object>> Delete(Guid id, CancellationToken cancellationToken)
        {
            if (id == Guid.Empty)
            {
                return new Result<object>(false, Message.IdIsNotValid);
            }
            var tagEntity = await _tagRepository.Get(cancellationToken, x => x.Id == id && !x.IsDeleted);
            if (tagEntity == null)
            {
                return new Result<Object>(false, Message.TagNotFound);
            }
            tagEntity.IsDeleted = true;
            tagEntity.DeletedDate = DateTime.Now;
            await _tagRepository.Update(tagEntity, cancellationToken);
            return new Result<object>(true);
        }

        public async Task<IResult<Tag>> FindIdByName(string name, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(name))
            {
                return new Result<Tag>(false, Message.NameNotFound);
            }
            var result = await _tagRepository.Get(cancellationToken, x => x.Name == name && !x.IsDeleted);
            if (result == null)
            {
                return new Result<Tag>(false, Message.TagNotFound);
            }
            return new Result<Tag>(true, result);
        }

        public async Task<IResult<IEnumerable<Tag>>> GetAll(CancellationToken cancellationToken)
        {
            var list = await _tagRepository.GetAll(cancellationToken, x => !x.IsDeleted);
            return new Result<IEnumerable<Tag>>(true, list);
        }

        public async Task<IResult<Tag>> GetById(Guid id, CancellationToken cancellationToken)
        {
            if (id == Guid.Empty)
            {
                return new Result<Tag>(false, Message.IdIsNotValid);
            }
            var tagEntity = await _tagRepository.Get(cancellationToken, x => x.Id == id && !x.IsDeleted);
            if (tagEntity == null)
            {
                return new Result<Tag>(false, Message.TagNotFound);
            }
            return new Result<Tag>(true, tagEntity);
        }

        public async Task<IResult<IEnumerable<Tag>>> GetByIds(List<Guid> ids, CancellationToken cancellationToken)
        {
            if (ids == null ||ids.Count==0)
            {
                return new Result<IEnumerable<Tag>>(false, Message.IdIsNotValid);
            }
            var tagEntities = await _tagRepository.GetAll(cancellationToken, x=> ids.Contains(x.Id));
            return new Result<IEnumerable<Tag>>(true,tagEntities);
        }

        public async Task<IResult<Tag>> Update(Tag tag, CancellationToken cancellationToken)
        {
            if (tag == null || string.IsNullOrEmpty(tag.Name) || string.IsNullOrEmpty(tag.SeoUrl))
            {
                return new Result<Tag>(false, Message.ModelNotValid);
            }
            var tagEntity = await _tagRepository.Get(cancellationToken, x => x.Id == tag.Id && !x.IsDeleted);
            if (tagEntity == null)
            {
                return new Result<Tag>(false, Message.TagNotFound);
            }
            tag.UpdatedDate = DateTime.Now;
            tagEntity = await _tagRepository.Update(tag, cancellationToken);
            return new Result<Tag>(true, tagEntity);
        }
    }
}
