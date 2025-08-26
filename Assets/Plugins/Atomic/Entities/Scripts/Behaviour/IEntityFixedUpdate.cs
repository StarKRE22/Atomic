namespace Atomic.Entities
{
    /// <summary>
    /// Defines a behavior that executes logic during the fixed update cycle of an <see cref="IEntity"/>.
    /// </summary>
    /// <remarks>
    /// This method is automatically called by <see cref="IEntity.OnFixedUpdate"/> at a consistent time interval,
    /// typically aligned with the physics simulation step.
    /// </remarks>
    public interface IEntityFixedUpdate : IEntityBehaviour
    {
        /// <summary>
        /// Called during the fixed update phase.
        /// </summary>
        /// <param name="entity">The entity being updated.</param>
        /// <param name="deltaTime">The fixed time step since the last update.</param>
        void OnFixedUpdate(IEntity entity, float deltaTime);
    }

    /// <summary>
    /// Provides a strongly-typed version of <see cref="IEntityFixedUpdate"/> for handling fixed update logic
    /// on a specific <see cref="IEntity"/> type.
    /// </summary>
    /// <typeparam name="T">The concrete entity type this behavior is associated with.</typeparam>
    /// <remarks>
    /// This method is automatically invoked by <see cref="IEntity.OnFixedUpdate"/> 
    /// when the behavior is registered on an entity of type <typeparamref name="T"/>.
    /// </remarks>
    public interface IEntityFixedUpdate<in T> : IEntityFixedUpdate where T : IEntity
    {
        /// <summary>
        /// Called during the fixed update phase for a strongly-typed entity.
        /// </summary>
        /// <param name="entity">The entity instance of type <typeparamref name="T"/>.</param>
        /// <param name="deltaTime">The fixed time step since the last update.</param>
        void OnFixedUpdate(T entity, float deltaTime);

        void IEntityFixedUpdate.OnFixedUpdate(IEntity entity, float deltaTime) =>
            this.OnFixedUpdate((T) entity, deltaTime);
    }
}