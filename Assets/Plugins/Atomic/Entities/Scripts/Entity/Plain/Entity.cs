using System;
using System.Collections.Generic;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Atomic.Entities
{
    public partial class Entity : IEntity
    {
        private const int UNDEFINED_INDEX = -1;

        private static readonly Dictionary<int, IEntity> s_entities = new();
        private static int s_maxId = -1;

        public event Action OnStateChanged;

        public int Id
        {
            get { return this.id; }
            set { this.SetId(value); }
        }

        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }

        private readonly IEntity owner;
        private string name;
        private int id;

        public Entity()
        {
            this.name = string.Empty;
            this.id = NextId();
            this.owner = this;

            this.InitializeTags();
            this.InitializeValues();
            this.InitializeBehaviours();
        }

        public Entity(in IEntity owner)
        {
            this.name = string.Empty;
            this.id = NextId();
            this.owner = owner;

            this.InitializeTags();
            this.InitializeValues();
            this.InitializeBehaviours();
        }
        
        public Entity(in string name)
        {
            this.name = name;
            this.id = NextId();
            this.owner = this;

            this.InitializeTags();
            this.InitializeValues();
            this.InitializeBehaviours();
        }

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

        ~Entity()
        {
            this.UnsubscribeAll();
        }

        public void UnsubscribeAll()
        {
            AtomicHelper.Unsubscribe(ref this.OnStateChanged);

            AtomicHelper.Unsubscribe(ref this.OnInitialized);
            AtomicHelper.Unsubscribe(ref this.OnEnabled);
            AtomicHelper.Unsubscribe(ref this.OnDisabled);
            AtomicHelper.Unsubscribe(ref this.OnUpdated);
            AtomicHelper.Unsubscribe(ref this.OnFixedUpdated);
            AtomicHelper.Unsubscribe(ref this.OnLateUpdated);
            AtomicHelper.Unsubscribe(ref this.OnDisposed);

            AtomicHelper.Unsubscribe(ref this.OnBehaviourAdded);
            AtomicHelper.Unsubscribe(ref this.OnBehaviourDeleted);

            AtomicHelper.Unsubscribe(ref this.OnValueAdded);
            AtomicHelper.Unsubscribe(ref this.OnValueDeleted);
            AtomicHelper.Unsubscribe(ref this.OnValueChanged);

            AtomicHelper.Unsubscribe(ref this.OnTagAdded);
            AtomicHelper.Unsubscribe(ref this.OnTagDeleted);
        }

        public override string ToString()
        {
            return $"{nameof(name)}: {name}, {nameof(id)}: {id}";
        }

        public bool Equals(IEntity other)
        {
            return this.id == other.Id;
        }

        public override bool Equals(object obj)
        {
            return obj is IEntity other && other.Id == this.id;
        }

        public override int GetHashCode()
        {
            return this.id;
        }

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

        public static bool Find(int id, out IEntity entity)
        {
            return s_entities.TryGetValue(id, out entity);
        }

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