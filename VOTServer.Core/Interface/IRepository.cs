using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VOTServer.Models.Interface;

namespace VOTServer.Core.Interface
{
    public interface IRepository<TEntity> where TEntity : class, IEntity
    {
        /// <summary>
        /// Add a <see cref="TEntity"/> entity.
        /// </summary>
        /// <param name="entity"><see cref="TEntity"/> entity.</param>
        /// <returns></returns>
        public Task AddAsync(TEntity entity);

        /// <summary>
        /// Add <see cref="TEntity"/> entitise.
        /// </summary>
        /// <param name="entities"><see cref="TEntity"/> entitise.</param>
        /// <returns></returns>
        public Task AddRangeAsync(IEnumerable<TEntity> entities);

        /// <summary>
        /// Remove a <see cref="TEntity"/> entity.
        /// </summary>
        /// <param name="entity"><see cref="TEntity"/> entity</param>
        /// <returns></returns>
        public Task RemoveAsync(TEntity entity);

        /// <summary>
        /// Remove <see cref="TEntity"/> entitise.
        /// </summary>
        /// <param name="entities"><see cref="TEntity"/> entitise.</param>
        /// <returns></returns>
        public Task RemoveRangeAsync(IEnumerable<TEntity> entities);

        /// <summary>
        /// update a <see cref="TEntity"/> entity.
        /// </summary>
        /// <param name="entity"><see cref="TEntity"/> entity.</param>
        /// <returns></returns>
        public Task UpdateAsync(TEntity entity);

        /// <summary>
        /// Get all <see cref="TEntity"/> entities in paged.
        /// </summary>
        /// <param name="pageSize">Size of page.</param>
        /// <param name="page">Number of page.</param>
        /// <returns></returns>
        public Task<IEnumerable<TEntity>> GetAllAsync(int pageSize, int page);

        /// <summary>
        /// Get <see cref="TEntity"/> entity by id.
        /// </summary>
        /// <param name="id">Id of <see cref="TEntity"/> entity.</param>
        /// <returns></returns>
        public Task<TEntity> GetEntityByIdAsync(long id);

        public Task<TEntity> FirstOrDefaultAsync(System.Linq.Expressions.Expression<Func<TEntity, bool>> expression);

        public Task<IEnumerable<TEntity>> SearchAsync(System.Linq.Expressions.Expression<Func<TEntity, bool>> expression, int pageSize, int page);

        public Task<int> CountAsync();

        public IQueryable<TEntity> Query();
    }
}
