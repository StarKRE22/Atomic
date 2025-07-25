#if UNITY_5_3_OR_NEWER
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Atomic.Entities
{
    /// <summary>
    /// A non-generic alias of <see cref="EntityViewCatalog{IEntity}"/> used for working with base <see cref="IEntity"/> views.
    /// </summary>
    /// <remarks>
    /// This ScriptableObject acts as a catalog of entity view prefabs and is intended for use in editor workflows or runtime instantiation.
    /// It can be created via Unity's asset menu.
    /// </remarks>
    [CreateAssetMenu(
        fileName = "EntityFactoryCatalog",
        menuName = "Atomic/Entities/New EntityViewCatalog"
    )]
    public class EntityViewCatalog : EntityViewCatalog<IEntity>
    {
    }

    /// <summary>
    /// A catalog of view prefabs for entities of type <typeparamref name="E"/>.
    /// Used to store and retrieve <see cref="EntityViewBase{E}"/> prefabs by name or index.
    /// </summary>
    /// <typeparam name="E">The type of entity associated with the view. Must implement <see cref="IEntity"/>.</typeparam>
    public abstract class EntityViewCatalog<E> : ScriptableObject where E : IEntity
    {
        [Tooltip("The list of view prefabs available in this catalog")]
        [SerializeField]
        private List<EntityViewBase<E>> _prefabs;

        /// <summary>
        /// Gets the number of prefabs stored in the catalog.
        /// </summary>
        public int Count => _prefabs.Count;

        /// <summary>
        /// Retrieves a prefab at the specified index along with its name.
        /// </summary>
        /// <param name="index">The index of the prefab to retrieve.</param>
        /// <returns>A key-value pair where the key is the name and the value is the prefab.</returns>
        public KeyValuePair<string, EntityViewBase<E>> GetPrefab(int index)
        {
            EntityViewBase<E> view = _prefabs[index];
            return new KeyValuePair<string, EntityViewBase<E>>(this.GetName(view), view);
        }

        /// <summary>
        /// Retrieves a prefab by its name.
        /// </summary>
        /// <param name="name">The name of the prefab to retrieve.</param>
        /// <returns>The matching <see cref="EntityViewBase{E}"/> instance.</returns>
        /// <exception cref="Exception">Thrown if no prefab with the specified name is found.</exception>
        public EntityViewBase<E> GetPrefab(string name)
        {
            for (int i = 0, count = _prefabs.Count; i < count; i++)
            {
                EntityViewBase<E> prefab = _prefabs[i];
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
        protected virtual string GetName(EntityViewBase<E> prefab) => prefab.Name;
    }
}
#endif