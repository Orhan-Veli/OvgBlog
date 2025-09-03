using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using OvgBlog.DAL.Abstract;
using OvgBlog.DAL.Data;

namespace OvgBlog.DAL.Concrete;

public class ArticleTagRelationRepository(OvgBlogContext context) : EntityRepository<ArticleTagRelation>(context), IArticleTagRelationRepository
{
    private readonly OvgBlogContext _context = context;
    
    public async Task CreateBulkArticleTagRelationAsync(List<ArticleTagRelation> articleTagRelations, CancellationToken cancellationToken)
    {
        await _context.ArticleTagRelations.AddRangeAsync(articleTagRelations, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }
    
    public async Task UpdateBulkArticleTagRelationsAsync(List<ArticleTagRelation> articleTagRelations, CancellationToken cancellationToken)
    {
        _context.ArticleTagRelations.UpdateRange(articleTagRelations);
        await _context.SaveChangesAsync(cancellationToken);
    }
}