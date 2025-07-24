using System;

namespace Atomic.Entities
{
    public interface IEntityTrigger : IEntityTrigger<IEntity>
    {
    }

    /// <summary>
    /// Represents a trigger that monitors specific aspects of an entity's state
    /// and signals when the entity should be re-evaluated by a filter.
    /// </summary>
    /// <typeparam name="E">The entity type being tracked.</typeparam>
    public interface IEntityTrigger<E> where E : IEntity
    {
        void SetCallback(Action<E> callback);

        /// <summary>
        /// Begins tracking the specified entity and registers a callback for when it should be re-evaluated.
        /// </summary>
        /// <param name="entity">The entity to start tracking.</param>
        void Observe(E entity);

        /// <summary>
        /// Stops tracking the specified entity and removes the previously registered callback.
        /// </summary>
        /// <param name="entity">The entity to stop tracking.</param>
        void Unobserve(E entity);
    }
}