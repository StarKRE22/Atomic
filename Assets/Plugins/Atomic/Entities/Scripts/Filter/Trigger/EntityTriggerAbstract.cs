using System;

namespace Atomic.Entities
{
    public abstract class EntityTriggerAbstract<E> : IEntityTrigger<E> where E : IEntity
    {
        private protected Action<E> _callback;

        Action<E> IEntityTrigger<E>.Callback
        {
            set => _callback = value;
        }

        public abstract void Observe(E entity);
        
        public abstract void Unobserve(E entity);

        protected void Invoke(E entity) => _callback?.Invoke(entity);
    }
}