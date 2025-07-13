using System;

namespace Atomic.Entities
{
    public partial class SceneEntityProxy<E>
    {
        public event Action OnInitialized
        {
            add => _source.OnInitialized += value;
            remove => _source.OnInitialized -= value;
        }

        public event Action OnEnabled
        {
            add => _source.OnEnabled += value;
            remove => _source.OnEnabled -= value;
        }

        public event Action OnDisabled
        {
            add => _source.OnDisabled += value;
            remove => _source.OnDisabled -= value;
        }

        public event Action OnDisposed
        {
            add => _source.OnDisposed += value;
            remove => _source.OnDisposed -= value;
        }

        public event Action<float> OnUpdated
        {
            add => _source.OnUpdated += value;
            remove => _source.OnUpdated -= value;
        }

        public event Action<float> OnFixedUpdated
        {
            add => _source.OnFixedUpdated += value;
            remove => _source.OnFixedUpdated -= value;
        }

        public event Action<float> OnLateUpdated
        {
            add => _source.OnLateUpdated += value;
            remove => _source.OnLateUpdated -= value;
        }
        
        public bool Initialized
        {
            get => _source.Initialized;
        }

        public bool Enabled
        {
            get => _source.Enabled;
            set => _source.Enabled = value;
        }
        
        public void Init() => _source.Init();
        public void Enable() => _source.Enable();
        public void Disable() => _source.Disable();
        public void Dispose() => _source.Dispose();
        public void OnUpdate(float deltaTime) => _source.OnUpdate(deltaTime);
        public void OnFixedUpdate(float deltaTime) => _source.OnFixedUpdate(deltaTime);
        public void OnLateUpdate(float deltaTime) => _source.OnLateUpdate(deltaTime);
    }
}