using System;
using System.Collections.Generic;

namespace Atomic.Entities
{
    /// <summary>
    /// Abstract base trigger for entities that uses subscriptions.
    /// Maintains and manages <see cref="IDisposable"/> objects for each tracked entity
    /// and releases resources when tracking is stopped.
    /// </summary>
    /// <typeparam name="E">The type of entity implementing <see cref="IEntity"/>.</typeparam>
    /// <typeparam name="S">The type of subscription.</typeparam>
    public abstract class SubscriptionEntityTrigger<E, S> : IEntityTrigger<E>
        where E : IEntity
        where S : IDisposable
    {
        private Action<E> _action;
        
        /// <summary>
        /// Stores active subscriptions, mapping each entity to its associated disposable subscription.
        /// </summary>
        private readonly Dictionary<IEntity, S> _subscriptions = new();

        /// <summary>
        /// Sets the action to be invoked when the trigger detects a relevant change in the entity.
        /// </summary>
        /// <param name="action">The callback action to invoke.</param>
        public void SetAction(Action<E> action) => _action = action ?? throw new ArgumentNullException(nameof(action));

        /// <summary>
        /// Starts tracking the specified entity.
        /// Creates a subscription via <see cref="Track(E, Action{E})"/> and stores it
        /// for later disposal when untracking.
        /// </summary>
        /// <param name="entity">The entity to track.</param>
        public void Track(E entity)
        {
            if (!_subscriptions.ContainsKey(entity))
            {
                S subscription = this.Track(entity, _action);
                _subscriptions.Add(entity, subscription);
            }
        }

        /// <summary>
        /// Stops tracking the specified entity.
        /// Removes the associated subscription and calls <see cref="IDisposable.Dispose"/> to release resources.
        /// </summary>
        /// <param name="entity">The entity to stop tracking.</param>
        public void Untrack(E entity)
        {
            if (_subscriptions.Remove(entity, out S subscription))
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
        protected abstract S Track(E entity, Action<E> callback);
    }
}