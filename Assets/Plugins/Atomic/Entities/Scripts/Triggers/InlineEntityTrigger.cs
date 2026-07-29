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
            Action<IEntity, Action<IEntity>> untrack
        ) : base(track, untrack)
        {
        }
    }
}