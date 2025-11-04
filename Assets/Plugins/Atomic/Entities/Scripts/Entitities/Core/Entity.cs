using System;
using System.Collections.Generic;
using System.Linq;

namespace Atomic.Entities
{
    /// <summary>
    /// Represents the core implementation of an <see cref="IEntity"/> in the framework.
    /// This class follows the Entity–State–Behaviour pattern, providing a modular container
    /// for dynamic state, tags, behaviours, and lifecycle management.
    /// </summary>
    public partial class Entity : IEntity
    {
        private const int UNDEFINED_INDEX = -1;

        /// <inheritdoc/>
        public event Action<IEntity> OnStateChanged;

        /// <inheritdoc cref="IEntity.InstanceID" />
        public int InstanceID => _instanceId;

        /// <inheritdoc/>
        int IEntity.InstanceID
        {
            get => _instanceId;
            set => _instanceId = value;
        }
        
        internal int _instanceId;
        /// <inheritdoc/>
        public string Name
        {
            get => _name;
            set
            {
                if (_name != value)
                {
                    _name = value;
                    this.OnStateChanged?.Invoke(this);
                }
            }
        }
        
        /// <inheritdoc/>
        /// <value>
        /// Returns <see langword="true"/> if the entity has a valid assigned <see cref="InstanceID"/>; otherwise, <see langword="false"/>.
        /// </value>
        public bool IsValid => _instanceId > 0;
        
        private string _name;

        /// <summary>
        /// Configuration settings for the entity.
        /// </summary>
        [Serializable]
        public struct Settings
        {
            /// <summary>
            /// Indicates whether stored values should be disposed automatically when <see cref="Dispose"/> is called.
            /// Default is <c>true</c>.
            /// </summary>
            public bool disposeValues;
        }

        private readonly Settings _settings;

        /// <summary>
        /// Creates a new entity with the specified name, tags, values, behaviours, and optional settings.
        /// </summary>
        /// <param name="name">The name of the entity. If <c>null</c>, an empty string is used.</param>
        /// <param name="tags">Optional collection of tag identifiers.</param>
        /// <param name="values">Optional collection of key-value pairs.</param>
        /// <param name="behaviours">Optional collection of behaviours to attach.</param>
        /// <param name="settings">Optional entity settings. If <c>null</c>, <see cref="Settings.disposeValues"/> defaults to <c>true</c>.</param>
        /// <remarks>
        /// The constructor initializes internal capacities based on the provided collections,
        /// then adds all specified tags, values, and behaviours immediately.
        /// </remarks>
        public Entity(
            string name,
            IEnumerable<string> tags,
            IEnumerable<KeyValuePair<string, object>> values,
            IEnumerable<IEntityBehaviour> behaviours,
            Settings? settings = null
        ) : this(
            name: name,
            tagCapacity: tags?.Count() ?? 0,
            valueCapacity: values?.Count() ?? 0,
            behaviourCapacity: behaviours?.Count() ?? 0,
            settings: settings)
        {
            this.AddTags(tags);
            this.AddValues(values);
            this.AddBehaviours(behaviours);
        }

        /// <summary>
        /// Creates a new entity with the specified name, tags, values, behaviours, and optional settings.
        /// </summary>
        /// <param name="name">The name of the entity. If <c>null</c>, an empty string is used.</param>
        /// <param name="tags">Optional collection of tag identifiers.</param>
        /// <param name="values">Optional collection of key-value pairs.</param>
        /// <param name="behaviours">Optional collection of behaviours to attach.</param>
        /// <param name="settings">Optional entity settings. If <c>null</c>, <see cref="Settings.disposeValues"/> defaults to <c>true</c>.</param>
        /// <remarks>
        /// The constructor initializes internal capacities based on the provided collections,
        /// then adds all specified tags, values, and behaviours immediately.
        /// </remarks>
        public Entity(
            string name,
            IEnumerable<int> tags,
            IEnumerable<KeyValuePair<int, object>> values,
            IEnumerable<IEntityBehaviour> behaviours,
            Settings? settings = null
        ) : this(
            name: name,
            tagCapacity: tags?.Count() ?? 0,
            valueCapacity: values?.Count() ?? 0,
            behaviourCapacity: behaviours?.Count() ?? 0,
            settings: settings)
        {
            this.AddTags(tags);
            this.AddValues(values);
            this.AddBehaviours(behaviours);
        }

        /// <summary>
        /// Creates a new entity with the specified name and initial capacities for tags, values, and behaviours.
        /// </summary>
        /// <param name="name">The name of the entity. If <c>null</c>, an empty string is used.</param>
        /// <param name="tagCapacity">Initial capacity for tag storage to minimize memory allocations.</param>
        /// <param name="valueCapacity">Initial capacity for value storage to minimize memory allocations.</param>
        /// <param name="behaviourCapacity">Initial capacity for behaviour storage to minimize memory allocations.</param>
        /// <param name="settings">Optional entity settings. If <c>null</c>, <see cref="Settings.disposeValues"/> defaults to <c>true</c>.</param>
        /// <remarks>
        /// Initializes internal structures for efficient usage and registers the entity in <see cref="EntityRegistry"/>.
        /// </remarks>
        public Entity(
            string name = null,
            int tagCapacity = 0,
            int valueCapacity = 0,
            int behaviourCapacity = 0,
            Settings? settings = null
        )
        {
            _name = name ?? string.Empty;
            _settings = settings ?? new Settings {disposeValues = true};

            this.ConstructTags(tagCapacity);
            this.ConstructValues(valueCapacity);
            this.ConstructBehaviours(behaviourCapacity);

            EntityRegistry.Instance.Register(this);
        }

        /// <inheritdoc/>
        public override string ToString() => $"{nameof(_name)}: {_name}, {nameof(_instanceId)}: {_instanceId}";

        /// <inheritdoc/>
        public override bool Equals(object obj) => obj is IEntity other && other.InstanceID == _instanceId;

        /// <summary>
        /// Checks equality between this entity and another <see cref="IEntity"/> instance.
        /// </summary>
        /// <param name="other">The entity to compare.</param>
        /// <returns><c>true</c> if the entities share the same <see cref="InstanceID"/>; otherwise, <c>false</c>.</returns>
        public bool Equals(IEntity other) => other != null && _instanceId == other.InstanceID;

        /// <inheritdoc/>
        public override int GetHashCode() => _instanceId;
    }
}