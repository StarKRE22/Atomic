using System;
using Atomic.Entities;
using UnityEngine.Scripting.APIUpdating;

namespace Atomic.Extensions
{
    [MovedFrom(true, null, null, "EntityConditionCreator_Entity_Initialized")]
    [Serializable]
    public sealed class EntityConditionAsset_Entity_Initialized : IEntityConditionAsset_Entity
    {
        public Func<IEntity, bool> Create(IEntity entity)
        {
            return target => target.Initialized;
        }
    }
}