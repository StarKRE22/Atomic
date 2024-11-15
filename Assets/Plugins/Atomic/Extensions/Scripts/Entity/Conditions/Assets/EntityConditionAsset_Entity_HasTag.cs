#if ODIN_INSPECTOR
using System;
using Atomic.Entities;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

namespace Atomic.Extensions
{
    [MovedFrom(true, null, null, "EntityConditionCreator_Entity_HasTag")]
    [Serializable]
    public sealed class EntityConditionAsset_Entity_HasTag : IEntityConditionAsset_Entity
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