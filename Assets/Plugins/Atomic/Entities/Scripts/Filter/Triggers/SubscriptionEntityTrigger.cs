using System;
using System.Collections.Generic;

namespace Atomic.Entities
{
    /// <summary>
    /// Base trigger for working with entities using subscriptions.
    /// Provides infrastructure for tracking entities and managing subscription resources.
    /// </summary>
    public abstract class SubscriptionEntityTrigger : SubscriptionEntityTrigger<IEntity>
    {
    }

    /// <summary>
    /// Abstract base trigger for entities that uses subscriptions.
    /// Maintains and manages <see cref="IDisposable"/> objects for each tracked entity
    /// and releases resources when tracking is stopped.
    /// </summary>
    /// <typeparam name="E">The type of entity implementing <see cref="IEntity"/>.</typeparam>
    public abstract class SubscriptionEntityTrigger<E> : EntityTriggerBase<E> where E : IEntity
    {
        /// <summary>
        /// Stores active subscriptions, mapping each entity to its associated disposable subscription.
        /// </summary>
        private readonly Dictionary<IEntity, IDisposable> _subscriptions = new();

        /// <summary>
        /// Starts tracking the specified entity.
        /// Creates a subscription via <see cref="Track(E, Action{E})"/> and stores it
        /// for later disposal when untracking.
        /// </summary>
        /// <param name="entity">The entity to track.</param>
        public sealed override void Track(E entity)
        {
            if (!_subscriptions.ContainsKey(entity))
            {
                IDisposable subscription = this.Track(entity, _action);
                _subscriptions.Add(entity, subscription);
            }
        }

        /// <summary>
        /// Stops tracking the specified entity.
        /// Removes the associated subscription and calls <see cref="IDisposable.Dispose"/> to release resources.
        /// </summary>
        /// <param name="entity">The entity to stop tracking.</param>
        public sealed override void Untrack(E entity)
        {
            if (_subscriptions.Remove(entity, out IDisposable subscription))
                subscription.Dispose();
        }

        /// <summary>
        /// Defines the logic for creating a subscription for a specific entity.
        /// Must return an <see cref="IDisposable"/> that will be stored and disposed of
        /// when <see cref="Untrack(E)"/> is called.
        /// </summary>
        /// <param name="entity">The entity to track.</param>
        /// <param name="callback">Synchronization callback.</param>
        /// The callback to invoke when the entity changes or needs re-evaluation.
        /// </param>
        /// <returns>An <see cref="IDisposable"/> representing the subscription for this entity.</returns>
        protected abstract IDisposable Track(E entity, Action<E> callback);
    }
}