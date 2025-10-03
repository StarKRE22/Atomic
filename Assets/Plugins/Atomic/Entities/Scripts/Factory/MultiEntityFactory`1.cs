using System;
using System.Collections.Generic;

namespace Atomic.Entities
{
    /// <summary>
    /// A generic implementation of <see cref="IMultiEntityFactory{TKey,E}"/> that manages entity factories by key.
    /// </summary>
    /// <typeparam name="TKey">The type of keys used to retrieve factories.</typeparam>
    public class MultiEntityFactory<TKey, E> : IMultiEntityFactory<TKey, E> where E : IEntity
    {
        private readonly Dictionary<TKey, IEntityFactory<E>> _factories;

        /// <summary>
        /// Initializes a new empty factory dictionary.
        /// </summary>
        public MultiEntityFactory()
        {
            _factories = new Dictionary<TKey, IEntityFactory<E>>();
        }

        /// <summary>
        /// Initializes the factory with a collection of key-factory pairs.
        /// </summary>
        /// <param name="factories">The key-factory pairs to initialize with.</param>
        public MultiEntityFactory(IEnumerable<KeyValuePair<TKey, IEntityFactory<E>>> factories)
        {
            _factories = new Dictionary<TKey, IEntityFactory<E>>(factories);
        }

        public MultiEntityFactory(IReadOnlyDictionary<TKey, IEntityFactory<E>> factories)
        {
            _factories = new Dictionary<TKey, IEntityFactory<E>>(factories);
        }

        /// <summary>
        /// Initializes the factory with a params array of key-factory pairs.
        /// </summary>
        /// <param name="factories">The key-factory pairs to initialize with.</param>
        public MultiEntityFactory(params KeyValuePair<TKey, IEntityFactory<E>>[] factories)
        {
            _factories = new Dictionary<TKey, IEntityFactory<E>>(factories);
        }

        /// <summary>
        /// Registers an entity factory with the specified string key.
        /// </summary>
        /// <param name="key">The string key to associate with the factory.</param>
        /// <param name="factory">The factory instance to register.</param>
        /// <exception cref="ArgumentException">Thrown when <paramref name="key"/> is already exits</exception>
        public void Register(TKey key, IEntityFactory<E> factory)
        {
            _factories.Add(key, factory);
        }

        /// <summary>
        /// Removes the entity factory associated with the specified key.
        /// </summary>
        /// <param name="key">The key of the factory to remove.</param>
        /// <remarks>If the key does not exist, the method does nothing.</remarks>
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