#if ODIN_INSPECTOR
using System;
using Atomic.Entities;
using UnityEngine;

#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

namespace Atomic.Entities
{
#if ODIN_INSPECTOR
    [InlineProperty]
#endif
    [Serializable]
    public class BehaviourEntityInstaller<T> : IEntityInstaller where T : IEntityBehaviour
    {
        [SerializeField]
        protected T value;

        public T Value => this.value;

        public BehaviourEntityInstaller()
        {
        }

        public BehaviourEntityInstaller(T value)
        {
            this.value = value;
        }

        public virtual void Install(IEntity entity)
        {
            entity.AddBehaviour(this.value);
        }
    }
}
#endif