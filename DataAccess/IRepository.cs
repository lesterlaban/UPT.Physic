using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace UPT.Physic.DataAccess
{
	public interface IRepository
	{
        /// <summary>
        /// Method to get all entities from postgre Repository
        /// </summary>
        /// <typeparam name="TEntity">Entity To Get</typeparam>
        /// <returns>list of entity</returns>
        Task<List<TEntity>> GetAll<TEntity>(params Expression<Func<TEntity, object>>[] includes) where TEntity : class;
        /// <summary>
        /// Method to get entity with their primary(s) key(s)
        /// </summary>
        /// <typeparam name="T">Entity To Get</typeparam>
        /// <returns>entity</returns>
        Task<T> GetByKeys<T>(params object[] keys) where T : class;
        /// <summary>
        /// Method to get entity by filters, and sort
        /// </summary>
        /// <typeparam name="TEntity">entity type</typeparam>
        Task<List<TEntity>> GetByFilterAndSort<TEntity>(Expression<Func<TEntity, bool>> filter,
            Expression<Func<TEntity, object>> sortByDescending,
            Expression<Func<TEntity, object>> sortThen,
            int take = 0, int skip = 0,
            params Expression<Func<TEntity, object>>[] includes) where TEntity : class;

        /// <summary>
        /// Method to get a list of entities from postgre repository apllying filters
        /// </summary>
        /// <typeparam name="T">Entity To Get</typeparam>
        /// <returns>list of entity</returns>
        Task<List<TEntity>> GetByFilter<TEntity>(Expression<Func<TEntity, bool>> filter,
            int take = 0, int skip = 0,
            params Expression<Func<TEntity, object>>[] includes) where TEntity : class;

        /// <summary>
        /// Method to add new entity
        /// </summary>
        /// <typeparam name="T">entity type</typeparam>
        ValueTask<EntityEntry<T>> Add<T>(T entity) where T : class;
        /// <summary>
        /// Method to remove entity
        /// </summary>
        /// <typeparam name="T">entity type</typeparam>
        Task<EntityEntry<T>> RemoveAsync<T>(T entity) where T : class;
        /// <summary>
        /// Method to commit changes in postgresql
        /// </summary>
        Task SaveChangesAsync();
        /// <summary>
        /// Method to get related virtual properties
        /// </summary>
        /// <param name="entity">entity to get</param>
        /// <param name="expressions">expresion to properies to get</param>
        /// <typeparam name="T">type of entity</typeparam>
        /// <returns>Same Entity with related properties</returns>
        T IncludeMultiple<T>(T entity, List<Expression<Func<T, IEnumerable<object>>>> expressions)
            where T : class;
    }
}
