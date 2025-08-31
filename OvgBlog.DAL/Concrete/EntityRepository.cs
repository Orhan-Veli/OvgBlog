using Microsoft.EntityFrameworkCore;
using OvgBlog.DAL.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using OvgBlog.DAL.Data.Base;

namespace OvgBlog.DAL.Concrete
{
    public class EntityRepository<TEntity>(OvgBlogContext context) : IEntityRepository<TEntity>
        where TEntity : BaseEntity, new()

    {
        public async Task Create(TEntity entity, CancellationToken cancellationToken)
        {
            var addedEntity = context.Entry(entity);
            addedEntity.State = EntityState.Added;
            await context.SaveChangesAsync(cancellationToken);
        }
        
        public async Task Delete(Guid id, CancellationToken cancellationToken)
        {
            var deletedEntity = context.Entry(id);
            deletedEntity.State = EntityState.Deleted;
            await context.SaveChangesAsync(cancellationToken);
        }

        public async Task<IEnumerable<TEntity>> GetAll(CancellationToken cancellationToken, Expression<Func<TEntity, bool>> filter = null, Expression<Func<TEntity, object>> includeFilter = null)
        {
            IEnumerable<TEntity> result = null;

            if (filter == null)
            {
                result = includeFilter == null
                    ? await context.Set<TEntity>().ToListAsync(cancellationToken)
                    : await context.Set<TEntity>().Include(includeFilter).ToListAsync(cancellationToken);
            }
            else
            {
                result = includeFilter == null
                    ? await context.Set<TEntity>().Where(filter).ToListAsync(cancellationToken)
                    : await context.Set<TEntity>().Where(filter).Include(includeFilter).ToListAsync(cancellationToken);
            }
            return result;
        }

        public async Task<TEntity> Get(CancellationToken cancellationToken, Expression<Func<TEntity, bool>> filter = null, List<Expression<Func<TEntity, object>>> includeFilters = null)
        {

            if (includeFilters == null || includeFilters.Count == 0)
            {
                return filter == null
                    ? await context.Set<TEntity>().SingleOrDefaultAsync(cancellationToken)
                    : await context.Set<TEntity>().SingleOrDefaultAsync(filter, cancellationToken);
            }
            else
            {
                var query = context.Set<TEntity>().AsQueryable();
                foreach (var item in includeFilters)
                {
                    query = query.Include(item);
                }
                return filter == null
                    ? await query.SingleOrDefaultAsync(cancellationToken)
                    : await query.SingleOrDefaultAsync(filter, cancellationToken);
            }

        }

        public async Task<TEntity> Update(TEntity model, CancellationToken cancellationToken)
        {
            var updatedEntity = context.Entry(model);
            updatedEntity.State = EntityState.Modified;
            await context.SaveChangesAsync(cancellationToken);
            return updatedEntity.Entity;
        }
    }
}
