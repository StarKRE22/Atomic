#if UNITY_5_3_OR_NEWER
using System.Collections.Generic;
using UnityEngine;

namespace Atomic.Entities
{
    /// <summary>
    /// A <see cref="ScriptableObject"/> that serves as a catalog of <see cref="EntityView{E}"/> prefabs.
    /// Provides centralized storage and retrieval of entity view prefabs by index or name.
    /// </summary>
    /// <typeparam name="E">The type of entity (<see cref="IEntity"/>) associated with the views in this catalog.</typeparam>
    /// <typeparam name="V">The type of entity view (<see cref="EntityView{E}"/>) stored in this catalog.</typeparam>
    [HelpURL("https://github.com/StarKRE22/Atomic/blob/main/Docs/Entities/UI/EntityViewCatalog%601.md")]
    public abstract class EntityViewCatalog<E, V> : ScriptableObject
        where E : class, IEntity
        where V : EntityView<E>
    {
        /// <summary>
        /// The list of entity view prefabs available in this catalog.
        /// </summary>
        [Tooltip("The list of view prefabs available in this catalog")]
        [SerializeField]
        private List<V> prefabs;

        /// <summary>
        /// Gets the number of prefabs stored in the catalog.
        /// </summary>
        public int Count => this.prefabs.Count;

        public V GetPrefab(int index) => prefabs[index];
    }
}
#endif