using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using OvgBlog.DAL.Concrete;
using OvgBlog.DAL.Data;

namespace OvgBlog.DAL.Abstract;

public interface IArticleTagRelationRepository : IEntityRepository<ArticleTagRelation>
{
    Task CreateBulkArticleTagRelationAsync(List<ArticleTagRelation> articleTagRelations, CancellationToken cancellationToken);

    Task UpdateBulkArticleTagRelationsAsync(List<ArticleTagRelation> articleTagRelations,
        CancellationToken cancellationToken);
}