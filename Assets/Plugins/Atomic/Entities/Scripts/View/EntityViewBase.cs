#if UNITY_5_3_OR_NEWER
using System;

#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

using UnityEngine;

namespace Atomic.Entities
{
    [AddComponentMenu("Atomic/Entities/Entity View Base")]
    [DisallowMultipleComponent]
    public class EntityViewBase : MonoBehaviour, IEntityView
    {
        [SerializeField]
        private bool _overrideName;

#if ODIN_INSPECTOR
        [ShowIf(nameof(_overrideName))]
#endif
        [SerializeField]
        private string _customName;

        /// <inheritdoc/>
#if ODIN_INSPECTOR
        [Title("Debug")]
        [ShowInInspector, HideInEditorMode]
#endif
        public virtual string Name => _overrideName ? _customName : this.name;

        /// <inheritdoc/>
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
        protected virtual void OnHide(IEntity entity)
        {
            if (this != null)
                this.gameObject.SetActive(false);
        }

        public static void Destroy(EntityViewBase view, float time = 0)
        {
            if (view == null) 
                return;
            
            view.Hide();
            Destroy(view.gameObject, time);
        }

        [ContextMenu("Assign Custom Name From GameObject")]
        private void AssignCustomNameFromGameObject() => _customName = this.name;

        #region Debug

#if ODIN_INSPECTOR
        [ShowInInspector, HideInEditorMode, LabelText("Entity")]
#endif
        private IEntity DebugEntity
        {
            get => _entity;
            set => _entity = value;
        }

        #endregion
    }
}
#endif