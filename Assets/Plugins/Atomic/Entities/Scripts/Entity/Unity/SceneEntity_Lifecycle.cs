#if UNITY_5_3_OR_NEWER
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

namespace Atomic.Entities
{
    /// <summary>
    /// Provides lifecycle and update phase event bindings for a <see cref="SceneEntity"/>,
    /// delegating lifecycle control and state to the internal <see cref="Entity"/> instance.
    /// </summary>
    public partial class SceneEntity
    {
        /// <summary>
        /// Equality comparer for IUpdate behaviours.
        /// </summary>
        private static readonly IEqualityComparer<IEntityUpdate> s_updateComparer =
            EqualityComparer<IEntityUpdate>.Default;

        /// <summary>
        /// Equality comparer for IFixedUpdate behaviours.
        /// </summary>
        private static readonly IEqualityComparer<IEntityFixedUpdate> s_fixedUpdateComparer =
            EqualityComparer<IEntityFixedUpdate>.Default;

        /// <>
        /// Equality comparer forLateUpdate behaviours.
        /// </summary>
        private static readonly IEqualityComparer<IEntityLateUpdate> s_lateUpdateComparer =
            EqualityComparer<IEntityLateUpdate>.Default;

        /// <summary>
        /// Called when the entity has been initialized
        /// </summary>
        public event Action OnInitialized;

        /// <summary>
        /// Called when the entity is disposed.
        /// </summary>
        public event Action OnDisposed;

        /// <summary>
        /// Called when the entity is enabled.
        /// </summary>
        public event Action OnEnabled;

        /// <summary>
        /// Called when the entity is disabled.
        /// </summary>
        public event Action OnDisabled;

        /// <summary>
        /// Called every frame while the entity is enabled.
        /// </summary>
        public event Action<float> OnUpdated;

        /// <summary>
        /// Called every fixed frame while the entity is enabled.
        /// </summary>
        public event Action<float> OnFixedUpdated;

        /// <summary>
        /// Called every late frame while the entity is enabled.
        /// </summary>
        public event Action<float> OnLateUpdated;

        /// <summary>
        /// Indicates whether the entity has been initialized.
        /// </summary>
        ///
#if ODIN_INSPECTOR
        [FoldoutGroup("Debug")]
        [LabelText("Initialized")]
        [ShowInInspector, ReadOnly]

#endif
        public bool Initialized => _initialized;

        /// <summary>
        /// Indicates whether the entity is currently enabled.
        /// </summary>
#if ODIN_INSPECTOR
        [FoldoutGroup("Debug")]
        [ShowInInspector, ReadOnly]
        [LabelText("Enabled")]
#endif
        public bool Enabled => _enabled;

        private bool _initialized;
        private bool _enabled;

        private IEntityUpdate[] updates;
        private IEntityFixedUpdate[] fixedUpdates;
        private IEntityLateUpdate[] lateUpdates;

        private int updateCount;
        private int fixedUpdateCount;
        private int lateUpdateCount;

        /// <summary>
        /// Initializes the entity.
        /// </summary>
        /// <remarks>
        /// <list type="bullet">
        /// <item><description>Transitions an entity to <c>Initialized</c> state.</description></item>
        /// <item><description>Calls <c>Init</c> on all behaviours implementing <see cref="IEntityInit"/>.</description></item>
        /// <item><description>Triggers <see cref="OnInitialized"/> event.</description></item>
        /// <item><description>If the entity is already initialized, this method does nothing.</description></item>
        /// </list>
        /// </remarks>
        public void Init()
        {
            if (_initialized)
                return;

            for (int i = 0; i < _behaviourCount; i++)
                if (_behaviours[i] is IEntityInit behaviour)
                    behaviour.Init(this);

            _initialized = true;

            this.OnStateChanged?.Invoke(this);
            this.OnInitialized?.Invoke();
        }

        /// <summary>
        /// Enables the entity for updates.
        /// </summary>
        /// <remarks>
        /// <list type="bullet">
        /// <item><description>Transitions an entity to <c>Enabled</c> state.</description></item>
        /// <item><description>Calls <c>Enable</c> on all behaviours implementing <see cref="IEntityEnable"/>.</description></item>
        /// <item><description>Triggers <see cref="OnEnabled"/> event.</description></item>
        /// <item><description>If the entity is not initialized yet, this method also initializes it.</description></item>
        /// <item><description>If the entity is already enabled, this method does nothing.</description></item>
        /// </list>
        /// </remarks>
        public void Enable()
        {
            this.Init();

            if (_enabled)
                return;

            for (int i = 0; i < _behaviourCount; i++)
                this.EnableBehaviour(_behaviours[i]);

            _enabled = true;

            this.OnStateChanged?.Invoke(this);
            this.OnEnabled?.Invoke();
        }

        /// <summary>
        /// Disables the entity for updates.
        /// </summary>
        /// <remarks>
        /// <list type="bullet">
        /// <item><description>Transitions an entity to a <c>not Enabled</c> state.</description></item>
        /// <item><description>Calls <c>Disable</c> on all behaviours implementing <see cref="IEntityDisable"/>.</description></item>
        /// <item><description>Triggers <see cref="OnDisabled"/> event.</description></item>
        /// <item><description>If the entity is not enabled yet, this method does nothing.</description></item>
        /// </list>
        /// </remarks>
        public void Disable()
        {
            if (!_enabled)
                return;

            for (int i = 0; i < _behaviourCount; i++)
                this.DisableBehaviour(_behaviours[i]);

            _enabled = false;

            this.OnStateChanged?.Invoke(this);
            this.OnDisabled?.Invoke();
        }

        /// <summary>
        /// Calls Update on the entity.
        /// </summary>
        /// <param name="deltaTime">Time elapsed since the last frame.</param>
        /// <remarks>
        /// <list type="bullet">
        /// <item><description>Calls <c>Update</c> on all <see cref="IEntityUpdate"/> behaviours.</description></item>
        /// <item><description>Triggers <see cref="OnUpdated"/> event.</description></item>
        /// <item><description>Can be invoked only if the entity is enabled.</description></item>
        /// </list>
        /// </remarks>
        public void OnUpdate(float deltaTime)
        {
            if (!_enabled)
                return;

            for (int i = 0; i < this.updateCount; i++)
                this.updates[i].Update(this, deltaTime);

            this.OnUpdated?.Invoke(deltaTime);
        }

        /// <summary>
        /// Calls FixedUpdate on the entity.
        /// </summary>
        /// <param name="deltaTime">Fixed delta time.</param>
        /// <remarks>
        /// <list type="bullet">
        /// <item><description>Calls <c>FixedUpdate</c> on all <see cref="IEntityFixedUpdate"/> behaviours.</description></item>
        /// <item><description>Triggers <see cref="OnFixedUpdated"/> event.</description></item>
        /// <item><description>Can be invoked only if the entity is enabled.</description></item>
        /// </list>
        /// </remarks>
        public void OnFixedUpdate(float deltaTime)
        {
            if (!_enabled)
                return;

            for (int i = 0; i < this.fixedUpdateCount; i++)
                this.fixedUpdates[i].FixedUpdate(this, deltaTime);

            this.OnFixedUpdated?.Invoke(deltaTime);
        }

        /// <summary>
        /// Calls LateUpdate on the entity.
        /// </summary>
        /// <param name="deltaTime">Time elapsed since last frame.</param>
        /// <remarks>
        /// <list type="bullet">
        /// <item><description>Calls <c>LateUpdate</c> on all <see cref="IEntityLateUpdate"/> behaviours.</description></item>
        /// <item><description>Triggers <see cref="OnLateUpdated"/> event.</description></item>
        /// <item><description>Can be invoked only if the entity is enabled.</description></item>
        /// </list>
        /// </remarks>
        public void OnLateUpdate(float deltaTime)
        {
            if (!_enabled)
                return;

            for (int i = 0; i < this.lateUpdateCount; i++)
                this.lateUpdates[i].LateUpdate(this, deltaTime);

            this.OnLateUpdated?.Invoke(deltaTime);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void DisposeInternal()
        {
            if (!_initialized)
                return;

            this.Disable();

            for (int i = 0; i < _behaviourCount; i++)
                if (_behaviours[i] is IEntityDispose behaviour)
                    behaviour.Dispose(this);

            _initialized = false;
            this.OnDisposed?.Invoke();
        }
    }
}
#endif