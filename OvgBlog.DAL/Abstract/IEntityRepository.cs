using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace OvgBlog.DAL.Abstract
{
    public interface IEntityRepository<TEntity> : IEntity where TEntity : class, new()
    {
        Task Create(TEntity Model);

        Task Delete(Guid id);

        Task<TEntity> Update(TEntity model);

        Task<IEnumerable<TEntity>> GetAll(Expression<Func<TEntity, bool>> filter = null, Expression<Func<TEntity, object>> includeFilter = null);

        Task<TEntity> Get(Expression<Func<TEntity, bool>> filter = null, List<Expression<Func<TEntity, object>>> includeFilters = null);
    }
}
