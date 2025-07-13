using System;
using System.Collections.Generic;
using UnityEngine;

namespace Atomic.Entities
{
    public sealed class EntityUpdater
    {
        public event Action OnInitialized;
        public event Action OnDisposed;
        public event Action OnEnabled;
        public event Action OnDisabled;

        public event Action<float> OnUpdated;
        public event Action<float> OnFixedUpdated;
        public event Action<float> OnLateUpdated;

        private readonly List<IEntity> _entities;
        private readonly List<IEntity> _cache = new();

        public bool Initialized => _initialized;
        public bool Enabled => _enabled;

        private bool _initialized;
        private bool _enabled;

        public EntityUpdater()
        {
            _entities = new List<IEntity>();
        }

        public EntityUpdater(IEnumerable<IEntity> entities)
        {
            _entities = new List<IEntity>(entities);
        }

        public void Init()
        {
            if (_initialized)
            {
                Debug.LogWarning("Entity Runner is already initialized!");
                return;
            }

            _initialized = true;

            int count = _entities.Count;
            if (count != 0)
            {
                _cache.Clear();
                _cache.AddRange(_entities);

                for (int i = 0; i < count; i++)
                    _cache[i].Init();
            }

            this.OnInitialized?.Invoke();
        }

        public void Dispose()
        {
            if (_enabled)
                this.Disable();

            if (!_initialized)
            {
                Debug.LogWarning("Entity Runner is already disposed!");
                return;
            }

            _initialized = false;

            int count = _entities.Count;
            if (count != 0)
            {
                _cache.Clear();
                _cache.AddRange(_entities);

                for (int i = 0; i < count; i++)
                    _cache[i].Dispose();
            }

            this.OnDisposed?.Invoke();

            //Auto unsubscribe events:
            AtomicHelper.Unsubscribe(ref this.OnInitialized);
            AtomicHelper.Unsubscribe(ref this.OnEnabled);
            AtomicHelper.Unsubscribe(ref this.OnDisabled);
            AtomicHelper.Unsubscribe(ref this.OnUpdated);
            AtomicHelper.Unsubscribe(ref this.OnFixedUpdated);
            AtomicHelper.Unsubscribe(ref this.OnLateUpdated);
            AtomicHelper.Unsubscribe(ref this.OnDisposed);
        }

        public void Enable()
        {
            if (!_initialized)
                this.Init();

            if (_enabled)
            {
                Debug.LogWarning("Entity Runner is already enabled!");
                return;
            }

            _enabled = true;

            int count = _entities.Count;
            if (count != 0)
            {
                _cache.Clear();
                _cache.AddRange(_entities);

                for (int i = 0; i < count; i++) 
                    _cache[i].Enable();
            }

            this.OnEnabled?.Invoke();
        }

        public void Disable()
        {
            if (!_enabled)
            {
                Debug.LogWarning("Entity Runner is already disabled!");
                return;
            }

            _enabled = false;

            int count = _entities.Count;
            if (count != 0)
            {
                _cache.Clear();
                _cache.AddRange(_entities);

                for (int i = 0; i < count; i++)
                    _cache[i].Disable();
            }

            this.OnDisabled?.Invoke();
        }

        public void OnUpdate(in float deltaTime)
        {
            if (!_enabled)
            {
                Debug.LogWarning("Update failed! Entity Runner is not enabled!");
                return;
            }

            int count = _entities.Count;
            if (count != 0)
            {
                _cache.Clear();
                _cache.AddRange(_entities);

                for (int i = 0; i < count; i++)
                    _cache[i].OnUpdate(deltaTime);
            }

            this.OnUpdated?.Invoke(deltaTime);
        }

        public void OnFixedUpdate(in float deltaTime)
        {
            if (!_enabled)
            {
                Debug.LogWarning("Fixed update failed! Entity Runner is not enabled!");
                return;
            }

            int count = _entities.Count;
            if (count != 0)
            {
                _cache.Clear();
                _cache.AddRange(_entities);

                for (int i = 0; i < count; i++)
                    _cache[i].OnFixedUpdate(deltaTime);
            }

            this.OnFixedUpdated?.Invoke(deltaTime);
        }

        public void OnLateUpdate(in float deltaTime)
        {
            if (!_enabled)
            {
                Debug.LogWarning("Late update failed! Entity Runner is not enabled!");
                return;
            }

            int count = _entities.Count;
            if (count != 0)
            {
                _cache.Clear();
                _cache.AddRange(_entities);

                for (int i = 0; i < count; i++)
                    _cache[i].OnLateUpdate(deltaTime);
            }

            this.OnLateUpdated?.Invoke(deltaTime);
        }

        public bool Add(in IEntity entity)
        {
            if (entity == null)
                return false;

            if (_entities.Contains(entity))
                return false;

            _entities.Add(entity);

            if (_initialized) entity.Init();
            if (_enabled) entity.Enable();

            return true;
        }

        public bool Del(in IEntity entity)
        {
            if (entity == null)
                return false;

            if (!_entities.Remove(entity))
                return false;

            if (_enabled) entity.Disable();
            if (_initialized) entity.Dispose();

            return true;
        }

        public void Clear()
        {
            int count = _entities.Count;
            if (count == 0)
                return;

            if (_enabled)
                for (int i = 0; i < count; i++)
                    _entities[i].Disable();

            if (_initialized)
                for (int i = 0; i < count; i++)
                    _entities[i].Dispose();

            _entities.Clear();
        }
    }
}