#if ODIN_INSPECTOR
using System;
using Atomic.Entities;
using UnityEngine;

namespace Atomic.Extensions
{
    [Serializable]
    public sealed class EntityPredicateAsset_Entity_HasTag : IEntityPredicateAsset_Entity
    {
        [EntityTag]
        [SerializeField]
        private int tag;
        
        public Func<IEntity, bool> Create(IEntity entity)
        {
            return target => target.HasTag(this.tag);
        }
    }
}
#endif