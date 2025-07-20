using Unity.Collections.LowLevel.Unsafe;

namespace Atomic.Entities
{
    /// <summary>
    /// Defines a unit of logic that can be attached to an <see cref="IEntity"/>.
    /// Behaviours encapsulate modular functionality and are used to compose entity behavior at runtime.
    /// </summary>
    public interface IBehaviour
    {
    }

    #region Init

    /// <summary>
    /// Defines a behavior that supports initialization logic for an <see cref="IEntity"/>.
    /// </summary>
    public interface IInit : IBehaviour
    {
        /// <summary>
        /// Initializes the behavior with the specified <see cref="IEntity"/>.
        /// </summary>
        /// <param name="entity">The entity to initialize this behavior with.</param>
        void Init(IEntity entity);
    }

    /// <summary>
    /// Generic version of <see cref="IInit"/> that provides strongly-typed initialization for a specific <see cref="IEntity"/> type.
    /// </summary>
    /// <typeparam name="T">The specific type of entity this behavior works with.</typeparam>
    public interface IInit<in T> : IInit where T : IEntity
    {
        /// <summary>
        /// Initializes the behavior with a strongly-typed entity context.
        /// </summary>
        /// <param name="entity">The entity of type <typeparamref name="T"/>.</param>
        void Init(T entity);

        void IInit.Init(IEntity entity) => this.Init((T) entity);
    }

    /// <summary>
    /// Unsafe generic version of <see cref="IInit"/> that uses low-level casting to initialize a strongly-typed <see cref="IEntity"/>.
    /// </summary>
    /// <typeparam name="T">The specific type of entity this behavior works with.</typeparam>
    public interface IUnsafeInit<in T> : IInit where T : IEntity
    {
        /// <summary>
        /// Initializes the behavior with a strongly-typed entity context.
        /// </summary>
        /// <param name="entity">The entity of type <typeparamref name="T"/>.</param>
        void Init(T entity);

        void IInit.Init(IEntity entity) => this.Init(UnsafeUtility.As<IEntity, T>(ref entity));
    }

    #endregion

    #region Enable

    /// <summary>
    /// Defines a behavior that supports an enable lifecycle event for an <see cref="IEntity"/>.
    /// This is typically called when the entity is activated or enters the active state.
    /// </summary>
    public interface IEnable : IBehaviour
    {
        /// <summary>
        /// Called when the entity is enabled.
        /// </summary>
        /// <param name="entity">The entity being enabled.</param>
        void Enable(IEntity entity);
    }

    /// <summary>
    /// Generic version of <see cref="IEnable"/> that provides strongly-typed enable logic for a specific <see cref="IEntity"/> type.
    /// </summary>
    /// <typeparam name="T">The specific type of entity this behavior applies to.</typeparam>
    public interface IEnable<in T> : IEnable where T : IEntity
    {
        void IEnable.Enable(IEntity entity) => this.Enable((T) entity);

        /// <summary>
        /// Called when the entity is enabled.
        /// </summary>
        /// <param name="entity">The strongly-typed entity being enabled.</param>
        void Enable(T entity);
    }

    /// <summary>
    /// Unsafe generic version of <see cref="IEnable"/> that provides strongly-typed enable logic
    /// for a specific <see cref="IEntity"/> type using low-level casting.
    /// </summary>
    /// <typeparam name="T">The specific type of entity this behavior applies to.</typeparam>
    public interface IUnsafeEnable<in T> : IEnable where T : IEntity
    {
        /// <summary>
        /// Called when the entity is enabled.
        /// </summary>
        /// <param name="entity">The strongly-typed entity being enabled.</param>
        void Enable(T entity);

        void IEnable.Enable(IEntity entity) => this.Enable(UnsafeUtility.As<IEntity, T>(ref entity));
    }

    #endregion


    #region Disable

    /// <summary>
    /// Defines a behavior that executes logic when an <see cref="IEntity"/> is disabled.
    /// </summary>
    public interface IDisable : IBehaviour
    {
        /// <summary>
        /// Called when the entity is disabled.
        /// </summary>
        /// <param name="entity">The entity being disabled.</param>
        void Disable(IEntity entity);
    }

    /// <summary>
    /// Generic version of <see cref="IDisable"/> that provides strongly-typed disable logic
    /// for a specific <see cref="IEntity"/> type.
    /// </summary>
    /// <typeparam name="T">The specific type of entity this behavior applies to.</typeparam>
    public interface IDisable<in T> : IDisable where T : IEntity
    {
        /// <summary>
        /// Called when the entity is disabled.
        /// </summary>
        /// <param name="entity">The strongly-typed entity being disabled.</param>
        void Disable(T entity);

        void IDisable.Disable(IEntity entity) => this.Disable((T) entity);
    }

    /// <summary>
    /// Unsafe generic version of <see cref="IDisable"/> that uses low-level casting for performance.
    /// </summary>
    /// <typeparam name="T">The specific type of entity this behavior applies to.</typeparam>
    public interface IUnsafeDisable<in T> : IDisable where T : IEntity
    {
        /// <summary>
        /// Called when the entity is disabled.
        /// </summary>
        /// <param name="entity">The strongly-typed entity being disabled.</param>
        void Disable(T entity);

        void IDisable.Disable(IEntity entity) => this.Disable(UnsafeUtility.As<IEntity, T>(ref entity));
    }

    #endregion

    #region Dispose

    /// <summary>
    /// Defines a behavior that is called when an <see cref="IEntity"/> is disposed.
    /// </summary>
    public interface IDispose
    {
        /// <summary>
        /// Called when the entity is being disposed.
        /// </summary>
        /// <param name="entity">The entity being disposed.</param>
        void Dispose(IEntity entity);
    }

    /// <summary>
    /// Generic version of <see cref="IDispose"/> that provides strongly-typed dispose logic
    /// for a specific <see cref="IEntity"/> type.
    /// </summary>
    /// <typeparam name="T">The specific entity type this logic applies to.</typeparam>
    public interface IDispose<in T> : IDispose where T : IEntity
    {
        /// <summary>
        /// Called when the entity is being disposed.
        /// </summary>
        /// <param name="entity">The strongly-typed entity being disposed.</param>
        void Dispose(T entity);

        void IDispose.Dispose(IEntity entity) => this.Dispose((T) entity);
    }

    /// <summary>
    /// Unsafe generic version of <see cref="IDispose"/> that performs low-level casting for performance.
    /// </summary>
    /// <typeparam name="T">The specific entity type this logic applies to.</typeparam>
    public interface IUnsafeDispose<in T> : IDispose where T : IEntity
    {
        /// <summary>
        /// Called when the entity is being disposed.
        /// </summary>
        /// <param name="entity">The strongly-typed entity being disposed.</param>
        void Dispose(T entity);

        void IDispose.Dispose(IEntity entity) => this.Dispose(UnsafeUtility.As<IEntity, T>(ref entity));
    }

    #endregion

    #region Update

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

    #endregion

    #region FixedUpdate

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

    #endregion

    #region LateUpdate

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

    #endregion

    #region Gizmos

    /// <summary>
    /// Defines a behavior that allows drawing gizmos for an entity during the editor or debug rendering phase.
    /// </summary>
    public interface IGizmos : IBehaviour
    {
        /// <summary>
        /// Called to draw gizmos for the specified entity.
        /// This is typically invoked during the Unity editor's OnDrawGizmos phase.
        /// </summary>
        /// <param name="entity">The entity for which gizmos should be drawn.</param>
        void OnGizmosDraw(IEntity entity);
    }

    /// <summary>
    /// Generic version of <see cref="IGizmos"/> that provides strongly-typed gizmo drawing
    /// for a specific <see cref="IEntity"/> type.
    /// </summary>
    /// <typeparam name="T">The specific type of entity this behavior applies to.</typeparam>
    public interface IGizmos<in T> : IGizmos where T : IEntity
    {
        /// <inheritdoc/>
        void IGizmos.OnGizmosDraw(IEntity entity) => this.OnGizmosDraw((T) entity);

        /// <summary>
        /// Called to draw gizmos for the specified entity.
        /// </summary>
        /// <param name="entity">The strongly-typed entity instance.</param>
        void OnGizmosDraw(T entity);
    }

    #endregion
}