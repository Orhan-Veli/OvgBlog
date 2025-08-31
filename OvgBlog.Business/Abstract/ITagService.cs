using OvgBlog.Business.Constants;
using OvgBlog.DAL.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OvgBlog.Business.Abstract
{
    public interface ITagService
    {
        Task<IResult<Tag>> Create(Tag tag, CancellationToken cancellationToken);
        Task<IResult<object>> Delete(Guid id, CancellationToken cancellationToken);
        Task<IResult<Tag>> Update(Tag tag, CancellationToken cancellationToken);
        Task<IResult<IEnumerable<Tag>>> GetAll(CancellationToken cancellationToken);
        Task<IResult<Tag>> GetById(Guid id, CancellationToken cancellationToken);
        Task<IResult<Tag>> FindIdByName(string name, CancellationToken cancellationToken);

        Task<IResult<IEnumerable<Tag>>> GetByIds(List<Guid> ids, CancellationToken cancellationToken);

    }
}
