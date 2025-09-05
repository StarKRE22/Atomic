using UnityEngine;

namespace Atomic.Entities
{
    public abstract class ScriptableEntityAspect : ScriptableEntityAspect<IEntity>, IEntityAspect
    {
    }

    public abstract class ScriptableEntityAspect<E> : ScriptableObject, IEntityAspect<E> where E : IEntity
    {
        public abstract void Apply(E entity);
        
        public abstract void Discard(E entity);
    }
}