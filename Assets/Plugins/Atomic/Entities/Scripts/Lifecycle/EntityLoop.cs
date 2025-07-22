using System;
using System.Collections.Generic;
using System.Linq;
using static Atomic.Entities.InternalUtils;

#if UNITY_5_3_OR_NEWER
using UnityEngine; 
#endif

namespace Atomic.Entities
{
    /// <summary>
    /// Manages and executes lifecycle events for a collection of <see cref="IEntity"/> instances.
    /// This includes initialization, enabling, disabling, disposal, and update loops.
    /// </summary>
    public sealed class EntityLoop
    {
        private static readonly IEqualityComparer<IEntity> s_entityComparer = EqualityComparer<IEntity>.Default;
        
        /// <summary>
        /// Occurs when all entities have been initialized.
        /// </summary>
        public event Action OnInitialized;
       
        /// <summary>
        /// Occurs when all entities have been disposed.
        /// </summary>
        public event Action OnDisposed;
      
        /// <summary>
        /// Occurs when all entities have been enabled.
        /// </summary>
        public event Action OnEnabled;
        
        /// <summary>
        /// Occurs when all entities have been disabled.
        /// </summary>
        public event Action OnDisabled;

        /// <summary>
        /// Occurs after calling <see cref="OnUpdate(float)"/> on all entities.
        /// </summary>
        public event Action<float> OnUpdated;
        
        /// <summary>
        /// Occurs after calling <see cref="OnFixedUpdate(float)"/> on all entities.
        /// </summary>
        public event Action<float> OnFixedUpdated;
        
        /// <summary>
        /// Occurs after calling <see cref="OnLateUpdate(float)"/> on all entities.
        /// </summary>
        public event Action<float> OnLateUpdated;
        
        /// <summary>
        /// Indicates whether the loop is initialized.
        /// </summary>
        public bool Initialized => _initialized;
        
        /// <summary>
        /// Indicates whether the loop is enabled.
        /// </summary>
        public bool Enabled => _enabled;

        /// <summary>
        /// Gets the number of registered entities.
        /// </summary>
        public int EntityCount => _entityCount;
        
        private IEntity[] _entities;
        private int _entityCount;
        
        private bool _initialized;
        private bool _enabled;

        /// <summary>
        /// Creates an empty <see cref="EntityLoop"/>.
        /// </summary>
        public EntityLoop()
        {
            _entities = Array.Empty<IEntity>();
            _entityCount = 0;
        }

        /// <summary>
        /// Creates an <see cref="EntityLoop"/> with the specified collection of entities.
        /// </summary>
        /// <param name="entities">Initial collection of entities.</param>
        public EntityLoop(IEnumerable<IEntity> entities)
        {
            _entities = entities.ToArray();
            _entityCount = _entities.Length;
        }
        
        /// <summary>
        /// Creates an <see cref="EntityLoop"/> with the specified entities.
        /// </summary>
        /// <param name="entities">Initial entities.</param>
        public EntityLoop(params IEntity[] entities)
        {
            _entities = entities;
            _entityCount = _entities.Length;
        }

        /// <summary>
        /// Removes all subscribed event listeners.
        /// </summary>
        public void UnsubscribeAll()
        {
            this.OnInitialized = null;
            this.OnEnabled = null;
            this.OnDisabled = null;
            this.OnUpdated = null;
            this.OnFixedUpdated = null;
            this.OnLateUpdated = null;
            this.OnDisposed = null;
        }

        /// <summary>
        /// Initializes all registered entities and raises <see cref="OnInitialized"/>.
        /// </summary>
        public void Init()
        {
            if (_initialized)
            {
#if UNITY_5_3_OR_NEWER
                Debug.LogWarning("Entity Updater is already initialized!");
#endif
                return;
            }

            _initialized = true;

            for (int i = 0; i < _entityCount; i++)
                _entities[i].Init();

            this.OnInitialized?.Invoke();
        }
        
        /// <summary>
        /// Disposes all registered entities and raises <see cref="OnDisposed"/>.
        /// </summary>
        public void Dispose()
        {
            if (_enabled)
                this.Disable();

            if (!_initialized)
            {
#if UNITY_5_3_OR_NEWER
                Debug.LogWarning("Entity Updater is already disposed!");
#endif
                return;
            }

            _initialized = false;

            for (int i = 0; i < _entityCount; i++)
                _entities[i].Dispose();

            this.OnDisposed?.Invoke();
        }

        /// <summary>
        /// Enables all registered entities and raises <see cref="OnEnabled"/>.
        /// </summary>
        public void Enable()
        {
            if (!_initialized)
                this.Init();

            if (_enabled)
            {
#if UNITY_5_3_OR_NEWER
                Debug.LogWarning("Entity Updater is already enabled!");
#endif
                return;
            }

            _enabled = true;

            for (int i = 0; i < _entityCount; i++)
                _entities[i].Enable();

            this.OnEnabled?.Invoke();
        }

        /// <summary>
        /// Disables all registered entities and raises <see cref="OnDisabled"/>.
        /// </summary>
        public void Disable()
        {
            if (!_enabled)
            {
#if UNITY_5_3_OR_NEWER
                Debug.LogWarning("Entity Updater is already disabled!");
#endif
                return;
            }

            _enabled = false;

            for (int i = 0; i < _entityCount; i++)
                _entities[i].Disable();

            this.OnDisabled?.Invoke();
        }

        /// <summary>
        /// Updates all enabled entities using the provided delta time and raises <see cref="OnUpdated"/>.
        /// </summary>
        /// <param name="deltaTime">The delta time passed to update.</param>
        public void OnUpdate(float deltaTime)
        {
            if (!_enabled)
            {
#if UNITY_5_3_OR_NEWER
                Debug.LogWarning("Update failed! Entity Updater is not enabled!");
#endif
                return;
            }

            for (int i = 0; i < _entityCount; i++)
                _entities[i].OnUpdate(deltaTime);

            this.OnUpdated?.Invoke(deltaTime);
        }

        /// <summary>
        /// Performs fixed update on all enabled entities and raises <see cref="OnFixedUpdated"/>.
        /// </summary>
        /// <param name="deltaTime">The delta time passed to fixed update.</param>
        public void OnFixedUpdate(float deltaTime)
        {
            if (!_enabled)
            {
#if UNITY_5_3_OR_NEWER
                Debug.LogWarning("Fixed update failed! Entity Updater is not enabled!");
#endif
                return;
            }

            for (int i = 0; i < _entityCount; i++)
                _entities[i].OnFixedUpdate(deltaTime);

            this.OnFixedUpdated?.Invoke(deltaTime);
        }

        /// <summary>
        /// Performs late update on all enabled entities and raises <see cref="OnLateUpdated"/>.
        /// </summary>
        /// <param name="deltaTime">The delta time passed to late update.</param>
        public void OnLateUpdate(float deltaTime)
        {
            if (!_enabled)
            {
#if UNITY_5_3_OR_NEWER
                Debug.LogWarning("Late update failed! Entity Updater is not enabled!");
#endif
                return;
            }

            for (int i = 0; i < _entityCount; i++)
                _entities[i].OnLateUpdate(deltaTime);

            this.OnLateUpdated?.Invoke(deltaTime);
        }

        /// <summary>
        /// Adds an entity to the loop and optionally initializes/enables it based on current state.
        /// </summary>
        /// <param name="entity">The entity to add.</param>
        /// <returns>True if the entity was added, false if already present or null.</returns>
        public bool Add(IEntity entity)
        {
            if (entity == null)
                return false;

            if (!AddIfAbsent(ref _entities, ref _entityCount, entity, s_entityComparer))
                return false;

            if (_initialized) entity.Init();
            if (_enabled) entity.Enable();

            return true;
        }

        /// <summary>
        /// Removes an entity from the loop and optionally disables/disposes it based on current state.
        /// </summary>
        /// <param name="entity">The entity to remove.</param>
        /// <returns>True if the entity was removed, false if not found or null.</returns>
        public bool Del(IEntity entity)
        {
            if (entity == null)
                return false;

            if (!Remove(ref _entities, ref _entityCount, entity, s_entityComparer))
                return false;

            if (_enabled) entity.Disable();
            if (_initialized) entity.Dispose();

            return true;
        }

        /// <summary>
        /// Disables and disposes all entities, and clears the internal collection.
        /// </summary>
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