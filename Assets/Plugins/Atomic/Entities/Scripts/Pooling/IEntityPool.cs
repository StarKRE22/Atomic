using System;

namespace Atomic.Entities
{
    /// <summary>
    /// A non-generic alias for <see cref="IEntityPool{T}"/> that operates on the base <see cref="IEntity"/> type.
    /// </summary>
    /// <remarks>
    /// Use this interface when you do not need type-specific access to entity creation or pooling.
    /// </remarks>
    public interface IEntityPool : IEntityPool<IEntity>
    {
    }

    /// <summary>
    /// Represents a pool for reusing <see cref="IEntity"/> instances to reduce allocation overhead.
    /// </summary>
    public interface IEntityPool<E> : IDisposable where E : IEntity
    {
        /// <summary>
        /// Retrieves an entity instance from the pool.
        /// </summary>
        /// <returns>An <see cref="IEntity"/> instance.</returns>
        E Rent();

        /// <summary>
        /// Returns an entity back to the pool for future reuse.
        /// </summary>
        /// <param name="entity">The entity instance to return.</param>
        void Return(E entity);

        /// <summary>
        /// Initializes the pool with a specified number of preallocated entities.
        /// </summary>
        /// <param name="initialCoint">The number of entities to preallocate in the pool.</param>
        void Init(int initialCoint);
    }
}