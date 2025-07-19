using System;

namespace Atomic.Entities
{
    /// <summary>
    /// Provides lifecycle and update phase event bindings for a <see cref="SceneEntity"/>,
    /// delegating lifecycle control and state to the internal <see cref="Entity"/> instance.
    /// </summary>
    public partial class SceneEntity<E>

    {
        /// <summary>
        /// Triggered when the entity is initialized.
        /// </summary>
        public event Action OnInitialized
        {
            add => this.Entity.OnInitialized += value;
            remove => this.Entity.OnInitialized -= value;
        }

        /// <summary>
        /// Triggered when the entity is disposed.
        /// </summary>
        public event Action OnDisposed
        {
            add => this.Entity.OnDisposed += value;
            remove => this.Entity.OnDisposed -= value;
        }

        /// <summary>
        /// Triggered when the entity is enabled.
        /// </summary>
        public event Action OnEnabled
        {
            add => this.Entity.OnEnabled += value;
            remove => this.Entity.OnEnabled -= value;
        }

        /// <summary>
        /// Triggered when the entity is disabled.
        /// </summary>
        public event Action OnDisabled
        {
            add => this.Entity.OnDisabled += value;
            remove => this.Entity.OnDisabled -= value;
        }
        
        /// <summary>
        /// Triggered during the entity's update phase.
        /// </summary>
        public event Action<float> OnUpdated
        {
            add => this.Entity.OnUpdated += value;
            remove => this.Entity.OnUpdated -= value;
        }

        /// <summary>
        /// Triggered during the entity's fixed update phase.
        /// </summary>
        public event Action<float> OnFixedUpdated
        {
            add => this.Entity.OnFixedUpdated += value;
            remove => this.Entity.OnFixedUpdated -= value;
        }

        
        /// <summary>
        /// Triggered during the entity's late update phase.
        /// </summary>
        public event Action<float> OnLateUpdated
        {
            add => this.Entity.OnLateUpdated += value;
            remove => this.Entity.OnLateUpdated -= value;
        }

        /// <summary>
        /// Returns true if the entity has been initialized.
        /// </summary>
        public bool Initialized => this.Entity.Initialized;

        /// <summary>
        /// Returns or sets whether the entity is currently enabled.
        /// </summary>
        public bool Enabled
        {
            get => this.Entity.Enabled;
            set => this.enabled = value;
        }
        /// <summary>
        /// Initializes the entity.
        /// </summary>
        public void Init() => this.Entity.Init();
        
        /// <summary>
        /// Enables the entity.
        /// </summary>
        public void Enable() => this.Entity.Enable();
        
        /// <summary>
        /// Disables the entity.
        /// </summary>
        public void Disable() => this.Entity.Disable();

        /// <summary>
        /// Disposes the entity and releases associated resources.
        /// </summary>
        public void Dispose() => this.Entity.Dispose();
        
        /// <summary>
        /// Performs update logic with the given delta time.
        /// </summary>
        public void OnUpdate(float deltaTime) => this.Entity.OnUpdate(deltaTime);
        
        /// <summary>
        /// Performs fixed update logic with the given delta time.
        /// </summary>
        public void OnFixedUpdate(float deltaTime) => this.Entity.OnFixedUpdate(deltaTime);
        
        /// <summary>
        /// Performs late update logic with the given delta time.
        /// </summary>
        public void OnLateUpdate(float deltaTime) => this.Entity.OnLateUpdate(deltaTime);
    }
}