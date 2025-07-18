using System.Collections.Generic;
using SampleGame;

namespace Atomic.Entities
{
    /// <summary>
    /// A concrete implementation of <see cref="IEntityFactoryRegistry"/> using <see cref="string"/> as key type.
    /// </summary>
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
        /// <param name="factoryCatalog">The catalog providing entity factories.</param>
        public EntityFactoryRegistry(ScriptableEntityFactoryCatalog<IEntity> factoryCatalog) : 
            base(factoryCatalog.GetAllFactories())
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
        public EntityFactoryRegistry(params KeyValuePair<string, IEntityFactory<IEntity>>[] factory) : base(factory)
        {
        }
    }

    /// <summary>
    /// A generic implementation of <see cref="IEntityFactoryRegistry{TKey}"/> that manages entity factories by key.
    /// </summary>
    /// <typeparam name="TKey">The type of keys used to retrieve factories.</typeparam>
    public class EntityFactoryRegistry<TKey, TEntity> : IEntityFactoryRegistry<TKey, TEntity> where TEntity : IEntity
    {
        private readonly Dictionary<TKey, IEntityFactory<TEntity>> _factories;

        /// <summary>
        /// Initializes a new empty factory dictionary.
        /// </summary>
        public EntityFactoryRegistry() => _factories = new Dictionary<TKey, IEntityFactory<TEntity>>();

        /// <summary>
        /// Initializes the factory with a collection of key-factory pairs.
        /// </summary>
        /// <param name="factories">The key-factory pairs to initialize with.</param>
        public EntityFactoryRegistry(in IEnumerable<KeyValuePair<TKey, IEntityFactory<TEntity>>> factories) =>
            _factories = new Dictionary<TKey, IEntityFactory<TEntity>>(factories);

        /// <summary>
        /// Initializes the factory with a params array of key-factory pairs.
        /// </summary>
        /// <param name="factory">The key-factory pairs to initialize with.</param>
        public EntityFactoryRegistry(params KeyValuePair<TKey, IEntityFactory<TEntity>>[] factory) =>
            _factories = new Dictionary<TKey, IEntityFactory<TEntity>>(factory);

        /// <inheritdoc />
        public void Add(TKey key, IEntityFactory<TEntity> factory) => _factories.Add(key, factory);

        /// <inheritdoc />
        public void Remove(TKey key) => _factories.Remove(key);

        /// <inheritdoc />
        public TEntity Create(TKey key)
        {
            return !_factories.TryGetValue(key, out IEntityFactory<TEntity> factory)
                ? throw new KeyNotFoundException($"Entity Factory with key \"{key}\" is not found")
                : factory.Create();
        }
    }
}