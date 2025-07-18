using System.Collections.Generic;
using SampleGame;

namespace Atomic.Entities
{
    /// <summary>
    /// A concrete implementation of <see cref="IEntityFactoryRegistry"/> using <see cref="string"/> as key type.
    /// </summary>
    public class EntityFactoryRegistry : EntityFactoryRegistry<string>, IEntityFactoryRegistry
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
        public EntityFactoryRegistry(ScriptableEntityFactoryCatalog factoryCatalog) : base(factoryCatalog.GetAllFactories())
        {
        }

        /// <summary>
        /// Initializes the factory using the specified collection of entity factories.
        /// </summary>
        /// <param name="factorys">Key-factory pairs to initialize the factory with.</param>
        public EntityFactoryRegistry(IEnumerable<KeyValuePair<string, IEntityFactory>> factorys) : base(factorys)
        {
        }

        /// <summary>
        /// Initializes the factory using a params array of key-factory pairs.
        /// </summary>
        /// <param name="factory">The key-factory pairs to initialize with.</param>
        public EntityFactoryRegistry(params KeyValuePair<string, IEntityFactory>[] factory) : base(factory)
        {
        }
    }

    /// <summary>
    /// A generic implementation of <see cref="IEntityFactoryRegistry{TKey}"/> that manages entity factories by key.
    /// </summary>
    /// <typeparam name="TKey">The type of keys used to retrieve factories.</typeparam>
    public class EntityFactoryRegistry<TKey> : IEntityFactoryRegistry<TKey>
    {
        private readonly Dictionary<TKey, IEntityFactory> _factories;

        /// <summary>
        /// Initializes a new empty factory dictionary.
        /// </summary>
        public EntityFactoryRegistry() => _factories = new Dictionary<TKey, IEntityFactory>();

        /// <summary>
        /// Initializes the factory with a collection of key-factory pairs.
        /// </summary>
        /// <param name="factorys">The key-factory pairs to initialize with.</param>
        public EntityFactoryRegistry(in IEnumerable<KeyValuePair<TKey, IEntityFactory>> factorys) =>
            _factories = new Dictionary<TKey, IEntityFactory>(factorys);

        /// <summary>
        /// Initializes the factory with a params array of key-factory pairs.
        /// </summary>
        /// <param name="factory">The key-factory pairs to initialize with.</param>
        public EntityFactoryRegistry(params KeyValuePair<TKey, IEntityFactory>[] factory) =>
            _factories = new Dictionary<TKey, IEntityFactory>(factory);

        /// <inheritdoc />
        public void Add(TKey key, IEntityFactory factory) => _factories.Add(key, factory);

        /// <inheritdoc />
        public void Remove(TKey key) => _factories.Remove(key);

        /// <inheritdoc />
        public IEntity Create(TKey key)
        {
            return !_factories.TryGetValue(key, out IEntityFactory factory)
                ? throw new KeyNotFoundException($"Entity Factory with key \"{key}\" is not found")
                : factory.Create();
        }
    }
}