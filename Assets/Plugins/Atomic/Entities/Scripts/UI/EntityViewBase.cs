using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Atomic.Entities
{
    public abstract class EntityViewBase : MonoBehaviour
    {
#if ODIN_INSPECTOR
        [Title("Debug")]
        [ShowInInspector, HideInEditorMode]
#endif
        public virtual string Name => this.name;

#if ODIN_INSPECTOR
        [ShowInInspector, HideInEditorMode]
#endif
        public IEntity Entity => _entity;

#if ODIN_INSPECTOR
        [ShowInInspector, HideInEditorMode]
#endif
        public bool IsShown => _isShown;

        private protected IEntity _entity;
        private protected bool _isShown;
        
        public void Show(IEntity entity)
        {
            _entity = entity ?? throw new ArgumentNullException(nameof(entity));
            _isShown = true;
            this.OnShow(entity);
        }

        public void Hide()
        {
            this.OnHide(_entity);
            _isShown = false;
            _entity = null;
        }

        protected virtual void OnShow(IEntity entity) => 
            this.gameObject.SetActive(true);

        protected virtual void OnHide(IEntity entity) => 
            this.gameObject.SetActive(false);
    }
}