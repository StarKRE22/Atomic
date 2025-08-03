using Unity.Collections.LowLevel.Unsafe;

namespace Atomic.Entities
{
    /// <summary>
    /// Defines a behavior that executes cleanup or deinitialization logic 
    /// when an <see cref="IEntity"/> is despawned from the world or runtime context.
    /// </summary>
    /// <remarks>
    /// Called automatically by <see cref="IEntity.Despawn"/> when the entity is removed or deactivated.
    /// </remarks>
    public interface IEntityDespawned : IEntityBehaviour
    {
        /// <summary>
        /// Called when the entity is despawned.
        /// </summary>
        /// <param name="entity">The entity being despawned.</param>
        void OnDespawn(IEntity entity);
    }

    /// <summary>
    /// Provides a strongly-typed version of <see cref="IEntityDespawned"/> for handling despawn-time logic 
    /// specific to a concrete <see cref="IEntity"/> type.
    /// </summary>
    /// <typeparam name="T">The specific type of entity this behavior targets.</typeparam>
    /// <remarks>
    /// This method is automatically invoked by <see cref="IEntity.Despawn"/> 
    /// when the behavior is registered on an entity of type <typeparamref name="T"/>.
    /// </remarks>
    public interface IEntityDespawned<in T> : IEntityDespawned where T : IEntity
    {
        /// <summary>
        /// Called when the typed entity is despawned.
        /// </summary>
        /// <param name="entity">The entity instance of type <typeparamref name="T"/>.</param>
        void OnDespawn(T entity);

        void IEntityDespawned.OnDespawn(IEntity entity) => this.OnDespawn((T) entity);
    }

    /// <summary>
    /// Provides a high-performance, unsafe version of <see cref="IEntityDespawned"/> 
    /// that uses low-level casting for optimized despawn-time logic.
    /// </summary>
    /// <typeparam name="T">The specific type of entity this behavior targets.</typeparam>
    /// <remarks>
    /// This method is automatically invoked by <see cref="IEntity.Despawn"/> 
    /// using low-level casting via <c>UnsafeUtility.As</c>.
    /// </remarks>
    public interface IEntityDespawnedUnsafe<in T> : IEntityDespawned where T : IEntity
    {
        /// <summary>
        /// Called when the typed entity is despawned.
        /// </summary>
        /// <param name="entity">The entity instance of type <typeparamref name="T"/>.</param>
        void OnDespawn(T entity);

        void IEntityDespawned.OnDespawn(IEntity entity) => this.OnDespawn(UnsafeUtility.As<IEntity, T>(ref entity));
    }
}