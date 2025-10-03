namespace Atomic.Entities
{
    /// <summary>
    /// A generic registry interface for storing and retrieving entity factories by key.
    /// </summary>
    /// <typeparam name="TKey">The type of the key used to identify factories.</typeparam>
    /// <typeparam name="E">The type of entity created by the factories.</typeparam>
    public interface IMultiEntityFactory<in TKey, E> where E : IEntity
    {
        /// <summary>
        /// Creates an entity using the factory associated with the specified key.
        /// </summary>
        /// <param name="key">The key of the factory to use.</param>
        /// <returns>A new instance of <typeparamref name="E"/>.</returns>
        E Create(TKey key);

        /// <summary>
        /// Tries to create an entity using the factory associated with the specified key.
        /// </summary>
        /// <param name="key">The key of the factory to use.</param>
        /// <param name="entity">When this method returns, contains the created entity if the key was found; otherwise, the default value for <typeparamref name="E"/>.</param>
        /// <returns><c>true</c> if a factory was found and the entity was created; otherwise, <c>false</c>.</returns>
        bool TryCreate(TKey key, out E entity);

        /// <summary>
        /// Determines whether a factory with the specified key exists in the registry.
        /// </summary>
        /// <param name="key">The key to locate.</param>
        /// <returns><c>true</c> if the factory exists; otherwise, <c>false</c>.</returns>
        bool Contains(TKey key);
    }
}