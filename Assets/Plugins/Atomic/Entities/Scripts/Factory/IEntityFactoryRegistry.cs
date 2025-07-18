namespace Atomic.Entities
{
    /// <summary>
    /// Non-generic entity factory registry interface using string as the key type.
    /// </summary>
    public interface IEntityFactoryRegistry : IEntityFactoryRegistry<string>
    {
    }

    /// <summary>
    /// Generic interface for registering and resolving entity factories by key.
    /// </summary>
    /// <typeparam name="TKey">The type used as a key for mapping factories.</typeparam>
    public interface IEntityFactoryRegistry<in TKey>
    {
        /// <summary>
        /// Adds a new entity factory associated with the specified key.
        /// </summary>
        /// <param name="key">The key to associate the factory with.</param>
        /// <param name="factory">The entity factory to add.</param>
        void Add(TKey key, IEntityFactory factory);

        /// <summary>
        /// Removes the factory associated with the specified key.
        /// </summary>
        /// <param name="key">The key of the factory to remove.</param>
        void Remove(TKey key);

        /// <summary>
        /// Creates an entity using the factory associated with the specified key.
        /// </summary>
        /// <param name="key">The key identifying the factory to use.</param>
        /// <returns>The created entity.</returns>
        IEntity Create(TKey key);
    }
}