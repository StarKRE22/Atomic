namespace Atomic.Entities
{
    /// <summary>
    /// Defines a behavior that executes logic when an <see cref="IEntity"/> is initialized.
    /// </summary>
    /// <remarks>
    /// This method is automatically called by <see cref="IEntity.Init"/> 
    /// when the entity transitions into its initialized state, 
    /// such as after construction or deserialization.
    /// </remarks>
    public interface IEntityInit : IEntityBehaviour
    {
        /// <summary>
        /// Called when the entity is initialized.
        /// </summary>
        /// <param name="entity">The entity being initialized.</param>
        void Init(IEntity entity);
    }

    /// <summary>
    /// Provides a strongly-typed version of <see cref="IEntityInit"/> 
    /// for handling initialization logic for a specific <see cref="IEntity"/> type.
    /// </summary>
    /// <typeparam name="E">
    /// The concrete entity type this behavior is associated with.
    /// </typeparam>
    /// <remarks>
    /// This method is automatically invoked by <see cref="IInitLifecycle.Init"/> 
    /// when the behavior is registered on an entity of type <typeparamref name="E"/>.
    /// </remarks>
    public interface IEntityInit<in E> : IEntityInit where E : IEntity
    {
        /// <summary>
        /// Called when the typed entity is initialized.
        /// </summary>
        /// <param name="entity">
        /// The entity instance of type <typeparamref name="E"/>.
        /// </param>
        void Init(E entity);

        void IEntityInit.Init(IEntity entity) => this.Init((E) entity);
    }
}