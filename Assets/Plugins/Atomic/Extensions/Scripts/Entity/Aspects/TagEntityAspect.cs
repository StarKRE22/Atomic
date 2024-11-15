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
    public sealed class TagEntityAspect : IEntityAspect
    {
        [EntityTag]
        [SerializeField]
        private int tag = -1;

        public int Tag => this.tag;

        public void Apply(IEntity entity)
        {
            entity.AddTag(this.tag);
        }

        public void Discard(IEntity entity)
        {
            entity.DelTag(this.tag);
        }
    }
}
#endif