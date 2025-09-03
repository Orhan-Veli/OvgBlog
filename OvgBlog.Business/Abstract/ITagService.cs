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
        Task<IResult<Tag>> CreateAsync(Tag tag, CancellationToken cancellationToken);
        Task<IResult<object>> DeleteAsync(Guid id, CancellationToken cancellationToken);
        Task<IResult<Tag>> UpdateAsync(Tag tag, CancellationToken cancellationToken);
        Task<IResult<IEnumerable<Tag>>> GetAllAsync(CancellationToken cancellationToken);
        Task<IResult<Tag>> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<IResult<Tag>> FindIdByNameAsync(string name, CancellationToken cancellationToken);

        Task<IResult<IEnumerable<Tag>>> GetByIdsAsync(List<Guid> ids, CancellationToken cancellationToken);

    }
}
