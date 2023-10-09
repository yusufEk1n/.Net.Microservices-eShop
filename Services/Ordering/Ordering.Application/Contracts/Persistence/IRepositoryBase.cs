using Ordering.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Contracts.Persistence
{
    public interface IRepositoryBase<TEntity> where TEntity : EntityBase
    {
        /// <summary>
        /// Get all entities
        /// </summary>
        /// <returns> The <see cref="IReadOnlyList{TEntity}"/>. </returns>
        Task<IReadOnlyList<TEntity>> GetAllAsync();

        /// <summary>
        /// Get all entities
        /// </summary>
        /// <param name="predicate"> The predicate. </param>
        /// <returns> The <see cref="IReadOnlyList{TEntity}"/>. </returns>
        Task<IReadOnlyList<TEntity>> GetAsync(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// Get all entities
        /// </summary>
        /// <param name="predicate"> The predicate. </param>
        /// <param name="orderBy"> The order by. </param>
        /// <param name="includeString"> The include string. </param>
        /// <param name="disableTracking"> The disable tracking. </param>
        /// <returns> The <see cref="IReadOnlyList{TEntity}"/>. </returns>

        Task<IReadOnlyList<TEntity>> GetAsync(Expression<Func<TEntity, bool>> predicate = null,
                                        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                                        string includeString = null,
                                        bool disableTracking = true);

        /// <summary>
        /// Get all entities
        /// </summary>
        /// <param name="predicate"> The predicate. </param>
        /// <param name="orderBy"> The order by. </param>
        /// <param name="includes"> The includes. </param>
        /// <param name="disableTracking"> The disable tracking. </param>
        /// <returns> The <see cref="IReadOnlyList{TEntity}"/>. </returns>
        Task<IReadOnlyList<TEntity>> GetAsync(Expression<Func<TEntity, bool>> predicate = null,
                                        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                                        List<Expression<Func<TEntity, object>>> inludes = null,
                                        bool disableTracking = true);

        /// <summary>
        /// Get entity by id
        /// </summary>
        /// <param name="id"> The id. </param>
        /// <returns> The <see cref="TEntity"/>. </returns>
        Task<TEntity?> GetByIdAsync(int id);

        /// <summary>
        /// Add entity
        /// </summary>
        /// <param name="entity"> The entity. </param>
        /// <returns> The <see cref="TEntity"/>. </returns>
        Task<TEntity> AddAsync(TEntity entity);

        /// <summary>
        /// Update entity
        /// </summary>
        /// <param name="entity"> The entity. </param>
        /// <returns> The <see cref="TEntity"/>. </returns>
        Task UpdateAsync(TEntity entity);

        /// <summary>
        /// Delete entity
        /// </summary>
        /// <param name="entity"> The entity. </param>
        /// <returns> The <see cref="TEntity"/>. </returns>

        Task DeleteAsync(TEntity entity);
    }
}
