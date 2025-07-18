using Unity.Collections.LowLevel.Unsafe;

namespace Atomic.Entities
{
    /// <summary>
    /// Defines a behavior that is updated at a fixed time interval.
    /// </summary>
    public interface IFixedUpdate : IBehaviour
    {
        /// <summary>
        /// Called every fixed update tick.
        /// </summary>
        /// <param name="entity">The entity this behavior is attached to.</param>
        /// <param name="deltaTime">The fixed delta time step.</param>
        void OnFixedUpdate(IEntity entity, float deltaTime);
    }
    
    /// <summary>
    /// Generic version of <see cref="IFixedUpdate"/> that provides strongly-typed fixed update logic
    /// for a specific <see cref="IEntity"/> type.
    /// </summary>
    /// <typeparam name="T">The specific type of entity this behavior applies to.</typeparam>
    public interface IFixedUpdate<in T> : IFixedUpdate where T : IEntity
    {
        /// <inheritdoc/>
        void IFixedUpdate.OnFixedUpdate(IEntity entity, float deltaTime) => this.OnFixedUpdate((T) entity, deltaTime);

        /// <summary>
        /// Called every fixed update tick with a strongly-typed entity.
        /// </summary>
        /// <param name="entity">The entity instance of type <typeparamref name="T"/>.</param>
        /// <param name="deltaTime">The fixed delta time step since the last update.</param>
        void OnFixedUpdate(T entity, float deltaTime);
    }

    /// <summary>
    /// Generic version of <see cref="IFixedUpdate"/> that provides strongly-typed fixed update logic
    /// for a specific <see cref="IEntity"/> type.
    /// </summary>
    /// <typeparam name="T">The specific type of entity this behavior applies to.</typeparam>
    public interface IUnsafeFixedUpdate<in T> : IFixedUpdate where T : IEntity
    {
        /// <inheritdoc />
        void IFixedUpdate.OnFixedUpdate(IEntity entity, float deltaTime) => 
            this.OnFixedUpdate(UnsafeUtility.As<IEntity, T>(ref entity), deltaTime);

        /// <summary>
        /// Called every fixed update tick with a strongly-typed entity.
        /// </summary>
        /// <param name="entity">The entity instance.</param>
        /// <param name="deltaTime">The fixed delta time step.</param>
        void OnFixedUpdate(T entity, float deltaTime);
    }
}