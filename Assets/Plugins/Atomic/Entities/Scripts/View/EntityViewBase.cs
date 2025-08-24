#if UNITY_5_3_OR_NEWER
using System;

#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

using UnityEngine;

namespace Atomic.Entities
{
    public abstract class EntityViewBase : MonoBehaviour, IEntityView
    {
        /// <inheritdoc/>
#if ODIN_INSPECTOR
        [Title("Debug")]
        [ShowInInspector, HideInEditorMode]
#endif
        public virtual string Name => this.name;

        /// <inheritdoc/>
#if ODIN_INSPECTOR
        [ShowInInspector, HideInEditorMode]
#endif
        public IEntity Entity => _entity;

        /// <inheritdoc/>
#if ODIN_INSPECTOR
        [ShowInInspector, HideInEditorMode]
#endif
        public bool IsVisible => _isVisible;

        private protected IEntity _entity;
        private protected bool _isVisible;

        /// <inheritdoc/>
        public void Show(IEntity entity)
        {
            _entity = entity ?? throw new ArgumentNullException(nameof(entity));
            _isVisible = true;
            this.OnShow(entity);
        }

        /// <inheritdoc/>
        public void Hide()
        {
            if (!_isVisible)
                return;

            this.OnHide(_entity);
            _isVisible = false;
            _entity = null;
        }

        /// <summary>
        /// Called when the view is shown. Override to implement custom behavior (e.g. enable visuals).
        /// </summary>
        /// <param name="entity">The entity being shown.</param>
        protected virtual void OnShow(IEntity entity) => this.gameObject.SetActive(true);

        /// <summary>
        /// Called when the view is hidden. Override to implement custom behavior (e.g. disable visuals).
        /// </summary>
        /// <param name="entity">The entity being hidden.</param>
        protected virtual void OnHide(IEntity entity) => this.gameObject.SetActive(false);

        public static void Destroy(EntityViewBase view, float time = 0)
        {
            if (view)
            {
                view.Hide();
                Destroy(view.gameObject, time);
            }
        }
    }
}
#endif