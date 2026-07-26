#if UNITY_5_3_OR_NEWER
using System;
using UnityEngine;

namespace Atomic.Entities
{
    /// <summary>
    /// A base class for managing collections of entity views in a Unity scene.
    /// Provides functionality to show, hide, add, remove, and clear entity views,
    /// backed by a pool of reusable instances.
    /// </summary>
    /// <typeparam name="K">The key type used to identify entities in the collection.</typeparam>
    /// <typeparam name="E">The type of entity (<see cref="IEntity"/>) managed by this collection.</typeparam>
    /// <typeparam name="V">The type of entity view (<see cref="EntityView{E}"/>) associated with entities.</typeparam>
    [HelpURL("https://github.com/StarKRE22/Atomic/blob/main/Docs/Entities/UI/EntityCollectionView%601.md")]
    public abstract class EntityWorldView<K, E, V> : EntityCollectionView<K, E, V>
        where E : class, IEntity
        where V : EntityView<E>
    {
        /// <summary>
        /// Gets a value indicating whether this collection is currently visible 
        /// (i.e. has a bound <see cref="IReadOnlyEntityCollection{E}"/> source).
        /// </summary>
        public bool IsActive => _source != null;
        
        private IReadOnlyEntityCollection<E> _source;

        /// <summary>
        /// Shows this collection, binding it to the specified source of entities.
        /// </summary>
        /// <param name="source">The entity collection to visualize.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="source"/> is null.</exception>
        public void Activate(IReadOnlyEntityCollection<E> source)
        {
            this.Deactivate();

            _source = source ?? throw new ArgumentNullException(nameof(source));
            _source.OnAdded += this.OnAdd;
            _source.OnRemoved += this.Remove;

            foreach (E entity in _source)
                this.Add(entity);
        }

        /// <summary>
        /// Hides this collection, detaching it from the bound entity source and removing all views.
        /// </summary>
        public void Deactivate()
        {
            this.Clear();

            if (_source != null)
            {
                _source.OnAdded -= this.OnAdd;
                _source.OnRemoved -= this.Remove;
                _source = null;
            }
        }

        private void OnAdd(E entity) => this.Add(entity);
    }
}
#endif