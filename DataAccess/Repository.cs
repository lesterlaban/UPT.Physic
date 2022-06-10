using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace UPT.Physic.DataAccess
{
	public class Repository : IRepository
    {
        private readonly Context _context;

        public Repository(Context context)
        {
            _context = context;
        }

        public async Task<T> GetByKeys<T>(params object[] keys) where T : class
        {
            return await _context.Set<T>().FindAsync(keys);
        }

        public T IncludeMultiple<T>(T entity, List<Expression<Func<T, IEnumerable<object>>>> expressions)
            where T : class
        {
            expressions.ForEach(e => _context.Entry(entity).Collection(e).Load());
            return entity;
        }

        public async ValueTask<EntityEntry<T>> Add<T>(T entity) where T : class
        {
            return await _context.Set<T>().AddAsync(entity);
        }

        public async Task<EntityEntry<T>> RemoveAsync<T>(T entity) where T : class
        {
            return await Task.Run(() => _context.Set<T>().Remove(entity));
        }

        public async Task<List<TEntity>> GetByFilterAndSort<TEntity>(Expression<Func<TEntity, bool>> filter,
            Expression<Func<TEntity, object>> sortByDescending,
            Expression<Func<TEntity, object>> sortThen,
            int take = 0, int skip = 0,
            params Expression<Func<TEntity, object>>[] includes
            ) where TEntity : class
        {
            IQueryable<TEntity> result;

            if (take == 0)
                result = _context.Set<TEntity>().OrderByDescending(sortByDescending).ThenByDescending(sortThen).Where(filter).Skip(skip);
            else
                result = _context.Set<TEntity>().OrderByDescending(sortByDescending).ThenByDescending(sortThen).Where(filter).Skip(skip).Take(take);

            includes.ToList().ForEach(i => result = result.Include(i));
            return await result.ToListAsync();
        }

        public async Task<List<TEntity>> GetByFilter<TEntity>(Expression<Func<TEntity, bool>> filter,
            int take = 0, int skip = 0,
            params Expression<Func<TEntity, object>>[] includes
            ) where TEntity : class
        {
            IQueryable<TEntity> result;

            if (take == 0)
                result = _context.Set<TEntity>().Where(filter).Skip(skip);
            else
                result = _context.Set<TEntity>().Where(filter).Skip(skip).Take(take);

            includes.ToList().ForEach(i => result = result.Include(i));
            return await result.ToListAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<List<TEntity>> GetAll<TEntity>(params Expression<Func<TEntity, object>>[] includes) where TEntity : class
        {
            var entities = _context.Set<TEntity>().AsQueryable();
            includes.ToList().ForEach(i => entities = entities.Include(i));
            return await entities.ToListAsync();
        }
    }
}
