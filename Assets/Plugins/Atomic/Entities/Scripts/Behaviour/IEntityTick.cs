namespace Atomic.Entities
{
    /// <summary>
    /// Defines a behavior that executes logic during the regular update cycle of an <see cref="IEntity"/>.
    /// </summary>
    /// <remarks>
    /// This method is automatically called by <see cref="ITickLifecycle.Tick"/> once per frame during the main game loop.
    /// </remarks>
    public interface IEntityTick : IEntityBehaviour
    {
        /// <summary>
        /// Called during the main update phase of the frame.
        /// </summary>
        /// <param name="entity">The entity being updated.</param>
        /// <param name="deltaTime">Elapsed time since the last frame.</param>
        void Tick(IEntity entity, float deltaTime);
    }

    /// <summary>
    /// Provides a strongly-typed version of <see cref="IEntityTick"/> for handling update logic
    /// on a specific <see cref="IEntity"/> type.
    /// </summary>
    /// <typeparam name="E">The concrete entity type this behavior is associated with.</typeparam>
    /// <remarks>
    /// This method is automatically invoked by <see cref="ITickLifecycle.Tick"/> 
    /// when the behavior is registered on an entity of type <typeparamref name="E"/>.
    /// </remarks>
    public interface IEntityTick<in E> : IEntityTick where E : IEntity
    {
        /// <summary>
        /// Called during the main update phase of the frame.
        /// </summary>
        /// <param name="entity">The strongly-typed entity being updated.</param>
        /// <param name="deltaTime">Elapsed time since the last frame.</param>
        void Tick(E entity, float deltaTime);

        void IEntityTick.Tick(IEntity entity, float deltaTime) => this.Tick((E) entity, deltaTime);
    }
}