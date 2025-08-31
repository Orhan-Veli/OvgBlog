using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using OvgBlog.DAL.Data.Base;

namespace OvgBlog.DAL.Abstract
{
    public interface IEntityRepository<TEntity> : IEntity where TEntity : BaseEntity, new()
    {
        Task Create(TEntity model, CancellationToken cancellationToken);

        Task Delete(Guid id, CancellationToken cancellationToken);

        Task<TEntity> Update(TEntity model, CancellationToken cancellationToken);

        Task<IEnumerable<TEntity>> GetAll(CancellationToken cancellationToken, Expression<Func<TEntity, bool>> filter = null, Expression<Func<TEntity, object>> includeFilter = null);

        Task<TEntity> Get(CancellationToken cancellationToken, Expression<Func<TEntity, bool>> filter = null, List<Expression<Func<TEntity, object>>> includeFilters = null);
    }
}
