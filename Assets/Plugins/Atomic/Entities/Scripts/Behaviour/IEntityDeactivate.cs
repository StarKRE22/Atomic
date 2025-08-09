using Unity.Collections.LowLevel.Unsafe;

namespace Atomic.Entities
{
    /// <summary>
    /// Defines a behavior that executes logic when an <see cref="IEntity"/> is disabled.
    /// </summary>
    /// <remarks>
    /// This method is automatically called by <see cref="IActivatable.Deactivate"/> when the entity exits the active state,
    /// such as during pause, unloading, or before being despawned.
    /// </remarks>
    public interface IEntityDeactivate : IEntityBehaviour
    {
        /// <summary>
        /// Called when the entity is disabled.
        /// </summary>
        /// <param name="entity">The entity being disabled.</param>
        void OnDeactivate(IEntity entity);
    }

    /// <summary>
    /// Provides a strongly-typed version of <see cref="IEntityDeactivate"/> for handling disable-time logic
    /// on a specific <see cref="IEntity"/> type.
    /// </summary>
    /// <typeparam name="T">The concrete entity type this behavior is associated with.</typeparam>
    /// <remarks>
    /// This method is automatically invoked by <see cref="IActivatable.Deactivate"/> 
    /// when the behavior is registered on an entity of type <typeparamref name="T"/>.
    /// </remarks>
    public interface IEntityDeactivate<in T> : IEntityDeactivate where T : IEntity
    {
        /// <summary>
        /// Called when the typed entity is disabled.
        /// </summary>
        /// <param name="entity">The entity instance of type <typeparamref name="T"/>.</param>
        void OnDeactivate(T entity);

        void IEntityDeactivate.OnDeactivate(IEntity entity) => this.OnDeactivate((T) entity);
    }

    /// <summary>
    /// Provides a high-performance, unsafe version of <see cref="IEntityDeactivate"/> that uses low-level casting
    /// to handle disable-time logic on a specific <see cref="IEntity"/> type.
    /// </summary>
    /// <typeparam name="T">The concrete entity type this behavior is associated with.</typeparam>
    /// <remarks>
    /// This method is automatically invoked by <see cref="IActivatable.Deactivate"/> using low-level casting via <c>UnsafeUtility.As</c>.
    /// </remarks>
    public interface IEntityDeactivateUnsafe<in T> : IEntityDeactivate where T : IEntity
    {
        /// <summary>
        /// Called when the typed entity is disabled.
        /// </summary>
        /// <param name="entity">The entity instance of type <typeparamref name="T"/>.</param>
        void OnDeactivate(T entity);

        void IEntityDeactivate.OnDeactivate(IEntity entity) => this.OnDeactivate(UnsafeUtility.As<IEntity, T>(ref entity));
    }
}