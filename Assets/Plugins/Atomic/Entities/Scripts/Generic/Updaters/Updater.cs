using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Atomic.Entities
{
    public class Updater<E> where E : IEntity<E>
    {
        private static readonly IEqualityComparer<E> s_entityComparer = EqualityComparer<E>.Default;
        
        public event Action OnInitialized;
        public event Action OnDisposed;
        public event Action OnEnabled;
        public event Action OnDisabled;

        public event Action<float> OnUpdated;
        public event Action<float> OnFixedUpdated;
        public event Action<float> OnLateUpdated;

        private E[] _entities;
        private int _entityCount;

        public bool Initialized => _initialized;
        public bool Enabled => _enabled;

        private bool _initialized;
        private bool _enabled;

        public Updater()
        {
            _entities = Array.Empty<E>();
            _entityCount = 0;
        }

        public Updater(IEnumerable<E> entities)
        {
            _entities = entities.ToArray();
            _entityCount = _entities.Length;
        }

        public void UnsubscribeAll()
        {
            InternalUtils.Unsubscribe(ref this.OnInitialized);
            InternalUtils.Unsubscribe(ref this.OnEnabled);
            InternalUtils.Unsubscribe(ref this.OnDisabled);
            InternalUtils.Unsubscribe(ref this.OnUpdated);
            InternalUtils.Unsubscribe(ref this.OnFixedUpdated);
            InternalUtils.Unsubscribe(ref this.OnLateUpdated);
            InternalUtils.Unsubscribe(ref this.OnDisposed);
        }

        public void Init()
        {
            if (_initialized)
            {
                Debug.LogWarning("Entity Updater is already initialized!");
                return;
            }

            _initialized = true;

            for (int i = 0; i < _entityCount; i++)
                _entities[i].Init();

            this.OnInitialized?.Invoke();
        }

        public void Dispose()
        {
            if (_enabled)
                this.Disable();

            if (!_initialized)
            {
                Debug.LogWarning("Entity Updater is already disposed!");
                return;
            }

            _initialized = false;

            for (int i = 0; i < _entityCount; i++)
                _entities[i].Dispose();

            this.OnDisposed?.Invoke();
        }

        public void Enable()
        {
            if (!_initialized)
                this.Init();

            if (_enabled)
            {
                Debug.LogWarning("Entity Updater is already enabled!");
                return;
            }

            _enabled = true;

            for (int i = 0; i < _entityCount; i++)
                _entities[i].Enable();

            this.OnEnabled?.Invoke();
        }

        public void Disable()
        {
            if (!_enabled)
            {
                Debug.LogWarning("Entity Updater is already disabled!");
                return;
            }

            _enabled = false;

            for (int i = 0; i < _entityCount; i++)
                _entities[i].Disable();

            this.OnDisabled?.Invoke();
        }

        public void OnUpdate(float deltaTime)
        {
            if (!_enabled)
            {
                Debug.LogWarning("Update failed! Entity Updater is not enabled!");
                return;
            }

            for (int i = 0; i < _entityCount; i++)
                _entities[i].OnUpdate(deltaTime);

            this.OnUpdated?.Invoke(deltaTime);
        }

        public void OnFixedUpdate(float deltaTime)
        {
            if (!_enabled)
            {
                Debug.LogWarning("Fixed update failed! Entity Updater is not enabled!");
                return;
            }

            for (int i = 0; i < _entityCount; i++)
                _entities[i].OnFixedUpdate(deltaTime);

            this.OnFixedUpdated?.Invoke(deltaTime);
        }

        public void OnLateUpdate(float deltaTime)
        {
            if (!_enabled)
            {
                Debug.LogWarning("Late update failed! Entity Updater is not enabled!");
                return;
            }

            for (int i = 0; i < _entityCount; i++)
                _entities[i].OnLateUpdate(deltaTime);

            this.OnLateUpdated?.Invoke(deltaTime);
        }

        public bool Add(E entity)
        {
            if (entity == null)
                return false;

            if (!InternalUtils.AddIfAbsent(ref _entities, ref _entityCount, entity, s_entityComparer))
                return false;

            if (_initialized) entity.Init();
            if (_enabled) entity.Enable();

            return true;
        }

        public bool Del(E entity)
        {
            if (entity == null)
                return false;

            if (!InternalUtils.Remove(ref _entities, ref _entityCount, entity, s_entityComparer))
                return false;

            if (_enabled) entity.Disable();
            if (_initialized) entity.Dispose();

            return true;
        }

        public void Clear()
        {
            if (_entityCount == 0)
                return;

            if (_enabled)
                for (int i = 0; i < _entityCount; i++)
                    _entities[i].Disable();

            if (_initialized)
                for (int i = 0; i < _entityCount; i++)
                    _entities[i].Dispose();

            _entityCount = 0;
        }
    }
}