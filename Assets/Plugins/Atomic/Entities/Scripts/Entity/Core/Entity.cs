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
    public partial class Entity : IEntity
    {
        private const int UNDEFINED_INDEX = -1;

        private static readonly Dictionary<int, IEntity> s_entities = new();
        private static int s_maxId = -1;

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
            set => this.SetId(value);
        }

        /// <summary>
        /// Gets or sets the name of the entity.
        /// </summary>
        public string Name
        {
            get => this.name;
            set => this.name = value;
        }

        private readonly IEntity owner;
        private string name;
        private int id;

        /// <summary>
        /// Initializes a new instance of the <see cref="Entity"/> class.
        /// </summary>
        public Entity()
        {
            this.name = string.Empty;
            this.id = NextId();
            this.owner = this;

            this.InitializeTags();
            this.InitializeValues();
            this.InitializeBehaviours();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Entity"/> class with a specified owner.
        /// </summary>
        public Entity(IEntity owner)
        {
            this.name = string.Empty;
            this.id = NextId();
            this.owner = owner;

            this.InitializeTags();
            this.InitializeValues();
            this.InitializeBehaviours();
        }
        
        /// <summary>
        /// Initializes a new instance of the <see cref="Entity"/> class with a specified name.
        /// </summary>
        public Entity(string name)
        {
            this.name = name;
            this.id = NextId();
            this.owner = this;

            this.InitializeTags();
            this.InitializeValues();
            this.InitializeBehaviours();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Entity"/> class
        /// with optional collections of tags, values, behaviours, and a custom ID.
        /// </summary>
        public Entity(
            in string name = null,
            in IEnumerable<int> tags = null,
            in IEnumerable<KeyValuePair<int, object>> values = null,
            in IEnumerable<IBehaviour> behaviours = null,
            in IEntity owner = null,
            in int id = -1
        )
        {
            this.name = name ?? string.Empty;
            this.id = id < 0 ? NextId() : id;
            this.owner = owner ?? this;

            this.InitializeTags(in tags);
            this.InitializeValues(in values);
            this.InitializeBehaviours(in behaviours);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Entity"/> class with optional capacity settings and custom ID.
        /// </summary>
        public Entity(
            in string name = null,
            in int tagCapacity = 0,
            in int valueCapacity = 0,
            in int behaviourCapacity = 0,
            in IEntity owner = null,
            in int id = -1
        )
        {
            this.name = name ?? string.Empty;
            this.id = id < 0 ? NextId() : id;
            this.owner = owner ?? this;

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

        public bool Equals(IEntity other) => this.id == other.Id;

        /// <inheritdoc/>
        public override bool Equals(object obj) => obj is IEntity other && other.Id == this.id;

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

        private static int NextId()
        {
            do s_maxId++;
            while (s_entities.ContainsKey(s_maxId));
            return s_maxId;
        }

        private void SetId(int id)
        {
            if (id < 0)
                throw new Exception($"Entity Id cannot be negative! Actual: {id}!");

            s_entities.Remove(this.id);
            s_entities[id] = this;

            this.id = id;
        }

        /// <summary>
        /// Finds an entity by its ID.
        /// </summary>
        /// <param name="id">The entity ID.</param>
        /// <param name="entity">The found entity, if any.</param>
        /// <returns>True if the entity exists; otherwise, false.</returns>
        public static bool Find(int id, out IEntity entity) => s_entities.TryGetValue(id, out entity);

        /// <summary>
        /// Resets all static entity tracking information (used internally on play mode enter).
        /// </summary>
#if UNITY_EDITOR
        [InitializeOnEnterPlayMode]
#endif
        public static void ResetAll()
        {
            s_maxId = -1;
            s_entities.Clear();
        }
    }
}