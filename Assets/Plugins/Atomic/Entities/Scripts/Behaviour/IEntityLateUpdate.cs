namespace Atomic.Entities
{
    /// <summary>
    /// Defines a behavior that executes logic during the late update phase of an <see cref="IEntity"/>.
    /// </summary>
    /// <remarks>
    /// This method is automatically called by <see cref="IEntity.OnLateUpdate"/> after all standard updates,
    /// and is typically used for post-processing, transform synchronization, or order-sensitive updates.
    /// </remarks>
    public interface IEntityLateUpdate : IEntityBehaviour
    {
        /// <summary>
        /// Called during the late update phase.
        /// </summary>
        /// <param name="entity">The entity being updated.</param>
        /// <param name="deltaTime">Elapsed time since the last frame.</param>
        void OnLateUpdate(IEntity entity, float deltaTime);
    }

    /// <summary>
    /// Provides a strongly-typed version of <see cref="IEntityLateUpdate"/> for handling late update logic
    /// on a specific <see cref="IEntity"/> type.
    /// </summary>
    /// <typeparam name="E">The concrete entity type this behavior is associated with.</typeparam>
    /// <remarks>
    /// This method is automatically invoked by <see cref="IEntity.OnLateUpdate"/> 
    /// when the behavior is registered on an entity of type <typeparamref name="E"/>.
    /// </remarks>
    public interface IEntityLateUpdate<in E> : IEntityLateUpdate where E : IEntity
    {
        /// <summary>
        /// Called during the late update phase for a strongly-typed entity.
        /// </summary>
        /// <param name="entity">The entity instance of type <typeparamref name="E"/>.</param>
        /// <param name="deltaTime">Elapsed time since the last frame.</param>
        void OnLateUpdate(E entity, float deltaTime);

        void IEntityLateUpdate.OnLateUpdate(IEntity entity, float deltaTime) =>
            this.OnLateUpdate((E) entity, deltaTime);
    }
}