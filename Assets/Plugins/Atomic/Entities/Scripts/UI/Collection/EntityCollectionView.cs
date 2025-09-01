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
    public class EntityCollectionView : MonoBehaviour, IEntityCollectionView
    {
        private static readonly ArrayPool<IEntity> s_entityPool = ArrayPool<IEntity>.Shared;

        /// <summary>
        /// Raised when a view is spawned for a newly added entity.
        /// </summary>
        public event Action<IEntity, IReadOnlyEntityView> OnAdded;

        /// <summary>
        /// Raised when a view is removed for a despawned or removed entity.
        /// </summary>
        public event Action<IEntity, IReadOnlyEntityView> OnRemoved;

        [Space]
        [Tooltip("The viewport or container under which views will be placed in the scene hierarchy")]
        [SerializeField]
        internal Transform _viewport;

        [Tooltip("The pool responsible for providing and recycling entity view instances")]
        [SerializeField]
        internal EntityViewPoolBase _viewPool;

        /// <summary>
        /// Internal dictionary mapping active entities to their associated views.
        /// </summary>
        private readonly Dictionary<IEntity, EntityViewBase> _views = new();

        /// <inheritdoc cref="IReadOnlyEntityCollectionView.Count"/>>
        public int Count => _views.Count;

        /// <summary>
        /// Gets the view instance associated with the specified entity.
        /// </summary>
        /// <param name="entity">The entity whose view is requested.</param>
        /// <returns>The active <see cref="IReadOnlyEntityView"/> instance associated with the entity.</returns>
        public IReadOnlyEntityView GetView(IEntity entity) => _views[entity];

        /// <summary>
        /// Creates and shows a view for the specified entity, if it does not already exist.
        /// </summary>
        /// <param name="entity">The entity to visualize.</param>
        public void AddView(IEntity entity)
        {
            if (_views.ContainsKey(entity))
                return;

            string name = this.GetEntityName(entity);
            EntityViewBase view = _viewPool.Rent(name);
            view.transform.SetParent(_viewport);
            view.Show(entity);

            _views.Add(entity, view);
            this.OnAdded?.Invoke(entity, view);
        }

        /// <summary>
        /// Hides and returns the view associated with the specified entity to the view pool.
        /// </summary>
        /// <param name="entity">The entity whose view should be removed.</param>
        public void RemoveView(IEntity entity)
        {
            if (!_views.Remove(entity, out EntityViewBase view))
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

            IEntity[] buffer = s_entityPool.Rent(_views.Count);
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
        /// Determines the name used to retrieve the view prefab for a given entity.
        /// Can be overridden to provide custom naming logic.
        /// </summary>
        /// <param name="entity">The entity to evaluate.</param>
        /// <returns>The name used in the view pool.</returns>
        protected virtual string GetEntityName(IEntity entity) => entity.Name;

        /// <summary>
        /// Returns an enumerator that iterates through the collection of entity-view pairs.
        /// </summary>
        /// <returns>An enumerator of <see cref="KeyValuePair{IEntity, EntityViewBase}"/> representing all active entity-view mappings.</returns>
        public IEnumerator<KeyValuePair<IEntity, IReadOnlyEntityView>> GetEnumerator()
        {
            foreach (KeyValuePair<IEntity, EntityViewBase> pair in _views)
                yield return new KeyValuePair<IEntity, IReadOnlyEntityView>(pair.Key, pair.Value);
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection of entity-view pairs (non-generic version).
        /// </summary>
        /// <returns>An <see cref="IEnumerator"/> that can be used to iterate through the collection.</returns>
        IEnumerator IEnumerable.GetEnumerator() => _views.GetEnumerator();
    }
}
#endif