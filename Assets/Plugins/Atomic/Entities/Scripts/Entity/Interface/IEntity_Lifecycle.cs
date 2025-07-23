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
        /// Spawns the entity.
        /// </summary>
        void Spawn();

        /// <summary>
        /// Enables the entity.
        /// </summary>
        void Enable();

        /// <summary>
        /// Disables the entity.
        /// </summary>
        void Disable();

        /// <summary>
        /// Despawns the entity.
        /// </summary>
        void Despawn();

        /// <summary>
        /// Called every Update tick.
        /// </summary>
        void OnUpdate(float deltaTime);

        /// <summary>
        /// Called every FixedUpdate tick.
        /// </summary>
        void OnFixedUpdate(float deltaTime);

        /// <summary>
        /// Called every LateUpdate tick.
        /// </summary>
        void OnLateUpdate(float deltaTime);
    }
}