#if UNITY_5_3_OR_NEWER
using System.Collections.Generic;

#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

using UnityEngine;

namespace Atomic.Entities
{
    /// <summary>
    /// A Unity <see cref="ScriptableObject"/>-based abstract implementation of <see cref="IMultiEntityFactory{TKey, E}"/> 
    /// that manages multiple <see cref="ScriptableEntityFactory{E}"/> instances.
    /// </summary>
    /// <typeparam name="K">The type of key used to identify factories.</typeparam>
    /// <typeparam name="E">The type of entity to be created, which must implement <see cref="IEntity"/>.</typeparam>
    /// <typeparam name="F">The type of scriptable entity factory, which must inherit from <see cref="ScriptableEntityFactory{E}"/>.</typeparam>
    [HelpURL("https://github.com/StarKRE22/Atomic/blob/main/Docs/Entities/Factories/ScriptableMultiEntityFactory%601.md")]
    public abstract class ScriptableMultiEntityFactory<K, E, F> : ScriptableObject, IMultiEntityFactory<K, E>
        where E : IEntity
        where F : ScriptableEntityFactory<E>
    {
#if ODIN_INSPECTOR
        [AssetsOnly]
        [ListDrawerSettings(ShowFoldout = true)]
#endif
        [SerializeField]
        internal F[] _factories;

        private Dictionary<K, F> _factoryMap;

        /// <inheritdoc />
        public E Create(K key)
        {
            this.EnsureInitialized();
            F factory = _factoryMap[key];
            return factory.Create();
        }

        /// <inheritdoc />
        public bool TryCreate(K key, out E entity)
        {
            this.EnsureInitialized();
            bool exists = _factoryMap.TryGetValue(key, out F factory);
            entity = exists ? factory.Create() : default;
            return exists;
        }

        /// <inheritdoc />
        public bool Contains(K key)
        {
            this.EnsureInitialized();
            return _factoryMap.ContainsKey(key);
        }

        /// <summary>
        /// Extracts the key for the given factory. Must be implemented by derived classes.
        /// </summary>
        /// <param name="factory">The factory instance from which to extract the key.</param>
        /// <returns>The key corresponding to the given factory.</returns>
        protected abstract K GetKey(F factory);

        /// <summary>
        /// Ensures that the internal dictionary of factories is initialized.
        /// Populates the dictionary from the serialized array of factories, handling duplicates with a warning.
        /// </summary>
        private void EnsureInitialized()
        {
            if (_factoryMap != null)
                return;

            _factoryMap = new Dictionary<K, F>();

            if (_factories == null)
                return;

            for (int i = 0, count = _factories.Length; i < count; i++)
            {
                F factory = _factories[i];
                if (factory == null)
                    continue;

                K key = this.GetKey(factory);

                if (_factoryMap.ContainsKey(key))
                    Debug.LogWarning(
                        $"Duplicate key '{key}' found in {name}. The last factory will overwrite the previous one."
                    );

                _factoryMap[key] = factory;
            }
        }
    }
}
#endif