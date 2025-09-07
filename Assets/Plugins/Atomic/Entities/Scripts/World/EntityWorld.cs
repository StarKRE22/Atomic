using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

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
    public class EntityWorld : EntityWorld<IEntity>, IEntityWorld
    {
        /// <summary>
        /// Initializes an empty <see cref="EntityWorld"/> instance with no name.
        /// </summary>
        public EntityWorld()
        {
        }

        /// <summary>
        /// Initializes a new <see cref="EntityWorld"/> with the specified entities and an empty name.
        /// </summary>
        /// <param name="entities">Entities to prepopulate the world with.</param>
        public EntityWorld(params IEntity[] entities) : base(entities)
        {
        }

        /// <summary>
        /// Initializes a new <see cref="EntityWorld"/> with the specified entities and an empty name.
        /// </summary>
        /// <param name="entities">Entities to prepopulate the world with.</param>
        public EntityWorld(string name = null, params IEntity[] entities) : base(name, entities)
        {
        }

        /// <summary>
        /// Initializes a new <see cref="EntityWorld"/> with a name and a collection of entities.
        /// </summary>
        /// <param name="name">The name of the world.</param>
        /// <param name="entities">The enumerable collection of entities to add.</param>
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
        public event Action OnEnabled;

        /// <inheritdoc/>
        public event Action OnDisabled;

        /// <inheritdoc/>
        public event Action<float> OnTicked;

        /// <inheritdoc/>
        public event Action<float> OnFixedTicked;

        /// <inheritdoc/>
        public event Action<float> OnLateTicked;

        /// <inheritdoc/>
#if ODIN_INSPECTOR
        [ShowInInspector]
#endif
        public string Name
        {
            get => _name;
            set => _name = value;
        }
        /// <summary>
        /// Indicates whether the world is enabled.
        /// </summary>
        public bool Enabled => _enabled;

        private string _name;
        private bool _enabled;

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
        public void Enable()
        {
            if (_enabled)
                return;

            int currentIndex = _head;
            while (currentIndex != UNDEFINED_INDEX)
            {
                ref readonly Slot slot = ref _slots[currentIndex];
                slot.value.Enable();
                currentIndex = slot.right;
            }
            
            _enabled = true;

            this.NotifyAboutStateChanged();
            this.OnEnabled?.Invoke();
        }
        
        /// <inheritdoc/>
        public void Disable()
        {
            if (!_enabled)
                return;

            int currentIndex = _head;
            while (currentIndex != UNDEFINED_INDEX)
            {
                ref readonly Slot slot = ref _slots[currentIndex];
                slot.value.Disable();
                currentIndex = slot.right;
            }
            
            _enabled = false;

            this.NotifyAboutStateChanged();
            this.OnDisabled?.Invoke();
        }

        /// <inheritdoc/>
        public void Tick(float deltaTime)
        {
            if (!_enabled)
                return;

            int currentIndex = _head;
            while (currentIndex != UNDEFINED_INDEX)
            {
                ref readonly Slot slot = ref _slots[currentIndex];
                slot.value.Tick(deltaTime);
                currentIndex = slot.right;
            }
            
            this.OnTicked?.Invoke(deltaTime);
        }

        /// <inheritdoc/>
        public void FixedTick(float deltaTime)
        {
            if (!_enabled)
                return;

            int currentIndex = _head;
            while (currentIndex != UNDEFINED_INDEX)
            {
                ref readonly Slot slot = ref _slots[currentIndex];
                slot.value.FixedTick(deltaTime);
                currentIndex = slot.right;
            }
            
            this.OnFixedTicked?.Invoke(deltaTime);
        }

        /// <inheritdoc/>
        public void LateTick(float deltaTime)
        {
            if (!_enabled)
                return;

            int currentIndex = _head;
            while (currentIndex != UNDEFINED_INDEX)
            {
                ref readonly Slot slot = ref _slots[currentIndex];
                slot.value.LateTick(deltaTime);
                currentIndex = slot.right;
            }
            
            this.OnLateTicked?.Invoke(deltaTime);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected override void OnAdd(E entity)
        {
            if (_enabled) entity.Enable();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected override void OnRemove(E entity)
        {
            if (_enabled) entity.Disable();
        }

        public override void Dispose()
        {
            if (_enabled) 
                this.Disable();
            
            base.Dispose();
            
            //Unsubscribe events:
            this.OnEnabled = null;
            this.OnDisabled = null;
            this.OnTicked = null;
            this.OnFixedTicked = null;
            this.OnLateTicked = null;
        }
    }
}