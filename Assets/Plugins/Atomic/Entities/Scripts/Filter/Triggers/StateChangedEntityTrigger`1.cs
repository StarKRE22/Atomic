namespace Atomic.Entities
{
    /// <summary>
    /// A trigger that responds to state changes on entities of type <typeparamref name="E"/>.
    /// </summary>
    /// <typeparam name="E">The entity type, which must implement <see cref="IEntity"/>.</typeparam>
    public class StateChangedEntityTrigger<E> : EntityTriggerBase<E> where E : IEntity
    {
        /// <summary>
        /// Subscribes to the state change event on the given entity.
        /// </summary>
        /// <param name="entity">The entity to track.</param>
        public override void Track(E entity) => entity.OnStateChanged += this.OnStateChanged;

        /// <summary>
        /// Unsubscribes from the state change event on the given entity.
        /// </summary>
        /// <param name="entity">The entity to untrack.</param>
        public override void Untrack(E entity) => entity.OnStateChanged -= this.OnStateChanged;

        /// <summary>
        /// Called when the entity's state changes. Invokes the configured action.
        /// </summary>
        /// <param name="entity">The entity whose state changed.</param>
        /// <param name="state">The new state (ignored).</param>
        private void OnStateChanged(IEntity entity) => _action.Invoke((E) entity);
    }
}