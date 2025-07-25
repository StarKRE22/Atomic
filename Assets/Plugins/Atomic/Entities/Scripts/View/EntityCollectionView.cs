#if UNITY_5_3_OR_NEWER
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Atomic.Entities
{
    /// <summary>
    /// A non-generic alias for <see cref="EntityCollectionView{IEntity}"/> used to manage views for <see cref="IEntity"/>.
    /// </summary>
    [AddComponentMenu("Atomic/Entities/Entity Collection View")]
    [DisallowMultipleComponent]
    public class EntityCollectionView : EntityCollectionView<IEntity>
    {
    }

    /// <summary>
    /// A Unity-based controller that visualizes a collection of entities by instantiating and managing their corresponding views.
    /// Utilizes a view pool for optimal reuse of UI elements or world representations.
    /// </summary>
    /// <typeparam name="E">The type of entity being visualized. Must implement <see cref="IEntity"/>.</typeparam>
    public abstract class EntityCollectionView<E> : MonoBehaviour where E : IEntity
    {
        /// <summary>
        /// Raised when a view is spawned for a newly added entity.
        /// </summary>
        public event Action<E, EntityViewBase<E>> OnAdded;
        
        /// <summary>
        /// Raised when a view is removed for a despawned or removed entity.
        /// </summary>
        public event Action<E, EntityViewBase<E>> OnRemoved;

        [Tooltip("The viewport or container under which views will be placed in the scene hierarchy")]
        [SerializeField]
        private Transform _viewport;

        [Tooltip("The pool responsible for providing and recycling entity view instances")]
        [SerializeField]
        private EntityViewPool<E> _viewPool;

        /// <summary>
        /// Internal dictionary mapping active entities to their associated views.
        /// </summary>
        private readonly Dictionary<E, EntityViewBase<E>> _views = new();
        
        private IReadOnlyEntityCollection<E> _source;

        /// <summary>
        /// Gets the view instance associated with the specified entity.
        /// </summary>
        /// <param name="entity">The entity whose view is requested.</param>
        /// <returns>The active <see cref="IReadOnlyEntityView{E}"/> instance.</returns>
        public IReadOnlyEntityView<E> GetView(E entity) => _views[entity];

        /// <summary>
        /// Attaches the view collection to an entity collection and begins listening for changes.
        /// Views will be created and displayed for all current and future entities.
        /// </summary>
        /// <param name="source">The entity collection to visualize.</param>
        public void Show(IReadOnlyEntityCollection<E> source)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            this.Hide();

            _source = source;
            _source.OnAdded += this.SpawnView;
            _source.OnRemoved += this.UnspawnView;

            foreach (E entity in _source)
                this.SpawnView(entity);
        }

        /// <summary>
        /// Detaches from the currently observed collection and hides all views.
        /// </summary>
        public void Hide()
        {
            if (_source == null)
                return;

            _source.OnAdded -= this.SpawnView;
            _source.OnRemoved -= this.UnspawnView;

            foreach (E entity in _source)
                this.UnspawnView(entity);
        }

        /// <summary>
        /// Determines the name used to retrieve the view prefab for a given entity.
        /// </summary>
        /// <param name="entity">The entity to evaluate.</param>
        /// <returns>The name used in the view pool.</returns>
        protected virtual string GetEntityName(E entity) => entity.Name;

        /// <summary>
        /// Determines whether a view should be spawned for the given entity.
        /// Override this method to implement conditional visualization.
        /// </summary>
        /// <param name="entity">The entity to evaluate.</param>
        /// <returns>True if the view should be created; otherwise, false.</returns>
        protected virtual bool IsSpawnConditionMet(E entity) => true;
        
        /// <summary>
        /// Creates and shows a view for the specified entity, if the spawn condition is met.
        /// </summary>
        /// <param name="entity">The entity to visualize.</param>
        private void SpawnView(E entity)
        {
            if (!this.IsSpawnConditionMet(entity))
                return;

            string name = this.GetEntityName(entity);
            EntityViewBase<E> view = _viewPool.Rent(name);
            view.transform.SetParent(_viewport);
            view.Show(entity);

            _views.Add(entity, view);
            this.OnAdded?.Invoke(entity, view);
        }

        /// <summary>
        /// Hides and returns the view associated with the specified entity.
        /// </summary>
        /// <param name="entity">The entity whose view should be removed.</param>
        private void UnspawnView(E entity)
        {
            if (!_views.Remove(entity, out EntityViewBase<E> view))
                return;

            view.Hide();
            this.OnRemoved?.Invoke(entity, view);

            string name = this.GetEntityName(entity);
            _viewPool.Return(name, view);
        }
    }
}
#endif