namespace Atomic.Entities
{
    /// <summary>
    /// Defines a behavior that executes logic when an <see cref="IEntity"/> is enabled.
    /// </summary>
    /// <remarks>
    /// This method is automatically called by <see cref="IEntity.Enable"/> when the entity enters the active state,
    /// such as after spawning or resuming from a disabled state.
    /// </remarks>
    public interface IEntityEnable : IEntityBehaviour
    {
        /// <summary>
        /// Called when the entity is enabled.
        /// </summary>
        /// <param name="entity">The entity being enabled.</param>
        void Enable(IEntity entity);
    }

    /// <summary>
    /// Provides a strongly-typed version of <see cref="IEntityEnable"/> for handling enable-time logic
    /// for a specific <see cref="IEntity"/> type.
    /// </summary>
    /// <typeparam name="E">The concrete entity type this behavior is associated with.</typeparam>
    /// <remarks>
    /// This method is automatically invoked by <see cref="_IEnable.Enable"/> 
    /// when the behavior is registered on an entity of type <typeparamref name="E"/>.
    /// </remarks>
    public interface IEntityEnable<in E> : IEntityEnable where E : IEntity
    {
        /// <summary>
        /// Called when the typed entity is enabled.
        /// </summary>
        /// <param name="entity">The entity instance of type <typeparamref name="E"/>.</param>
        void Enable(E entity);

        void IEntityEnable.Enable(IEntity entity) => this.Enable((E) entity);
    }
}