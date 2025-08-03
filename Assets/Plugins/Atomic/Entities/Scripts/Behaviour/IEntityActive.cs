using Unity.Collections.LowLevel.Unsafe;

namespace Atomic.Entities
{
    /// <summary>
    /// Defines a behavior that executes logic when an <see cref="IEntity"/> is enabled.
    /// </summary>
    /// <remarks>
    /// This method is automatically called by <see cref="IActivatable.Activate"/> when the entity enters the active state,
    /// such as after spawning or resuming from a disabled state.
    /// </remarks>
    public interface IEntityActive : IEntityBehaviour
    {
        /// <summary>
        /// Called when the entity is enabled.
        /// </summary>
        /// <param name="entity">The entity being enabled.</param>
        void OnActive(IEntity entity);
    }

    /// <summary>
    /// Provides a strongly-typed version of <see cref="IEntityActive"/> for handling enable-time logic
    /// for a specific <see cref="IEntity"/> type.
    /// </summary>
    /// <typeparam name="T">The concrete entity type this behavior is associated with.</typeparam>
    /// <remarks>
    /// This method is automatically invoked by <see cref="IActivatable.Activate"/> 
    /// when the behavior is registered on an entity of type <typeparamref name="T"/>.
    /// </remarks>
    public interface IEntityActive<in T> : IEntityActive where T : IEntity
    {
        /// <summary>
        /// Called when the typed entity is enabled.
        /// </summary>
        /// <param name="entity">The entity instance of type <typeparamref name="T"/>.</param>
        void OnActive(T entity);

        void IEntityActive.OnActive(IEntity entity) => this.OnActive((T) entity);
    }

    /// <summary>
    /// Provides a high-performance, unsafe version of <see cref="IEntityActive"/> by using low-level casting
    /// to handle enable-time logic on a specific <see cref="IEntity"/> type.
    /// </summary>
    /// <typeparam name="T">The concrete entity type this behavior is associated with.</typeparam>
    /// <remarks>
    /// This method is automatically invoked by <see cref="IActivatable.Activate"/> using low-level casting via <c>UnsafeUtility.As</c>.
    /// </remarks>
    public interface IEntityActiveUnsafe<in T> : IEntityActive where T : IEntity
    {
        /// <summary>
        /// Called when the typed entity is enabled.
        /// </summary>
        /// <param name="entity">The entity instance of type <typeparamref name="T"/>.</param>
        void OnActive(T entity);

        void IEntityActive.OnActive(IEntity entity) => this.OnActive(UnsafeUtility.As<IEntity, T>(ref entity));
    }
}