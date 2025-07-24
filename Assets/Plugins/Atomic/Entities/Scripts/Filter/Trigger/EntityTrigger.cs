using System;

namespace Atomic.Entities
{
    public sealed class EntityTrigger<E> : EntityTriggerAbstract<E> where E : IEntity
    {
        private readonly Action<E, Action<E>> _observe;
        private readonly Action<E, Action<E>> _unobserve;

        public EntityTrigger(Action<E, Action<E>> observe, Action<E, Action<E>> unobserve)
        {
            _observe = observe;
            _unobserve = unobserve;
        }

        public override void Observe(E entity) => _observe.Invoke(entity, _callback);
        public override void Unobserve(E entity) => _unobserve.Invoke(entity, _callback);
    }
}