using System;

#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

using UnityEngine;

namespace Atomic.Entities
{
    public abstract class AbstractView<E> : MonoBehaviour where E : IEntity<E>
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
        public bool IsShown => _isShown;

        private protected E _entity;
        private protected bool _isShown;
        
        public void Show(E entity)
        {
            _entity = entity ?? throw new ArgumentNullException(nameof(entity));
            _isShown = true;
            this.OnShow(entity);
        }

        public void Hide()
        {
            this.OnHide(_entity);
            _isShown = false;
            _entity = default;
        }

        protected virtual void OnShow(E entity) => 
            this.gameObject.SetActive(true);

        protected virtual void OnHide(E entity) => 
            this.gameObject.SetActive(false);
    }
}