using System.Collections.Generic;
using SampleGame;

namespace Atomic.Entities
{
    /// <summary>
    /// A non-generic interface for entity factories using <see cref="string"/> as key type.
    /// </summary>
    public interface IGenericEntityFactory : IGenericEntityFactory<string>
    {
    }

    /// <summary>
    /// A generic interface for entity factories that can create <see cref="IEntity"/> instances using a specific key type.
    /// </summary>
    /// <typeparam name="TKey">The key type used to retrieve factories.</typeparam>
    public interface IGenericEntityFactory<TKey>
    {
        /// <summary>
        /// Adds a new entity factory associated with the specified key.
        /// </summary>
        /// <param name="key">The key to associate the factory with.</param>
        /// <param name="factory">The entity factory to add.</param>
        void Add(in TKey key, in IEntityFactory factory);

        /// <summary>
        /// Removes the factory associated with the specified key.
        /// </summary>
        /// <param name="key">The key of the factory to remove.</param>
        void Remove(in TKey key);

        /// <summary>
        /// Creates an entity using the factory associated with the specified key.
        /// </summary>
        /// <param name="key">The key identifying the factory to use.</param>
        /// <returns>The created entity.</returns>
        IEntity Create(in TKey key);
    }

    /// <summary>
    /// A concrete implementation of <see cref="IGenericEntityFactory"/> using <see cref="string"/> as key type.
    /// </summary>
    public class GenericEntityFactory : GenericEntityFactory<string>, IGenericEntityFactory
    {
        /// <summary>
        /// Initializes a new empty factory.
        /// </summary>
        public GenericEntityFactory()
        {
        }

        /// <summary>
        /// Initializes the factory using a catalog of scriptable entity factories.
        /// </summary>
        /// <param name="factoryCatalog">The catalog providing entity factories.</param>
        public GenericEntityFactory(ScriptableEntityFactoryCatalog factoryCatalog) : base(factoryCatalog.GetEntities())
        {
        }

        /// <summary>
        /// Initializes the factory using the specified collection of entity factories.
        /// </summary>
        /// <param name="factorys">Key-factory pairs to initialize the factory with.</param>
        public GenericEntityFactory(IEnumerable<KeyValuePair<string, IEntityFactory>> factorys) : base(factorys)
        {
        }

        /// <summary>
        /// Initializes the factory using a params array of key-factory pairs.
        /// </summary>
        /// <param name="factory">The key-factory pairs to initialize with.</param>
        public GenericEntityFactory(params KeyValuePair<string, IEntityFactory>[] factory) : base(factory)
        {
        }
    }

    /// <summary>
    /// A generic implementation of <see cref="IGenericEntityFactory{TKey}"/> that manages entity factories by key.
    /// </summary>
    /// <typeparam name="TKey">The type of keys used to retrieve factories.</typeparam>
    public class GenericEntityFactory<TKey> : IGenericEntityFactory<TKey>
    {
        private readonly Dictionary<TKey, IEntityFactory> _factories;

        /// <summary>
        /// Initializes a new empty factory dictionary.
        /// </summary>
        public GenericEntityFactory() => _factories = new Dictionary<TKey, IEntityFactory>();

        /// <summary>
        /// Initializes the factory with a collection of key-factory pairs.
        /// </summary>
        /// <param name="factorys">The key-factory pairs to initialize with.</param>
        public GenericEntityFactory(in IEnumerable<KeyValuePair<TKey, IEntityFactory>> factorys) => 
            _factories = new Dictionary<TKey, IEntityFactory>(factorys);

        /// <summary>
        /// Initializes the factory with a params array of key-factory pairs.
        /// </summary>
        /// <param name="factory">The key-factory pairs to initialize with.</param>
        public GenericEntityFactory(params KeyValuePair<TKey, IEntityFactory>[] factory) => 
            _factories = new Dictionary<TKey, IEntityFactory>(factory);

        /// <inheritdoc />
        public void Add(in TKey key, in IEntityFactory factory) => _factories.Add(key, factory);

        /// <inheritdoc />
        public void Remove(in TKey key) => _factories.Remove(key);

        /// <inheritdoc />
        public IEntity Create(in TKey key)
        {
            return !_factories.TryGetValue(key, out IEntityFactory factory)
                ? throw new KeyNotFoundException($"Can't create entity with key: {key}")
                : factory.Create();
        }
    }
}