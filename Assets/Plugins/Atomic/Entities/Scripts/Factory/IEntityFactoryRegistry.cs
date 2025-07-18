namespace Atomic.Entities
{
    /// <summary>
    /// Non-generic registry interface for accessing <see cref="IEntityFactory"/> instances by string key.
    /// </summary>
    public interface IEntityFactoryRegistry : IEntityFactoryRegistry<string, IEntity>
    {
    }

    /// <summary>
    /// A generic registry interface for storing and retrieving entity factories by key.
    /// </summary>
    /// <typeparam name="TKey">The type of the key used to identify factories.</typeparam>
    /// <typeparam name="TEntity">The type of entity created by the factories.</typeparam>
    public interface IEntityFactoryRegistry<in TKey, TEntity> where TEntity : IEntity
    {
        /// <summary>
        /// Registers an entity factory with the specified key.
        /// </summary>
        /// <param name="key">The key to associate with the factory.</param>
        /// <param name="factory">The factory to register.</param>
        void Add(TKey key, IEntityFactory<TEntity> factory);

        /// <summary>
        /// Removes the entity factory associated with the specified key.
        /// </summary>
        /// <param name="key">The key of the factory to remove.</param>
        void Remove(TKey key);

        /// <summary>
        /// Creates an entity using the factory associated with the specified key.
        /// </summary>
        /// <param name="key">The key of the factory to use.</param>
        /// <returns>A new instance of <typeparamref name="TEntity"/>.</returns>
        TEntity Create(TKey key);
    }
}