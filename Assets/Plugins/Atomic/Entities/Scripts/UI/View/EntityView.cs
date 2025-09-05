#if UNITY_5_3_OR_NEWER
using System;
using UnityEngine;

#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

namespace Atomic.Entities
{
    /// <summary>
    /// Base class for all entity views. Implements <see cref="IEntityView"/> to provide
    /// basic functionality for showing, hiding, and naming views associated with <see cref="IEntity"/>.
    /// </summary>
    [AddComponentMenu("Atomic/Entities/Entity View")]
    [DisallowMultipleComponent]
    public class EntityView : MonoBehaviour, IEntityView
    {
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
        public IEntity Entity => _entity;

        /// <summary>
        /// Gets a value indicating whether the view is currently visible (active in the scene).
        /// </summary>
        public bool IsVisible => _isVisible;

        private protected IEntity _entity;
        private protected bool _isVisible;

        /// <summary>
        /// Displays the view and associates it with the specified entity.
        /// </summary>
        /// <param name="entity">The entity to associate with and display through this view.</param>
        public void Show(IEntity entity)
        {
            _entity = entity ?? throw new ArgumentNullException(nameof(entity));
            _isVisible = true;
            this.OnShow(entity);
        }

        /// <summary>
        /// Hides the view and removes its association with the entity.
        /// </summary>
        public void Hide()
        {
            if (_isVisible)
            {
                this.OnHide(_entity);
                _isVisible = false;
                _entity = null;
            }
        }

        /// <summary>
        /// Called when the view is shown. Override to implement custom behavior, such as enabling visuals.
        /// </summary>
        /// <param name="entity">The entity being shown.</param>
        protected virtual void OnShow(IEntity entity)
        {
            this.gameObject.SetActive(true);
        }

        /// <summary>
        /// Called when the view is hidden. Override to implement custom behavior, such as disabling visuals.
        /// </summary>
        /// <param name="entity">The entity being hidden.</param>
        protected virtual void OnHide(IEntity entity)
        {
            if (this != null)
                this.gameObject.SetActive(false);
        }

        /// <summary>
        /// Destroys the view and its associated GameObject after an optional delay.
        /// </summary>
        /// <param name="view">The <see cref="EntityView"/> instance to destroy.</param>
        /// <param name="time">Optional delay in seconds before destruction. Defaults to 0.</param>
        public static void Destroy(EntityView view, float time = 0)
        {
            if (view == null) 
                return;

            view.Hide();
            Destroy(view.gameObject, time);
        }

        /// <summary>
        /// Assigns the GameObject's name to the custom name field.
        /// </summary>
        [ContextMenu("Assign Custom Name From GameObject")]
        private void AssignCustomNameFromGameObject() => _customName = this.name;
    }
}
#endif
