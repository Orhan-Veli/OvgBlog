using OvgBlog.Business.Constants;
using OvgBlog.DAL.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OvgBlog.Business.Abstract
{
    public interface ITagService
    {
        Task<IResult<Tag>> Create(Tag tag);

        Task<IResult<object>> Delete(Guid id);
        Task<IResult<Tag>> Update(Tag tag);
        Task<IResult<IEnumerable<Tag>>> GetAll();
        Task<IResult<Tag>> GetById(Guid id);

        Task<IResult<Tag>> FindIdByName(string name);
    }
}
