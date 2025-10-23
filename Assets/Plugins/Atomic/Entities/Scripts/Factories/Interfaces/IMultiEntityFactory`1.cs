namespace Atomic.Entities
{
    /// <summary>
    /// Defines a factory for creating and managing multiple entities identified by a key of type <typeparamref name="K"/>.
    /// </summary>
    /// <typeparam name="K">
    /// The type of key used to identify an entity.
    /// </typeparam>
    /// <typeparam name="E">
    /// The type of entity to be created, which must implement the <see cref="IEntity"/> interface.
    /// </typeparam>
    public interface IMultiEntityFactory<in K, E> where E : IEntity
    {
        /// <summary>
        /// Creates a new entity based on the specified key.
        /// </summary>
        /// <param name="key">The key used to create the entity.</param>
        /// <returns>The newly created entity of type <typeparamref name="E"/>.</returns>
        E Create(K key);

        /// <summary>
        /// Attempts to create an entity based on the specified key.
        /// </summary>
        /// <param name="key">The key used to create the entity.</param>
        /// <param name="entity">
        /// When this method returns, contains the created entity if the operation succeeded; otherwise, the default value of <typeparamref name="E"/>.
        /// </param>
        /// <returns>
        /// <c>true</c> if the entity was successfully created; 
        /// <c>false</c> if creation failed (e.g., an entity with the same key already exists or the key is invalid).
        /// </returns>
        bool TryCreate(K key, out E entity);

        /// <summary>
        /// Checks whether an entity with the specified key exists.
        /// </summary>
        /// <param name="key">The key of the entity to check for existence.</param>
        /// <returns>
        /// <c>true</c> if an entity with the given key exists; 
        /// <c>false</c> otherwise.
        /// </returns>
        bool Contains(K key);
    }
}