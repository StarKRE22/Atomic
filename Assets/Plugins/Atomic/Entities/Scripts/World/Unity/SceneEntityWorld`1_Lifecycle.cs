#if UNITY_5_3_OR_NEWER
using System;
using UnityEngine;

#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

namespace Atomic.Entities
{
    public partial class SceneEntityWorld<E>
    {
        /// <inheritdoc />
        public event Action OnEnabled
        {
            add => _world.OnEnabled += value;
            remove => _world.OnEnabled -= value;
        }

        /// <inheritdoc />
        public event Action OnDisabled
        {
            add => _world.OnDisabled += value;
            remove => _world.OnDisabled -= value;
        }

        /// <inheritdoc />
        public event Action<float> OnTicked
        {
            add => _world.OnTicked += value;
            remove => _world.OnTicked -= value;
        }

        /// <inheritdoc />
        public event Action<float> OnFixedTicked
        {
            add => _world.OnFixedTicked += value;
            remove => _world.OnFixedTicked -= value;
        }

        /// <inheritdoc />
        public event Action<float> OnLateTicked
        {
            add => _world.OnLateTicked += value;
            remove => _world.OnLateTicked -= value;
        }

        /// <inheritdoc />
#if ODIN_INSPECTOR
        [Title("Debug")]
        [ShowInInspector, ReadOnly, HideInEditorMode]
#endif
        public bool Enabled => _world.Enabled;

        /// <inheritdoc />
#if ODIN_INSPECTOR
        [Button, HideInEditorMode]
#endif
        public void Enable() => _world.Enable();

        /// <inheritdoc />
#if ODIN_INSPECTOR
        [Button, HideInEditorMode]
#endif
        public void Disable() => _world.Disable();

        /// <inheritdoc />
#if ODIN_INSPECTOR
        [Button, HideInEditorMode]
#endif
        public void Tick(float deltaTime) => _world.Tick(deltaTime);

        /// <inheritdoc />
#if ODIN_INSPECTOR
        [Button, HideInEditorMode]
#endif
        public void FixedTick(float deltaTime) => _world.FixedTick(deltaTime);

        /// <inheritdoc />
#if ODIN_INSPECTOR
        [Button, HideInEditorMode]
#endif
        public void LateTick(float deltaTime) => _world.LateTick(deltaTime);

        /// <inheritdoc />
        public void Dispose() => _world?.Dispose();
    }
}
#endif