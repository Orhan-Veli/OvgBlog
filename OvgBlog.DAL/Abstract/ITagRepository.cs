using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using OvgBlog.DAL.Data;

namespace OvgBlog.DAL.Abstract;

public interface ITagRepository : IEntityRepository<Tag>
{
    Task<List<Tag>> GetAllTagsByNamesAsync(List<string> names, CancellationToken cancellationToken);
    
    Task CreateBulkAsync(List<Tag> entities, CancellationToken cancellationToken);
}