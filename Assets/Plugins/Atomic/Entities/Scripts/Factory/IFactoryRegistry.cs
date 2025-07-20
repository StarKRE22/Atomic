namespace Atomic.Entities
{

    /// <summary>
    /// A generic registry interface for storing and retrieving entity factories by key.
    /// </summary>
    /// <typeparam name="TKey">The type of the key used to identify factories.</typeparam>
    /// <typeparam name="E">The type of entity created by the factories.</typeparam>
    public interface IFactoryRegistry<in TKey, E> where E : IEntity<E>
    {
        /// <summary>
        /// Registers an entity factory with the specified key.
        /// </summary>
        /// <param name="key">The key to associate with the factory.</param>
        /// <param name="factory">The factory to register.</param>
        void Add(TKey key, IFactory<E> factory);

        /// <summary>
        /// Removes the entity factory associated with the specified key.
        /// </summary>
        /// <param name="key">The key of the factory to remove.</param>
        void Remove(TKey key);

        /// <summary>
        /// Creates an entity using the factory associated with the specified key.
        /// </summary>
        /// <param name="key">The key of the factory to use.</param>
        /// <returns>A new instance of <typeparamref name="E"/>.</returns>
        E Create(TKey key);
    }
}