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
    public interface IEntityDespawn : IEntityBehaviour
    {
        /// <summary>
        /// Called when the entity is despawned.
        /// </summary>
        /// <param name="entity">The entity being despawned.</param>
        void Despawn(IEntity entity);
    }

    /// <summary>
    /// Provides a strongly-typed version of <see cref="IEntityDespawn"/> for handling despawn-time logic 
    /// specific to a concrete <see cref="IEntity"/> type.
    /// </summary>
    /// <typeparam name="T">The specific type of entity this behavior targets.</typeparam>
    /// <remarks>
    /// This method is automatically invoked by <see cref="IEntity.Despawn"/> 
    /// when the behavior is registered on an entity of type <typeparamref name="T"/>.
    /// </remarks>
    public interface IEntityDespawn<in T> : IEntityDespawn where T : IEntity
    {
        /// <summary>
        /// Called when the typed entity is despawned.
        /// </summary>
        /// <param name="entity">The entity instance of type <typeparamref name="T"/>.</param>
        void Despawn(T entity);

        void IEntityDespawn.Despawn(IEntity entity) => this.Despawn((T) entity);
    }

    /// <summary>
    /// Provides a high-performance, unsafe version of <see cref="IEntityDespawn"/> 
    /// that uses low-level casting for optimized despawn-time logic.
    /// </summary>
    /// <typeparam name="T">The specific type of entity this behavior targets.</typeparam>
    /// <remarks>
    /// This method is automatically invoked by <see cref="IEntity.Despawn"/> 
    /// using low-level casting via <c>UnsafeUtility.As</c>.
    /// </remarks>
    public interface IEntityDespawnUnsafe<in T> : IEntityDespawn where T : IEntity
    {
        /// <summary>
        /// Called when the typed entity is despawned.
        /// </summary>
        /// <param name="entity">The entity instance of type <typeparamref name="T"/>.</param>
        void Despawn(T entity);

        void IEntityDespawn.Despawn(IEntity entity) => this.Despawn(UnsafeUtility.As<IEntity, T>(ref entity));
    }
}