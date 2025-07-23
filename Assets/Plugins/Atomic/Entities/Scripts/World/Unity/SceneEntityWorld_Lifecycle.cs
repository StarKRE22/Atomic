using System;
#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

namespace Atomic.Entities
{
    public partial class SceneEntityWorld<E>
    {
        public event Action OnSpawned
        {
            add => _world.OnSpawned += value;
            remove => _world.OnSpawned -= value;
        }

        public event Action OnDespawned
        {
            add => _world.OnDespawned += value;
            remove => _world.OnDespawned -= value;
        }

        public event Action OnEnabled
        {
            add => _world.OnEnabled += value;
            remove => _world.OnEnabled -= value;
        }

        public event Action OnDisabled
        {
            add => _world.OnDisabled += value;
            remove => _world.OnDisabled -= value;
        }

        public event Action<float> OnUpdated
        {
            add => _world.OnUpdated += value;
            remove => _world.OnUpdated -= value;
        }

        public event Action<float> OnFixedUpdated
        {
            add => _world.OnFixedUpdated += value;
            remove => _world.OnFixedUpdated -= value;
        }

        public event Action<float> OnLateUpdated
        {
            add => _world.OnLateUpdated += value;
            remove => _world.OnLateUpdated -= value;
        }

        public bool Spawned => _world.Spawned;

        public bool Enabled => _world.Enabled;

#if ODIN_INSPECTOR
        [Title("Lifecycle")]
        [Button, HideInEditorMode]
#endif
        public void Spawn() => _world.Spawn();

#if ODIN_INSPECTOR
        [Button, HideInEditorMode]
#endif
        public void Enable() => _world.Enable();

#if ODIN_INSPECTOR
        [Button, HideInEditorMode]
#endif
        public void Disable() => _world.Disable();

#if ODIN_INSPECTOR
        [Button, HideInEditorMode]
#endif
        public void Despawn() => _world.Despawn();

#if ODIN_INSPECTOR
        [Button, HideInEditorMode]
#endif
        public void OnUpdate(float deltaTime) => _world.OnUpdate(deltaTime);

#if ODIN_INSPECTOR
        [Button, HideInEditorMode]
#endif
        public void OnFixedUpdate(float deltaTime) => _world.OnFixedUpdate(deltaTime);

#if ODIN_INSPECTOR
        [Button, HideInEditorMode]
#endif
        public void OnLateUpdate(float deltaTime) => _world.OnLateUpdate(deltaTime);
    }
}