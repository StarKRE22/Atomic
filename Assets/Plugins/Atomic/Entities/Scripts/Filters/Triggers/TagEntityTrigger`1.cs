using System;

namespace Atomic.Entities
{
    /// <summary>
    /// A trigger that responds to tag changes (added or removed) on entities of type <typeparamref name="E"/>.
    /// </summary>
    /// <typeparam name="E">The entity type, which must implement <see cref="IEntity"/>.</typeparam>
    public class TagEntityTrigger<E> : IEntityTrigger<E> where E : IEntity
    {
        private Action<E> _action;
        
        private readonly bool _added;
        private readonly bool _deleted;

        /// <summary>
        /// Initializes a new instance of the <see cref="TagEntityTrigger{E}"/> class.
        /// </summary>
        /// <param name="added">Whether to react to tag additions via <c>OnTagAdded</c>.</param>
        /// <param name="deleted">Whether to react to tag removals via <c>OnTagDeleted</c>.</param>
        public TagEntityTrigger(bool added = true, bool deleted = true)
        {
            _added = added;
            _deleted = deleted;
        }
        
        /// <summary>
        /// Sets the action to be invoked when the trigger detects a relevant change in the entity.
        /// </summary>
        /// <param name="action">The callback action to invoke.</param>
        public void SetAction(Action<E> action) => _action = action ?? throw new ArgumentNullException(nameof(action));

        /// <summary>
        /// Subscribes to the tag-related events on the given entity.
        /// </summary>
        /// <param name="entity">The entity to track.</param>
        public void Track(E entity)
        {
            if (_added) entity.OnTagAdded += this.OnTagAdded;
            if (_deleted) entity.OnTagDeleted += this.OnTagDeleted;
        }

        /// <summary>
        /// Unsubscribes from the tag-related events on the given entity.
        /// </summary>
        /// <param name="entity">The entity to untrack.</param>
        public void Untrack(E entity)
        {
            if (_added) entity.OnTagAdded -= this.OnTagAdded;
            if (_deleted) entity.OnTagDeleted -= this.OnTagDeleted;
        }

        /// <summary>
        /// Called when a tag is removed from the entity. Invokes the configured action.
        /// </summary>
        /// <param name="entity">The entity from which the tag was removed.</param>
        /// <param name="tag">The tag that was removed (ignored).</param>
        private void OnTagDeleted(IEntity entity, int tag) => _action.Invoke((E) entity);

        /// <summary>
        /// Called when a tag is added to the entity. Invokes the configured action.
        /// </summary>
        /// <param name="entity">The entity to which the tag was added.</param>
        /// <param name="tag">The tag that was added (ignored).</param>
        private void OnTagAdded(IEntity entity, int tag) => _action.Invoke((E) entity);
    }
}