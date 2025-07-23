using System;

namespace Atomic.Entities
{
    /// <summary>
    /// Represents a lifecycle controller for a collection of <see cref="IEntity"/> instances.
    /// Provides methods and events to manage initialization, enabling, disabling, updating, and disposal.
    /// </summary>
    public interface IEntityRunner : IEntityRunner<IEntity>
    {
    }

    /// <summary>
    /// Represents a lifecycle controller for a generic collection of entities.
    /// Extends <see cref="IEntityCollection{E}"/> with lifecycle management capabilities such as Spawn, Enable, Disable, and Update loops.
    /// </summary>
    /// <typeparam name="E">The type of entity managed by the loop, which must implement <see cref="IEntity"/>.</typeparam>
    public interface IEntityRunner<E> : IEntityCollection<E> where E : IEntity
    {
        /// <summary>
        /// Occurs when all entities have been spawned.
        /// </summary>
        event Action OnSpawned;

        /// <summary>
        /// Occurs when all entities have been despawned.
        /// </summary>
        event Action OnDespawned;

        /// <summary>
        /// Occurs when all entities have been enabled.
        /// </summary>
        event Action OnEnabled;

        /// <summary>
        /// Occurs when all entities have been disabled.
        /// </summary>
        event Action OnDisabled;

        /// <summary>
        /// Occurs after calling <see cref="EntityRunner.OnUpdate"/> on all entities.
        /// </summary>
        event Action<float> OnUpdated;

        /// <summary>
        /// Occurs after calling <see cref="EntityRunner.OnFixedUpdate"/> on all entities.
        /// </summary>
        event Action<float> OnFixedUpdated;

        /// <summary>
        /// Occurs after calling <see cref="EntityRunner.OnLateUpdate"/> on all entities.
        /// </summary>
        event Action<float> OnLateUpdated;

        /// <summary>
        /// Indicates whether the loop is spawned.
        /// </summary>
        bool Spawned { get; }

        /// <summary>
        /// Indicates whether the loop is enabled.
        /// </summary>
        bool Enabled { get; }

        /// <summary>
        /// Spawns all registered entities and raises <see cref="EntityRunner{E}.OnSpawned"/>.
        /// </summary>
        void Spawn();

        /// <summary>
        /// Despawns all registered entities and raises <see cref="EntityRunner{E}.OnDespawned"/>.
        /// </summary>
        void Despawn();

        /// <summary>
        /// Enables all registered entities and raises <see cref="EntityRunner.OnEnabled"/>.
        /// </summary>
        void Enable();

        /// <summary>
        /// Disables all registered entities and raises <see cref="EntityRunner.OnDisabled"/>.
        /// </summary>
        void Disable();

        /// <summary>
        /// Updates all enabled entities using the provided delta time and raises <see cref="EntityRunner.OnUpdated.OnUpdated"/>.
        /// </summary>
        /// <param name="deltaTime">The delta time passed to update.</param>
        void OnUpdate(float deltaTime);

        /// <summary>
        /// Performs fixed update on all enabled entities and raises <see cref="EntityRunner.OnFixedUpdated.OnFixedUpdated"/>.
        /// </summary>
        /// <param name="deltaTime">The delta time passed to fixed update.</param>
        void OnFixedUpdate(float deltaTime);

        /// <summary>
        /// Performs late update on all enabled entities and raises <see cref="EntityRunner.OnLateUpdated.OnLateUpdated"/>.
        /// </summary>
        /// <param name="deltaTime">The delta time passed to late update.</param>
        void OnLateUpdate(float deltaTime);
    }
}