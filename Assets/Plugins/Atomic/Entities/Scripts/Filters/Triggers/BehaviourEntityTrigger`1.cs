using System;

namespace Atomic.Entities
{
    /// <summary>
    /// A trigger that responds to behaviour changes (added or removed) on entities of type <typeparamref name="E"/>.
    /// </summary>
    /// <typeparam name="E">The entity type, which must implement <see cref="IEntity"/>.</typeparam>
    public class BehaviourEntityTrigger<E> : IEntityTrigger<E> where E : IEntity
    {
        private Action<E> _action;

        private readonly bool _added;
        private readonly bool _removed;

        /// <summary>
        /// Initializes a new instance of the <see cref="BehaviourEntityTrigger{E}"/> class.
        /// </summary>
        /// <param name="added">Whether to react to behaviour additions via <c>OnBehaviourAdded</c>.</param>
        /// <param name="removed">Whether to react to behaviour removals via <c>OnBehaviourRemoved</c>.</param>
        public BehaviourEntityTrigger(bool added = true, bool removed = true)
        {
            _added = added;
            _removed = removed;
        }

        /// <summary>
        /// Sets the action to be invoked when the trigger detects a relevant change in the entity.
        /// </summary>
        /// <param name="action">The callback action to invoke.</param>
        public void SetAction(Action<E> action) => _action = action ?? throw new ArgumentNullException(nameof(action));

        /// <summary>
        /// Subscribes to the behaviour-related events on the given entity.
        /// </summary>
        /// <param name="entity">The entity to track.</param>
        public void Track(E entity)
        {
            if (_added) entity.OnBehaviourAdded += this.OnBehaviourAdded;
            if (_removed) entity.OnBehaviourDeleted += this.OnBehaviourRemoved;
        }

        /// <summary>
        /// Unsubscribes from the behaviour-related events on the given entity.
        /// </summary>
        /// <param name="entity">The entity to untrack.</param>
        public void Untrack(E entity)
        {
            if (_added) entity.OnBehaviourAdded -= this.OnBehaviourAdded;
            if (_removed) entity.OnBehaviourDeleted -= this.OnBehaviourRemoved;
        }

        /// <summary>
        /// Called when a behaviour is removed from the entity. Invokes the configured action.
        /// </summary>
        /// <param name="entity">The entity from which the behaviour was removed.</param>
        /// <param name="behaviour">The behaviour that was removed (ignored).</param>
        private void OnBehaviourRemoved(IEntity entity, IEntityBehaviour _) => _action.Invoke((E) entity);

        /// <summary>
        /// Called when a behaviour is added to the entity. Invokes the configured action.
        /// </summary>
        /// <param name="entity">The entity to which the behaviour was added.</param>
        /// <param name="behaviour">The behaviour that was added (ignored).</param>
        private void OnBehaviourAdded(IEntity entity, IEntityBehaviour _) => _action.Invoke((E) entity);
    }
}