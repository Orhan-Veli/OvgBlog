using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using OvgBlog.DAL.Data;

namespace OvgBlog.DAL.Abstract;

public interface IArticleCategoryRelationRepository : IEntityRepository<ArticleCategoryRelation>
{
    Task UpdateBulkArticleCategoryRelationsAsync(List<ArticleCategoryRelation> articleCategoryRelations, CancellationToken cancellationToken);
}