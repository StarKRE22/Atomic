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
        private static readonly IEqualityComparer<IEntityTick> s_updateComparer =
            EqualityComparer<IEntityTick>.Default;

        /// <summary>
        /// Equality comparer for IFixedUpdate behaviours.
        /// </summary>
        private static readonly IEqualityComparer<IEntityFixedTick> s_fixedUpdateComparer =
            EqualityComparer<IEntityFixedTick>.Default;

        /// <>
        /// Equality comparer forLateUpdate behaviours.
        /// </summary>
        private static readonly IEqualityComparer<IEntityLateTick> s_lateUpdateComparer =
            EqualityComparer<IEntityLateTick>.Default;

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
        public event Action<float> OnTicked;

        /// <summary>
        /// Called every fixed frame while the entity is enabled.
        /// </summary>
        public event Action<float> OnFixedTicked;

        /// <summary>
        /// Called every late frame while the entity is enabled.
        /// </summary>
        public event Action<float> OnLateTicked;

        /// <summary>
        /// Indicates whether the entity has been initialized.
        /// </summary>
        ///
#if ODIN_INSPECTOR
        [FoldoutGroup("Debug", 0)]
        [LabelText("Initialized")]
        [ShowInInspector, ReadOnly, PropertyOrder(98)]

#endif
        public bool Initialized => _initialized;

        /// <summary>
        /// Indicates whether the entity is currently enabled.
        /// </summary>
#if ODIN_INSPECTOR
        [FoldoutGroup("Debug", order: 1)]
        [ShowInInspector, ReadOnly, PropertyOrder(99)]
        [LabelText("Enabled")]
#endif
        public bool Enabled => _enabled;

        private bool _initialized;
        private bool _enabled;

        private IEntityTick[] updates;
        private IEntityFixedTick[] fixedUpdates;
        private IEntityLateTick[] lateUpdates;

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
        /// <item><description>Calls <c>Update</c> on all <see cref="IEntityTick"/> behaviours.</description></item>
        /// <item><description>Triggers <see cref="OnTicked"/> event.</description></item>
        /// <item><description>Can be invoked only if the entity is enabled.</description></item>
        /// </list>
        /// </remarks>
        public void Tick(float deltaTime)
        {
            if (!_enabled)
                return;

            for (int i = 0; i < this.updateCount; i++)
                this.updates[i].Tick(this, deltaTime);

            this.OnTicked?.Invoke(deltaTime);
        }

        /// <summary>
        /// Calls FixedUpdate on the entity.
        /// </summary>
        /// <param name="deltaTime">Fixed delta time.</param>
        /// <remarks>
        /// <list type="bullet">
        /// <item><description>Calls <c>FixedUpdate</c> on all <see cref="IEntityFixedTick"/> behaviours.</description></item>
        /// <item><description>Triggers <see cref="OnFixedTicked"/> event.</description></item>
        /// <item><description>Can be invoked only if the entity is enabled.</description></item>
        /// </list>
        /// </remarks>
        public void FixedTick(float deltaTime)
        {
            if (!_enabled)
                return;

            for (int i = 0; i < this.fixedUpdateCount; i++)
                this.fixedUpdates[i].FixedTick(this, deltaTime);

            this.OnFixedTicked?.Invoke(deltaTime);
        }

        /// <summary>
        /// Calls LateUpdate on the entity.
        /// </summary>
        /// <param name="deltaTime">Time elapsed since last frame.</param>
        /// <remarks>
        /// <list type="bullet">
        /// <item><description>Calls <c>LateUpdate</c> on all <see cref="IEntityLateTick"/> behaviours.</description></item>
        /// <item><description>Triggers <see cref="OnLateTicked"/> event.</description></item>
        /// <item><description>Can be invoked only if the entity is enabled.</description></item>
        /// </list>
        /// </remarks>
        public void LateTick(float deltaTime)
        {
            if (!_enabled)
                return;

            for (int i = 0; i < this.lateUpdateCount; i++)
                this.lateUpdates[i].LateTick(this, deltaTime);

            this.OnLateTicked?.Invoke(deltaTime);
        }

        #region Dispose

        /// <summary>
        /// Cleans up all resources used by the entity.
        /// </summary>
        /// <remarks>
        /// <list type="bullet">
        /// <item><description>Transitions an entity to not <c>Initialized</c> state.</description></item>
        /// <item><description>Calls <see cref="IEntityDispose.Dispose"/> on all registered behaviours implementing <see cref="IEntityDispose"/>.</description></item>
        /// <item><description>Clears all tags, values, and behaviours.</description></item>
        /// <item><description>Unsubscribes from all events.</description></item>
        /// <item><description>Unregisters the entity from <see cref="EntityRegistry"/>.</description></item>
        /// <item><description>Disposes stored values if <see cref="disposeValues"/> is <c>true</c>.</description></item>
        /// <item><description>If the entity is enabled, this method automatically calls <see cref="Disable"/>.</description></item>
        /// <item><description>If the entity is not initialized yet, this method does not call <see cref="IEntityDispose.Dispose"/> or <see cref="OnDisposed"/>.</description></item>
        /// </list>
        /// </remarks>
        public void Dispose()
        {
            this.OnDispose();
            this.Deinitialize();

            if (this.disposeValues)
                this.DisposeValues();

            this.ClearTags();
            this.ClearValues();
            this.ClearBehaviours();

            this.OnStateChanged?.Invoke(this);

            this.UnsubscribeEvents();
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void Deinitialize()
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual void OnDispose()
        {
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void UnsubscribeEvents()
        {
            this.OnStateChanged = null;

            this.OnInitialized = null;
            this.OnDisposed = null;
            this.OnEnabled = null;
            this.OnDisabled = null;

            this.OnTicked = null;
            this.OnFixedTicked = null;
            this.OnLateTicked = null;

            this.OnBehaviourAdded = null;
            this.OnBehaviourDeleted = null;

            this.OnValueAdded = null;
            this.OnValueDeleted = null;
            this.OnValueChanged = null;

            this.OnTagAdded = null;
            this.OnTagDeleted = null;
        }

        #endregion
    }
}
#endif