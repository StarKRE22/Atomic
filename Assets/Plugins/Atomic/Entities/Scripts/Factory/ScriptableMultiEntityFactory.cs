#if UNITY_5_3_OR_NEWER
using System.Collections.Generic;

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
    [CreateAssetMenu(
        fileName = "EntityFactoryCatalog",
        menuName = "Atomic/Entities/New EntityFactoryCatalog"
    )]
    public class ScriptableMultiEntityFactory :
        ScriptableMultiEntityFactory<string, IEntity, ScriptableEntityFactory>,
        IMultiEntityFactory
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
    public abstract class ScriptableMultiEntityFactory<TKey, E, F> :
        ScriptableObject,
        IMultiEntityFactory<TKey, E>
        where E : IEntity
        where F : ScriptableEntityFactory<E>
    {
#if ODIN_INSPECTOR
        [AssetsOnly]
        [ListDrawerSettings(ShowFoldout = true)]
#endif
        [SerializeField]
        internal F[] _factories;

        private Dictionary<TKey, F> _factoryMap;
        
        public E Create(TKey key)
        {
            this.EnsureInitialized();
            F factory = _factoryMap[key];
            return factory.Create();
        }
        
        public bool TryCreate(TKey key, out E entity)
        {
            this.EnsureInitialized();
            bool exists = _factoryMap.TryGetValue(key, out F factory);
            entity = exists ? factory.Create() : default;
            return exists;
        }
      
        /// <summary>
        /// Checks whether a factory exists for the given key.
        /// </summary>
        public bool Contains(TKey key)
        {
            this.EnsureInitialized();
            return _factoryMap.ContainsKey(key);
        }

        /// <summary>
        /// Extracts a key for the given factory. Must be implemented by derived class.
        /// </summary>
        protected abstract TKey GetKey(F factory);

        /// <summary>
        /// Initializes the internal dictionary from the serialized factory list.
        /// </summary>
        private void EnsureInitialized()
        {
            if (_factoryMap != null)
                return;

            _factoryMap = new Dictionary<TKey, F>();

            if (_factories == null)
                return;

            for (int i = 0, count = _factories.Length; i < count; i++)
            {
                F factory = _factories[i];
                if (factory == null)
                    continue;

                TKey key = this.GetKey(factory);

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