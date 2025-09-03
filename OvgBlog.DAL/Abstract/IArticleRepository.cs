using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using OvgBlog.DAL.Data;

namespace OvgBlog.DAL.Abstract;

public interface IArticleRepository : IEntityRepository<Article>
{
    Task<List<Article>> GetFilteredArticlesAsync(int size, CancellationToken cancellationToken);
}