using System;
using System.Collections.Generic;
using static Atomic.Entities.InternalUtils;

namespace Atomic.Entities
{
    public partial class Entity
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

        /// <summary>
        /// Equality comparer for ILateUpdate behaviours.
        /// </summary>
        private static readonly IEqualityComparer<IEntityLateUpdate> s_lateUpdateComparer =
            EqualityComparer<IEntityLateUpdate>.Default;

        /// <summary>
        /// Called when the entity has been initialized.
        /// </summary>
        public event Action OnSpawned;

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
        public event Action OnDespawned;

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
        /// Indicates whether the entity has been spawned.
        /// </summary>
        public bool Spawned => _spawned;

        /// <summary>
        /// Indicates whether the entity is currently enabled.
        /// </summary>
        public bool Enabled => _enabled;

        private bool _spawned;
        private bool _enabled;

        private IEntityUpdate[] _updates;
        private IEntityFixedUpdate[] _fixedUpdates;
        private IEntityLateUpdate[] _lateUpdates;

        private int _updateCount;
        private int _fixedUpdateCount;
        private int _lateUpdateCount;

        /// <summary>
        /// Spawns the entity.
        /// </summary>
        public void Spawn()
        {
            if (_spawned)
                return;

            _spawned = true;

            for (int i = 0; i < _behaviourCount; i++)
                if (_behaviours[i] is IEntitySpawn spawnBehaviour)
                    spawnBehaviour.Spawn(this);

            this.OnSpawn();
            this.OnSpawned?.Invoke();
            this.OnStateChanged?.Invoke();
        }

        protected virtual void OnSpawn()
        {
        }

        /// <summary>
        /// Despawns the entity.
        /// </summary>
        public void Despawn()
        {
            if (!_spawned)
                return;

            if (_enabled)
                this.Disable();

            this.OnDespawn();

            for (int i = 0; i < _behaviourCount; i++)
                if (_behaviours[i] is IEntityDespawn despawnBehaviour)
                    despawnBehaviour.Despawn(this);

            _spawned = false;
            this.OnDespawned?.Invoke();
            this.OnStateChanged?.Invoke();
        }

        protected virtual void OnDespawn()
        {
        }

        /// <summary>
        /// Enables the entity and registers update behaviours.
        /// </summary>
        public void Enable()
        {
            if (!_spawned)
                this.Spawn();

            if (_enabled)
                return;

            _enabled = true;

            for (int i = 0; i < _behaviourCount; i++)
                this.EnableBehaviour(_behaviours[i]);

            this.EnableInternal();
            this.OnEnabled?.Invoke();
            this.OnStateChanged?.Invoke();
        }

        protected virtual void EnableInternal()
        {
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

            this.OnStateChanged?.Invoke();
            this.OnDisabled?.Invoke();
        }

        /// <summary>
        /// Invokes OnUpdate for all registered IUpdate behaviours.
        /// </summary>
        public void OnUpdate(float deltaTime)
        {
            if (!_enabled)
                return;

            for (int i = 0; i < this._updateCount && _enabled; i++)
                this._updates[i].OnUpdate(this, deltaTime);

            this.OnUpdated?.Invoke(deltaTime);
        }

        /// <summary>
        /// Invokes OnFixedUpdate for all registered IFixedUpdate behaviours.
        /// </summary>
        public void OnFixedUpdate(float deltaTime)
        {
            if (!_enabled)
                return;

            for (int i = 0; i < this._fixedUpdateCount && _enabled; i++)
                this._fixedUpdates[i].OnFixedUpdate(this, deltaTime);

            this.OnFixedUpdated?.Invoke(deltaTime);
        }

        /// <summary>
        /// Invokes OnLateUpdate for all registered ILateUpdate behaviours.
        /// </summary>
        public void OnLateUpdate(float deltaTime)
        {
            if (!_enabled)
                return;

            for (int i = 0; i < this._lateUpdateCount && _enabled; i++)
                this._lateUpdates[i].OnLateUpdate(this, deltaTime);

            this.OnLateUpdated?.Invoke(deltaTime);
        }

        private void EnableBehaviour(IEntityBehaviour behaviour)
        {
            if (behaviour is IEntityEnable entityEnable)
                entityEnable.Enable(this);

            if (behaviour is IEntityUpdate update)
                Add(ref this._updates, ref this._updateCount, update);

            if (behaviour is IEntityFixedUpdate fixedUpdate)
                Add(ref this._fixedUpdates, ref this._fixedUpdateCount, fixedUpdate);

            if (behaviour is IEntityLateUpdate lateUpdate)
                Add(ref this._lateUpdates, ref this._lateUpdateCount, lateUpdate);
        }

        private void DisableBehaviour(IEntityBehaviour behaviour)
        {
            if (behaviour is IEntityDisable entityDisable)
                entityDisable.Disable(this);

            if (behaviour is IEntityUpdate update)
                Remove(ref this._updates, ref this._updateCount, update, s_updateComparer);

            if (behaviour is IEntityFixedUpdate fixedUpdate)
                Remove(ref this._fixedUpdates, ref this._fixedUpdateCount, fixedUpdate, s_fixedUpdateComparer);

            if (behaviour is IEntityLateUpdate lateUpdate)
                Remove(ref this._lateUpdates, ref this._lateUpdateCount, lateUpdate, s_lateUpdateComparer);
        }
    }
}