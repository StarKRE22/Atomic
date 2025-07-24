using System;

namespace Atomic.Entities
{
    /// <summary>
    /// Represents the core implementation of an <see cref="IEntity"/> in the Atomic framework.
    /// </summary>
    public partial class Entity : IEntity, IDisposable
    {
        private const int UNDEFINED_INDEX = -1;

        /// <inheritdoc/>
        public event Action OnStateChanged;

        /// <inheritdoc/>
        public int InstanceID => this.instanceId;

        /// <inheritdoc/>
        public string Name
        {
            get => this.name;
            set => this.name = value;
        }

        private string name;
        private int instanceId;

        /// <summary>
        /// Initializes a new instance of the <see cref="Entity"/> class with optional name, tag, value, and behaviour capacities.
        /// </summary>
        /// <param name="name">The name of the entity. Defaults to an empty string if <c>null</c>.</param>
        /// <param name="tagCapacity">Initial capacity for tags. Improves performance by reducing allocations.</param>
        /// <param name="valueCapacity">Initial capacity for values. Improves performance by reducing allocations.</param>
        /// <param name="behaviourCapacity">Initial capacity for behaviours. Improves performance by reducing allocations.</param>
        public Entity(
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
        public bool Equals(IEntity other) => this.instanceId == other.InstanceID;

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