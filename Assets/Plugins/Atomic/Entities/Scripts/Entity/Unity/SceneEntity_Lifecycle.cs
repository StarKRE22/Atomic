#if UNITY_5_3_OR_NEWER
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using static Atomic.Entities.EntityUtils;

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
        /// Called when the entity has been spawned.
        /// </summary>
        public event Action OnSpawned;

        /// <summary>
        /// Called when the entity is despawned.
        /// </summary>
        public event Action OnDespawned;

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
        [LabelText("Spawned")]
        [ShowInInspector, ReadOnly]

#endif
        public bool IsSpawned => _spawned;

        /// <summary>
        /// Indicates whether the entity is currently enabled.
        /// </summary>
#if ODIN_INSPECTOR
        [FoldoutGroup("Debug")]
        [ShowInInspector, ReadOnly]
        [LabelText("Active")]
#endif
        public bool Enabled => _active;

        private bool _spawned;
        private bool _active;

        private IEntityUpdate[] updates;
        private IEntityFixedUpdate[] fixedUpdates;
        private IEntityLateUpdate[] lateUpdates;

        private int updateCount;
        private int fixedUpdateCount;
        private int lateUpdateCount;

        /// <summary>
        /// Spawns the entity.
        /// </summary>
        public void Spawn()
        {
            if (_spawned)
                return;

            this.ProcessSpawn();
            _spawned = true;

            this.OnStateChanged?.Invoke();
            this.OnSpawned?.Invoke();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual void ProcessSpawn()
        {
            for (int i = 0; i < _behaviourCount; i++)
                if (_behaviours[i] is IEntityInit spawnBehaviour)
                    spawnBehaviour.Init(this);
        }

        /// <summary>
        /// Despawns the entity.
        /// </summary>
        public void Despawn()
        {
            if (!_spawned)
                return;

            if (_active)
                this.Disable();

            this.ProcessDespawn();
            _spawned = false;

            this.OnStateChanged?.Invoke();
            this.OnDespawned?.Invoke();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual void ProcessDespawn()
        {
            for (int i = 0; i < _behaviourCount; i++)
                if (_behaviours[i] is IEntityDispose despawnBehaviour)
                    despawnBehaviour.Dispose(this);
        }

        /// <summary>
        /// Enables the entity and registers update behaviours.
        /// </summary>
        public void Enable()
        {
            if (!_spawned)
                this.Spawn();

            if (_active)
                return;

            this.ProcessActivate();
            _active = true;

            this.OnStateChanged?.Invoke();
            this.OnEnabled?.Invoke();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual void ProcessActivate()
        {
            for (int i = 0; i < _behaviourCount; i++)
                this.ActivateBehaviour(_behaviours[i]);
        }

        /// <summary>
        /// Disables the entity and unregisters update behaviours.
        /// </summary>
        public void Disable()
        {
            if (!_active)
                return;

            this.ProcessInactivate();
            _active = false;

            this.OnStateChanged?.Invoke();
            this.OnDisabled?.Invoke();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual void ProcessInactivate()
        {
            for (int i = 0; i < _behaviourCount; i++)
                this.InactivateBehaviour(_behaviours[i]);
        }

        /// <summary>
        /// Invokes OnUpdate for all registered IUpdate behaviours.
        /// </summary>
        public void OnUpdate(float deltaTime)
        {
            if (!_active)
                return;

            this.ProcessUpdate(deltaTime);
            this.OnUpdated?.Invoke(deltaTime);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual void ProcessUpdate(float deltaTime)
        {
            for (int i = 0; i < this.updateCount && _active; i++)
                this.updates[i].Update(this, deltaTime);
        }

        /// <summary>
        /// Invokes OnFixedUpdate for all registered IFixedUpdate behaviours.
        /// </summary>
        public void OnFixedUpdate(float deltaTime)
        {
            if (!_active)
                return;

            this.ProcessFixedUpdate(deltaTime);
            this.OnFixedUpdated?.Invoke(deltaTime);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual void ProcessFixedUpdate(float deltaTime)
        {
            for (int i = 0; i < this.fixedUpdateCount && _active; i++)
                this.fixedUpdates[i].FixedUpdate(this, deltaTime);
        }

        /// <summary>
        /// Invokes OnLateUpdate for all registered ILateUpdate behaviours.
        /// </summary>
        public void OnLateUpdate(float deltaTime)
        {
            if (!_active)
                return;

            this.ProcessLateUpdate(deltaTime);
            this.OnLateUpdated?.Invoke(deltaTime);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual void ProcessLateUpdate(float deltaTime)
        {
            for (int i = 0; i < this.lateUpdateCount && _active; i++)
                this.lateUpdates[i].LateUpdate(this, deltaTime);
        }

        private void ActivateBehaviour(IEntityBehaviour behaviour)
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

        private void InactivateBehaviour(IEntityBehaviour behaviour)
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