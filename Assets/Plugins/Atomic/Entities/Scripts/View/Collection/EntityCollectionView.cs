#if UNITY_5_3_OR_NEWER
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Atomic.Entities
{
    [AddComponentMenu("Atomic/Entities/Entity Collection View")]
    [DisallowMultipleComponent]
    public class EntityCollectionView : MonoBehaviour, IEntityCollectionView
    {
        /// <summary>
        /// Raised when a view is spawned for a newly added entity.
        /// </summary>
        public event Action<IEntity, EntityViewBase> OnAdded;

        /// <summary>
        /// Raised when a view is removed for a despawned or removed entity.
        /// </summary>
        public event Action<IEntity, EntityViewBase> OnRemoved;

        [Space]
        [Tooltip("The viewport or container under which views will be placed in the scene hierarchy")]
        [SerializeField]
        internal Transform _viewport;

        [Tooltip("The pool responsible for providing and recycling entity view instances")]
        [SerializeField]
        internal EntityViewPoolAbstract _viewPool;

        /// <summary>
        /// Internal dictionary mapping active entities to their associated views.
        /// </summary>
        private readonly Dictionary<IEntity, EntityViewBase> _views = new();

        /// <summary>
        /// Gets the view instance associated with the specified entity.
        /// </summary>
        /// <param name="entity">The entity whose view is requested.</param>
        /// <returns>The active <see cref="IReadOnlyEntityView{E}"/> instance.</returns>
        public IReadOnlyEntityView GetView(IEntity entity) => _views[entity];

        /// <summary>
        /// Creates and shows a view for the specified entity, if the spawn condition is met.
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
        /// Hides and returns the view associated with the specified entity.
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

        public void ClearViews()
        {
            foreach (IEntity entity in _views.Keys.ToArray())
                this.RemoveView(entity);
        }

        /// <summary>
        /// Determines the name used to retrieve the view prefab for a given entity.
        /// </summary>
        /// <param name="entity">The entity to evaluate.</param>
        /// <returns>The name used in the view pool.</returns>
        protected virtual string GetEntityName(IEntity entity) => entity.Name;
    }
}
#endif