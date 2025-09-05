#if UNITY_5_3_OR_NEWER
using System;
using System.Runtime.CompilerServices;
using UnityEngine;

#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

namespace Atomic.Entities
{
    public abstract class EntityView : EntityView<IEntity>
    {
    }

    /// <summary>
    /// Base class for all entity views. Implements <see cref="IEntityView"/> to provide
    /// basic functionality for showing, hiding, and naming views associated with <see cref="IEntity"/>.
    /// </summary>
    public abstract class EntityView<E> : MonoBehaviour, IEntityView<E> where E : IEntity
    {
        [Tooltip("Should activate and deactivate GameObject when Show/Hide happens?")]
        [SerializeField]
        private bool _controlGameObject = true;

        [Header("Name")]
        [Tooltip("If true, the view will use the custom name instead of the GameObject's name")]
        [SerializeField]
        private bool _overrideName;

        [Tooltip("The custom name to use for the view when _overrideName is enabled")]
        [SerializeField]
        private string _customName;

        /// <summary>
        /// Gets the display name of the view. Returns <see cref="_customName"/> if <see cref="_overrideName"/> is true; otherwise, returns the GameObject's name.
        /// </summary>
        public virtual string Name => _overrideName ? _customName : this.name;

        /// <summary>
        /// Gets the entity currently associated with this view.
        /// </summary>
#if ODIN_INSPECTOR
        [ShowInInspector]
#endif
        public E Entity => _entity;

        /// <summary>
        /// Gets a value indicating whether the view is currently visible (active in the scene).
        /// </summary>
        public bool IsVisible => _entity != null;

        private bool _initialized;
        private E _entity;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Init()
        {
            if (!_initialized)
            {
                this.OnInit();
                _initialized = true;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Dispose()
        {
            this.Hide();
            
            if (_initialized)
            {
                this.OnDispose();
                _initialized = false;
            }
        }

        /// <summary>
        /// Displays the view and associates it with the specified entity.
        /// </summary>
        /// <param name="entity">The entity to associate with and display through this view.</param>
        public void Show(E entity)
        {
            _entity = entity ?? throw new ArgumentNullException(nameof(entity));
            this.Init();
            
            if (_controlGameObject)
                this.gameObject.SetActive(true);

            this.OnShow(entity);
        }

        /// <summary>
        /// Hides the view and removes its association with the entity.
        /// </summary>
        public void Hide()
        {
            if (_entity == null)
                return;

            if (_controlGameObject)
                this.gameObject.SetActive(false);

            this.OnHide(_entity);
            _entity = default;
        }

        protected abstract void OnInit();

        /// <summary>
        /// Called when the view is shown. Override to implement custom behavior, such as enabling visuals.
        /// </summary>
        /// <param name="entity">The entity being shown.</param>
        protected abstract void OnShow(IEntity entity);

        /// <summary>
        /// Called when the view is hidden. Override to implement custom behavior, such as disabling visuals.
        /// </summary>
        /// <param name="entity">The entity being hidden.</param>
        protected abstract void OnHide(IEntity entity);

        protected abstract void OnDispose();

        /// <summary>
        /// Destroys the view and its associated GameObject after an optional delay.
        /// </summary>
        /// <param name="view">The <see cref="EntityView"/> instance to destroy.</param>
        /// <param name="time">Optional delay in seconds before destruction. Defaults to 0.</param>
        public static void Destroy(EntityView<E> view, float time = 0)
        {
            if (view != null)
            {
                view.Dispose();
                Destroy(view.gameObject, time);
            }
        }

        /// <summary>
        /// Assigns the GameObject's name to the custom name field.
        /// </summary>
        [ContextMenu("Assign Custom Name From GameObject")]
        private void AssignCustomNameFromGameObject()
        {
            _customName = this.name;
        }
    }
}
#endif