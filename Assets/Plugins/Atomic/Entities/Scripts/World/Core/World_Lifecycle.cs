using System;

namespace Atomic.Entities
{
    public partial class World<E>
    {
        public event Action OnInitialized
        {
            add => _updater.OnInitialized += value;
            remove => _updater.OnInitialized -= value;
        }

        public event Action OnDisposed
        {
            add => _updater.OnDisposed += value;
            remove => _updater.OnDisposed -= value;
        }

        public event Action OnEnabled
        {
            add => _updater.OnEnabled += value;
            remove => _updater.OnEnabled -= value;
        }

        public event Action OnDisabled
        {
            add => _updater.OnDisabled += value;
            remove => _updater.OnDisabled -= value;
        }

        public event Action<float> OnUpdated
        {
            add => _updater.OnUpdated += value;
            remove => _updater.OnUpdated -= value;
        }

        public event Action<float> OnFixedUpdated
        {
            add => _updater.OnFixedUpdated += value;
            remove => _updater.OnFixedUpdated -= value;
        }

        public event Action<float> OnLateUpdated
        {
            add => _updater.OnLateUpdated += value;
            remove => _updater.OnLateUpdated -= value;
        }

        public bool Initialized => _updater.Initialized;
        public bool Enabled => _updater.Enabled;

        private readonly Updater<E> _updater = new();
        
        public void Init() => _updater.Init();
        public void Enable() => _updater.Enable();
        public void Disable() => _updater.Disable();
        public void Dispose() => _updater.Dispose();

        public void OnUpdate(float deltaTime) => _updater.OnUpdate(deltaTime);
        public void OnFixedUpdate(float deltaTime) => _updater.OnFixedUpdate(deltaTime);
        public void OnLateUpdate(float deltaTime) => _updater.OnLateUpdate(deltaTime);
    }
}