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

namespace OvgBlog.Business.Services
{
    public class ContactService : IContactService
    {
        private readonly IEntityRepository<Contact> _contactRepository;
        public ContactService(IEntityRepository<Contact> contactRepository)
        {
            _contactRepository = contactRepository;
        }
        public async Task<IResult<Contact>> Create(Contact contact, CancellationToken cancellationToken)
        {
            if (contact==null || string.IsNullOrEmpty(contact.Name) || string.IsNullOrEmpty(contact.Email) || string.IsNullOrEmpty(contact.Body))
            {
                return new Result<Contact>(false,Message.ModelNotValid);
            }
            contact.Id = Guid.NewGuid();
            contact.IsDeleted = false;
            contact.SendDate = DateTime.Now;
            await _contactRepository.Create(contact, cancellationToken);
            return new Result<Contact>(true);
        }

        public async Task<IResult<object>> Delete(Guid id, CancellationToken cancellationToken)
        {
            if (id==Guid.Empty)
            {
                return new Result<object>(false, Message.IdIsNotValid);
            }
            var model = await _contactRepository.Get(cancellationToken, x=> x.Id==id);
            model.IsDeleted = true;
            await _contactRepository.Update(model, cancellationToken);
            return new Result<object>(true);
        }

        public async Task<IResult<Contact>> Get(Guid id, CancellationToken cancellationToken)
        {
            if (id==Guid.Empty)
            {
                return new Result<Contact>(false, Message.IdIsNotValid);
            }
            var result = await _contactRepository.Get(cancellationToken, x=> x.Id==id);
            return new Result<Contact>(true, result);
        }

        public async Task<IResult<List<Contact>>> GetAll(CancellationToken cancellationToken)
        {
            var list = await _contactRepository.GetAll(cancellationToken);
           return new Result<List<Contact>>(true, list.Where(x=> !x.IsDeleted).ToList());
        }
    }
}
