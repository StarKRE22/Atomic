using Unity.Collections.LowLevel.Unsafe;

namespace Atomic.Entities
{
    /// <summary>
    /// Defines a unit of logic that can be attached to an <see cref="IEntity"/>.
    /// Behaviours encapsulate modular functionality and are used to compose entity behavior at runtime.
    /// </summary>
    public interface IEntityBehaviour
    {
    }

    #region Spawn

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
        void Spawn(IEntity entity);
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
        void Spawn(T entity);

        void IEntitySpawn.Spawn(IEntity entity) => this.Spawn((T) entity);
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
        void Spawn(T entity);

        void IEntitySpawn.Spawn(IEntity entity) => this.Spawn(UnsafeUtility.As<IEntity, T>(ref entity));
    }

    #endregion

    #region Despawn

    /// <summary>
    /// Defines a behavior that executes cleanup or deinitialization logic 
    /// when an <see cref="IEntity"/> is despawned from the world or runtime context.
    /// </summary>
    /// <remarks>
    /// Called automatically by <see cref="IEntity.Despawn"/> when the entity is removed or deactivated.
    /// </remarks>
    public interface IEntityDespawn
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

    #endregion

    #region Enable

    /// <summary>
    /// Defines a behavior that executes logic when an <see cref="IEntity"/> is enabled.
    /// </summary>
    /// <remarks>
    /// This method is automatically called by <see cref="IEntity.Enable"/> when the entity enters the active state,
    /// such as after spawning or resuming from a disabled state.
    /// </remarks>
    public interface IEntityEnable : IEntityBehaviour
    {
        /// <summary>
        /// Called when the entity is enabled.
        /// </summary>
        /// <param name="entity">The entity being enabled.</param>
        void Enable(IEntity entity);
    }

    /// <summary>
    /// Provides a strongly-typed version of <see cref="IEntityEnable"/> for handling enable-time logic
    /// for a specific <see cref="IEntity"/> type.
    /// </summary>
    /// <typeparam name="T">The concrete entity type this behavior is associated with.</typeparam>
    /// <remarks>
    /// This method is automatically invoked by <see cref="IEntity.Enable"/> 
    /// when the behavior is registered on an entity of type <typeparamref name="T"/>.
    /// </remarks>
    public interface IEntityEnable<in T> : IEntityEnable where T : IEntity
    {
        /// <summary>
        /// Called when the typed entity is enabled.
        /// </summary>
        /// <param name="entity">The entity instance of type <typeparamref name="T"/>.</param>
        void Enable(T entity);

        void IEntityEnable.Enable(IEntity entity) => this.Enable((T) entity);
    }

    /// <summary>
    /// Provides a high-performance, unsafe version of <see cref="IEntityEnable"/> by using low-level casting
    /// to handle enable-time logic on a specific <see cref="IEntity"/> type.
    /// </summary>
    /// <typeparam name="T">The concrete entity type this behavior is associated with.</typeparam>
    /// <remarks>
    /// This method is automatically invoked by <see cref="IEntity.Enable"/> using low-level casting via <c>UnsafeUtility.As</c>.
    /// </remarks>
    public interface IEntityEnableUnsafe<in T> : IEntityEnable where T : IEntity
    {
        /// <summary>
        /// Called when the typed entity is enabled.
        /// </summary>
        /// <param name="entity">The entity instance of type <typeparamref name="T"/>.</param>
        void Enable(T entity);

        void IEntityEnable.Enable(IEntity entity) => this.Enable(UnsafeUtility.As<IEntity, T>(ref entity));
    }

    #endregion

    #region Disable

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

    #endregion

    #region Update

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

    #endregion

    #region FixedUpdate

    /// <summary>
    /// Defines a behavior that executes logic during the fixed update cycle of an <see cref="IEntity"/>.
    /// </summary>
    /// <remarks>
    /// This method is automatically called by <see cref="IEntity.OnFixedUpdate"/> at a consistent time interval,
    /// typically aligned with the physics simulation step.
    /// </remarks>
    public interface IEntityFixedUpdate : IEntityBehaviour
    {
        /// <summary>
        /// Called during the fixed update phase.
        /// </summary>
        /// <param name="entity">The entity being updated.</param>
        /// <param name="deltaTime">The fixed time step since the last update.</param>
        void OnFixedUpdate(IEntity entity, float deltaTime);
    }

    /// <summary>
    /// Provides a strongly-typed version of <see cref="IEntityFixedUpdate"/> for handling fixed update logic
    /// on a specific <see cref="IEntity"/> type.
    /// </summary>
    /// <typeparam name="T">The concrete entity type this behavior is associated with.</typeparam>
    /// <remarks>
    /// This method is automatically invoked by <see cref="IEntity.OnFixedUpdate"/> 
    /// when the behavior is registered on an entity of type <typeparamref name="T"/>.
    /// </remarks>
    public interface IEntityFixedUpdate<in T> : IEntityFixedUpdate where T : IEntity
    {
        /// <summary>
        /// Called during the fixed update phase for a strongly-typed entity.
        /// </summary>
        /// <param name="entity">The entity instance of type <typeparamref name="T"/>.</param>
        /// <param name="deltaTime">The fixed time step since the last update.</param>
        void OnFixedUpdate(T entity, float deltaTime);

        void IEntityFixedUpdate.OnFixedUpdate(IEntity entity, float deltaTime) =>
            this.OnFixedUpdate((T) entity, deltaTime);
    }

    /// <summary>
    /// Provides a high-performance, unsafe version of <see cref="IEntityFixedUpdate"/> that uses low-level casting
    /// to handle fixed update logic for a specific <see cref="IEntity"/> type.
    /// </summary>
    /// <typeparam name="T">The concrete entity type this behavior is associated with.</typeparam>
    /// <remarks>
    /// This method is automatically invoked by <see cref="IEntity.OnFixedUpdate"/> using low-level casting via <c>UnsafeUtility.As</c>.
    /// </remarks>
    public interface IEntityFixedUpdateUnsafe<in T> : IEntityFixedUpdate where T : IEntity
    {
        /// <summary>
        /// Called during the fixed update phase for a strongly-typed entity.
        /// </summary>
        /// <param name="entity">The entity instance of type <typeparamref name="T"/>.</param>
        /// <param name="deltaTime">The fixed time step since the last update.</param>
        void OnFixedUpdate(T entity, float deltaTime);

        void IEntityFixedUpdate.OnFixedUpdate(IEntity entity, float deltaTime) =>
            this.OnFixedUpdate(UnsafeUtility.As<IEntity, T>(ref entity), deltaTime);
    }

    #endregion

    #region LateUpdate

    /// <summary>
    /// Defines a behavior that executes logic during the late update phase of an <see cref="IEntity"/>.
    /// </summary>
    /// <remarks>
    /// This method is automatically called by <see cref="IEntity.OnLateUpdate"/> after all standard updates,
    /// and is typically used for post-processing, transform synchronization, or order-sensitive updates.
    /// </remarks>
    public interface IEntityLateUpdate : IEntityBehaviour
    {
        /// <summary>
        /// Called during the late update phase.
        /// </summary>
        /// <param name="entity">The entity being updated.</param>
        /// <param name="deltaTime">Elapsed time since the last frame.</param>
        void OnLateUpdate(IEntity entity, float deltaTime);
    }

    /// <summary>
    /// Provides a strongly-typed version of <see cref="IEntityLateUpdate"/> for handling late update logic
    /// on a specific <see cref="IEntity"/> type.
    /// </summary>
    /// <typeparam name="T">The concrete entity type this behavior is associated with.</typeparam>
    /// <remarks>
    /// This method is automatically invoked by <see cref="IEntity.OnLateUpdate"/> 
    /// when the behavior is registered on an entity of type <typeparamref name="T"/>.
    /// </remarks>
    public interface IEntityLateUpdate<in T> : IEntityLateUpdate where T : IEntity
    {
        /// <summary>
        /// Called during the late update phase for a strongly-typed entity.
        /// </summary>
        /// <param name="entity">The entity instance of type <typeparamref name="T"/>.</param>
        /// <param name="deltaTime">Elapsed time since the last frame.</param>
        void OnLateUpdate(T entity, float deltaTime);

        void IEntityLateUpdate.OnLateUpdate(IEntity entity, float deltaTime) =>
            this.OnLateUpdate((T) entity, deltaTime);
    }

    /// <summary>
    /// Provides a high-performance, unsafe version of <see cref="IEntityLateUpdate"/> that uses low-level casting
    /// to handle late update logic for a specific <see cref="IEntity"/> type.
    /// </summary>
    /// <typeparam name="T">The concrete entity type this behavior is associated with.</typeparam>
    /// <remarks>
    /// This method is automatically invoked by <see cref="IEntity.OnLateUpdate"/> using low-level casting via <c>UnsafeUtility.As</c>.
    /// </remarks>
    public interface IEntityLateUpdateUnsafe<in T> : IEntityLateUpdate where T : IEntity
    {
        /// <summary>
        /// Called during the late update phase for a strongly-typed entity.
        /// </summary>
        /// <param name="entity">The entity instance of type <typeparamref name="T"/>.</param>
        /// <param name="deltaTime">Elapsed time since the last frame.</param>
        void OnLateUpdate(T entity, float deltaTime);

        void IEntityLateUpdate.OnLateUpdate(IEntity entity, float deltaTime) =>
            this.OnLateUpdate(UnsafeUtility.As<IEntity, T>(ref entity), deltaTime);
    }

    #endregion

    #region Gizmos

#if UNITY_5_3_OR_NEWER

    /// <summary>
    /// Defines a behavior that allows drawing gizmos for an <see cref="IEntity"/> during the editor or debug rendering phase.
    /// </summary>
    /// <remarks>
    /// This method is automatically called by <c>SceneEntity.OnDrawGizmos()</c> or <c>SceneEntity.OnDrawGizmosSelected()</c>
    /// in the Unity Editor, allowing you to visualize entity data in the scene view.
    /// </remarks>
    public interface IEntityGizmos : IEntityBehaviour
    {
        /// <summary>
        /// Called to draw gizmos for the specified entity.
        /// </summary>
        /// <param name="entity">The entity for which gizmos should be drawn.</param>
        void OnGizmosDraw(IEntity entity);
    }

    /// <summary>
    /// Provides a strongly-typed version of <see cref="IEntityGizmos"/> for drawing gizmos
    /// related to a specific <see cref="IEntity"/> type.
    /// </summary>
    /// <typeparam name="T">The concrete entity type this behavior is associated with.</typeparam>
    /// <remarks>
    /// This method is automatically invoked by <c>SceneEntity.OnDrawGizmos()</c> or <c>SceneEntity.OnDrawGizmosSelected()</c>
    /// if the entity implements this behavior and is currently visible in the editor.
    /// </remarks>
    public interface IEntityGizmos<in T> : IEntityGizmos where T : IEntity
    {
        /// <summary>
        /// Called to draw gizmos for the specified strongly-typed entity.
        /// </summary>
        /// <param name="entity">The entity instance of type <typeparamref name="T"/>.</param>
        void OnGizmosDraw(T entity);

        void IEntityGizmos.OnGizmosDraw(IEntity entity) => this.OnGizmosDraw((T)entity);
    }


#endif

    #endregion
}