#if UNITY_5_3_OR_NEWER
using System;

#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

using UnityEngine;

namespace Atomic.Entities
{
    public abstract class EntityViewAbstract<E> : MonoBehaviour, IEntityView<E> where E : IEntity
    {
#if ODIN_INSPECTOR
        [Title("Debug")]
        [ShowInInspector, HideInEditorMode]
#endif
        public virtual string Name => this.name;

#if ODIN_INSPECTOR
        [ShowInInspector, HideInEditorMode]
#endif
        public E Entity => _entity;

#if ODIN_INSPECTOR
        [ShowInInspector, HideInEditorMode]
#endif
        public bool IsVisible => _isVisible;

        private protected E _entity;
        private protected bool _isVisible;
        
        public void Show(E entity)
        {
            _entity = entity ?? throw new ArgumentNullException(nameof(entity));
            _isVisible = true;
            this.OnShow(entity);
        }

        public void Hide()
        {
            if (!_isVisible)
                return;
            
            this.OnHide(_entity);
            _isVisible = false;
            _entity = default;
        }

        protected virtual void OnShow(E entity) => this.gameObject.SetActive(true);

        protected virtual void OnHide(E entity) => this.gameObject.SetActive(false);
    }
}
#endif