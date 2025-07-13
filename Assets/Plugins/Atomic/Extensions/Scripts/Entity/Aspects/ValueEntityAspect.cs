#if ODIN_INSPECTOR
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
    public class ValueEntityAspect<T> : IEntityAspect
    {
        [EntityValue]
#if ODIN_INSPECTOR
        [HorizontalGroup]
#endif
        [SerializeField]
        private int id = -1;

#if ODIN_INSPECTOR
        [HideLabel]
        [HorizontalGroup]
#endif
        [SerializeField]
        protected T value;

        public T Value => this.value;

        public ValueEntityAspect()
        {
        }

        public ValueEntityAspect(T value)
        {
            this.value = value;
        }

        public virtual void Apply(IEntity entity)
        {
            entity.AddValue(this.id, this.value);
        }

        public virtual void Discard(IEntity entity)
        {
            entity.DelValue(this.id);
        }
    }
}
#endif