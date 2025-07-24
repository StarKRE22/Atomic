using Unity.Collections.LowLevel.Unsafe;

namespace Atomic.Entities
{
    /// <summary>
    /// Defines a behavior that executes logic during the regular update cycle of an <see cref="IEntity"/>.
    /// </summary>
    /// <remarks>
    /// This method is automatically called by <see cref="IEntity.OnUpdate"/> once per frame during the main game loop.
    /// </remarks>
    public interface IEntityUpdate : IEntityBehaviour
    {
        /// <summary>
        /// Called during the main update phase of the frame.
        /// </summary>
        /// <param name="entity">The entity being updated.</param>
        /// <param name="deltaTime">Elapsed time since the last frame.</param>
        void OnUpdate(IEntity entity, float deltaTime);
    }

    /// <summary>
    /// Provides a strongly-typed version of <see cref="IEntityUpdate"/> for handling update logic
    /// on a specific <see cref="IEntity"/> type.
    /// </summary>
    /// <typeparam name="T">The concrete entity type this behavior is associated with.</typeparam>
    /// <remarks>
    /// This method is automatically invoked by <see cref="IEntity.OnUpdate"/> 
    /// when the behavior is registered on an entity of type <typeparamref name="T"/>.
    /// </remarks>
    public interface IEntityUpdate<in T> : IEntityUpdate where T : IEntity
    {
        /// <summary>
        /// Called during the main update phase of the frame.
        /// </summary>
        /// <param name="entity">The strongly-typed entity being updated.</param>
        /// <param name="deltaTime">Elapsed time since the last frame.</param>
        void OnUpdate(T entity, float deltaTime);

        void IEntityUpdate.OnUpdate(IEntity entity, float deltaTime) => this.OnUpdate((T) entity, deltaTime);
    }

    /// <summary>
    /// Provides a high-performance, unsafe version of <see cref="IEntityUpdate"/> by using low-level casting
    /// to handle update logic for a specific <see cref="IEntity"/> type.
    /// </summary>
    /// <typeparam name="T">The concrete entity type this behavior is associated with.</typeparam>
    /// <remarks>
    /// This method is automatically invoked by <see cref="IEntity.OnUpdate"/> using low-level casting via <c>UnsafeUtility.As</c>.
    /// </remarks>
    public interface IEntityUpdateUnsafe<in T> : IEntityUpdate where T : IEntity
    {
        /// <summary>
        /// Called during the main update phase of the frame.
        /// </summary>
        /// <param name="entity">The strongly-typed entity being updated.</param>
        /// <param name="deltaTime">Elapsed time since the last frame.</param>
        void OnUpdate(T entity, float deltaTime);

        void IEntityUpdate.OnUpdate(IEntity entity, float deltaTime) =>
            this.OnUpdate(UnsafeUtility.As<IEntity, T>(ref entity), deltaTime);
    }
}