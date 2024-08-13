using System;
using Atomic.Entities;
using UnityEngine;

#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

namespace Atomic.Extensions
{
#if ODIN_INSPECTOR
    [InlineProperty]
#endif
    [Serializable]
    public class BehaviourEntityAspect<T> : IEntityAspect where T : IEntityBehaviour
    {
        [SerializeField]
        protected T value;

        public T Value => this.value;

        public BehaviourEntityAspect()
        {
        }

        public BehaviourEntityAspect(T value)
        {
            this.value = value;
        }

        public virtual void Apply(IEntity entity)
        {
            entity.AddBehaviour(this.value);
        }

        public void Discard(IEntity entity)
        {
            entity.DelBehaviour(this.value);
        }
    }
}