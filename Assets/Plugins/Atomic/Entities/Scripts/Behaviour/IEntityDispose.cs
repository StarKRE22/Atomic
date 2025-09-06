namespace Atomic.Entities
{
    /// <summary>
    /// Defines a behavior that executes cleanup or resource release logic 
    /// when an <see cref="IEntity"/> is being disposed.
    /// </summary>
    /// <remarks>
    /// This method is automatically called by <see cref="IEntity.Dispose"/> 
    /// when the entity is permanently destroyed, removed from the game, 
    /// or otherwise released from use. 
    /// Implementations should use this to release references, unsubscribe from events,
    /// or return pooled resources.
    /// </remarks>
    public interface IEntityDispose : IEntityBehaviour
    {
        /// <summary>
        /// Called when the entity is being disposed.
        /// </summary>
        /// <param name="entity">The entity being disposed.</param>
        void Dispose(IEntity entity);
    }

    /// <summary>
    /// Provides a strongly-typed version of <see cref="IEntityDispose"/> 
    /// for handling cleanup logic for a specific <see cref="IEntity"/> type.
    /// </summary>
    /// <typeparam name="E">
    /// The concrete entity type this behavior is associated with.
    /// </typeparam>
    /// <remarks>
    /// This method is automatically invoked by <see cref="IDisposeSource.Dispose"/> 
    /// when the behavior is registered on an entity of type <typeparamref name="E"/>.
    /// </remarks>
    public interface IEntityDispose<in E> : IEntityDispose where E : IEntity
    {
        /// <summary>
        /// Called when the typed entity is being disposed.
        /// </summary>
        /// <param name="entity">
        /// The entity instance of type <typeparamref name="E"/>.
        /// </param>
        void Dispose(E entity);

        void IEntityDispose.Dispose(IEntity entity) => this.Dispose((E) entity);
    }
}