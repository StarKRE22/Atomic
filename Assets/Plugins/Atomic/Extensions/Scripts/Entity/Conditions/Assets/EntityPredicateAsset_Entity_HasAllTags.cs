#if ODIN_INSPECTOR
using System;
using Atomic.Entities;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

namespace Atomic.Extensions
{
    [Serializable]
    public sealed class EntityPredicateAsset_Entity_HasAllTags : IEntityPredicateAsset_Entity
    {
        [EntityTag]
        [SerializeField]
        private int[] tags;
        
        public Func<IEntity, bool> Create(IEntity entity) => target => target.HasAllTags(this.tags);
    }
}
#endif