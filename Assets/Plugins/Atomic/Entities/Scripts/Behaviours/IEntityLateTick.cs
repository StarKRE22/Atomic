using System;

namespace Atomic.Entities
{
    /// <summary>
    /// Defines a behavior that executes logic during the late update phase of an <see cref="IEntity"/>.
    /// </summary>
    /// <remarks>
    /// This method is automatically called by <see cref="ITickSource.LateTick"/> after all standard updates,
    /// and is typically used for post-processing, transform synchronization, or order-sensitive updates.
    /// </remarks>
    public interface IEntityLateTick : IEntityBehaviour
    {
        /// <summary>
        /// Called during the late update phase.
        /// </summary>
        /// <param name="entity">The entity being updated.</param>
        /// <param name="deltaTime">Elapsed time since the last frame.</param>
        void LateTick(IEntity entity, float deltaTime);
    }

    /// <summary>
    /// Provides a strongly-typed version of <see cref="IEntityLateTick"/> for handling late update logic
    /// on a specific <see cref="IEntity"/> type.
    /// </summary>
    /// <typeparam name="E">The concrete entity type this behavior is associated with.</typeparam>
    /// <remarks>
    /// This method is automatically invoked by <see cref="ITickSource.LateTick"/> 
    /// when the behavior is registered on an entity of type <typeparamref name="E"/>.
    /// </remarks>
    public interface IEntityLateTick<in E> : IEntityLateTick where E : IEntity
    {
        /// <summary>
        /// Called during the late update phase for a strongly-typed entity.
        /// </summary>
        /// <param name="entity">The entity instance of type <typeparamref name="E"/>.</param>
        /// <param name="deltaTime">Elapsed time since the last frame.</param>
        void LateTick(E entity, float deltaTime);

        void IEntityLateTick.LateTick(IEntity entity, float deltaTime)
        {
            if (entity is not E e)
                throw new InvalidCastException(
                    $"[IEntityLateTick<{typeof(E).Name}>] Invalid entity type for {this.GetType().Name}.\n" +
                    $"Expected: {typeof(E).FullName}\n" +
                    $"Received: {entity?.GetType().FullName ?? "null"}\n" +
                    "Please make sure the correct IEntityLateTick is used for this entity type."
                );

            this.LateTick(e, deltaTime);
        }
    }
}