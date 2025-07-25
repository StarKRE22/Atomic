using System;

namespace Atomic.Entities
{
    /// <summary>
    /// A non-generic shortcut for <see cref="InlineEntityTrigger{IEntity}"/>.
    /// Provides inline tracking logic for basic <see cref="IEntity"/> instances.
    /// </summary>
    public class InlineEntityTrigger : InlineEntityTrigger<IEntity>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InlineEntityTrigger"/> class
        /// with the specified tracking and untracking delegates.
        /// </summary>
        /// <param name="track">A delegate that defines how to start tracking the entity, given an action.</param>
        /// <param name="untrack">A delegate that defines how to stop tracking the entity, given an action.</param>
        public InlineEntityTrigger(
            Action<IEntity, Action<IEntity>> track,
            Action<IEntity, Action<IEntity>> untrack)
            : base(track, untrack)
        {
        }
    }

    /// <summary>
    /// An inline-configurable entity trigger that allows custom tracking and untracking logic
    /// for a specific entity type.
    /// </summary>
    /// <typeparam name="E">The type of entity to track, constrained to <see cref="IEntity"/>.</typeparam>
    public class InlineEntityTrigger<E> : EntityTriggerBase<E> where E : IEntity
    {
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

        /// <inheritdoc />
        public override void Track(E entity) => _track.Invoke(entity, _action);

        /// <inheritdoc />
        public override void Untrack(E entity) => _untrack.Invoke(entity, _action);
    }
}