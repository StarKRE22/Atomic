using System;

namespace Atomic.Entities
{
    /// <summary>
    /// Represents a trigger that monitors specific aspects of an entity's state
    /// and signals when the entity should be re-evaluated by a filter.
    /// </summary>
    /// <typeparam name="E">The entity type being tracked.</typeparam>
    public interface IEntityTrigger<E> where E : IEntity
    {
        /// <summary>
        /// Sets the callback to be invoked when the tracked entity should be re-evaluated.
        /// </summary>
        /// <param name="action">The action to invoke for re-evaluation.</param>
        void SetAction(Action<E> action);

        /// <summary>
        /// Starts tracking the specified entity for relevant changes.
        /// </summary>
        void Track(E entity);

        /// <summary>
        /// Stops tracking the specified entity.
        /// </summary>
        void Untrack(E entity);
    }
}