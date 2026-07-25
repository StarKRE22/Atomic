#if UNITY_5_3_OR_NEWER
using System;
using System.Buffers;
using System.Collections;
using System.Collections.Generic;
using Unity.Profiling;
using UnityEngine;

#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

namespace Atomic.Entities
{
    public abstract class EntityCollectionView<K, E, V> : MonoBehaviour, IReadOnlyCollection<KeyValuePair<E, V>>
        where E : class, IEntity
        where V : EntityView<E>
    {
        private static readonly ProfilerMarker s_removeMarker = new($"EntityCollectionView<{typeof(E).Name}>.Remove");
        private static readonly ProfilerMarker s_addMarker = new($"EntityCollectionView<{typeof(E).Name}>.Add");

        private static readonly ArrayPool<E> s_arrayPool = ArrayPool<E>.Shared;

        /// <summary>
        /// Raised when a view is spawned for a newly added entity.
        /// </summary>
        public event Action<E, V> OnAdded;

        /// <summary>
        /// Raised when a view is removed for a despawned or removed entity.
        /// </summary>
        public event Action<E, V> OnRemoved;

        /// <summary>
        /// Gets the number of active entity views currently tracked by this collection.
        /// </summary>
        public int Count => _views.Count;

        internal EntityViewPool<K, E, V> Pool => _pool;

        [Tooltip("The viewport or container under which views will be placed in the scene hierarchy")]
        [SerializeField]
        private Transform viewport;

        [Space, SerializeField]
        private EntityViewPool<K, E, V> _pool;

#if ODIN_INSPECTOR
        [ShowInInspector, ReadOnly, HideInEditorMode]
#endif
        private readonly Dictionary<E, V> _views = new();

        /// <summary>
        /// Gets the view instance associated with the specified entity.
        /// </summary>
        /// <param name="entity">The entity whose view is requested.</param>
        /// <returns>The active <see cref="EntityView{E}"/> instance associated with the entity.</returns>
        /// <exception cref="KeyNotFoundException">Thrown if the entity is not in the collection.</exception>
        public V Get(E entity) => _views[entity];

        /// <summary>
        /// Tries to retrieve the view for a given entity.
        /// </summary>
        /// <param name="entity">The entity whose view is requested.</param>
        /// <param name="view">The active <see cref="EntityView{E}"/> instance associated with the entity.</param>
        /// <returns>"true" if a view exists, "false" otherwise.</returns>
        public bool TryGet(E entity, out V view) => _views.TryGetValue(entity, out view);

        /// <summary>
        /// Checks whether a view exists for the specified entity.
        /// </summary>
        /// <param name="entity">The entity whose view is requested.</param>
        /// <returns>"true" if a view exists, "false" otherwise.</returns>
        public bool Contains(E entity) => _views.ContainsKey(entity);

        /// <summary>
        /// Creates and shows a view for the specified entity, if it does not already exist.
        /// </summary>
        /// <param name="entity">The entity to visualize.</param>
        public V Add(E entity)
        {
            using (s_addMarker.Auto())
            {
                if (_views.TryGetValue(entity, out V view))
                    return view;

                view = _pool.Rent(this.GetKey(entity), this.viewport);
                view.Activate(entity);

                _views.Add(entity, view);
                this.OnAdded?.Invoke(entity, view);
                return view;
            }
        }

        protected abstract K GetKey(E entity);

        /// <summary>
        /// Hides and returns the view associated with the specified entity to the view pool.
        /// </summary>
        /// <param name="entity">The entity whose view should be removed.</param>
        public void Remove(E entity)
        {
            using (s_removeMarker.Auto())
            {
                if (!_views.Remove(entity, out V view))
                    return;

                this.OnRemoved?.Invoke(entity, view);
                view.Deactivate();
                _pool.Return(view);
            }
        }

        public void Remove(V view) => 
            this.Remove(view.Entity);

        public Dictionary<E, V>.Enumerator GetEnumerator() => _views.GetEnumerator();

        /// <summary>
        /// Returns an enumerator that iterates through the collection of entity-view pairs.
        /// </summary>
        /// <returns>An enumerator of <see cref="KeyValuePair{TKey, TValue}"/> containing entity-view pairs.</returns>
        IEnumerator<KeyValuePair<E, V>> IEnumerable<KeyValuePair<E, V>>.GetEnumerator() => _views.GetEnumerator();

        /// <summary>
        /// Returns an enumerator that iterates through the collection of entity-view pairs.
        /// </summary>
        /// <returns>An enumerator containing entity-view pairs.</returns>
        IEnumerator IEnumerable.GetEnumerator() => _views.GetEnumerator();

        /// <summary>
        /// Removes active entity views, returning them to the view pool.
        /// </summary>
        public void Clear()
        {
            int viewCount = _views.Count;
            if (viewCount == 0)
                return;

            E[] buffer = s_arrayPool.Rent(viewCount);
            _views.Keys.CopyTo(buffer, 0);

            try
            {
                for (int i = 0; i < viewCount; i++)
                    this.Remove(buffer[i]);
            }
            finally
            {
                s_arrayPool.Return(buffer);
            }
        }
    }
}
#endif