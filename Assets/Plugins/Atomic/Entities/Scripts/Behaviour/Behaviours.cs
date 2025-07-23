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

    #region Init

    /// <summary>
    /// Defines a behavior that supports initialization logic for an <see cref="IEntity"/>.
    /// </summary>
    public interface IEntityInit : IEntityBehaviour
    {
        /// <summary>
        /// Initializes the behavior with the specified <see cref="IEntity"/>.
        /// </summary>
        /// <param name="entity">The entity to initialize this behavior with.</param>
        void Init(IEntity entity);
    }

    /// <summary>
    /// Generic version of <see cref="IEntityInit"/> that provides strongly-typed initialization for a specific <see cref="IEntity"/> type.
    /// </summary>
    /// <typeparam name="T">The specific type of entity this behavior works with.</typeparam>
    public interface IEntityInit<in T> : IEntityInit where T : IEntity
    {
        /// <summary>
        /// Initializes the behavior with a strongly-typed entity context.
        /// </summary>
        /// <param name="entity">The entity of type <typeparamref name="T"/>.</param>
        void Init(T entity);

        void IEntityInit.Init(IEntity entity) => this.Init((T) entity);
    }

    /// <summary>
    /// Unsafe generic version of <see cref="IEntityInit"/> that uses low-level casting to initialize a strongly-typed <see cref="IEntity"/>.
    /// </summary>
    /// <typeparam name="T">The specific type of entity this behavior works with.</typeparam>
    public interface IEntityInitUnsafe<in T> : IEntityInit where T : IEntity
    {
        /// <summary>
        /// Initializes the behavior with a strongly-typed entity context.
        /// </summary>
        /// <param name="entity">The entity of type <typeparamref name="T"/>.</param>
        void Init(T entity);

        void IEntityInit.Init(IEntity entity) => this.Init(UnsafeUtility.As<IEntity, T>(ref entity));
    }

    #endregion

    #region Enable

    /// <summary>
    /// Defines a behavior that supports an enable lifecycle event for an <see cref="IEntity"/>.
    /// This is typically called when the entity is activated or enters the active state.
    /// </summary>
    public interface IEntityEnable : IEntityBehaviour
    {
        /// <summary>
        /// Called when the entity is enabled.
        /// </summary>
        /// <param name="entity">The entity being enabled.</param>
        void Enable(IEntity entity);
    }

    /// <summary>
    /// Generic version of <see cref="IEntityEnable"/> that provides strongly-typed enable logic for a specific <see cref="IEntity"/> type.
    /// </summary>
    /// <typeparam name="T">The specific type of entity this behavior applies to.</typeparam>
    public interface IEntityEnable<in T> : IEntityEnable where T : IEntity
    {
        void IEntityEnable.Enable(IEntity entity) => this.Enable((T) entity);

        /// <summary>
        /// Called when the entity is enabled.
        /// </summary>
        /// <param name="entity">The strongly-typed entity being enabled.</param>
        void Enable(T entity);
    }

    /// <summary>
    /// Unsafe generic version of <see cref="IEntityEnable"/> that provides strongly-typed enable logic
    /// for a specific <see cref="IEntity"/> type using low-level casting.
    /// </summary>
    /// <typeparam name="T">The specific type of entity this behavior applies to.</typeparam>
    public interface IEntityEnableUnsafe<in T> : IEntityEnable where T : IEntity
    {
        /// <summary>
        /// Called when the entity is enabled.
        /// </summary>
        /// <param name="entity">The strongly-typed entity being enabled.</param>
        void Enable(T entity);

        void IEntityEnable.Enable(IEntity entity) => this.Enable(UnsafeUtility.As<IEntity, T>(ref entity));
    }

    #endregion
    
    #region Disable

    /// <summary>
    /// Defines a behavior that executes logic when an <see cref="IEntity"/> is disabled.
    /// </summary>
    public interface IEntityDisable : IEntityBehaviour
    {
        /// <summary>
        /// Called when the entity is disabled.
        /// </summary>
        /// <param name="entity">The entity being disabled.</param>
        void Disable(IEntity entity);
    }

    /// <summary>
    /// Generic version of <see cref="IEntityDisable"/> that provides strongly-typed disable logic
    /// for a specific <see cref="IEntity"/> type.
    /// </summary>
    /// <typeparam name="T">The specific type of entity this behavior applies to.</typeparam>
    public interface IEntityDisable<in T> : IEntityDisable where T : IEntity
    {
        /// <summary>
        /// Called when the entity is disabled.
        /// </summary>
        /// <param name="entity">The strongly-typed entity being disabled.</param>
        void Disable(T entity);

        void IEntityDisable.Disable(IEntity entity) => this.Disable((T) entity);
    }

    /// <summary>
    /// Unsafe generic version of <see cref="IEntityDisable"/> that uses low-level casting for performance.
    /// </summary>
    /// <typeparam name="T">The specific type of entity this behavior applies to.</typeparam>
    public interface IEntityDisableUnsafe<in T> : IEntityDisable where T : IEntity
    {
        /// <summary>
        /// Called when the entity is disabled.
        /// </summary>
        /// <param name="entity">The strongly-typed entity being disabled.</param>
        void Disable(T entity);

        void IEntityDisable.Disable(IEntity entity) => this.Disable(UnsafeUtility.As<IEntity, T>(ref entity));
    }

    #endregion

    #region Shutdown

    /// <summary>
    /// Defines a behavior that is called when an <see cref="IEntity"/> is disposed.
    /// </summary>
    public interface IEntityDispose
    {
        /// <summary>
        /// Called when the entity is being disposed.
        /// </summary>
        /// <param name="entity">The entity being disposed.</param>
        void Dispose(IEntity entity);
    }

    /// <summary>
    /// Generic version of <see cref="IEntityDispose"/> that provides strongly-typed dispose logic
    /// for a specific <see cref="IEntity"/> type.
    /// </summary>
    /// <typeparam name="T">The specific entity type this logic applies to.</typeparam>
    public interface IEntityDispose<in T> : IEntityDispose where T : IEntity
    {
        /// <summary>
        /// Called when the entity is being disposed.
        /// </summary>
        /// <param name="entity">The strongly-typed entity being disposed.</param>
        void Dispose(T entity);

        void IEntityDispose.Dispose(IEntity entity) => this.Dispose((T) entity);
    }

    /// <summary>
    /// Unsafe generic version of <see cref="IEntityDispose"/> that performs low-level casting for performance.
    /// </summary>
    /// <typeparam name="T">The specific entity type this logic applies to.</typeparam>
    public interface IEntityDisposeUnsafe<in T> : IEntityDispose where T : IEntity
    {
        /// <summary>
        /// Called when the entity is being disposed.
        /// </summary>
        /// <param name="entity">The strongly-typed entity being disposed.</param>
        void Dispose(T entity);

        void IEntityDispose.Dispose(IEntity entity) => this.Dispose(UnsafeUtility.As<IEntity, T>(ref entity));
    }

    #endregion

    #region Update

    /// <summary>
    /// Defines a behavior that supports logic during the regular update cycle of an <see cref="IEntity"/>.
    /// Called once per frame in the main game loop.
    /// </summary>
    public interface IEntityUpdate : IEntityBehaviour
    {
        /// <summary>
        /// Called during the main update phase.
        /// </summary>
        /// <param name="entity">The entity being updated.</param>
        /// <param name="deltaTime">Elapsed time since the last frame.</param>
        void OnUpdate(IEntity entity, float deltaTime);
    }

    /// <summary>
    /// Generic version of <see cref="IEntityUpdate"/> providing strongly-typed update logic for a specific <see cref="IEntity"/> type.
    /// </summary>
    /// <typeparam name="T">The specific type of entity this behavior applies to.</typeparam>
    public interface IEntityUpdate<in T> : IEntityUpdate where T : IEntity
    {
        /// <summary>
        /// Called during the main update phase.
        /// </summary>
        /// <param name="entity">The strongly-typed entity being updated.</param>
        /// <param name="deltaTime">Elapsed time since the last frame.</param>
        void OnUpdate(T entity, float deltaTime);

        void IEntityUpdate.OnUpdate(IEntity entity, float deltaTime) => this.OnUpdate((T) entity, deltaTime);
    }

    /// <summary>
    /// Generic version of <see cref="IEntityUpdateUnsafe{T}"/> providing unsafe, strongly-typed update logic for a specific <see cref="IEntity"/> type.
    /// </summary>
    /// <typeparam name="T">The specific type of entity this behavior applies to.</typeparam>
    public interface IEntityUpdateUnsafe<in T> : IEntityUpdate where T : IEntity
    {
        /// <inheritdoc/>
        void IEntityUpdate.OnUpdate(IEntity entity, float deltaTime) =>
            this.OnUpdate(UnsafeUtility.As<IEntity, T>(ref entity), deltaTime);

        /// <summary>
        /// Called during the main update phase for the strongly-typed entity.
        /// </summary>
        /// <param name="entity">The strongly-typed entity being updated.</param>
        /// <param name="deltaTime">Elapsed time since the last frame.</param>
        void OnUpdate(T entity, float deltaTime);
    }

    #endregion

    #region FixedUpdate

    /// <summary>
    /// Defines a behavior that is updated at a fixed time interval.
    /// </summary>
    public interface IEntityFixedUpdate : IEntityBehaviour
    {
        /// <summary>
        /// Called every fixed update tick.
        /// </summary>
        /// <param name="entity">The entity this behavior is attached to.</param>
        /// <param name="deltaTime">The fixed delta time step.</param>
        void OnFixedUpdate(IEntity entity, float deltaTime);
    }

    /// <summary>
    /// Generic version of <see cref="IEntityFixedUpdate"/> that provides strongly-typed fixed update logic
    /// for a specific <see cref="IEntity"/> type.
    /// </summary>
    /// <typeparam name="T">The specific type of entity this behavior applies to.</typeparam>
    public interface IEntityFixedUpdate<in T> : IEntityFixedUpdate where T : IEntity
    {
        /// <inheritdoc/>
        void IEntityFixedUpdate.OnFixedUpdate(IEntity entity, float deltaTime) => this.OnFixedUpdate((T) entity, deltaTime);

        /// <summary>
        /// Called every fixed update tick with a strongly-typed entity.
        /// </summary>
        /// <param name="entity">The entity instance of type <typeparamref name="T"/>.</param>
        /// <param name="deltaTime">The fixed delta time step since the last update.</param>
        void OnFixedUpdate(T entity, float deltaTime);
    }

    /// <summary>
    /// Generic version of <see cref="IEntityFixedUpdate"/> that provides strongly-typed fixed update logic
    /// for a specific <see cref="IEntity"/> type.
    /// </summary>
    /// <typeparam name="T">The specific type of entity this behavior applies to.</typeparam>
    public interface IEntityFixedUpdateUnsafe<in T> : IEntityFixedUpdate where T : IEntity
    {
        /// <inheritdoc />
        void IEntityFixedUpdate.OnFixedUpdate(IEntity entity, float deltaTime) =>
            this.OnFixedUpdate(UnsafeUtility.As<IEntity, T>(ref entity), deltaTime);

        /// <summary>
        /// Called every fixed update tick with a strongly-typed entity.
        /// </summary>
        /// <param name="entity">The entity instance.</param>
        /// <param name="deltaTime">The fixed delta time step.</param>
        void OnFixedUpdate(T entity, float deltaTime);
    }

    #endregion

    #region LateUpdate

    /// <summary>
    /// Defines a behavior that executes logic during the late update phase of an <see cref="IEntity"/>.
    /// Called after all standard updates, typically used for post-processing logic or transform synchronization.
    /// </summary>
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
    /// Generic version of <see cref="IEntityLateUpdate"/> providing strongly-typed late update logic for a specific <see cref="IEntity"/> type.
    /// </summary>
    /// <typeparam name="T">The specific type of entity this behavior applies to.</typeparam>
    public interface IEntityLateUpdate<in T> : IEntityLateUpdate where T : IEntity
    {
        /// <summary>
        /// Called during the late update phase.
        /// </summary>
        /// <param name="entity">The strongly-typed entity being updated.</param>
        /// <param name="deltaTime">Elapsed time since the last frame.</param>
        void OnLateUpdate(T entity, float deltaTime);

        void IEntityLateUpdate.OnLateUpdate(IEntity entity, float deltaTime) => this.OnLateUpdate((T) entity, deltaTime);
    }

    /// <summary>
    /// Generic version of <see cref="IEntityLateUpdate"/> that uses unsafe casting for fast strongly-typed late updates.
    /// </summary>
    /// <typeparam name="T">The specific type of <see cref="IEntity"/> this behavior operates on.</typeparam>
    public interface IEntityLateUpdateUnsafe<in T> : IEntityLateUpdate where T : IEntity
    {
        /// <inheritdoc/>
        void IEntityLateUpdate.OnLateUpdate(IEntity entity, float deltaTime) =>
            this.OnLateUpdate(UnsafeUtility.As<IEntity, T>(ref entity), deltaTime);

        /// <summary>
        /// Called during the LateUpdate phase for the strongly-typed entity.
        /// </summary>
        /// <param name="entity">The strongly-typed entity instance.</param>
        /// <param name="deltaTime">The delta time since the last update.</param>
        void OnLateUpdate(T entity, float deltaTime);
    }

    #endregion

    #region Gizmos

    /// <summary>
    /// Defines a behavior that allows drawing gizmos for an entity during the editor or debug rendering phase.
    /// </summary>
    public interface IEntityGizmos : IEntityBehaviour
    {
        /// <summary>
        /// Called to draw gizmos for the specified entity.
        /// This is typically invoked during the Unity editor's OnDrawGizmos phase.
        /// </summary>
        /// <param name="entity">The entity for which gizmos should be drawn.</param>
        void OnGizmosDraw(IEntity entity);
    }

    /// <summary>
    /// Generic version of <see cref="IEntityGizmos"/> that provides strongly-typed gizmo drawing
    /// for a specific <see cref="IEntity"/> type.
    /// </summary>
    /// <typeparam name="T">The specific type of entity this behavior applies to.</typeparam>
    public interface IEntityGizmos<in T> : IEntityGizmos where T : IEntity
    {
        /// <inheritdoc/>
        void IEntityGizmos.OnGizmosDraw(IEntity entity) => this.OnGizmosDraw((T) entity);

        /// <summary>
        /// Called to draw gizmos for the specified entity.
        /// </summary>
        /// <param name="entity">The strongly-typed entity instance.</param>
        void OnGizmosDraw(T entity);
    }

    #endregion
}