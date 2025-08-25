using System;
using System.Collections.Generic;

namespace Atomic.Entities
{
    public abstract class DisposableEntityTrigger : DisposableEntityTrigger<IEntity>
    {
    }

    public abstract class DisposableEntityTrigger<E> : EntityTriggerBase<E> where E : IEntity
    {
        private readonly Dictionary<IEntity, IDisposable> _subscriptions = new();

        public sealed override void Track(E entity)
        {
            if (!_subscriptions.ContainsKey(entity))
                _subscriptions.Add(entity, this.Track(entity, this.InvokeAction));
        }

        public sealed override void Untrack(E entity)
        {
            if (_subscriptions.Remove(entity, out IDisposable subscription))
                subscription.Dispose();
        }

        protected abstract IDisposable Track(E entity, Action<E> callback);
    }
}