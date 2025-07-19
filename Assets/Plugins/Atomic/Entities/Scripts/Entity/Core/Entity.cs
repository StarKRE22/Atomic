using System;
using System.Collections.Generic;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Atomic.Entities
{
    /// <summary>
    /// Represents the core implementation of an <see cref="IEntity"/> in the Atomic framework.
    /// </summary>
    public abstract partial class Entity<E> : IEntity<E> where E : class, IEntity<E>
    {
        
        /// <summary>
        /// Occurs when the state of the entity changes.
        /// </summary>
        public event Action OnStateChanged;

        /// <summary>
        /// Gets or sets the unique identifier of the entity.
        /// </summary>
        public int Id
        {
            get => this.id;
            set
            {
                EntityRegistry<E>.ChangeId(this, this.id, value);
                this.id = value;
            }
        }

        /// <summary>
        /// Gets or sets the name of the entity.
        /// </summary>
        public string Name
        {
            get => this.name;
            set => this.name = value;
        }

        private string name;
        private int id;

        /// <summary>
        /// Initializes a new instance of the <see cref="Entity"/> class.
        /// </summary>
        protected Entity()
        {
            this.name = string.Empty;
            this.id = EntityRegistry<E>.NextId(this);

            this.InitializeTags();
            this.InitializeValues();
            this.InitializeBehaviours();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Entity"/> class with a specified name.
        /// </summary>
        protected Entity(string name)
        {
            this.name = name;
            this.id = EntityRegistry<E>.NextId(this);

            this.InitializeTags();
            this.InitializeValues();
            this.InitializeBehaviours();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Entity"/> class
        /// with optional collections of tags, values, behaviours, and a custom ID.
        /// </summary>
        protected Entity(
            string name = null,
            IEnumerable<int> tags = null,
            IEnumerable<KeyValuePair<int, object>> values = null,
            IEnumerable<IBehaviour<E>> behaviours = null,
            int id = -1
        )
        {
            this.name = name ?? string.Empty;
            this.id = id < 0 ? NextId() : id;

            this.InitializeTags(tags);
            this.InitializeValues(values);
            this.InitializeBehaviours(behaviours);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Entity"/> class with optional capacity settings and custom ID.
        /// </summary>
        protected Entity(
            string name = null,
            int tagCapacity = 0,
            int valueCapacity = 0,
            int behaviourCapacity = 0,
            int id = -1
        )
        {
            this.name = name ?? string.Empty;
            this.id = id < 0 ? NextId() : id;

            this.InitializeTags(in tagCapacity);
            this.InitializeValues(in valueCapacity);
            this.InitializeBehaviours(in behaviourCapacity);
        }

        ~Entity() => this.UnsubscribeAll();

        /// <summary>
        /// Removes all subscriptions and callbacks associated with this entity.
        /// </summary>
        public void UnsubscribeAll()
        {
            InternalUtils.Unsubscribe(ref this.OnStateChanged);

            InternalUtils.Unsubscribe(ref this.OnInitialized);
            InternalUtils.Unsubscribe(ref this.OnEnabled);
            InternalUtils.Unsubscribe(ref this.OnDisabled);
            InternalUtils.Unsubscribe(ref this.OnUpdated);
            InternalUtils.Unsubscribe(ref this.OnFixedUpdated);
            InternalUtils.Unsubscribe(ref this.OnLateUpdated);
            InternalUtils.Unsubscribe(ref this.OnDisposed);

            InternalUtils.Unsubscribe(ref this.OnBehaviourAdded);
            InternalUtils.Unsubscribe(ref this.OnBehaviourDeleted);

            InternalUtils.Unsubscribe(ref this.OnValueAdded);
            InternalUtils.Unsubscribe(ref this.OnValueDeleted);
            InternalUtils.Unsubscribe(ref this.OnValueChanged);

            InternalUtils.Unsubscribe(ref this.OnTagAdded);
            InternalUtils.Unsubscribe(ref this.OnTagDeleted);
        }

        /// <inheritdoc/>
        public override string ToString() => $"{nameof(name)}: {name}, {nameof(id)}: {id}";

        public bool Equals(IEntity<E> other) => this.id == other.Id;

        /// <inheritdoc/>
        public override bool Equals(object obj) => obj is IEntity<E> other && other.Id == this.id;

        /// <inheritdoc/>
        public override int GetHashCode() => this.id;

        /// <summary>
        /// Clears all data (tags, values, behaviours) from this entity.
        /// </summary>
        public void Clear()
        {
            this.ClearTags();
            this.ClearValues();
            this.ClearBehaviours();
        }
    }

    // public class Entity : Entity<Entity>
    // {
    //     
    // }
}