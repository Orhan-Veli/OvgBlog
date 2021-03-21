using Microsoft.EntityFrameworkCore;
using OvgBlog.DAL.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace OvgBlog.DAL.Concrete
{
    public class EntityRepository<TEntity> : IEntityRepository<TEntity> where TEntity : class, new()

    {
        private readonly OvgBlogContext _context;
        public EntityRepository(OvgBlogContext context)
        {
            _context = context;
        }
        public async Task Create(TEntity entity)
        {
            var addedEntity = _context.Entry(entity);
            addedEntity.State = EntityState.Added;
            await _context.SaveChangesAsync();
        }
        public async Task Delete(Guid id)
        {
            var deletedEntity = _context.Entry(id);
            deletedEntity.State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAll(Expression<Func<TEntity, bool>> filter = null, Expression<Func<TEntity, object>> includeFilter = null)
        {
            IEnumerable<TEntity> result = null;

            if (filter == null)
            {
                result = includeFilter == null
                    ? await _context.Set<TEntity>().ToListAsync()
                    : await _context.Set<TEntity>().Include(includeFilter).ToListAsync();
            }
            else
            {
                result = includeFilter == null
                    ? await _context.Set<TEntity>().Where(filter).ToListAsync()
                    : await _context.Set<TEntity>().Where(filter).Include(includeFilter).ToListAsync();
            }
            return result;
        }

        public async Task<TEntity> Get(Expression<Func<TEntity, bool>> filter = null, List<Expression<Func<TEntity, object>>> includeFilters = null)
        {

            if (includeFilters == null || includeFilters.Count == 0)
            {
                return filter == null
                    ? await _context.Set<TEntity>().SingleOrDefaultAsync()
                    : await _context.Set<TEntity>().SingleOrDefaultAsync(filter);
            }
            else
            {
                var query = _context.Set<TEntity>().AsQueryable();
                foreach (var item in includeFilters)
                {
                    query = query.Include(item);
                }
                return filter == null
                    ? await query.SingleOrDefaultAsync()
                    : await query.SingleOrDefaultAsync(filter);
            }

        }

        public async Task<TEntity> Update(TEntity model)
        {
            var updatedEntity = _context.Entry(model);
            updatedEntity.State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return updatedEntity.Entity;
        }
    }
}
