using System;

namespace Atomic.Entities
{
    public partial class EntityWorld<E>
    {
        public event Action OnSpawned
        {
            add => _runner.OnSpawned += value;
            remove => _runner.OnSpawned -= value;
        }

        public event Action OnDespawned
        {
            add => _runner.OnDespawned += value;
            remove => _runner.OnDespawned -= value;
        }

        public event Action OnEnabled
        {
            add => _runner.OnEnabled += value;
            remove => _runner.OnEnabled -= value;
        }

        public event Action OnDisabled
        {
            add => _runner.OnDisabled += value;
            remove => _runner.OnDisabled -= value;
        }

        public event Action<float> OnUpdated
        {
            add => _runner.OnUpdated += value;
            remove => _runner.OnUpdated -= value;
        }

        public event Action<float> OnFixedUpdated
        {
            add => _runner.OnFixedUpdated += value;
            remove => _runner.OnFixedUpdated -= value;
        }

        public event Action<float> OnLateUpdated
        {
            add => _runner.OnLateUpdated += value;
            remove => _runner.OnLateUpdated -= value;
        }

        public bool Spawned => _runner.Spawned;
        public bool Enabled => _runner.Enabled;

        private readonly EntityRunner<E> _runner = new();
        
        public void Spawn() => _runner.Spawn();
        public void Enable() => _runner.Enable();
        public void Disable() => _runner.Disable();
        public void Despawn() => _runner.Despawn();

        public void OnUpdate(float deltaTime) => _runner.OnUpdate(deltaTime);
        public void OnFixedUpdate(float deltaTime) => _runner.OnFixedUpdate(deltaTime);
        public void OnLateUpdate(float deltaTime) => _runner.OnLateUpdate(deltaTime);
    }
}