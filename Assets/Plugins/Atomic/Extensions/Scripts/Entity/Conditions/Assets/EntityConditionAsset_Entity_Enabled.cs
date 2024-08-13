using System;
using Atomic.Entities;
using UnityEngine.Scripting.APIUpdating;

namespace Atomic.Extensions
{
    [MovedFrom(true, null, null, "EntityConditionCreator_Entity_Enabled")] 
    [Serializable]
    public sealed class EntityConditionAsset_Entity_Enabled : IEntityConditionAsset_Entity
    {
        public Func<IEntity, bool> Create(IEntity entity)
        {
            return target => target.Enabled;
        }
    }
}