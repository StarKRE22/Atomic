using System;

namespace Atomic.Entities
{
    /// <summary>
    /// Represents a lifecycle controller for a collection of <see cref="IEntity"/> instances.
    /// Provides methods and events to manage initialization, enabling, disabling, updating, and disposal.
    /// </summary>
    public interface IEntityLoop : IEntityLoop<IEntity>
    {
    }

    /// <summary>
    /// Represents a lifecycle controller for a generic collection of entities.
    /// Extends <see cref="IEntityCollection{E}"/> with lifecycle management capabilities such as Init, Enable, Disable, and Update loops.
    /// </summary>
    /// <typeparam name="E">The type of entity managed by the loop, which must implement <see cref="IEntity"/>.</typeparam>
    public interface IEntityLoop<E> : IEntityCollection<E> where E : IEntity
    {
        /// <summary>
        /// Occurs when all entities have been initialized.
        /// </summary>
        event Action OnInitialized;

        /// <summary>
        /// Occurs when all entities have been disposed.
        /// </summary>
        event Action OnDisposed;

        /// <summary>
        /// Occurs when all entities have been enabled.
        /// </summary>
        event Action OnEnabled;

        /// <summary>
        /// Occurs when all entities have been disabled.
        /// </summary>
        event Action OnDisabled;

        /// <summary>
        /// Occurs after calling <see cref="EntityLoop.OnUpdate"/> on all entities.
        /// </summary>
        event Action<float> OnUpdated;

        /// <summary>
        /// Occurs after calling <see cref="EntityLoop.OnFixedUpdate"/> on all entities.
        /// </summary>
        event Action<float> OnFixedUpdated;

        /// <summary>
        /// Occurs after calling <see cref="EntityLoop.OnLateUpdate"/> on all entities.
        /// </summary>
        event Action<float> OnLateUpdated;

        /// <summary>
        /// Indicates whether the loop is initialized.
        /// </summary>
        bool Initialized { get; }

        /// <summary>
        /// Indicates whether the loop is enabled.
        /// </summary>
        bool Enabled { get; }

        /// <summary>
        /// Initializes all registered entities and raises <see cref="EntityLoop.OnInitialized"/>.
        /// </summary>
        void Init();

        /// <summary>
        /// Disposes all registered entities and raises <see cref="EntityLoop.OnDisposed"/>.
        /// </summary>
        void Dispose();

        /// <summary>
        /// Enables all registered entities and raises <see cref="EntityLoop.OnEnabled"/>.
        /// </summary>
        void Enable();

        /// <summary>
        /// Disables all registered entities and raises <see cref="EntityLoop.OnDisabled"/>.
        /// </summary>
        void Disable();

        /// <summary>
        /// Updates all enabled entities using the provided delta time and raises <see cref="EntityLoop.OnUpdated.OnUpdated"/>.
        /// </summary>
        /// <param name="deltaTime">The delta time passed to update.</param>
        void OnUpdate(float deltaTime);

        /// <summary>
        /// Performs fixed update on all enabled entities and raises <see cref="EntityLoop.OnFixedUpdated.OnFixedUpdated"/>.
        /// </summary>
        /// <param name="deltaTime">The delta time passed to fixed update.</param>
        void OnFixedUpdate(float deltaTime);

        /// <summary>
        /// Performs late update on all enabled entities and raises <see cref="EntityLoop.OnLateUpdated.OnLateUpdated"/>.
        /// </summary>
        /// <param name="deltaTime">The delta time passed to late update.</param>
        void OnLateUpdate(float deltaTime);
    }
}