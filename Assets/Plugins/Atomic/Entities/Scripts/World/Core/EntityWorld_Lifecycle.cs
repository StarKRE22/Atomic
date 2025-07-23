using System;

namespace Atomic.Entities
{
    public partial class EntityWorld<E>
    {
        public event Action OnInitialized
        {
            add => _loop.OnInitialized += value;
            remove => _loop.OnInitialized -= value;
        }

        public event Action OnDisposed
        {
            add => _loop.OnDisposed += value;
            remove => _loop.OnDisposed -= value;
        }

        public event Action OnEnabled
        {
            add => _loop.OnEnabled += value;
            remove => _loop.OnEnabled -= value;
        }

        public event Action OnDisabled
        {
            add => _loop.OnDisabled += value;
            remove => _loop.OnDisabled -= value;
        }

        public event Action<float> OnUpdated
        {
            add => _loop.OnUpdated += value;
            remove => _loop.OnUpdated -= value;
        }

        public event Action<float> OnFixedUpdated
        {
            add => _loop.OnFixedUpdated += value;
            remove => _loop.OnFixedUpdated -= value;
        }

        public event Action<float> OnLateUpdated
        {
            add => _loop.OnLateUpdated += value;
            remove => _loop.OnLateUpdated -= value;
        }

        public bool Initialized => _loop.Initialized;
        public bool Enabled => _loop.Enabled;

        private readonly EntityLoop<E> _loop = new();
        
        public void Init() => _loop.Init();
        public void Enable() => _loop.Enable();
        public void Disable() => _loop.Disable();
        public void Dispose() => _loop.Dispose();

        public void OnUpdate(float deltaTime) => _loop.OnUpdate(deltaTime);
        public void OnFixedUpdate(float deltaTime) => _loop.OnFixedUpdate(deltaTime);
        public void OnLateUpdate(float deltaTime) => _loop.OnLateUpdate(deltaTime);
    }
}