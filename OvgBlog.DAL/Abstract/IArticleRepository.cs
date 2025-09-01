using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using OvgBlog.DAL.Data;

namespace OvgBlog.DAL.Abstract;

public interface IArticleRepository : IEntityRepository<Article>
{
    Task<List<Article>> GetFilteredArticles(int size, CancellationToken cancellationToken);
}