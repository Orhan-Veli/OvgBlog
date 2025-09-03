using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OvgBlog.DAL.Abstract;
using OvgBlog.DAL.Data;

namespace OvgBlog.DAL.Concrete;

public class ArticleRepository(OvgBlogContext context) : EntityRepository<Article>(context), IArticleRepository
{
    private readonly OvgBlogContext _context = context;

    public async Task<List<Article>> GetFilteredArticlesAsync(int size, CancellationToken cancellationToken)
    {
        var result = await _context.Articles
            .Include(x => x.ArticleCategoryRelations)
            .Include(x => x.ArticleTagRelations)
            .Where(x => !x.IsDeleted)
            .OrderBy(x => x.CreatedDate)
            .Take(size)
            .ToListAsync(cancellationToken);

        return result;
    }
}