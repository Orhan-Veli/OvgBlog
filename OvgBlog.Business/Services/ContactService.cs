using OvgBlog.Business.Abstract;
using OvgBlog.Business.Constants;
using OvgBlog.DAL.Abstract;
using OvgBlog.DAL.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Mapster;
using OvgBlog.Business.Dto.Contact;

namespace OvgBlog.Business.Services
{
    public class ContactService : IContactService
    {
        private readonly IEntityRepository<Contact> _contactRepository;
        public ContactService(IEntityRepository<Contact> contactRepository)
        {
            _contactRepository = contactRepository;
        }
        public async Task<IResult<ContactDto>> CreateAsync(CreateContactDto dto, CancellationToken cancellationToken)
        {
            if (dto==null)
            {
                return new Result<ContactDto>(false,ErrorMessages.ModelNotValid);
            }
            
            var entity = dto.Adapt<Contact>();
            entity.IsDeleted = false;
            entity.SendDate = DateTime.UtcNow;
            
            await _contactRepository.CreateAsync(entity, cancellationToken);
            
            var result = entity.Adapt<ContactDto>();
            return new Result<ContactDto>(true, result);
        }

        public async Task<IResult<object>> DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            if (id==Guid.Empty)
            {
                return new Result<object>(false, ErrorMessages.IdIsNotValid);
            }
            
            var entity = await _contactRepository.GetAsync(cancellationToken, x=> x.Id==id);
            entity.IsDeleted = true;
            entity.DeletedDate = DateTime.UtcNow;

            await _contactRepository.UpdateAsync(entity, cancellationToken);
            return new Result<object>(true);
        }

        public async Task<IResult<ContactDto>> GetAsync(Guid id, CancellationToken cancellationToken)
        {
            if (id==Guid.Empty)
            {
                return new Result<ContactDto>(false, ErrorMessages.IdIsNotValid);
            }
            
            var entity = await _contactRepository.GetAsync(cancellationToken, x=> x.Id==id);
            
            var dto = entity.Adapt<ContactDto>();
            return new Result<ContactDto>(true, dto);
        }

        public async Task<IResult<IEnumerable<ContactDto>>> GetAllAsync(ContactFilterDto filterDto, CancellationToken cancellationToken)
        {
            var entities = await _contactRepository.GetAllAsync(cancellationToken, x=> !x.IsDeleted && filterDto.Ids.Contains(x.Id));
            var dto = entities.Adapt<IEnumerable<ContactDto>>();
           return new Result<IEnumerable<ContactDto>>(true, dto);
        }
    }
}
