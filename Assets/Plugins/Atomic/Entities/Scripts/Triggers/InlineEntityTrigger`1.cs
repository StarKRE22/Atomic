using System;

namespace Atomic.Entities
{
    /// <summary>
    /// An inline-configurable entity trigger that allows custom tracking and untracking logic
    /// for a specific entity type.
    /// </summary>
    /// <typeparam name="E">The type of entity to track, constrained to <see cref="IEntity"/>.</typeparam>
    public class InlineEntityTrigger<E> : IEntityTrigger<E> where E : IEntity
    {
        private Action<E> _action;

        private readonly Action<E, Action<E>> _track;
        private readonly Action<E, Action<E>> _untrack;

        /// <summary>
        /// Initializes a new instance of the <see cref="InlineEntityTrigger{E}"/> class
        /// with the specified tracking and untracking logic.
        /// </summary>
        /// <param name="track">
        /// A delegate that receives the entity and the trigger action,
        /// used to define how the entity should be subscribed or monitored.
        /// </param>
        /// <param name="untrack">
        /// A delegate that receives the entity and the trigger action,
        /// used to define how the entity should be unsubscribed or ignored.
        /// </param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="track"/> or <paramref name="untrack"/> is null.</exception>
        public InlineEntityTrigger(Action<E, Action<E>> track, Action<E, Action<E>> untrack)
        {
            _track = track ?? throw new ArgumentNullException(nameof(track));
            _untrack = untrack ?? throw new ArgumentNullException(nameof(untrack));
        }

        /// <summary>
        /// Sets the action to be invoked when the trigger detects a relevant change in the entity.
        /// </summary>
        /// <param name="action">The callback action to invoke.</param>
        public void SetAction(Action<E> action) => _action = action ?? throw new ArgumentNullException(nameof(action));

        /// <summary>
        /// Begins tracking the specified entity for changes.
        /// </summary>
        /// <param name="entity">The entity to track.</param>
        public void Track(E entity) => _track.Invoke(entity, _action);

        /// <summary>
        /// Stops tracking the specified entity.
        /// </summary>
        /// <param name="entity">The entity to stop tracking.</param>
        public void Untrack(E entity) => _untrack.Invoke(entity, _action);
    }
}