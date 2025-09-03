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
        Task<IResult<Contact>> CreateAsync(Contact contact, CancellationToken cancellationToken);

        Task<IResult<List<Contact>>> GetAllAsync(CancellationToken cancellationToken);

        Task<IResult<Contact>> GetAsync(Guid id, CancellationToken cancellationToken);

        Task<IResult<object>> DeleteAsync(Guid id, CancellationToken cancellationToken);
    }
}
