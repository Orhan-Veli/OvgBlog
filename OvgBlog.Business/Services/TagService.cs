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
    public class TagService : ITagService
    {
        readonly IEntityRepository<Tag> _tagRepository;
        public TagService(IEntityRepository<Tag> tagRepository)
        {
            _tagRepository = tagRepository;
        }
        public async Task<IResult<Tag>> Create(Tag tag)
        {
            if (tag == null || string.IsNullOrEmpty(tag.Name) || string.IsNullOrEmpty(tag.SeoUrl))
            {
                return new Result<Tag>(false, Message.ModelNotValid);
            }
            tag.Id = Guid.NewGuid();
            tag.CreatedDate = DateTime.Now;
            await _tagRepository.Create(tag);
            return new Result<Tag>(true, tag);
        }

        public async Task<IResult<object>> Delete(Guid id)
        {
            if (id== Guid.Empty)
            {
                return new Result<object>(false,Message.IdIsNotValid);
            }
            var tagEntity =await _tagRepository.Get(x => x.Id == id && !x.IsDeleted);
            if (tagEntity==null)
            {
                return new Result<Object>(false,Message.TagNotFound);
            }
            tagEntity.IsDeleted = true;
            tagEntity.DeletedDate = DateTime.Now;
            await _tagRepository.Update(tagEntity);
            return new Result<object>(true);
        }

        public async Task<IResult<Tag>> FindIdByName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return new Result<Tag>(false,Message.NameNotFound);
            }
            var result = await _tagRepository.Get(x => x.Name == name && !x.IsDeleted);
            if (result == null)
            {
                return new Result<Tag>(false, Message.TagNotFound);
            }
            return new Result<Tag>(true,result);
        }

        public async Task<IResult<IEnumerable<Tag>>> GetAll()
        {
            var list =await _tagRepository.GetAll(x => !x.IsDeleted);
            return new Result<IEnumerable<Tag>>(true, list);
        }

        public async Task<IResult<Tag>> GetById(Guid id)
        {
            if(id==Guid.Empty)
            {
                return new Result<Tag>(false, Message.IdIsNotValid);
            }
            var tagEntity = await _tagRepository.Get(x => x.Id == id && !x.IsDeleted);
            if (tagEntity == null)
            {
                return new Result<Tag>(false, Message.TagNotFound);
            }
            return new Result<Tag>(true,tagEntity);
        }

        public async Task<IResult<Tag>> Update(Tag tag)
        {
            if (tag == null || string.IsNullOrEmpty(tag.Name) || string.IsNullOrEmpty(tag.SeoUrl))
            {
                return new Result<Tag>(false, Message.ModelNotValid);
            }
            var tagEntity = await _tagRepository.Get(x => x.Id == tag.Id && !x.IsDeleted);
            if (tagEntity==null)
            {
                return new Result<Tag>(false,Message.TagNotFound);
            }
            tag.UpdatedDate = DateTime.Now;
            tagEntity = await _tagRepository.Update(tag);
            return new Result<Tag>(true,tagEntity);
        }
    }
}
