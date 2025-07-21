namespace Atomic.Entities
{
    /// <summary>
    /// Represents a registry that manages multiple entity pools, each identified by a key.
    /// </summary>
    /// <typeparam name="TKey">The key type used to identify individual pools.</typeparam>
    /// <typeparam name="E">The type of entity managed by the pools. Must implement <see cref="IEntity"/>.</typeparam>
    public interface IEntityPoolRegistry<in TKey, E> where E : IEntity
    {
        /// <summary>
        /// Initializes the pool associated with the specified key by pre-populating it with entities.
        /// </summary>
        /// <param name="key">The key that identifies the pool.</param>
        /// <param name="count">The number of entities to preallocate in the pool.</param>
        void Initialize(TKey key, int count);

        /// <summary>
        /// Rents (retrieves) an entity from the pool associated with the specified key.
        /// If the pool is empty, a new entity may be created.
        /// </summary>
        /// <param name="key">The key identifying the pool to rent from.</param>
        /// <returns>An entity instance.</returns>
        E Rent(TKey key);

        /// <summary>
        /// Returns an entity to its corresponding pool, making it available for reuse.
        /// </summary>
        /// <param name="entity">The entity to return to the pool.</param>
        void Return(E entity);

        /// <summary>
        /// Clears all entities from the pool associated with the specified key.
        /// </summary>
        /// <param name="key">The key identifying the pool to clear.</param>
        void Clear(TKey key);

        /// <summary>
        /// Clears all entity pools in the registry.
        /// </summary>
        void Clear();
    }

    /// <summary>
    /// A non-generic alias of <see cref="IEntityPoolRegistry{TKey,E}"/> for managing pools of <see cref="IEntity"/> using string keys.
    /// </summary>
    public interface IEntityPoolRegistry : IEntityPoolRegistry<string, IEntity>
    {
    }
}