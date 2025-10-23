using System;

namespace Atomic.Entities
{
    /// <summary>
    /// A trigger that responds to value changes on an entity of type <typeparamref name="E"/>.
    /// It listens for value additions, removals, and modifications using corresponding entity events.
    /// </summary>
    /// <typeparam name="E">The type of entity to track, constrained to <see cref="IEntity"/>.</typeparam>
    public class ValueEntityTrigger<E> : IEntityTrigger<E> where E : IEntity
    {
        private Action<E> _action;
        
        private readonly bool _added;
        private readonly bool _deleted;
        private readonly bool _changed;

        /// <summary>
        /// Initializes a new instance of the <see cref="ValueEntityTrigger{E}"/> class
        /// with configurable listening behavior for value events.
        /// </summary>
        /// <param name="added">Whether to react to value additions via <c>OnValueAdded</c>.</param>
        /// <param name="deleted">Whether to react to value removals via <c>OnValueDeleted</c>.</param>
        /// <param name="changed">Whether to react to value changes via <c>OnValueChanged</c>.</param>
        public ValueEntityTrigger(bool added = true, bool deleted = true, bool changed = true)
        {
            _added = added;
            _deleted = deleted;
            _changed = changed;
        }
        
        /// <summary>
        /// Sets the action to be invoked when the trigger detects a relevant change in the entity.
        /// </summary>
        /// <param name="action">The callback action to invoke.</param>
        public void SetAction(Action<E> action)
        {
            _action = action ?? throw new ArgumentNullException(nameof(action));
        }

        /// <summary>
        /// Subscribes to value-related events on the given entity, based on the configured flags.
        /// </summary>
        /// <param name="entity">The entity to track.</param>
        public void Track(E entity)
        {
            if (_added) entity.OnValueAdded += this.OnValueAdded;
            if (_deleted) entity.OnValueDeleted += this.OnValueDeleted;
            if (_changed) entity.OnValueChanged += this.OnValueChanged;
        }

        /// <summary>
        /// Unsubscribes from value-related events on the given entity.
        /// </summary>
        /// <param name="entity">The entity to stop tracking.</param>
        public void Untrack(E entity)
        {
            if (_added) entity.OnValueAdded -= this.OnValueAdded;
            if (_deleted) entity.OnValueDeleted -= this.OnValueDeleted;
            if (_changed) entity.OnValueChanged -= this.OnValueChanged;
        }

        /// <summary>
        /// Handles the <c>OnValueDeleted</c> event and invokes the configured trigger action.
        /// </summary>
        /// <param name="entity">The entity from which the value was removed.</param>
        /// <param name="key">The key of the removed value.</param>
        private void OnValueDeleted(IEntity entity, int key) => _action.Invoke((E) entity);

        /// <summary>
        /// Handles the <c>OnValueAdded</c> event and invokes the configured trigger action.
        /// </summary>
        /// <param name="entity">The entity to which the value was added.</param>
        /// <param name="key">The key of the added value.</param>
        private void OnValueAdded(IEntity entity, int key) => _action.Invoke((E) entity);

        /// <summary>
        /// Handles the <c>OnValueChanged</c> event and invokes the configured trigger action.
        /// </summary>
        /// <param name="entity">The entity whose value was changed.</param>
        /// <param name="key">The key of the changed value.</param>
        private void OnValueChanged(IEntity entity, int key) => _action.Invoke((E) entity);
    }
}