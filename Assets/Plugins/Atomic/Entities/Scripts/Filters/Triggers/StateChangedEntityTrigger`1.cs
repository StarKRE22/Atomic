using System;

namespace Atomic.Entities
{
    /// <summary>
    /// A trigger that responds to state changes on entities of type <typeparamref name="E"/>.
    /// </summary>
    /// <typeparam name="E">The entity type, which must implement <see cref="IEntity"/>.</typeparam>
    public class StateChangedEntityTrigger<E> : IEntityTrigger<E> where E : IEntity
    {
        private Action<E> _action;
        
        /// <summary>
        /// Sets the action to be invoked when the trigger detects a relevant change in the entity.
        /// </summary>
        /// <param name="action">The callback action to invoke.</param>
        public void SetAction(Action<E> action) => _action = action ?? throw new ArgumentNullException(nameof(action));

        /// <summary>
        /// Subscribes to the state change event on the given entity.
        /// </summary>
        /// <param name="entity">The entity to track.</param>
        public void Track(E entity) => entity.OnStateChanged += this.OnStateChanged;

        /// <summary>
        /// Unsubscribes from the state change event on the given entity.
        /// </summary>
        /// <param name="entity">The entity to untrack.</param>
        public void Untrack(E entity) => entity.OnStateChanged -= this.OnStateChanged;

        /// <summary>
        /// Called when the entity's state changes. Invokes the configured action.
        /// </summary>
        /// <param name="entity">The entity whose state changed.</param>
        /// <param name="state">The new state (ignored).</param>
        private void OnStateChanged(IEntity entity) => _action.Invoke((E) entity);
    }
}