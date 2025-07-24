using System;

namespace Atomic.Entities
{
    public sealed class EntityTrigger<E> : EntityTriggerAbstract<E> where E : IEntity
    {
        private readonly Action<E, Action<E>> _observe;
        private readonly Action<E, Action<E>> _unobserve;

        public override void Observe(E entity) => _observe.Invoke(entity, _trigger);

        public override void Unobserve(E entity) => _unobserve.Invoke(entity, _trigger);
    }
}