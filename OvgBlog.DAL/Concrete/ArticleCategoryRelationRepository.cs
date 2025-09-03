using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OvgBlog.DAL.Abstract;
using OvgBlog.DAL.Data;

namespace OvgBlog.DAL.Concrete;

public class ArticleCategoryRelationRepository(OvgBlogContext context)
    : EntityRepository<ArticleCategoryRelation>(context), IArticleCategoryRelationRepository
{
    private readonly OvgBlogContext _context = context;

    public async Task UpdateBulkArticleCategoryRelationsAsync(
        List<ArticleCategoryRelation> articleCategoryRelations, CancellationToken cancellationToken)
    {
        _context.ArticleCategoryRelations.UpdateRange(articleCategoryRelations);
        await _context.SaveChangesAsync(cancellationToken);
    }
}