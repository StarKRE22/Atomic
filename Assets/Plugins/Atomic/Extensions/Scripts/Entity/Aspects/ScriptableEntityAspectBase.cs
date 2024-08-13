using Atomic.Entities;
using UnityEngine;

namespace Atomic.Extensions
{
    public abstract class ScriptableEntityAspectBase : ScriptableObject, IEntityAspect
    {
        public abstract void Apply(IEntity entity);
        public abstract void Discard(IEntity entity);
    }
}