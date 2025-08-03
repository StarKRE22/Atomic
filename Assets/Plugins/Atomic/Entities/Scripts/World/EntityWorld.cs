using System;
using System.Collections.Generic;
using UnityEngine;

#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

namespace Atomic.Entities
{
    /// <summary>
    /// A non-generic shortcut for <see cref="EntityWorld{IEntity}"/>.
    /// Manages a world of general-purpose <see cref="IEntity"/> instances with support for spawning, enabling,
    /// updating, and disposing all contained entities.
    /// </summary>
    /// <remarks>
    /// This class provides a convenient entry point for working with untyped or heterogeneous entities
    /// without requiring generic parameters.
    /// </remarks>
    public class EntityWorld : EntityWorld<IEntity>
    {
        /// <inheritdoc/>
        public EntityWorld()
        {
        }

        /// <inheritdoc/>
        public EntityWorld(params IEntity[] entities) : base(entities)
        {
        }

        /// <inheritdoc/>
        public EntityWorld(string name = null, params IEntity[] entities) : base(name, entities)
        {
        }

        /// <inheritdoc/>
        public EntityWorld(string name, IEnumerable<IEntity> entities) : base(name, entities)
        {
        }
    }

    /// <summary>
    /// Represents a runtime-managed world composed of entities of type <typeparamref name="E"/>.
    /// Provides lifecycle management including spawning, enabling, updating, and disposing all entities in the collection.
    /// </summary>
    /// <typeparam name="E">The specific type of entity managed by this world. Must implement <see cref="IEntity"/>.</typeparam>
    public class EntityWorld<E> : EntityCollection<E>, IEntityWorld<E> where E : IEntity
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

        /// <summary>
        /// Initializes an empty <see cref="EntityWorld{E}"/> instance with no name.
        /// </summary>
        public EntityWorld() => _name = string.Empty;

        /// <summary>
        /// Initializes a new <see cref="EntityWorld{E}"/> with the specified entities and an empty name.
        /// </summary>
        /// <param name="entities">Entities to prepopulate the world with.</param>
        public EntityWorld(params E[] entities)
        {
            _name = string.Empty;
            this.AddRange(entities);
        }

        /// <summary>
        /// Initializes a new <see cref="EntityWorld{E}"/> with a name and optional entities.
        /// </summary>
        /// <param name="name">The name of the world.</param>
        /// <param name="entities">Entities to prepopulate the world with.</param>
        public EntityWorld(string name = null, params E[] entities)
        {
            _name = name;
            this.AddRange(entities);
        }

        /// <summary>
        /// Initializes a new <see cref="EntityWorld{E}"/> with a name and a collection of entities.
        /// </summary>
        /// <param name="name">The name of the world.</param>
        /// <param name="entities">The enumerable collection of entities to add.</param>
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

            int currentIndex = _head;
            while (currentIndex != UNDEFINED_INDEX)
            {
                ref readonly Slot slot = ref _slots[currentIndex];
                slot.value.Spawn();
                currentIndex = slot.right;
            }

            this.NotifyAboutStateChanged();
            this.OnSpawned?.Invoke();
        }

        /// <inheritdoc/>
        public void Despawn()
        {
            if (_enabled)
                this.Disable();

            if (!_spawned)
                return;

            int currentIndex = _head;
            while (currentIndex != UNDEFINED_INDEX)
            {
                ref readonly Slot slot = ref _slots[currentIndex];
                slot.value.Despawn();
                currentIndex = slot.right;
            }

            _spawned = false;
            this.NotifyAboutStateChanged();
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

            int currentIndex = _head;
            while (currentIndex != UNDEFINED_INDEX)
            {
                ref readonly Slot slot = ref _slots[currentIndex];
                slot.value.Enable();
                currentIndex = slot.right;
            }

            this.NotifyAboutStateChanged();
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

            int currentIndex = _head;
            while (currentIndex != UNDEFINED_INDEX)
            {
                ref readonly Slot slot = ref _slots[currentIndex];
                slot.value.Disable();
                currentIndex = slot.right;
            }

            this.NotifyAboutStateChanged();
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

            int currentIndex = _head;
            while (currentIndex != UNDEFINED_INDEX)
            {
                ref readonly Slot slot = ref _slots[currentIndex];
                slot.value.OnUpdate(deltaTime);
                currentIndex = slot.right;
            }

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

            int currentIndex = _head;
            while (currentIndex != UNDEFINED_INDEX)
            {
                ref readonly Slot slot = ref _slots[currentIndex];
                slot.value.OnFixedUpdate(deltaTime);
                currentIndex = slot.right;
            }

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

            int currentIndex = _head;
            while (currentIndex != UNDEFINED_INDEX)
            {
                ref readonly Slot slot = ref _slots[currentIndex];
                slot.value.OnLateUpdate(deltaTime);
                currentIndex = slot.right;
            }

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
        
        /// <summary>
        /// Disposes the world and its entities by despawning them and releasing all resources.
        /// </summary>
        public override void Dispose()
        {
            this.Despawn();
            base.Dispose();
        }

        /// <summary>
        /// Unsubscribes all internal world-level event listeners and clears event handlers.
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