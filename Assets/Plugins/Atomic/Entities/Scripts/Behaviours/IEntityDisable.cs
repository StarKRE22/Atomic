using System;

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
    /// <typeparam name="E">The concrete entity type this behavior is associated with.</typeparam>
    /// <remarks>
    /// This method is automatically invoked by <see cref="IEnable.Disable"/> 
    /// when the behavior is registered on an entity of type <typeparamref name="E"/>.
    /// </remarks>
    public interface IEntityDisable<in E> : IEntityDisable where E : IEntity
    {
        /// <summary>
        /// Called when the typed entity is disabled.
        /// </summary>
        /// <param name="entity">The entity instance of type <typeparamref name="E"/>.</param>
        void Disable(E entity);

        void IEntityDisable.Disable(IEntity entity)
        {
            if (entity is not E e)
                throw new InvalidCastException(
                    $"[IEntityDisable<{typeof(E).Name}>] Invalid entity type for {this.GetType().Name}.\n" +
                    $"Expected: {typeof(E).FullName}\n" +
                    $"Received: {entity?.GetType().FullName ?? "null"}\n" +
                    "Please make sure the correct IEntityDisable is used for this entity type."
                );

            this.Disable(e);
        }
    }
}