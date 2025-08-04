using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
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
        public event Action OnActivated;

        /// <summary>
        /// Called when the entity is disabled.
        /// </summary>
        public event Action OnInactivated;

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
        public bool IsSpawned => _spawned;

        /// <summary>
        /// Indicates whether the entity is currently enabled.
        /// </summary>
        public bool IsActive => _active;

        private bool _spawned;
        private bool _active;

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

            this.ProcessSpawn();
            _spawned = true;

            this.OnSpawned?.Invoke();
            this.OnStateChanged?.Invoke();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual void ProcessSpawn()
        {
            for (int i = 0; i < _behaviourCount; i++)
                if (_behaviours[i] is IEntitySpawn spawnBehaviour)
                    spawnBehaviour.OnSpawn(this);
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

            this.ProcessDespawn();
            _spawned = false;

            this.OnDespawned?.Invoke();
            this.OnStateChanged?.Invoke();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual void ProcessDespawn()
        {
            for (int i = 0; i < _behaviourCount; i++)
                if (_behaviours[i] is IEntityDespawn despawnBehaviour)
                    despawnBehaviour.OnDespawn(this);
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

            this.ProcessActivate();
            _active = true;

            this.OnActivated?.Invoke();
            this.OnStateChanged?.Invoke();
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
        public void Inactivate()
        {
            if (!_active)
                return;

            this.ProcessInactivate();
            _active = false;

            this.OnStateChanged?.Invoke();
            this.OnInactivated?.Invoke();
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
            for (int i = 0; i < _updateCount && _active; i++)
                _updates[i].OnUpdate(this, deltaTime);
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
            for (int i = 0; i < _fixedUpdateCount && _active; i++)
                _fixedUpdates[i].OnFixedUpdate(this, deltaTime);
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
            for (int i = 0; i < _lateUpdateCount && _active; i++)
                _lateUpdates[i].OnLateUpdate(this, deltaTime);
        }

        private void ActivateBehaviour(IEntityBehaviour behaviour)
        {
            if (behaviour is IEntityActive activate)
                activate.OnActive(this);

            if (behaviour is IEntityUpdate update)
                Add(ref _updates, ref _updateCount, update);

            if (behaviour is IEntityFixedUpdate fixedUpdate)
                Add(ref _fixedUpdates, ref _fixedUpdateCount, fixedUpdate);

            if (behaviour is IEntityLateUpdate lateUpdate)
                Add(ref _lateUpdates, ref _lateUpdateCount, lateUpdate);
        }

        private void InactivateBehaviour(IEntityBehaviour behaviour)
        {
            if (behaviour is IEntityInactive inactive)
                inactive.OnInactive(this);

            if (behaviour is IEntityUpdate update)
                Remove(ref _updates, ref _updateCount, update, s_updateComparer);

            if (behaviour is IEntityFixedUpdate fixedUpdate)
                Remove(ref _fixedUpdates, ref _fixedUpdateCount, fixedUpdate, s_fixedUpdateComparer);

            if (behaviour is IEntityLateUpdate lateUpdate)
                Remove(ref _lateUpdates, ref _lateUpdateCount, lateUpdate, s_lateUpdateComparer);
        }
    }
}