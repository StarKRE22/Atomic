using Unity.Collections.LowLevel.Unsafe;

namespace Atomic.Entities
{
    /// <summary>
    /// Defines a behavior that executes custom logic when an <see cref="IEntity"/> is spawned.
    /// </summary>
    /// <remarks>
    /// Called automatically by <see cref="IEntity.Spawn"/> when the entity enters the world or runtime context.
    /// </remarks>
    public interface IEntitySpawn : IEntityBehaviour
    {
        /// <summary>
        /// Called when the entity is spawned.
        /// </summary>
        /// <param name="entity">The entity being spawned.</param>
        void OnSpawn(IEntity entity);
    }

    /// <summary>
    /// Provides a strongly-typed version of <see cref="IEntitySpawn"/> for handling spawn-time logic 
    /// on a specific <see cref="IEntity"/> type.
    /// </summary>
    /// <typeparam name="T">The specific entity type this behavior is associated with.</typeparam>
    /// <remarks>
    /// This method is automatically invoked by <see cref="IEntity.Spawn"/> 
    /// when the behavior is registered on an entity of type <typeparamref name="T"/>.
    /// </remarks>
    public interface IEntitySpawn<in T> : IEntitySpawn where T : IEntity
    {
        /// <summary>
        /// Called when the entity of type <typeparamref name="T"/> is spawned.
        /// </summary>
        /// <param name="entity">The typed entity being spawned.</param>
        void OnSpawn(T entity);

        void IEntitySpawn.OnSpawn(IEntity entity) => this.OnSpawn((T) entity);
    }

    /// <summary>
    /// Provides an optimized version of <see cref="IEntitySpawn"/> that uses unsafe casting for better performance
    /// in high-frequency spawn operations.
    /// </summary>
    /// <typeparam name="T">The specific entity type this behavior is associated with.</typeparam>
    /// <remarks>
    /// This method is automatically invoked by <see cref="IEntity.Spawn"/> 
    /// using low-level casting via <c>UnsafeUtility.As</c>.
    /// </remarks>
    public interface IEntitySpawnUnsafe<in T> : IEntitySpawn where T : IEntity
    {
        /// <summary>
        /// Called when the entity of type <typeparamref name="T"/> is spawned.
        /// </summary>
        /// <param name="entity">The typed entity being spawned.</param>
        void OnSpawn(T entity);

        void IEntitySpawn.OnSpawn(IEntity entity) => this.OnSpawn(UnsafeUtility.As<IEntity, T>(ref entity));
    }
}