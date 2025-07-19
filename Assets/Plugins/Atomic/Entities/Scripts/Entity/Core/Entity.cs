using System;
using System.Collections.Generic;

namespace Atomic.Entities
{
    /// <summary>
    /// Represents the core implementation of an <see cref="IEntity"/> in the Atomic framework.
    /// </summary>
    public abstract partial class Entity<E> : IEntity<E> where E : class, IEntity<E>
    {
        private const int UNDEFINED_INDEX = -1;
        
        /// <summary>
        /// Occurs when the state of the entity changes.
        /// </summary>
        public event Action OnStateChanged;

        /// <summary>
        /// Gets or sets the unique identifier of the entity.
        /// </summary>
        public int InstanceID => this.instanceId;

        /// <summary>
        /// Gets or sets the name of the entity.
        /// </summary>
        public string Name
        {
            get => this.name;
            set => this.name = value;
        }

        private string name;
        private int instanceId;

        /// <summary>
        /// Initializes a new instance of the <see cref="Entity"/> class.
        /// </summary>
        protected Entity()
        {
            this.name = string.Empty;
            this.ConstructTags();
            this.ConstructValues();
            this.ConstructBehaviours();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Entity"/> class with a specified name.
        /// </summary>
        protected Entity(string name)
        {
            this.name = name;
            this.ConstructTags();
            this.ConstructValues();
            this.ConstructBehaviours();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Entity"/> class
        /// with optional collections of tags, values, behaviours, and a custom ID.
        /// </summary>
        protected Entity(
            string name = null,
            IEnumerable<int> tags = null,
            IEnumerable<KeyValuePair<int, object>> values = null,
            IEnumerable<IBehaviour<E>> behaviours = null
        )
        {
            this.name = name ?? string.Empty;
            this.ConstructTags(tags);
            this.ConstructValues(values);
            this.ConstructBehaviours(behaviours);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Entity"/> class with optional capacity settings and custom ID.
        /// </summary>
        protected Entity(
            string name = null,
            int tagCapacity = 0,
            int valueCapacity = 0,
            int behaviourCapacity = 0
        )
        {
            this.name = name ?? string.Empty;
            this.ConstructTags(tagCapacity);
            this.ConstructValues(valueCapacity);
            this.ConstructBehaviours(behaviourCapacity);
        }

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
        public override string ToString() => $"{nameof(name)}: {name}, {nameof(instanceId)}: {instanceId}";

        public bool Equals(IEntity<E> other) => this.instanceId == other.InstanceID;

        /// <inheritdoc/>
        public override bool Equals(object obj) => obj is IEntity<E> other && other.InstanceID == this.instanceId;

        /// <inheritdoc/>
        public override int GetHashCode() => this.instanceId;

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
}