using System;

namespace Atomic.Entities
{
    public abstract class EntityTriggerAbstract<E> : IEntityTrigger<E> where E : IEntity
    {
        private protected Action<E> _trigger;
        
        public void SetCallback(Action<E> callback) => _trigger = callback;

        public abstract void Observe(E entity);
      
        public abstract void Unobserve(E entity);

        protected void Trigger(E entity) => _trigger?.Invoke(entity);
    }
}