using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OvgBlog.DAL.Abstract;
using OvgBlog.DAL.Data;

namespace OvgBlog.DAL.Concrete;

public class TagRepository(OvgBlogContext context) : EntityRepository<Tag>(context), ITagRepository
{
    private readonly OvgBlogContext _context = context;

    public async Task<List<Tag>> GetAllTagsByNamesAsync(List<string> names, CancellationToken cancellationToken)
    {
        var loweredNames = names.Select(n => n.ToLower()).ToList();

        var query = _context.Tags
            .Where(x => !x.IsDeleted && loweredNames.Contains(x.Name.ToLower()));

        return await query.ToListAsync(cancellationToken);
    }

    public async Task CreateBulkAsync(List<Tag> entities, CancellationToken cancellationToken)
    {
        await _context.Tags.AddRangeAsync(entities, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }
}