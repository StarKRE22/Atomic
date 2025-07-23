using System;

#if UNITY_5_3_OR_NEWER
using UnityEngine;
#endif

namespace Atomic.Entities
{
    public class EntityRunner : EntityRunner<IEntity>, IEntityRunner
    {
    }

    /// <summary>
    /// Manages and executes lifecycle events for a collection of <see cref="IEntity"/> instances.
    /// This includes spawn, enabling, disabling, despawn, and update loops.
    /// </summary>
    public class EntityRunner<E> : EntityCollection<E>, IEntityRunner<E> where E : IEntity
    {
        /// <inheritdoc/>
        public event Action OnSpawned;

        /// <inheritdoc/>
        public event Action OnDespawned;

        /// <inheritdoc/>
        public event Action OnEnabled;

        /// <inheritdoc/>
        public event Action OnDisabled;

        /// <inheritdoc/>
        public event Action<float> OnUpdated;

        /// <inheritdoc/>
        public event Action<float> OnFixedUpdated;

        /// <inheritdoc/>
        public event Action<float> OnLateUpdated;

        /// <inheritdoc/>
        public bool Spawned => _spawned;

        /// <summary>
        /// Indicates whether the loop is enabled.
        /// </summary>
        public bool Enabled => _enabled;

        private protected bool _spawned;
        private protected bool _enabled;

        /// <inheritdoc/>
        public void Spawn()
        {
            if (_spawned)
            {
#if UNITY_5_3_OR_NEWER
                Debug.LogWarning($"{this.GetType().Name} is already spawned!");
#endif
                return;
            }

            _spawned = true;

            for (int i = 0; i < _count; i++)
                _items[i].Spawn();

            this.OnSpawned?.Invoke();
        }

        /// <inheritdoc/>
        public void Despawn()
        {
            if (_enabled)
                this.Disable();

            if (!_spawned)
                return;

            for (int i = 0; i < _count; i++)
                _items[i].Despawn();

            _spawned = false;
            this.OnDespawned?.Invoke();
        }

        /// <inheritdoc/>
        public void Enable()
        {
            if (!_spawned)
                this.Spawn();

            if (_enabled)
            {
#if UNITY_5_3_OR_NEWER
                Debug.LogWarning($"{this.GetType().Name} is already enabled!");
#endif
                return;
            }

            _enabled = true;

            for (int i = 0; i < _count; i++)
                _items[i].Enable();

            this.OnEnabled?.Invoke();
        }

        /// <inheritdoc/>
        public void Disable()
        {
            if (!_enabled)
            {
#if UNITY_5_3_OR_NEWER
                Debug.LogWarning($"{this.GetType().Name} is already disabled!");
#endif
                return;
            }

            _enabled = false;

            for (int i = 0; i < _count; i++)
                _items[i].Disable();

            this.OnDisabled?.Invoke();
        }

        /// <inheritdoc/>
        public void OnUpdate(float deltaTime)
        {
            if (!_enabled)
            {
#if UNITY_5_3_OR_NEWER
                Debug.LogWarning($"Update failed! {this.GetType().Name} is not enabled!");
#endif
                return;
            }

            for (int i = 0; i < _count; i++)
                _items[i].OnUpdate(deltaTime);

            this.OnUpdated?.Invoke(deltaTime);
        }

        /// <inheritdoc/>
        public void OnFixedUpdate(float deltaTime)
        {
            if (!_enabled)
            {
#if UNITY_5_3_OR_NEWER
                Debug.LogWarning($"Fixed update failed! {this.GetType().Name} is not enabled!");
#endif
                return;
            }

            for (int i = 0; i < _count; i++)
                _items[i].OnFixedUpdate(deltaTime);

            this.OnFixedUpdated?.Invoke(deltaTime);
        }

        /// <inheritdoc/>
        public void OnLateUpdate(float deltaTime)
        {
            if (!_enabled)
            {
#if UNITY_5_3_OR_NEWER
                Debug.LogWarning($"Late update failed! {this.GetType().Name} is not enabled!");
#endif
                return;
            }

            for (int i = 0; i < _count; i++)
                _items[i].OnLateUpdate(deltaTime);

            this.OnLateUpdated?.Invoke(deltaTime);
        }

        protected override void OnAdd(E entity)
        {
            if (_spawned) entity.Spawn();
            if (_enabled) entity.Enable();
        }

        protected override void OnRemove(E entity)
        {
            if (_enabled) entity.Disable();
            if (_spawned) entity.Despawn();
        }

        /// <inheritdoc/>
        public override void Dispose()
        {
            this.Despawn();
            base.Dispose();
        }

        /// <summary>
        /// Removes all subscribed event listeners.
        /// </summary>
        public void UnsubscribeAll()
        {
            this.OnSpawned = null;
            this.OnEnabled = null;
            this.OnDisabled = null;
            this.OnUpdated = null;
            this.OnFixedUpdated = null;
            this.OnLateUpdated = null;
            this.OnDespawned = null;
        }
    }
}