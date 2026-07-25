using System;

namespace Atomic.Entities
{
    /// <summary>
    /// Represents a pool for reusing <see cref="IEntity"/> instances to reduce allocation overhead.
    /// </summary>
    public interface IEntityPool<TEntity> : IDisposable where TEntity : IEntity
    {
        /// <summary>
        /// Retrieves an entity instance from the pool.
        /// </summary>
        /// <returns>An <see cref="IEntity"/> instance.</returns>
        TEntity Rent();

        /// <summary>
        /// Returns an entity back to the pool for future reuse.
        /// </summary>
        /// <param name="entity">The entity instance to return.</param>
        void Return(TEntity entity);

        /// <summary>
        /// Initializes the pool with a specified number of preallocated entities.
        /// </summary>
        /// <param name="initialCount">The number of entities to preallocate in the pool.</param>
        void Init(int initialCount); 
    }
}