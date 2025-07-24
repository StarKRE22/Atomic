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
        /// Fully disposes the entity by performing the following steps:
        /// <list type="bullet">
        /// <item><description>Calls <see cref="Despawn"/> to release external resources and invoke disposal callbacks.</description></item>
        /// <item><description>Clears all internal state (tags, values, and behaviours).</description></item>
        /// <item><description>Removes all subscriptions to avoid memory leaks via <see cref="UnsubscribeAll"/>.</description></item>
        /// </list>
        /// </summary>
        /// <remarks>
        /// This method is intended to safely and completely dismantle the entity,
        /// making it eligible for reuse or garbage collection.
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