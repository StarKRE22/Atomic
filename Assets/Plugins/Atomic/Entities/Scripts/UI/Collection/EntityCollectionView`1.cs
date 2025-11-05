#if UNITY_5_3_OR_NEWER
using System;
using System.Buffers;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Atomic.Entities
{
    /// <summary>
    /// A base class for managing collections of entity views in a Unity scene.
    /// Provides functionality to show, hide, add, remove, and clear entity views,
    /// backed by a pool of reusable instances.
    /// </summary>
    /// <typeparam name="E">The type of entity (<see cref="IEntity"/>) managed by this collection.</typeparam>
    /// <typeparam name="V">The type of entity view (<see cref="EntityView{E}"/>) associated with entities.</typeparam>
    [HelpURL("https://github.com/StarKRE22/Atomic/blob/main/Docs/Entities/UI/EntityCollectionView%601.md")]
    public abstract class EntityCollectionView<E, V> : MonoBehaviour, IEnumerable<KeyValuePair<E, V>>
        where E : class, IEntity
        where V : EntityView<E>
    {
        private static readonly ArrayPool<E> s_entityPool = ArrayPool<E>.Shared;

        [Space]
        [Tooltip("The viewport or container under which views will be placed in the scene hierarchy")]
        [SerializeField]
        internal Transform viewport;

        [Tooltip("The pool responsible for providing and recycling entity view instances")]
        [SerializeField]
        internal EntityViewPool<E, V> viewPool;

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

        /// <summary>
        /// Gets a value indicating whether this collection is currently visible 
        /// (i.e., has a bound <see cref="IReadOnlyEntityCollection{E}"/> source).
        /// </summary>
        public bool IsVisible => _source != null;

        private readonly Dictionary<E, V> _views = new();

        private IReadOnlyEntityCollection<E> _source;

        /// <summary>
        /// Shows this collection, binding it to the specified source of entities.
        /// </summary>
        /// <param name="source">The entity collection to visualize.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="source"/> is null.</exception>
        public void Show(IReadOnlyEntityCollection<E> source)
        {
            this.Hide();

            _source = source ?? throw new ArgumentNullException(nameof(source));
            _source.OnAdded += this.Add;
            _source.OnRemoved += this.Remove;

            foreach (E entity in _source)
                this.Add(entity);
        }

        /// <summary>
        /// Hides this collection, detaching it from the bound entity source and removing all views.
        /// </summary>
        public void Hide()
        {
            this.Clear();

            if (_source != null)
            {
                _source.OnAdded -= this.Add;
                _source.OnRemoved -= this.Remove;
                _source = null;
            }
        }

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
        public void Add(E entity)
        {
            if (_views.ContainsKey(entity))
                return;

            string name = this.GetName(entity);
            V view = this.viewPool.Rent(name);
            view.transform.SetParent(this.viewport);
            view.Show(entity);

            _views.Add(entity, view);
            this.OnAdded?.Invoke(entity, view);
        }

        /// <summary>
        /// Hides and returns the view associated with the specified entity to the view pool.
        /// </summary>
        /// <param name="entity">The entity whose view should be removed.</param>
        public void Remove(E entity)
        {
            if (!_views.Remove(entity, out V view))
                return;

            view.Hide();
            this.OnRemoved?.Invoke(entity, view);

            string name = this.GetName(entity);
            this.viewPool.Return(name, view);
        }

        /// <summary>
        /// Removes all active entity views, returning them to the view pool.
        /// </summary>
        public void Clear()
        {
            int viewCount = _views.Count;
            if (viewCount == 0)
                return;

            E[] buffer = s_entityPool.Rent(viewCount);
            _views.Keys.CopyTo(buffer, 0);

            try
            {
                for (int i = 0; i < viewCount; i++)
                    this.Remove(buffer[i]);
            }
            finally
            {
                s_entityPool.Return(buffer);
            }
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection of entity-view pairs.
        /// </summary>
        /// <returns>An enumerator of <see cref="KeyValuePair{TKey, TValue}"/> containing entity-view pairs.</returns>
        public IEnumerator<KeyValuePair<E, V>> GetEnumerator() => _views.GetEnumerator();

        /// <summary>
        /// Returns an enumerator that iterates through the collection of entity-view pairs.
        /// </summary>
        /// <returns>An enumerator containing entity-view pairs.</returns>
        IEnumerator IEnumerable.GetEnumerator() => _views.GetEnumerator();

        /// <summary>
        /// Determines the name used to retrieve the view prefab for a given entity.
        /// </summary>
        /// <param name="entity">The entity to evaluate.</param>
        /// <returns>The name used in the view pool.</returns>
        /// <remarks>
        /// The default implementation returns <see cref="IEntity.Name"/>.
        /// Override this method to implement custom naming logic 
        /// (e.g., categorizing entities or supporting localization).
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual string GetName(E entity) => entity.Name;
    }
}
#endif