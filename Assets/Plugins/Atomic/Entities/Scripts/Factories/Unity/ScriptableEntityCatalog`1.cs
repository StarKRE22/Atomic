#if UNITY_5_3_OR_NEWER
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

namespace Atomic.Entities
{
    /// <summary>
    /// A <see cref="ScriptableObject"/>-based catalog of entity factories.
    /// Provides creation and lookup of entities using a key.
    /// </summary>
    /// <typeparam name="K">Key used to identify factories (enum, string, etc).</typeparam>
    /// <typeparam name="E">Entity type (must implement <see cref="IEntity"/>).</typeparam>
    /// <typeparam name="F">Factory type (inherits <see cref="ScriptableEntityFactory{TArgs}"/>).</typeparam>
    [HelpURL("https://github.com/StarKRE22/Atomic/blob/main/Docs/Entities/Factories/ScriptableMultiEntityFactory%601.md")]
    public abstract class ScriptableEntityCatalog<K, E, F, TArgs> : ScriptableObject,
        IMultiEntityFactory<K, E, TArgs>,
        IReadOnlyDictionary<K, F>
        where E : IEntity
        where F : ScriptableObject, IEntityFactory<E, TArgs>
        where TArgs : IArgs
    {
#if ODIN_INSPECTOR
        [AssetsOnly]
        [ValidateInput(nameof(ValidateFactories), "Factories contain duplicates or nulls")]
        [ListDrawerSettings(ShowFoldout = true)]
#endif
        [SerializeField]
        private F[] _factories;

        private Dictionary<K, F> _factoryMap;

        #region Factory API

        /// <inheritdoc />
        public E Create(K key, TArgs args)
        {
            EnsureInitialized();

            return !_factoryMap.TryGetValue(key, out var factory)
                ? throw new KeyNotFoundException($"Factory with key '{key}' not found in '{name}'")
                : factory.Create(args);
        }

        /// <inheritdoc />
        public bool TryCreate(K key, TArgs args, out E entity)
        {
            EnsureInitialized();

            if (_factoryMap.TryGetValue(key, out var factory))
            {
                entity = factory.Create(args);
                return true;
            }

            entity = default;
            return false;
        }

        /// <inheritdoc />
        public bool Contains(K key)
        {
            EnsureInitialized();
            return _factoryMap.ContainsKey(key);
        }

        #endregion

        #region Initialization

        /// <summary>
        /// Extracts a key from a given factory.
        /// </summary>
        protected abstract K GetKey(F factory);

        private void EnsureInitialized()
        {
            if (_factoryMap != null)
                return;

            int capacity = _factories?.Length ?? 0;
            _factoryMap = new Dictionary<K, F>(capacity);

            if (_factories == null || _factories.Length == 0)
            {
                Debug.LogWarning($"{name} has no factories assigned", this);
                return;
            }

            foreach (var factory in _factories)
            {
                if (factory == null)
                    continue;

                var key = GetKey(factory);
                if (_factoryMap.ContainsKey(key))
                    Debug.LogWarning($"Duplicate key '{key}' in {name}. Overwriting with '{factory.name}'", this);

                _factoryMap[key] = factory;
            }
        }

#if ODIN_INSPECTOR
        private bool ValidateFactories(F[] factories)
        {
            if (factories == null)
                return true;

            var set = new HashSet<K>();
            foreach (F factory in factories)
            {
                if (factory == null)
                    return false;

                var key = GetKey(factory);
                if (!set.Add(key))
                    return false;
            }

            return true;
        }
#endif

        #endregion

        #region IReadOnlyDictionary

        public int Count
        {
            get
            {
                EnsureInitialized();
                return _factoryMap.Count;
            }
        }

        public IEnumerable<K> Keys
        {
            get
            {
                EnsureInitialized();
                return _factoryMap.Keys;
            }
        }

        public IEnumerable<F> Values
        {
            get
            {
                EnsureInitialized();
                return _factoryMap.Values;
            }
        }

        public F this[K key]
        {
            get
            {
                EnsureInitialized();
                return _factoryMap[key];
            }
        }

        public bool ContainsKey(K key)
        {
            EnsureInitialized();
            return _factoryMap.ContainsKey(key);
        }

        public bool TryGetValue(K key, out F value)
        {
            EnsureInitialized();
            return _factoryMap.TryGetValue(key, out value);
        }

        public IEnumerator<KeyValuePair<K, F>> GetEnumerator()
        {
            EnsureInitialized();
            return _factoryMap.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion
    }
}
#endif