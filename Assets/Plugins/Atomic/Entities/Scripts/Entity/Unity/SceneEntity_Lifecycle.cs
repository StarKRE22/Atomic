#if UNITY_5_3_OR_NEWER
using System;
using System.Collections.Generic;
using static Atomic.Entities.InternalUtils;

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
        /// Called when the entity has been initialized.
        /// </summary>
        public event Action OnInitialized;

        /// <summary>
        /// Called when the entity is enabled.
        /// </summary>
        public event Action OnEnabled;

        /// <summary>
        /// Called when the entity is disabled.
        /// </summary>
        public event Action OnDisabled;

        /// <summary>
        /// Called when the entity is disposed.
        /// </summary>
        public event Action OnDisposed;

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
        /// Initializes the entity and all IInit behaviours.
        /// </summary>
        public void Init()
        {
            if (_initialized)
                return;
            
            _initialized = true;
            this._instanceId = EntityRegistry.Instance.Register(this);

            for (int i = 0; i < _behaviourCount; i++)
                if (_behaviours[i] is IEntityInit initBehaviour)
                    initBehaviour.Init(this);

            this.OnInitialized?.Invoke();
        }

        /// <summary>
        /// Disposes the entity and all IDispose behaviours.
        /// </summary>
        public void Dispose()
        {
            if (!_initialized)
                return;

            if (_enabled)
                this.Disable();

            for (int i = 0; i < _behaviourCount; i++)
                if (_behaviours[i] is IEntityDispose disposeBehaviour)
                    disposeBehaviour.Dispose(this);

            EntityRegistry.Instance.Unregister(this._instanceId);
            this._instanceId = UNDEFINED_INDEX;

            _initialized = false;
            this.OnDisposed?.Invoke();
        }

        /// <summary>
        /// Enables the entity and registers update behaviours.
        /// </summary>
        public void Enable()
        {
            if (!_initialized)
                this.Init();

            if (_enabled)
                return;

            _enabled = true;

            for (int i = 0; i < _behaviourCount; i++)
                this.EnableBehaviour(_behaviours[i]);

            this.OnEnabled?.Invoke();
        }

        /// <summary>
        /// Disables the entity and unregisters update behaviours.
        /// </summary>
        public void Disable()
        {
            if (!_enabled)
                return;

            for (int i = 0; i < _behaviourCount; i++)
                this.DisableBehaviour(_behaviours[i]);

            _enabled = false;
            this.OnDisabled?.Invoke();
        }

        /// <summary>
        /// Invokes OnUpdate for all registered IUpdate behaviours.
        /// </summary>
        public void OnUpdate(float deltaTime)
        {
            if (!_enabled)
                return;

            for (int i = 0; i < this.updateCount && _enabled; i++)
                this.updates[i].OnUpdate(this, deltaTime);

            this.OnUpdated?.Invoke(deltaTime);
        }

        /// <summary>
        /// Invokes OnFixedUpdate for all registered IFixedUpdate behaviours.
        /// </summary>
        public void OnFixedUpdate(float deltaTime)
        {
            if (!_enabled)
                return;

            for (int i = 0; i < this.fixedUpdateCount && _enabled; i++)
                this.fixedUpdates[i].OnFixedUpdate(this, deltaTime);

            this.OnFixedUpdated?.Invoke(deltaTime);
        }

        /// <summary>
        /// Invokes OnLateUpdate for all registered ILateUpdate behaviours.
        /// </summary>
        public void OnLateUpdate(float deltaTime)
        {
            if (!_enabled)
                return;

            for (int i = 0; i < this.lateUpdateCount && _enabled; i++)
                this.lateUpdates[i].OnLateUpdate(this, deltaTime);

            this.OnLateUpdated?.Invoke(deltaTime);
        }

        private void EnableBehaviour(IEntityBehaviour behaviour)
        {
            if (behaviour is IEntityEnable entityEnable)
                entityEnable.Enable(this);

            if (behaviour is IEntityUpdate update)
                Add(ref this.updates, ref this.updateCount, update);

            if (behaviour is IEntityFixedUpdate fixedUpdate)
                Add(ref this.fixedUpdates, ref this.fixedUpdateCount, fixedUpdate);

            if (behaviour is IEntityLateUpdate lateUpdate)
                Add(ref this.lateUpdates, ref this.lateUpdateCount, lateUpdate);
        }

        private void DisableBehaviour(IEntityBehaviour behaviour)
        {
            if (behaviour is IEntityDisable entityDisable)
                entityDisable.Disable(this);

            if (behaviour is IEntityUpdate update)
                Remove(ref this.updates, ref this.updateCount, update, s_updateComparer);

            if (behaviour is IEntityFixedUpdate fixedUpdate)
                Remove(ref this.fixedUpdates, ref this.fixedUpdateCount, fixedUpdate, s_fixedUpdateComparer);

            if (behaviour is IEntityLateUpdate lateUpdate)
                Remove(ref this.lateUpdates, ref this.lateUpdateCount, lateUpdate, s_lateUpdateComparer);
        }
    }
}
#endif