using System.Collections.Generic;

namespace Atomic.Entities
{
    /// <summary>
    /// A generic implementation of <see cref="IFactoryRegistry{TKey,E}"/> that manages entity factories by key.
    /// </summary>
    /// <typeparam name="TKey">The type of keys used to retrieve factories.</typeparam>
    public class FactoryRegistry<TKey, E> : IFactoryRegistry<TKey, E> where E : IEntity<E>
    {
        private readonly Dictionary<TKey, IEntityFactory<E>> _factories;

        /// <summary>
        /// Initializes a new empty factory dictionary.
        /// </summary>
        public FactoryRegistry() => _factories = new Dictionary<TKey, IEntityFactory<E>>();

        /// <summary>
        /// Initializes the factory with a collection of key-factory pairs.
        /// </summary>
        /// <param name="factories">The key-factory pairs to initialize with.</param>
        public FactoryRegistry(in IEnumerable<KeyValuePair<TKey, IEntityFactory<E>>> factories) =>
            _factories = new Dictionary<TKey, IEntityFactory<E>>(factories);

        /// <summary>
        /// Initializes the factory with a params array of key-factory pairs.
        /// </summary>
        /// <param name="factory">The key-factory pairs to initialize with.</param>
        public FactoryRegistry(params KeyValuePair<TKey, IEntityFactory<E>>[] factory) =>
            _factories = new Dictionary<TKey, IEntityFactory<E>>(factory);

        /// <inheritdoc />
        public void Add(TKey key, IEntityFactory<E> factory) => _factories.Add(key, factory);

        /// <inheritdoc />
        public void Remove(TKey key) => _factories.Remove(key);

        /// <inheritdoc />
        public E Create(TKey key)
        {
            return !_factories.TryGetValue(key, out IEntityFactory<E> factory)
                ? throw new KeyNotFoundException($"Entity Factory with key \"{key}\" is not found")
                : factory.Create();
        }
    }
}