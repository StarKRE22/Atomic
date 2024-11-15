#if ODIN_INSPECTOR
using System;
using Atomic.Entities;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

namespace Atomic.Extensions
{
    [MovedFrom(true, null, null, "EntityConditionCreator_Entity_HasAllTags")]
    [Serializable]
    public sealed class EntityConditionAsset_Entity_HasAllTags : IEntityConditionAsset_Entity
    {
        [EntityTag]
        [SerializeField]
        private int[] tags;
        
        public Func<IEntity, bool> Create(IEntity entity)
        {
            return target => target.HasAllTags(this.tags);
        }
    }
}
#endif