using System;
#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

namespace Atomic.Entities
{
    public partial class SceneEntityWorld
    {
        public event Action OnInitialized
        {
            add => _world.OnInitialized += value;
            remove => _world.OnInitialized -= value;
        }

        public event Action OnDisposed
        {
            add => _world.OnDisposed += value;
            remove => _world.OnDisposed -= value;
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

        public bool Initialized => _world.Initialized;

        public bool Enabled => _world.Enabled;

#if ODIN_INSPECTOR
        [Title("Lifecycle")]
        [Button, HideInEditorMode]
#endif
        public void Init() => _world.Init();

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
        public void Dispose() => _world.Dispose();

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