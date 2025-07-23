using System;
using System.Collections.Generic;
using UnityEngine;

#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

namespace Atomic.Entities
{
    public class EntityWorld : EntityWorld<IEntity>
    {
    }

    public class EntityWorld<E> : EntityCollection<E>, IEntityWorld<E> where E : IEntity
    {
        public override event Action OnStateChanged;

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
        
        /// <inheritdoc/>
#if ODIN_INSPECTOR
        [ShowInInspector]
#endif
        public string Name
        {
            get => _name;
            set => _name = value;
        }

        private protected bool _spawned;
        private protected bool _enabled;

        private string _name;

        public EntityWorld() => _name = string.Empty;

        public EntityWorld(params E[] entities)
        {
            _name = string.Empty;
            this.AddRange(entities);
        }

        public EntityWorld(string name = null, params E[] entities)
        {
            _name = name;
            this.AddRange(entities);
        }

        public EntityWorld(string name, IEnumerable<E> entities)
        {
            _name = name;
            this.AddRange(entities);
        }
        
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

            this.OnStateChanged?.Invoke();
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
            this.OnStateChanged?.Invoke();
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

            this.OnStateChanged?.Invoke();
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

            this.OnStateChanged?.Invoke();
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
        public override void UnsubscribeAll()
        {
            base.UnsubscribeAll();

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