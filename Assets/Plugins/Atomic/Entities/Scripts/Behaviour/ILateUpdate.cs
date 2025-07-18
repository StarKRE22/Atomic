using Unity.Collections.LowLevel.Unsafe;

namespace Atomic.Entities
{
    /// <summary>
    /// Defines a behavior that executes logic during the late update phase of an <see cref="IEntity"/>.
    /// Called after all standard updates, typically used for post-processing logic or transform synchronization.
    /// </summary>
    public interface ILateUpdate : IBehaviour
    {
        /// <summary>
        /// Called during the late update phase.
        /// </summary>
        /// <param name="entity">The entity being updated.</param>
        /// <param name="deltaTime">Elapsed time since the last frame.</param>
        void OnLateUpdate(IEntity entity, float deltaTime);
    }

    /// <summary>
    /// Generic version of <see cref="ILateUpdate"/> providing strongly-typed late update logic for a specific <see cref="IEntity"/> type.
    /// </summary>
    /// <typeparam name="T">The specific type of entity this behavior applies to.</typeparam>
    public interface ILateUpdate<in T> : ILateUpdate where T : IEntity
    {
        /// <summary>
        /// Called during the late update phase.
        /// </summary>
        /// <param name="entity">The strongly-typed entity being updated.</param>
        /// <param name="deltaTime">Elapsed time since the last frame.</param>
        void OnLateUpdate(T entity, float deltaTime);

        void ILateUpdate.OnLateUpdate(IEntity entity, float deltaTime) => this.OnLateUpdate((T) entity, deltaTime);
    }
    
    /// <summary>
    /// Generic version of <see cref="ILateUpdate"/> that uses unsafe casting for fast strongly-typed late updates.
    /// </summary>
    /// <typeparam name="T">The specific type of <see cref="IEntity"/> this behavior operates on.</typeparam>
    public interface IUnsafeLateUpdate<in T> : ILateUpdate where T : IEntity
    {
        /// <inheritdoc/>
        void ILateUpdate.OnLateUpdate(IEntity entity, float deltaTime) =>
            this.OnLateUpdate(UnsafeUtility.As<IEntity, T>(ref entity), deltaTime);
        
        /// <summary>
        /// Called during the LateUpdate phase for the strongly-typed entity.
        /// </summary>
        /// <param name="entity">The strongly-typed entity instance.</param>
        /// <param name="deltaTime">The delta time since the last update.</param>
        void OnLateUpdate(T entity, float deltaTime);
    }
}