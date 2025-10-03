using System;
using System.Collections.Generic;

namespace Atomic.Entities
{
    /// <summary>
    /// A generic implementation of <see cref="IMultiEntityFactory{TKey, E}"/> that manages multiple entity factories.
    /// </summary>
    /// <typeparam name="K">The type of key used to identify factories.</typeparam>
    /// <typeparam name="E">The type of entity to be created, which must implement <see cref="IEntity"/>.</typeparam>
    public class MultiEntityFactory<K, E> : IMultiEntityFactory<K, E> where E : IEntity
    {
        private readonly Dictionary<K, IEntityFactory<E>> _factories;

        /// <summary>
        /// Initializes a new empty <see cref="MultiEntityFactory{TKey, E}"/>.
        /// </summary>
        public MultiEntityFactory()
        {
            _factories = new Dictionary<K, IEntityFactory<E>>();
        }

        /// <summary>
        /// Initializes the factory with a collection of key-factory pairs.
        /// </summary>
        /// <param name="factories">The key-factory pairs to initialize the factory with.</param>
        public MultiEntityFactory(IEnumerable<KeyValuePair<K, IEntityFactory<E>>> factories)
        {
            _factories = new Dictionary<K, IEntityFactory<E>>(factories);
        }

        /// <summary>
        /// Initializes the factory with a read-only dictionary of key-factory pairs.
        /// </summary>
        /// <param name="factories">The dictionary of key-factory pairs to initialize with.</param>
        public MultiEntityFactory(IReadOnlyDictionary<K, IEntityFactory<E>> factories)
        {
            _factories = new Dictionary<K, IEntityFactory<E>>(factories);
        }

        /// <summary>
        /// Initializes the factory with a params array of key-factory pairs.
        /// </summary>
        /// <param name="factories">The key-factory pairs to initialize the factory with.</param>
        public MultiEntityFactory(params KeyValuePair<K, IEntityFactory<E>>[] factories)
        {
            _factories = new Dictionary<K, IEntityFactory<E>>(factories);
        }

        /// <summary>
        /// Registers a new entity factory with the specified key.
        /// </summary>
        /// <param name="key">The key to associate with the factory.</param>
        /// <param name="factory">The factory instance to register.</param>
        /// <exception cref="ArgumentException">Thrown if a factory with the same key already exists.</exception>
        public void Register(K key, IEntityFactory<E> factory)
        {
            _factories.Add(key, factory);
        }

        /// <summary>
        /// Unregisters the entity factory associated with the specified key.
        /// </summary>
        /// <param name="key">The key of the factory to remove.</param>
        /// <remarks>If the key does not exist, this method does nothing.</remarks>
        public void Unregister(K key)
        {
            _factories.Remove(key);
        }

        /// <inheritdoc />
        public E Create(K key)
        {
            IEntityFactory<E> factory = _factories[key];
            return factory.Create();
        }

        /// <inheritdoc />
        public bool TryCreate(K key, out E entity)
        {
            bool exists = _factories.TryGetValue(key, out IEntityFactory<E> factory);
            entity = exists ? factory.Create() : default;
            return exists;
        }

        /// <inheritdoc />
        public bool Contains(K key)
        {
            return _factories.ContainsKey(key);
        }
    }
}