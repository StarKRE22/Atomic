using System;

namespace Atomic.Entities
{
    public partial class EntityWorld
    {
        public event Action OnInitialized
        {
            add => _entityUpdater.OnInitialized += value;
            remove => _entityUpdater.OnInitialized -= value;
        }

        public event Action OnDisposed
        {
            add => _entityUpdater.OnDisposed += value;
            remove => _entityUpdater.OnDisposed -= value;
        }

        public event Action OnEnabled
        {
            add => _entityUpdater.OnEnabled += value;
            remove => _entityUpdater.OnEnabled -= value;
        }

        public event Action OnDisabled
        {
            add => _entityUpdater.OnDisabled += value;
            remove => _entityUpdater.OnDisabled -= value;
        }

        public event Action<float> OnUpdated
        {
            add => _entityUpdater.OnUpdated += value;
            remove => _entityUpdater.OnUpdated -= value;
        }

        public event Action<float> OnFixedUpdated
        {
            add => _entityUpdater.OnFixedUpdated += value;
            remove => _entityUpdater.OnFixedUpdated -= value;
        }

        public event Action<float> OnLateUpdated
        {
            add => _entityUpdater.OnLateUpdated += value;
            remove => _entityUpdater.OnLateUpdated -= value;
        }

        public bool Initialized => _entityUpdater.Initialized;
        public bool Enabled => _entityUpdater.Enabled;

        private readonly EntityUpdater _entityUpdater = new();
        
        public void Init() => _entityUpdater.Init();
        public void Enable() => _entityUpdater.Enable();
        public void Disable() => _entityUpdater.Disable();
        public void Dispose() => _entityUpdater.Dispose();

        public void OnUpdate(float deltaTime) => _entityUpdater.OnUpdate(deltaTime);
        public void OnFixedUpdate(float deltaTime) => _entityUpdater.OnFixedUpdate(deltaTime);
        public void OnLateUpdate(float deltaTime) => _entityUpdater.OnLateUpdate(deltaTime);
    }
}