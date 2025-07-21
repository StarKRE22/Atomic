using System.Collections.Generic;

namespace Atomic.Entities
{
    /// <summary>
    /// A generic implementation of <see cref="IEntityFactoryRegistry{TKey,E}"/> that manages entity factories by key.
    /// </summary>
    /// <typeparam name="TKey">The type of keys used to retrieve factories.</typeparam>
    public class EntityFactoryRegistry<TKey, E> : IEntityFactoryRegistry<TKey, E> where E : IEntity
    {
        private readonly Dictionary<TKey, IEntityFactory<E>> _factories;

        /// <summary>
        /// Initializes a new empty factory dictionary.
        /// </summary>
        public EntityFactoryRegistry() => _factories = new Dictionary<TKey, IEntityFactory<E>>();

        /// <summary>
        /// Initializes the factory with a collection of key-factory pairs.
        /// </summary>
        /// <param name="factories">The key-factory pairs to initialize with.</param>
        public EntityFactoryRegistry(IEnumerable<KeyValuePair<TKey, IEntityFactory<E>>> factories) =>
            _factories = new Dictionary<TKey, IEntityFactory<E>>(factories);

        public EntityFactoryRegistry(IReadOnlyDictionary<TKey, IEntityFactory<E>> factories) =>
            _factories = new Dictionary<TKey, IEntityFactory<E>>(factories);

        /// <summary>
        /// Initializes the factory with a params array of key-factory pairs.
        /// </summary>
        /// <param name="factory">The key-factory pairs to initialize with.</param>
        public EntityFactoryRegistry(params KeyValuePair<TKey, IEntityFactory<E>>[] factory) =>
            _factories = new Dictionary<TKey, IEntityFactory<E>>(factory);

        /// <inheritdoc />
        public void Add(TKey key, IEntityFactory<E> factory) => _factories.Add(key, factory);

        /// <inheritdoc />
        public void Remove(TKey key) => _factories.Remove(key);

        /// <inheritdoc />
        public E Create(TKey key)
        {
            return !_factories.TryGetValue(key, out IEntityFactory<E> factory)
                ? throw new KeyNotFoundException($"Entity Factory with key \"{key}\" is not found!")
                : factory.Create();
        }
    }

    /// <summary>
    /// A specialized registry for managing <see cref="IEntity"/> factories, 
    /// using string-based keys for lookup and registration.
    /// </summary>
    /// <remarks>
    /// Inherits from the generic <see cref="EntityFactoryRegistry{TKey, TValue}"/> 
    /// with <c>TKey</c> as <see cref="string"/> and <c>TValue</c> as <see cref="IEntity"/>.
    /// </remarks>
    /// <example>
    /// Example usage:
    /// <code>
    /// var registry = new EntityFactoryRegistry();
    /// registry.Register("Enemy", () => new EnemyEntity());
    /// var entity = registry.Create("Enemy");
    /// </code>
    /// </example>
    public class EntityFactoryRegistry : EntityFactoryRegistry<string, IEntity>, IEntityFactoryRegistry
    {
        /// <summary>
        /// Initializes a new empty factory.
        /// </summary>
        public EntityFactoryRegistry()
        {
        }

        /// <summary>
        /// Initializes the factory using a catalog of scriptable entity factories.
        /// </summary>
        /// <param name="factories">The map providing entity factories.</param>
        public EntityFactoryRegistry(IReadOnlyDictionary<string, IEntityFactory<IEntity>> factories) :
            base(factories)
        {
        }

        /// <summary>
        /// Initializes the factory using the specified collection of entity factories.
        /// </summary>
        /// <param name="factories">Key-factory pairs to initialize the factory with.</param>
        public EntityFactoryRegistry(IEnumerable<KeyValuePair<string, IEntityFactory<IEntity>>> factories) :
            base(factories)
        {
        }

        /// <summary>
        /// Initializes the factory using a params array of key-factory pairs.
        /// </summary>
        /// <param name="factory">The key-factory pairs to initialize with.</param>
        public EntityFactoryRegistry(params KeyValuePair<string, IEntityFactory<IEntity>>[] factory) : 
            base(factory)
        {
        }
    }
}