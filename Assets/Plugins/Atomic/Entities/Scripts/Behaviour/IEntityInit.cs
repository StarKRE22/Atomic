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
    /// <typeparam name="T">
    /// The concrete entity type this behavior is associated with.
    /// </typeparam>
    /// <remarks>
    /// This method is automatically invoked by <see cref="IInitSource.Init"/> 
    /// when the behavior is registered on an entity of type <typeparamref name="T"/>.
    /// </remarks>
    public interface IEntityInit<in T> : IEntityInit where T : IEntity
    {
        /// <summary>
        /// Called when the typed entity is initialized.
        /// </summary>
        /// <param name="entity">
        /// The entity instance of type <typeparamref name="T"/>.
        /// </param>
        void Init(T entity);

        void IEntityInit.Init(IEntity entity) => this.Init((T) entity);
    }
}