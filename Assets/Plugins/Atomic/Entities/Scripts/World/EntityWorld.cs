using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
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
        public event Action OnActivated;

        /// <inheritdoc/>
        public event Action OnDeactivated;

        /// <inheritdoc/>
        public event Action<float> OnUpdated;

        /// <inheritdoc/>
        public event Action<float> OnFixedUpdated;

        /// <inheritdoc/>
        public event Action<float> OnLateUpdated;

        /// <inheritdoc/>
        public bool IsSpawned => _spawned;

        /// <summary>
        /// Indicates whether the loop is enabled.
        /// </summary>
        public bool IsActive => _active;

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
        private protected bool _active;

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

            this.ProcessSpawn();
            _spawned = true;

            this.NotifyAboutStateChanged();
            this.OnSpawned?.Invoke();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual void ProcessSpawn()
        {
            int currentIndex = _head;
            while (currentIndex != UNDEFINED_INDEX)
            {
                ref readonly Slot slot = ref _slots[currentIndex];
                slot.value.Spawn();
                currentIndex = slot.right;
            }
        }

        /// <inheritdoc/>
        public void Despawn()
        {
            if (_active)
                this.Deactivate();

            if (!_spawned)
                return;

            this.ProcessDespawn();
            _spawned = false;

            this.NotifyAboutStateChanged();
            this.OnDespawned?.Invoke();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual void ProcessDespawn()
        {
            int currentIndex = _head;
            while (currentIndex != UNDEFINED_INDEX)
            {
                ref readonly Slot slot = ref _slots[currentIndex];
                slot.value.Despawn();
                currentIndex = slot.right;
            }
        }

        /// <inheritdoc/>
        public void Activate()
        {
            if (!_spawned)
                this.Spawn();

            if (_active)
            {
#if UNITY_5_3_OR_NEWER
                Debug.LogWarning($"{this.GetType().Name} is already enabled!");
#endif
                return;
            }

            this.ProcessActivate();
            _active = true;

            this.NotifyAboutStateChanged();
            this.OnActivated?.Invoke();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual void ProcessActivate()
        {
            int currentIndex = _head;
            while (currentIndex != UNDEFINED_INDEX)
            {
                ref readonly Slot slot = ref _slots[currentIndex];
                slot.value.Activate();
                currentIndex = slot.right;
            }
        }

        /// <inheritdoc/>
        public void Deactivate()
        {
            if (!_active)
            {
#if UNITY_5_3_OR_NEWER
                Debug.LogWarning($"{this.GetType().Name} is already disabled!");
#endif
                return;
            }

            this.ProcessDeactivate();
            _active = false;

            this.NotifyAboutStateChanged();
            this.OnDeactivated?.Invoke();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual void ProcessDeactivate()
        {
            int currentIndex = _head;
            while (currentIndex != UNDEFINED_INDEX)
            {
                ref readonly Slot slot = ref _slots[currentIndex];
                slot.value.Deactivate();
                currentIndex = slot.right;
            }
        }

        /// <inheritdoc/>
        public void OnUpdate(float deltaTime)
        {
            if (!_active)
            {
#if UNITY_5_3_OR_NEWER
                Debug.LogWarning($"Update failed! {this.GetType().Name} is not enabled!");
#endif
                return;
            }

            this.ProcessUpdate(deltaTime);
            this.OnUpdated?.Invoke(deltaTime);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual void ProcessUpdate(float deltaTime)
        {
            int currentIndex = _head;
            while (currentIndex != UNDEFINED_INDEX)
            {
                ref readonly Slot slot = ref _slots[currentIndex];
                slot.value.OnUpdate(deltaTime);
                currentIndex = slot.right;
            }
        }

        /// <inheritdoc/>
        public void OnFixedUpdate(float deltaTime)
        {
            if (!_active)
            {
#if UNITY_5_3_OR_NEWER
                Debug.LogWarning($"Fixed update failed! {this.GetType().Name} is not enabled!");
#endif
                return;
            }

            this.ProcessFixedUpdate(deltaTime);
            this.OnFixedUpdated?.Invoke(deltaTime);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual void ProcessFixedUpdate(float deltaTime)
        {
            int currentIndex = _head;
            while (currentIndex != UNDEFINED_INDEX)
            {
                ref readonly Slot slot = ref _slots[currentIndex];
                slot.value.OnFixedUpdate(deltaTime);
                currentIndex = slot.right;
            }
        }

        /// <inheritdoc/>
        public void OnLateUpdate(float deltaTime)
        {
            if (!_active)
            {
#if UNITY_5_3_OR_NEWER
                Debug.LogWarning($"Late update failed! {this.GetType().Name} is not enabled!");
#endif
                return;
            }

            this.ProcessLateUpdate(deltaTime);
            this.OnLateUpdated?.Invoke(deltaTime);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual void ProcessLateUpdate(float deltaTime)
        {
            int currentIndex = _head;
            while (currentIndex != UNDEFINED_INDEX)
            {
                ref readonly Slot slot = ref _slots[currentIndex];
                slot.value.OnLateUpdate(deltaTime);
                currentIndex = slot.right;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected override void OnAdd(E entity)
        {
            if (_spawned) entity.Spawn();
            if (_active) entity.Activate();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected override void OnRemove(E entity)
        {
            if (_active) entity.Deactivate();
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
            this.OnActivated = null;
            this.OnDeactivated = null;
            this.OnUpdated = null;
            this.OnFixedUpdated = null;
            this.OnLateUpdated = null;
            this.OnDespawned = null;
        }
    }
}