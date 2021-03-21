using OvgBlog.Business.Constants;
using OvgBlog.DAL.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OvgBlog.Business.Abstract
{
    public interface IContactService
    {
        Task<IResult<Contact>> Create(Contact contact);

        Task<IResult<List<Contact>>> GetAll();

        Task<IResult<Contact>> Get(Guid id);

        Task<IResult<object>> Delete(Guid id);
    }
}
