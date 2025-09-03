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
        Task CreateAsync(TEntity model, CancellationToken cancellationToken);

        Task DeleteAsync(Guid id, CancellationToken cancellationToken);

        Task<TEntity> UpdateAsync(TEntity model, CancellationToken cancellationToken);

        Task<List<TEntity>> GetAllAsync(CancellationToken cancellationToken, Expression<Func<TEntity, bool>> filter = null, Expression<Func<TEntity, object>> includeFilter = null);

        Task<TEntity> GetAsync(CancellationToken cancellationToken, Expression<Func<TEntity, bool>> filter = null, List<Expression<Func<TEntity, object>>> includeFilters = null);

        Task<List<TEntity>> GetListByExpressionsAsync(Expression<Func<TEntity, bool>> filter,
            CancellationToken cancellationToken, bool isTracking = false, List<Expression<Func<TEntity, object>>> includeFilters = null);
    }
}
