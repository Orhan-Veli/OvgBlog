using OvgBlog.Business.Constants;
using OvgBlog.DAL.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OvgBlog.Business.Abstract
{
    public interface IContactService
    {
        Task<IResult<Contact>> Create(Contact contact, CancellationToken cancellationToken);

        Task<IResult<List<Contact>>> GetAll(CancellationToken cancellationToken);

        Task<IResult<Contact>> Get(Guid id, CancellationToken cancellationToken);

        Task<IResult<object>> Delete(Guid id, CancellationToken cancellationToken);
    }
}
