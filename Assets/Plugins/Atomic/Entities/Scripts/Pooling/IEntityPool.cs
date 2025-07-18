namespace Atomic.Entities
{
    /// <summary>
    /// Represents a pool for reusing <see cref="IEntity"/> instances to reduce allocation overhead.
    /// </summary>
    public interface IEntityPool
    {
        /// <summary>
        /// Retrieves an entity instance from the pool.
        /// </summary>
        /// <returns>An <see cref="IEntity"/> instance.</returns>
        IEntity Rent();

        /// <summary>
        /// Returns an entity back to the pool for future reuse.
        /// </summary>
        /// <param name="entity">The entity instance to return.</param>
        void Return(IEntity entity);

        /// <summary>
        /// Initializes the pool with a specified number of preallocated entities.
        /// </summary>
        /// <param name="initialCoint">The number of entities to preallocate in the pool.</param>
        void Init(int initialCoint);

        /// <summary>
        /// Clears all cached entities from the pool.
        /// </summary>
        void Clear();
    }
}