using System;
using System.Collections.Generic;

namespace Atomic.Entities
{
    public abstract class EntityTriggerDisposable : EntityTriggerDisposable<IEntity>
    {
    }

    public abstract class EntityTriggerDisposable<E> : EntityTriggerBase<E> where E : IEntity
    {
        private readonly Dictionary<IEntity, IDisposable> _subscriptions = new();

        public sealed override void Track(E entity)
        {
            if (!_subscriptions.ContainsKey(entity))
                _subscriptions.Add(entity, this.ProvideSubscription(entity, this.InvokeAction));
        }

        public sealed override void Untrack(E entity)
        {
            if (_subscriptions.Remove(entity, out IDisposable subscription))
                subscription.Dispose();
        }

        protected abstract IDisposable ProvideSubscription(E entity, Action<E> action);
    }
}