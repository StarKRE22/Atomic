using System;

namespace Atomic.Entities
{
    public abstract class EntityTriggerAbstract<E> : IEntityTrigger<E> where E : IEntity
    {
        private Action<E> _callback;
        
        public void SetCallback(Action<E> callback) => _callback = callback;

        public abstract void Observe(E entity);
        public abstract void Unobserve(E entity);

        protected void Invoke(E entity) => _callback?.Invoke(entity);
    }
}