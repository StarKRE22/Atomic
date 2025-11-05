#if UNITY_5_3_OR_NEWER
using System;
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
        internal List<V> prefabs;

        /// <summary>
        /// Gets the number of prefabs stored in the catalog.
        /// </summary>
        public int Count => this.prefabs.Count;

        /// <summary>
        /// Retrieves a prefab at the specified index along with its name.
        /// </summary>
        /// <param name="index">The index of the prefab to retrieve.</param>
        /// <returns>
        /// A <see cref="KeyValuePair{TKey, TValue}"/> where the key is the prefab's name 
        /// (as determined by <see cref="GetName(V)"/>) and the value is the prefab itself.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if <paramref name="index"/> is out of range.</exception>
        public KeyValuePair<string, V> GetPrefab(int index)
        {
            V view = this.prefabs[index];
            return new KeyValuePair<string, V>(this.GetName(view), view);
        }

        /// <summary>
        /// Retrieves a prefab by its name.
        /// </summary>
        /// <param name="name">The name of the prefab to retrieve.</param>
        /// <returns>The matching <typeparamref name="V"/> prefab instance.</returns>
        /// <exception cref="Exception">Thrown if no prefab with the specified name is found.</exception>
        public V GetPrefab(string name)
        {
            for (int i = 0, count = this.prefabs.Count; i < count; i++)
            {
                V prefab = this.prefabs[i];
                if (this.GetName(prefab) == name)
                    return prefab;
            }

            throw new Exception($"Prefab with name {name} is not found!");
        }

        /// <summary>
        /// Extracts the name of a given prefab.
        /// </summary>
        /// <param name="prefab">The prefab whose name to retrieve.</param>
        /// <returns>The name used to identify the prefab.</returns>
        /// <remarks>
        /// The default implementation returns <see cref="EntityView{E}.Name"/>.
        /// Override this method to implement custom naming logic (e.g., 
        /// using tags, metadata, or localization).
        /// </remarks>
        protected internal virtual string GetName(V prefab) => prefab.Name;
    }
}
#endif