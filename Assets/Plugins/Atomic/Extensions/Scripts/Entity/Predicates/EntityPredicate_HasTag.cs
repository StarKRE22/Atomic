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
    public sealed class EntityPredicate_HasTag : IEntityPredicate
    {
        [EntityTag, SerializeField]
        private int tag;
        
        public bool Invoke(IEntity entity)
        {
            return entity.HasTag(this.tag);
        }
    }
}
#endif