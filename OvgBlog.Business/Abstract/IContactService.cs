using OvgBlog.Business.Constants;
using OvgBlog.DAL.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using OvgBlog.Business.Dto.Contact;

namespace OvgBlog.Business.Abstract
{
    public interface IContactService
    {
        Task<IResult<ContactDto>> CreateAsync(CreateContactDto contact, CancellationToken cancellationToken);

        Task<IResult<IEnumerable<ContactDto>>> GetAllAsync(ContactFilterDto filterDto, CancellationToken cancellationToken);

        Task<IResult<ContactDto>> GetAsync(Guid id, CancellationToken cancellationToken);

        Task<IResult<object>> DeleteAsync(Guid id, CancellationToken cancellationToken);
    }
}
