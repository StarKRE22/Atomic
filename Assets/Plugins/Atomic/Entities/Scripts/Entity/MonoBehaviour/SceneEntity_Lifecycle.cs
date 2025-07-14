using System;

namespace Atomic.Entities
{
    public partial class SceneEntity
    {
        public event Action OnInitialized
        {
            add => this.Entity.OnInitialized += value;
            remove => this.Entity.OnInitialized -= value;
        }

        public event Action OnDisposed
        {
            add => this.Entity.OnDisposed += value;
            remove => this.Entity.OnDisposed -= value;
        }

        public event Action OnEnabled
        {
            add => this.Entity.OnEnabled += value;
            remove => this.Entity.OnEnabled -= value;
        }

        public event Action OnDisabled
        {
            add => this.Entity.OnDisabled += value;
            remove => this.Entity.OnDisabled -= value;
        }

        public event Action<float> OnUpdated
        {
            add => this.Entity.OnUpdated += value;
            remove => this.Entity.OnUpdated -= value;
        }

        public event Action<float> OnFixedUpdated
        {
            add => this.Entity.OnFixedUpdated += value;
            remove => this.Entity.OnFixedUpdated -= value;
        }

        public event Action<float> OnLateUpdated
        {
            add => this.Entity.OnLateUpdated += value;
            remove => this.Entity.OnLateUpdated -= value;
        }

        public bool Initialized => this.Entity.Initialized;

        public bool Enabled
        {
            get => this.Entity.Enabled;
            set => this.enabled = value;
        }

        public void Init() => this.Entity.Init();
        public void Enable() => this.Entity.Enable();
        public void Disable() => this.Entity.Disable();
        public void Dispose() => this.Entity.Dispose();
        public void OnUpdate(float deltaTime) => this.Entity.OnUpdate(deltaTime);
        public void OnFixedUpdate(float deltaTime) => this.Entity.OnFixedUpdate(deltaTime);
        public void OnLateUpdate(float deltaTime) => this.Entity.OnLateUpdate(deltaTime);
    }
}