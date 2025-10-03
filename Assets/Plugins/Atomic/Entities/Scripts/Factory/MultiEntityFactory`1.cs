using System;
using System.Collections.Generic;

namespace Atomic.Entities
{
    /// <summary>
    /// A generic implementation of <see cref="IMultiEntityFactory{TKey, E}"/> that manages multiple entity factories.
    /// </summary>
    /// <typeparam name="TKey">The type of key used to identify factories.</typeparam>
    /// <typeparam name="E">The type of entity to be created, which must implement <see cref="IEntity"/>.</typeparam>
    public class MultiEntityFactory<TKey, E> : IMultiEntityFactory<TKey, E> where E : IEntity
    {
        private readonly Dictionary<TKey, IEntityFactory<E>> _factories;

        /// <summary>
        /// Initializes a new empty <see cref="MultiEntityFactory{TKey, E}"/>.
        /// </summary>
        public MultiEntityFactory()
        {
            _factories = new Dictionary<TKey, IEntityFactory<E>>();
        }

        /// <summary>
        /// Initializes the factory with a collection of key-factory pairs.
        /// </summary>
        /// <param name="factories">The key-factory pairs to initialize the factory with.</param>
        public MultiEntityFactory(IEnumerable<KeyValuePair<TKey, IEntityFactory<E>>> factories)
        {
            _factories = new Dictionary<TKey, IEntityFactory<E>>(factories);
        }

        /// <summary>
        /// Initializes the factory with a read-only dictionary of key-factory pairs.
        /// </summary>
        /// <param name="factories">The dictionary of key-factory pairs to initialize with.</param>
        public MultiEntityFactory(IReadOnlyDictionary<TKey, IEntityFactory<E>> factories)
        {
            _factories = new Dictionary<TKey, IEntityFactory<E>>(factories);
        }

        /// <summary>
        /// Initializes the factory with a params array of key-factory pairs.
        /// </summary>
        /// <param name="factories">The key-factory pairs to initialize the factory with.</param>
        public MultiEntityFactory(params KeyValuePair<TKey, IEntityFactory<E>>[] factories)
        {
            _factories = new Dictionary<TKey, IEntityFactory<E>>(factories);
        }

        /// <summary>
        /// Registers a new entity factory with the specified key.
        /// </summary>
        /// <param name="key">The key to associate with the factory.</param>
        /// <param name="factory">The factory instance to register.</param>
        /// <exception cref="ArgumentException">Thrown if a factory with the same key already exists.</exception>
        public void Register(TKey key, IEntityFactory<E> factory)
        {
            _factories.Add(key, factory);
        }

        /// <summary>
        /// Unregisters the entity factory associated with the specified key.
        /// </summary>
        /// <param name="key">The key of the factory to remove.</param>
        /// <remarks>If the key does not exist, this method does nothing.</remarks>
        public void Unregister(TKey key)
        {
            _factories.Remove(key);
        }

        /// <inheritdoc />
        public E Create(TKey key)
        {
            IEntityFactory<E> factory = _factories[key];
            return factory.Create();
        }

        /// <inheritdoc />
        public bool TryCreate(TKey key, out E entity)
        {
            bool exists = _factories.TryGetValue(key, out IEntityFactory<E> factory);
            entity = exists ? factory.Create() : default;
            return exists;
        }

        /// <inheritdoc />
        public bool Contains(TKey key)
        {
            return _factories.ContainsKey(key);
        }
    }
}