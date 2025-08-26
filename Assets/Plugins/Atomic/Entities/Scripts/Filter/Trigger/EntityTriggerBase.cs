using System;
using System.Runtime.CompilerServices;

namespace Atomic.Entities
{
    /// <summary>
    /// Provides a base implementation of <see cref="EntityTriggerBase{IEntity}"/> for triggers that operate on <see cref="IEntity"/>.
    /// This serves as a convenient base class when no specific entity type is needed.
    /// </summary>
    public abstract class EntityTriggerBase : EntityTriggerBase<IEntity>
    {
    }

    /// <summary>
    /// Provides a base implementation for entity triggers that monitor changes in entity state
    /// and invoke a configured action when such changes occur.
    /// </summary>
    /// <typeparam name="E">The type of entity being tracked.</typeparam>
    public abstract class EntityTriggerBase<E> : IEntityTrigger<E> where E : IEntity
    {
        /// <summary>
        /// The callback action to invoke when a tracked entity requires re-evaluation.
        /// </summary>
        private protected Action<E> _action;

        /// <summary>
        /// Sets the action to be invoked when the trigger detects a relevant change in the entity.
        /// </summary>
        /// <param name="action">The callback action to invoke.</param>
        public void SetAction(Action<E> action) => _action = action ?? throw new ArgumentNullException(nameof(action));

        /// <summary>
        /// Begins tracking the specified entity for changes.
        /// </summary>
        /// <param name="entity">The entity to track.</param>
        public abstract void Track(E entity);

        /// <summary>
        /// Stops tracking the specified entity.
        /// </summary>
        /// <param name="entity">The entity to stop tracking.</param>
        public abstract void Untrack(E entity);

        /// <summary>
        /// Invokes the configured action for the given entity, if any.
        /// </summary>
        /// <param name="entity">The entity to pass to the callback action.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected void InvokeAction(E entity) => _action?.Invoke(entity);
    }
}