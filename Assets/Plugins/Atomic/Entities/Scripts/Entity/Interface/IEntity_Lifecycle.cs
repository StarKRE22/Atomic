using System;

namespace Atomic.Entities
{
    ///Represents a lifecycle management
    public partial interface IEntity
    {
        /// <summary>
        /// Invoked when the entity is spawned.
        /// </summary>
        event Action OnSpawned;

        /// <summary>
        /// Invoked when the entity is despawned
        /// </summary>
        event Action OnDespawned;

        /// <summary>
        /// Invoked when the entity is enabled.
        /// </summary>
        event Action OnEnabled;

        /// <summary>
        /// Invoked when the entity is disabled.
        /// </summary>
        event Action OnDisabled;

        /// <summary>
        /// Invoked on every Update tick.
        /// </summary>
        event Action<float> OnUpdated;

        /// <summary>
        /// Invoked on every FixedUpdate tick.
        /// </summary>
        event Action<float> OnFixedUpdated;

        /// <summary>
        /// Invoked on every LateUpdate tick.
        /// </summary>
        event Action<float> OnLateUpdated;

        /// <summary>
        /// Whether the entity has been spawned.
        /// </summary>
        bool Spawned { get; }

        /// <summary>
        /// Whether the entity is currently enabled.
        /// </summary>
        bool Enabled { get; }

        /// <summary>
        /// Spawns the entity and invokes <see cref="IEntitySpawn.Spawn"/> on all attached behaviors that implement it.
        /// </summary>
        void Spawn();

        /// <summary>
        /// Enables the entity and invokes <see cref="IEntityEnable.Enable"/> on all attached behaviors that implement it.
        /// </summary>
        void Enable();

        /// <summary>
        /// Disables the entity and invokes <see cref="IEntityDisable.Disable"/> on all attached behaviors that implement it.
        /// </summary>
        void Disable();

        /// <summary>
        /// Despawns the entity and invokes <see cref="IEntityDespawn.Despawn"/> on all attached behaviors that implement it.
        /// </summary>
        void Despawn();

        /// <summary>
        /// Called once per frame during the main game loop and invokes <see cref="IEntityUpdate.OnUpdate"/>
        /// on all attached behaviors that implement it.
        /// </summary>
        /// <param name="deltaTime">Elapsed time since the last frame.</param>
        void OnUpdate(float deltaTime);

        /// <summary>
        /// Called at a fixed time interval (e.g., for physics) and invokes <see cref="IEntityFixedUpdate.OnFixedUpdate"/>
        /// on all attached behaviors that implement it.
        /// </summary>
        /// <param name="deltaTime">Fixed time step since the last update.</param>
        void OnFixedUpdate(float deltaTime);

        /// <summary>
        /// Called after all standard updates during the late update phase,
        /// and invokes <see cref="IEntityLateUpdate.OnLateUpdate"/> on all attached behaviors that implement it.
        /// </summary>
        /// <param name="deltaTime">Elapsed time since the last frame.</param>
        void OnLateUpdate(float deltaTime);
    }
}