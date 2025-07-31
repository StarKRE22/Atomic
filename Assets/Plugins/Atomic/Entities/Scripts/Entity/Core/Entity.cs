using System;
using System.Collections.Generic;
using System.Linq;

namespace Atomic.Entities
{
    /// <summary>
    /// Represents the core implementation of an <see cref="IEntity"/> in the Atomic framework.
    /// </summary>
    public partial class Entity : IEntity, IDisposable
    {
        private const int UNDEFINED_INDEX = -1;

        /// <inheritdoc/>
        public virtual event Action OnStateChanged;

        /// <inheritdoc/>
        public int InstanceID => this.instanceId;

        /// <inheritdoc/>
        public string Name
        {
            get => this.name;
            set => this.name = value;
        }

        internal int instanceId;
        private string name;
        
        /// <summary>
        /// Initializes a new instance of the <see cref="Entity"/> class with the specified name, tags, values, and behaviours.
        /// </summary>
        /// <param name="name">The name of the entity. If <c>null</c>, an empty string is used.</param>
        /// <param name="tags">A collection of tag identifiers to add to the entity. May be <c>null</c>.</param>
        /// <param name="values">A collection of key-value pairs to add as values to the entity. May be <c>null</c>.</param>
        /// <param name="behaviours">A collection of behaviours to attach to the entity. May be <c>null</c>.</param>
        /// <remarks>
        /// This constructor initializes the internal capacities for tags, values, and behaviours based on the sizes of the provided collections,
        /// and immediately adds all specified items to the entity.
        /// </remarks>
        public Entity(
            string name,
            IEnumerable<int> tags,
            IEnumerable<KeyValuePair<int, object>> values,
            IEnumerable<IEntityBehaviour> behaviours
        ) : this(name, tags?.Count() ?? 0, values?.Count() ?? 0, behaviours?.Count() ?? 0)
        {
            this.AddTags(tags);
            this.AddValues(values);
            this.AddBehaviours(behaviours);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Entity"/> class with the specified name and initial capacities
        /// for tags, values, and behaviours.
        /// </summary>
        /// <param name="name">The name of the entity. If <c>null</c>, an empty string will be used.</param>
        /// <param name="tagCapacity">Initial capacity for tag storage. Used to reduce memory allocations.</param>
        /// <param name="valueCapacity">Initial capacity for value storage. Used to reduce memory allocations.</param>
        /// <param name="behaviourCapacity">Initial capacity for behaviour storage. Used to reduce memory allocations.</param>
        /// <remarks>
        /// This constructor prepares internal structures for efficient use by preallocating capacity, and registers the entity
        /// in the <see cref="EntityRegistry"/>.
        /// </remarks>
        public Entity(string name = null, int tagCapacity = 0, int valueCapacity = 0, int behaviourCapacity = 0)
        {
            this.name = name ?? string.Empty;
            this.ConstructTags(tagCapacity);
            this.ConstructValues(valueCapacity);
            this.ConstructBehaviours(behaviourCapacity);
            EntityRegistry.Instance.Register(this, out this.instanceId);
        }
        
        /// <summary>
        /// Releases all resources used by the entity.
        /// </summary>
        /// <remarks>
        /// This method performs cleanup by:
        /// <list type="bullet">
        /// <item><description>Calling <see cref="Despawn"/> to deactivate the entity.</description></item>
        /// <item><description>Clearing all tags, values, and behaviours.</description></item>
        /// <item><description>Unsubscribing from all events.</description></item>
        /// <item><description>Unregistering the entity from the <see cref="EntityRegistry"/>.</description></item>
        /// </list>
        /// </remarks>
        public void Dispose()
        {
            this.Despawn();
            this.ClearTags();
            this.ClearValues();
            this.ClearBehaviours();
            this.UnsubscribeAll();

            EntityRegistry.Instance.Unregister(ref this.instanceId);
        }

        /// <inheritdoc/>
        public override string ToString() => $"{nameof(name)}: {name}, {nameof(instanceId)}: {instanceId}";

        /// <inheritdoc/>
        public override bool Equals(object obj) => obj is IEntity other && other.InstanceID == this.instanceId;

        // ReSharper disable once UnusedMember.Global
        public bool Equals(IEntity other) => other != null && this.instanceId == other.InstanceID;

        /// <inheritdoc/>
        public override int GetHashCode() => this.instanceId;

        /// <summary>
        /// Removes all subscriptions and callbacks associated with this entity.
        /// </summary>
        public void UnsubscribeAll()
        {
            this.OnStateChanged = null;

            this.OnSpawned = null;
            this.OnEnabled = null;
            this.OnDisabled = null;

            this.OnUpdated = null;
            this.OnFixedUpdated = null;
            this.OnLateUpdated = null;
            this.OnDespawned = null;

            this.OnBehaviourAdded = null;
            this.OnBehaviourDeleted = null;

            this.OnValueAdded = null;
            this.OnValueDeleted = null;
            this.OnValueChanged = null;

            this.OnTagAdded = null;
            this.OnTagDeleted = null;
        }
    }
}