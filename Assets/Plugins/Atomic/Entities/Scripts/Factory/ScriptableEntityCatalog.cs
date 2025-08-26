#if UNITY_5_3_OR_NEWER
using System.Collections;
using System.Collections.Generic;
using Atomic.Entities;

#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif
using UnityEngine;

namespace Atomic.Entities
{
    /// <summary>
    /// A concrete implementation of <see cref="ScriptableEntityCatalog{TKey,E}"/> that maps
    /// <see cref="ScriptableEntityFactory{IEntity}"/> instances by their asset name as a string key.
    /// </summary>
    /// <remarks>
    /// This catalog can be used as a drop-in asset to register entity factories by name for use in
    /// entity spawning, prototyping, or in-editor tools.
    /// </remarks>
    /// <example>
    /// <code>
    /// var catalog = Resources.Load&lt;ScriptableEntityFactoryCatalog&gt;("EntityFactoryCatalog");
    /// var factory = catalog["Enemy"];
    /// var enemy = factory.Create();
    /// </code>
    /// </example>
    [CreateAssetMenu(
        fileName = "EntityFactoryCatalog",
        menuName = "Atomic/Entities/New EntityFactoryCatalog"
    )]
    public class ScriptableEntityCatalog :
        ScriptableEntityCatalog<string, IEntity, ScriptableEntityFactory>,
        IEntityFactoryCatalog
    {
        /// <summary>
        /// Extracts the string key for a given factory.
        /// Uses the factory's asset name as the key.
        /// </summary>
        /// <param name="factory">The factory to extract a key from.</param>
        /// <returns>The name of the factory asset.</returns>
        protected override string GetKey(ScriptableEntityFactory factory) => factory.name;
    }

    /// <summary>
    /// A ScriptableObject-based catalog that stores a collection of <see cref="ScriptableEntityFactory{E}"/> instances,
    /// exposing them as a dictionary keyed by <typeparamref name="TKey"/> for use in factory registries.
    /// </summary>
    /// <typeparam name="TKey">The type of the key used to identify each factory.</typeparam>
    /// <typeparam name="E">The type of entity each factory creates.</typeparam>
    /// <typeparam name="F">The type of entity factory.</typeparam>
    public abstract class ScriptableEntityCatalog<TKey, E, F> :
        ScriptableObject,
        IEntityFactoryCatalog<TKey, E>
        where E : IEntity
        where F : ScriptableEntityFactory<E>
    {
#if ODIN_INSPECTOR
        [AssetsOnly]
        [ListDrawerSettings(ShowFoldout = true)]
#endif
        [SerializeField]
        internal F[] _factories;

        private Dictionary<TKey, IEntityFactory<E>> _map;

        /// <summary>
        /// Returns the number of factories in the catalog.
        /// </summary>
        public int Count
        {
            get
            {
                this.EnsureInitialized();
                return _map.Count;
            }
        }

        /// <summary>
        /// Gets all keys in the catalog.
        /// </summary>
        public IEnumerable<TKey> Keys
        {
            get
            {
                this.EnsureInitialized();
                return _map.Keys;
            }
        }

        /// <summary>
        /// Gets all factory instances in the catalog.
        /// </summary>
        public IEnumerable<IEntityFactory<E>> Values
        {
            get
            {
                this.EnsureInitialized();
                return _map.Values;
            }
        }

        /// <summary>
        /// Returns whether a factory with the specified key exists.
        /// </summary>
        public bool ContainsKey(TKey key)
        {
            this.EnsureInitialized();
            return _map.ContainsKey(key);
        }

        /// <summary>
        /// Attempts to retrieve a factory by key.
        /// </summary>
        public bool TryGetValue(TKey key, out IEntityFactory<E> value)
        {
            this.EnsureInitialized();
            return _map.TryGetValue(key, out value);
        }

        /// <summary>
        /// Indexer access to a factory by key.
        /// </summary>
        public IEntityFactory<E> this[TKey key]
        {
            get
            {
                this.EnsureInitialized();
                return _map[key];
            }
        }

        /// <summary>
        /// Returns an enumerator over key-factory pairs.
        /// </summary>
        public IEnumerator<KeyValuePair<TKey, IEntityFactory<E>>> GetEnumerator()
        {
            this.EnsureInitialized();
            return _map.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        /// <summary>
        /// Extracts a key for the given factory. Must be implemented by derived class.
        /// </summary>
        /// <param name="factory">The factory to extract a key from.</param>
        /// <returns>A key of type <typeparamref name="TKey"/>.</returns>
        protected abstract TKey GetKey(F factory);

        /// <summary>
        /// Initializes the internal dictionary from the serialized factory list.
        /// </summary>
        private void EnsureInitialized()
        {
            if (_map != null)
                return;

            _map = new Dictionary<TKey, IEntityFactory<E>>();

            if (_factories == null)
                return;

            for (int i = 0, count = _factories.Length; i < count; i++)
            {
                F factory = _factories[i];
                TKey key = this.GetKey(factory);

                if (!_map.TryAdd(key, factory))
                    Debug.LogWarning($"Duplicate key '{key}' in {this.GetType().Name} on " +
                                     $"{this.name}. Skipping duplicate.");
            }
        }
    }
}
#endif