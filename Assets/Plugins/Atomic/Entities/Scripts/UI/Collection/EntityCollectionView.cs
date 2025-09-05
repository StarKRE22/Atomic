#if UNITY_5_3_OR_NEWER
using System;
using System.Buffers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Atomic.Entities
{
    /// <summary>
    /// A MonoBehaviour that manages a collection of entity views in the scene.
    /// Implements <see cref="IEntityCollectionView"/> to provide add, remove, and clear operations for entity views.
    /// </summary>
    [AddComponentMenu("Atomic/Entities/Entity Collection View")]
    [DisallowMultipleComponent]
    public class EntityCollectionView : EntityCollectionView<IEntity, EntityView>
    {
    }

    public abstract class EntityCollectionView<E, V> : MonoBehaviour, IEnumerable<KeyValuePair<E, V>>
        where E : IEntity
        where V : EntityView<E>
    {
        private static readonly ArrayPool<E> s_entityPool = ArrayPool<E>.Shared;

        [Space]
        [Tooltip("The viewport or container under which views will be placed in the scene hierarchy")]
        [SerializeField]
        internal Transform _viewport;

        [Tooltip("The pool responsible for providing and recycling entity view instances")]
        [SerializeField]
        internal EntityViewPool<E, V> _viewPool;

        /// <summary>
        /// Raised when a view is spawned for a newly added entity.
        /// </summary>
        public event Action<E, V> OnAdded;

        /// <summary>
        /// Raised when a view is removed for a despawned or removed entity.
        /// </summary>
        public event Action<E, V> OnRemoved;

        public int Count => _views.Count;

        public bool IsVisible => _source != null;

        private readonly Dictionary<E, V> _views = new();

        private IReadOnlyEntityCollection<E> _source;

        public void Show(IReadOnlyEntityCollection<E> source)
        {
            this.Hide();
            
            _source = source ?? throw new ArgumentNullException(nameof(source));
            _source.OnAdded += this.AddView;
            _source.OnRemoved += this.RemoveView;

            foreach (E entity in _source)
                this.AddView(entity);
        }

        public void Hide()
        {
            if (_source == null)
                return;

            _source.OnAdded -= this.AddView;
            _source.OnRemoved -= this.RemoveView;

            this.ClearViews();
        }

        /// <summary>
        /// Gets the view instance associated with the specified entity.
        /// </summary>
        /// <param name="entity">The entity whose view is requested.</param>
        /// <returns>The active <see cref="IReadOnlyEntityView"/> instance associated with the entity.</returns>
        public EntityView<E> GetView(E entity) => _views[entity];

        /// <summary>
        /// Creates and shows a view for the specified entity, if it does not already exist.
        /// </summary>
        /// <param name="entity">The entity to visualize.</param>
        public void AddView(E entity)
        {
            if (_views.ContainsKey(entity))
                return;

            string name = this.GetEntityName(entity);
            V view = _viewPool.Rent(name);
            view.transform.SetParent(_viewport);
            view.Show(entity);

            _views.Add(entity, view);
            this.OnAdded?.Invoke(entity, view);
        }

        /// <summary>
        /// Hides and returns the view associated with the specified entity to the view pool.
        /// </summary>
        /// <param name="entity">The entity whose view should be removed.</param>
        public void RemoveView(E entity)
        {
            if (!_views.Remove(entity, out V view))
                return;

            view.Hide();
            this.OnRemoved?.Invoke(entity, view);

            string name = this.GetEntityName(entity);
            _viewPool.Return(name, view);
        }

        /// <summary>
        /// Removes all active entity views, returning them to the view pool.
        /// </summary>
        public void ClearViews()
        {
            if (_views.Count == 0)
                return;

            E[] buffer = s_entityPool.Rent(_views.Count);
            _views.Keys.CopyTo(buffer, 0);

            try
            {
                for (int i = 0, count = buffer.Length; i < count; i++)
                    this.RemoveView(buffer[i]);
            }
            finally
            {
                s_entityPool.Return(buffer);
            }
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection of entity-view pairs (non-generic version).
        /// </summary>
        /// <returns>An <see cref="IEnumerator"/> that can be used to iterate through the collection.</returns>
        public IEnumerator<KeyValuePair<E, V>> GetEnumerator() => _views.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => _views.GetEnumerator();

        /// <summary>
        /// Determines the name used to retrieve the view prefab for a given entity.
        /// Can be overridden to provide custom naming logic.
        /// </summary>
        /// <param name="entity">The entity to evaluate.</param>
        /// <returns>The name used in the view pool.</returns>
        protected virtual string GetEntityName(E entity) => entity.Name;
    }
}
#endif