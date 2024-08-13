#if ODIN_INSPECTOR
using System;
using Atomic.Entities;

namespace Atomic.Extensions
{
    [Serializable]
    public class ElementEntityAspect<T> : ValueEntityAspect<T> where T : IEntityBehaviour
    {
        public override void Apply(IEntity entity)
        {
            base.Apply(entity);
            entity.AddBehaviour(this.value);
        }

        public override void Discard(IEntity entity)
        {
            base.Discard(entity);
            entity.DelBehaviour(this.value);
        }
    }
}
#endif