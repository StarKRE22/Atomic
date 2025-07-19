using System;
using System.Collections.Generic;
using UnityEngine;

#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

namespace Atomic.Entities
{
    [DisallowMultipleComponent]
    public abstract class View<E> : ViewBase<E> where E : IEntity<E>
    {
#if ODIN_INSPECTOR
        [SceneObjectsOnly]
#endif
        [SerializeField]
        private List<ViewInstaller<E>> _installers;

        private readonly List<IBehaviour<E>> _behaviours = new();
        
        private bool _installed;

        protected override void OnShow(E entity)
        {
            this.Install();
            entity.AddBehaviours(_behaviours);
            base.OnShow(entity);
        }

        protected override void OnHide(E entity)
        {
            entity.DelBehaviours(_behaviours);
            base.OnHide(entity);
        }

        public void AddBehaviour(IBehaviour<E> behaviour)
        {
            _behaviours.Add(behaviour);
            
            if (_isShown) 
                _entity.AddBehaviour(behaviour);
        }

        public void DelBehaviour(IBehaviour<E> behaviour)
        {
            _behaviours.Remove(behaviour);

            if (_isShown) 
                _entity.DelBehaviour(behaviour);
        }

        private void Install()
        {
            if (_installed)
                return;

            _installed = true;

            for (int i = 0, count = _installers.Count; i < count; i++)
            {
                ViewInstaller<E> installer = _installers[i];
                if (installer != null)
                    installer.Install(this);
            }
        }
        
        private void OnDrawGizmosSelected()
        {
            if (_entity == null)
                return;

            try
            {
                for (int i = 0, count = _behaviours.Count; i < count; i++)
                    if (_behaviours[i] is IGizmos<E> gizmos)
                        gizmos.OnGizmosDraw(_entity);
            }
            catch (Exception e)
            {
                Debug.LogWarning($"Ops: detected exception in gizmos: {e.Message}");
            }
        }
    }
}