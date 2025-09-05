#if UNITY_5_3_OR_NEWER
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Atomic.Entities
{
    /// <summary>
    /// A ScriptableObject that serves as a catalog of <see cref="EntityView"/> prefabs.
    /// Allows retrieving prefabs by index or by name and provides a centralized collection for entity view assets.
    /// </summary>
    [CreateAssetMenu(
        fileName = "EntityViewCatalog",
        menuName = "Atomic/Entities/New EntityViewCatalog"
    )]
    public class EntityViewCatalog<E, V> : ScriptableObject 
        where E : IEntity
        where V : EntityView<E>
    {
        [Tooltip("The list of view prefabs available in this catalog")]
        [SerializeField]
        internal List<V> _prefabs;

        /// <summary>
        /// Gets the number of prefabs stored in the catalog.
        /// </summary>
        public int Count => _prefabs.Count;

        /// <summary>
        /// Retrieves a prefab at the specified index along with its name.
        /// </summary>
        /// <param name="index">The index of the prefab to retrieve.</param>
        /// <returns>A <see cref="KeyValuePair{TKey, TValue}"/> where the key is the prefab's name and the value is the prefab itself.</returns>
        public KeyValuePair<string, V> GetPrefab(int index)
        {
            V view = _prefabs[index];
            return new KeyValuePair<string, V>(this.GetName(view), view);
        }

        /// <summary>
        /// Retrieves a prefab by its name.
        /// </summary>
        /// <param name="name">The name of the prefab to retrieve.</param>
        /// <returns>The matching <see cref="EntityView"/> instance.</returns>
        /// <exception cref="Exception">Thrown if no prefab with the specified name is found.</exception>
        public V GetPrefab(string name)
        {
            for (int i = 0, count = _prefabs.Count; i < count; i++)
            {
                V prefab = _prefabs[i];
                if (this.GetName(prefab) == name)
                    return prefab;
            }

            throw new Exception($"Prefab with name {name} is not found!");
        }

        /// <summary>
        /// Extracts the name of a given prefab.
        /// Can be overridden to implement custom naming logic.
        /// </summary>
        /// <param name="prefab">The prefab whose name to retrieve.</param>
        /// <returns>The name used to identify the prefab.</returns>
        protected internal virtual string GetName(V prefab) => prefab.Name;
    }
}
#endif