using System;
using Atomic.Entities;

namespace Atomic.Extensions
{
    [Serializable]
    public sealed class EntityPredicateAsset_Entity_Initialized : IEntityPredicateAsset_Entity
    {
        public Func<IEntity, bool> Create(IEntity entity)
        {
            return target => target.Initialized;
        }
    }
}