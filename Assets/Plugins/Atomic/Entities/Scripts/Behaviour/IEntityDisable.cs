using Unity.Collections.LowLevel.Unsafe;

namespace Atomic.Entities
{
    /// <summary>
    /// Defines a behavior that executes logic when an <see cref="IEntity"/> is disabled.
    /// </summary>
    /// <remarks>
    /// This method is automatically called by <see cref="IEntity.Disable"/> when the entity exits the active state,
    /// such as during pause, unloading, or before being despawned.
    /// </remarks>
    public interface IEntityDisable : IEntityBehaviour
    {
        /// <summary>
        /// Called when the entity is disabled.
        /// </summary>
        /// <param name="entity">The entity being disabled.</param>
        void Disable(IEntity entity);
    }

    /// <summary>
    /// Provides a strongly-typed version of <see cref="IEntityDisable"/> for handling disable-time logic
    /// on a specific <see cref="IEntity"/> type.
    /// </summary>
    /// <typeparam name="T">The concrete entity type this behavior is associated with.</typeparam>
    /// <remarks>
    /// This method is automatically invoked by <see cref="IEntity.Disable"/> 
    /// when the behavior is registered on an entity of type <typeparamref name="T"/>.
    /// </remarks>
    public interface IEntityDisable<in T> : IEntityDisable where T : IEntity
    {
        /// <summary>
        /// Called when the typed entity is disabled.
        /// </summary>
        /// <param name="entity">The entity instance of type <typeparamref name="T"/>.</param>
        void Disable(T entity);

        void IEntityDisable.Disable(IEntity entity) => this.Disable((T) entity);
    }

    /// <summary>
    /// Provides a high-performance, unsafe version of <see cref="IEntityDisable"/> that uses low-level casting
    /// to handle disable-time logic on a specific <see cref="IEntity"/> type.
    /// </summary>
    /// <typeparam name="T">The concrete entity type this behavior is associated with.</typeparam>
    /// <remarks>
    /// This method is automatically invoked by <see cref="IEntity.Disable"/> using low-level casting via <c>UnsafeUtility.As</c>.
    /// </remarks>
    public interface IEntityDisableUnsafe<in T> : IEntityDisable where T : IEntity
    {
        /// <summary>
        /// Called when the typed entity is disabled.
        /// </summary>
        /// <param name="entity">The entity instance of type <typeparamref name="T"/>.</param>
        void Disable(T entity);

        void IEntityDisable.Disable(IEntity entity) => this.Disable(UnsafeUtility.As<IEntity, T>(ref entity));
    }
}