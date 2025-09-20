namespace Atomic.Entities
{
    /// <summary>
    /// Defines a behavior that executes logic during the fixed update cycle of an <see cref="IEntity"/>.
    /// </summary>
    /// <remarks>
    /// This method is automatically called by <see cref="ITickLifecycle.FixedTick"/> at a consistent time interval,
    /// typically aligned with the physics simulation step.
    /// </remarks>
    public interface IEntityFixedTick : IEntityBehaviour
    {
        /// <summary>
        /// Called during the fixed update phase.
        /// </summary>
        /// <param name="entity">The entity being updated.</param>
        /// <param name="deltaTime">The fixed time step since the last update.</param>
        void FixedTick(IEntity entity, float deltaTime);
    }

    /// <summary>
    /// Provides a strongly-typed version of <see cref="IEntityFixedTick"/> for handling fixed update logic
    /// on a specific <see cref="IEntity"/> type.
    /// </summary>
    /// <typeparam name="E">The concrete entity type this behavior is associated with.</typeparam>
    /// <remarks>
    /// This method is automatically invoked by <see cref="ITickLifecycle.FixedTick"/> 
    /// when the behavior is registered on an entity of type <typeparamref name="E"/>.
    /// </remarks>
    public interface IEntityFixedTick<in E> : IEntityFixedTick where E : IEntity
    {
        /// <summary>
        /// Called during the fixed update phase for a strongly-typed entity.
        /// </summary>
        /// <param name="entity">The entity instance of type <typeparamref name="E"/>.</param>
        /// <param name="deltaTime">The fixed time step since the last update.</param>
        void FixedTick(E entity, float deltaTime);

        void IEntityFixedTick.FixedTick(IEntity entity, float deltaTime) =>
            this.FixedTick((E) entity, deltaTime);
    }
}