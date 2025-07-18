using Unity.Collections.LowLevel.Unsafe;

namespace Atomic.Entities
{
    /// <summary>
    /// Defines a behavior that supports logic during the regular update cycle of an <see cref="IEntity"/>.
    /// Called once per frame in the main game loop.
    /// </summary>
    public interface IUpdate : IBehaviour
    {
        /// <summary>
        /// Called during the main update phase.
        /// </summary>
        /// <param name="entity">The entity being updated.</param>
        /// <param name="deltaTime">Elapsed time since the last frame.</param>
        void OnUpdate(IEntity entity, float deltaTime);
    }

    /// <summary>
    /// Generic version of <see cref="IUpdate"/> providing strongly-typed update logic for a specific <see cref="IEntity"/> type.
    /// </summary>
    /// <typeparam name="T">The specific type of entity this behavior applies to.</typeparam>
    public interface IUpdate<in T> : IUpdate where T : IEntity
    {
        /// <summary>
        /// Called during the main update phase.
        /// </summary>
        /// <param name="entity">The strongly-typed entity being updated.</param>
        /// <param name="deltaTime">Elapsed time since the last frame.</param>
        void OnUpdate(T entity, float deltaTime);

        void IUpdate.OnUpdate(IEntity entity, float deltaTime) => this.OnUpdate((T) entity, deltaTime);
    }

    /// <summary>
    /// Generic version of <see cref="IUnsafeUpdate"/> providing unsafe, strongly-typed update logic for a specific <see cref="IEntity"/> type.
    /// </summary>
    /// <typeparam name="T">The specific type of entity this behavior applies to.</typeparam>
    public interface IUnsafeUpdate<in T> : IUpdate where T : IEntity
    {
        /// <inheritdoc/>
        void IUpdate.OnUpdate(IEntity entity, float deltaTime) =>
            this.OnUpdate(UnsafeUtility.As<IEntity, T>(ref entity), deltaTime);

        /// <summary>
        /// Called during the main update phase for the strongly-typed entity.
        /// </summary>
        /// <param name="entity">The strongly-typed entity being updated.</param>
        /// <param name="deltaTime">Elapsed time since the last frame.</param>
        void OnUpdate(T entity, float deltaTime);
    }
}