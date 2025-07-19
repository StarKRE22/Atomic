using System;

namespace Atomic.Entities
{
    ///Represents a lifecycle management (initialization, update, disposal)
    public partial interface IEntity<E>
    {
        /// <summary>
        /// Invoked when the entity is initialized.
        /// </summary>
        event Action OnInitialized;

        /// <summary>
        /// Invoked when the entity is disposed.
        /// </summary>
        event Action OnDisposed;

        /// <summary>
        /// Invoked when the entity is enabled.
        /// </summary>
        event Action OnEnabled;

        /// <summary>
        /// Invoked when the entity is disabled.
        /// </summary>
        event Action OnDisabled;

        /// <summary>
        /// Invoked when the internal state changes.
        /// </summary>
        event Action OnStateChanged;

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
        /// Whether the entity has been initialized.
        /// </summary>
        bool Initialized { get; }

        /// <summary>
        /// Whether the entity is currently enabled.
        /// </summary>
        bool Enabled { get; }
        
        /// <summary>
        /// Initializes the entity.
        /// </summary>
        void Init();

        /// <summary>
        /// Enables the entity.
        /// </summary>
        void Enable();

        /// <summary>
        /// Disables the entity.
        /// </summary>
        void Disable();

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