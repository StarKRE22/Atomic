using System;

namespace Atomic.Entities
{
    public sealed class BaseEntityTrigger<E> : EntityTriggerBase<E> where E : IEntity
    {
        private readonly Action<E, Action<E>> _track;
        private readonly Action<E, Action<E>> _untrack;

        public BaseEntityTrigger(Action<E, Action<E>> track, Action<E, Action<E>> untrack)
        {
            _track = track;
            _untrack = untrack;
        }

        public override void Track(E entity) => _track.Invoke(entity, _action);
        public override void Untrack(E entity) => _untrack.Invoke(entity, _action);
    }
}