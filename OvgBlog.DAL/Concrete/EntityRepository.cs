using Microsoft.EntityFrameworkCore;
using OvgBlog.DAL.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace OvgBlog.DAL.Concrete
{
    public class EntityRepository<TEntity, TContext> : IEntityRepository<TEntity> where TEntity : class, new()
        where TContext : DbContext, new()
    {
        public async Task Create(TEntity entity)
        {
            using (TContext context = new TContext())
            {
                var addedEntity = context.Entry(entity);
                addedEntity.State = EntityState.Added;
                await context.SaveChangesAsync();
            }
        }
        public async Task Delete(Guid id)
        {
            using (TContext context = new TContext())
            {
                var deletedEntity = context.Entry(id);
                deletedEntity.State = EntityState.Deleted;
                await context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<TEntity>> GetAll(Expression<Func<TEntity, bool>> filter = null, Expression<Func<TEntity, object>> includeFilter = null)
        {
            IEnumerable<TEntity> result = null;
            using (TContext context = new TContext())
            {
                if (filter == null)
                {
                    result = includeFilter == null
                        ? await context.Set<TEntity>().ToListAsync()
                        : await context.Set<TEntity>().Include(includeFilter).ToListAsync();
                }
                else
                {
                    result = includeFilter == null
                        ? await context.Set<TEntity>().Where(filter).ToListAsync()
                        : await context.Set<TEntity>().Where(filter).Include(includeFilter).ToListAsync();
                }

                return result;
            }
        }

        public async Task<TEntity> Get(Expression<Func<TEntity, bool>> filter = null, Expression<Func<TEntity, object>> includeFilter = null)
        {
            using (TContext context = new TContext())
            {
                return includeFilter == null
                    ? await context.Set<TEntity>().SingleOrDefaultAsync(filter)
                    : await context.Set<TEntity>().Include(includeFilter).SingleOrDefaultAsync(filter);
            }
        }

        public async Task<TEntity> Update(TEntity model)
        {
            using (TContext context = new TContext())
            {
                var updatedEntity = context.Entry(model);
                updatedEntity.State = EntityState.Modified;
                await context.SaveChangesAsync();
                return updatedEntity.Entity;
            }
        }
    }
}
