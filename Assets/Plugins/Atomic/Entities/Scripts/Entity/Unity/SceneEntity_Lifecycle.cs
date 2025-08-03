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
        public event Action OnActivated;

        /// <summary>
        /// Called when the entity is disabled.
        /// </summary>
        public event Action OnInactivated;

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
        public bool IsSpawned => _spawned;

        /// <summary>
        /// Indicates whether the entity is currently enabled.
        /// </summary>
#if ODIN_INSPECTOR
        [FoldoutGroup("Debug")]
        [ShowInInspector, ReadOnly]
        [LabelText("Enabled")]
#endif
        public bool IsActive => _active;

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

            _spawned = true;

            for (int i = 0; i < _behaviourCount; i++)
                if (_behaviours[i] is IEntitySpawned spawnBehaviour)
                    spawnBehaviour.OnSpawn(this);

            this.OnStateChanged?.Invoke();
            this.OnSpawned?.Invoke();
        }

        /// <summary>
        /// Despawns the entity.
        /// </summary>
        public void Despawn()
        {
            if (!_spawned)
                return;

            if (_active)
                this.Inactivate();

            for (int i = 0; i < _behaviourCount; i++)
                if (_behaviours[i] is IEntityDespawned despawnBehaviour)
                    despawnBehaviour.OnDespawn(this);
            
            _spawned = false;
            this.OnStateChanged?.Invoke();
            this.OnDespawned?.Invoke();
        }

        /// <summary>
        /// Enables the entity and registers update behaviours.
        /// </summary>
        public void Activate()
        {
            if (!_spawned)
                this.Spawn();

            if (_active)
                return;

            _active = true;

            for (int i = 0; i < _behaviourCount; i++)
                this.EnableBehaviour(_behaviours[i]);

            this.OnStateChanged?.Invoke();
            this.OnActivated?.Invoke();
        }

        /// <summary>
        /// Disables the entity and unregisters update behaviours.
        /// </summary>
        public void Inactivate()
        {
            if (!_active)
                return;

            for (int i = 0; i < _behaviourCount; i++)
                this.DisableBehaviour(_behaviours[i]);

            _active = false;
            this.OnStateChanged?.Invoke();
            this.OnInactivated?.Invoke();
        }

        /// <summary>
        /// Invokes OnUpdate for all registered IUpdate behaviours.
        /// </summary>
        public void OnUpdate(float deltaTime)
        {
            if (!_active)
                return;

            for (int i = 0; i < this.updateCount && _active; i++)
                this.updates[i].OnUpdate(this, deltaTime);
            
            this.OnUpdated?.Invoke(deltaTime);
        }

        /// <summary>
        /// Invokes OnFixedUpdate for all registered IFixedUpdate behaviours.
        /// </summary>
        public void OnFixedUpdate(float deltaTime)
        {
            if (!_active)
                return;

            for (int i = 0; i < this.fixedUpdateCount && _active; i++)
                this.fixedUpdates[i].OnFixedUpdate(this, deltaTime);

            this.OnFixedUpdated?.Invoke(deltaTime);
        }

        /// <summary>
        /// Invokes OnLateUpdate for all registered ILateUpdate behaviours.
        /// </summary>
        public void OnLateUpdate(float deltaTime)
        {
            if (!_active)
                return;

            for (int i = 0; i < this.lateUpdateCount && _active; i++)
                this.lateUpdates[i].OnLateUpdate(this, deltaTime);

            this.OnLateUpdated?.Invoke(deltaTime);
        }

        private void EnableBehaviour(IEntityBehaviour behaviour)
        {
            if (behaviour is IEntityActive entityEnable)
                entityEnable.OnActive(this);

            if (behaviour is IEntityUpdate update)
                Add(ref this.updates, ref this.updateCount, update);

            if (behaviour is IEntityFixedUpdate fixedUpdate)
                Add(ref this.fixedUpdates, ref this.fixedUpdateCount, fixedUpdate);

            if (behaviour is IEntityLateUpdate lateUpdate)
                Add(ref this.lateUpdates, ref this.lateUpdateCount, lateUpdate);
        }

        private void DisableBehaviour(IEntityBehaviour behaviour)
        {
            if (behaviour is IEntityInactive entityDisable)
                entityDisable.OnInactive(this);

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