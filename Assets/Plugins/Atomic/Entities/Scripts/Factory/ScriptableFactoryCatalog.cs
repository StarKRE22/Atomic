using System.Collections.Generic;
using System.Linq;
using Atomic.Entities;

#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif
using UnityEngine;

namespace SampleGame
{
    /// <summary>
    /// A catalog that holds a list of <see cref="ScriptableFactory{E}"/> instances
    /// and exposes them as a collection of key-factory pairs for use in entity factory registries.
    /// </summary>
    public abstract class ScriptableFactoryCatalog<E> : ScriptableObject where E : IEntity<E>
    {
        /// <summary>
        /// The list of entity factories stored in this catalog.
        /// </summary>
#if ODIN_INSPECTOR
        [AssetsOnly]
#endif
        [SerializeField]
        private ScriptableFactory<E>[] _factories;

        /// <summary>
        /// Returns all entity factories as a collection of key-value pairs,
        /// where the key is the factory's name and the value is the factory instance.
        /// </summary>
        /// <returns>An enumerable of key-value pairs representing registered entity factories.</returns>
        public IEnumerable<KeyValuePair<string, IEntityFactory<E>>> GetAllFactories() =>
            _factories.Select(it => new KeyValuePair<string, IEntityFactory<E>>(it.name, it));
    }
}