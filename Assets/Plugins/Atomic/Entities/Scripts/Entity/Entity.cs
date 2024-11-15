using System;
using System.Collections.Generic;
// ReSharper disable UnusedMember.Global

namespace Atomic.Entities
{
    public sealed class Entity : IEntity
    {
        private static int idGenerator;

        #region Main

        public int InstanceId
        {
            get { return this.id; }
        }

        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }

        private readonly int id;
        private string name;

        public Entity(
            string name = null,
            IEnumerable<int> tags = null,
            IDictionary<int, object> values = null,
            IEnumerable<IEntityBehaviour> behaviours = null
        )
        {
            this.id = ++idGenerator;
            this.name = name ?? string.Empty;
            this.tags = tags == null
                ? new HashSet<int>()
                : new HashSet<int>(tags);
            this.values = values == null
                ? new Dictionary<int, object>()
                : new Dictionary<int, object>(values);
            this.behaviours = behaviours == null
                ? new HashSet<IEntityBehaviour>()
                : new HashSet<IEntityBehaviour>(behaviours);
        }
        
        public override string ToString()
        {
            return $"{nameof(name)}: {this.name}";
        }

        private bool Equals(Entity other)
        {
            return id == other.id;
        }

        public override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj) || obj is Entity other && Equals(other);
        }

        public override int GetHashCode()
        {
            return id;
        }

        #endregion

        #region Lifecycle

        public event Action OnInitialized;
        public event Action OnEnabled;
        public event Action OnDisabled;
        public event Action OnDisposed;

        public event Action<float> OnUpdated;
        public event Action<float> OnFixedUpdated;
        public event Action<float> OnLateUpdated;

        public bool Initialized
        {
            get { return initialized; }
        }

        public bool Enabled
        {
            get { return this.enabled; }
            set
            {
                if (value)
                    this.Enable();
                else
                    this.Disable();
            }
        }

        private bool initialized;
        private bool enabled;

        private readonly List<IEntityUpdate> updates = new();
        private readonly List<IEntityFixedUpdate> fixedUpdates = new();
        private readonly List<IEntityLateUpdate> lateUpdates = new();

        private readonly List<IEntityUpdate> _updateCache = new();
        private readonly List<IEntityFixedUpdate> _fixedUpdateCache = new();
        private readonly List<IEntityLateUpdate> _lateUpdateCache = new();

        public void Init()
        {
            if (this.initialized)
            {
                return;
            }

            foreach (IEntityBehaviour behaviour in this.behaviours)
            {
                if (behaviour is IEntityInit initBehaviour)
                {
                    initBehaviour.Init(this);
                }
            }

            this.initialized = true;
            this.OnInitialized?.Invoke();
        }

        public void Dispose()
        {
            if (!this.initialized)
            {
                return;
            }

            if (this.enabled)
            {
                this.Disable();
            }

            foreach (IEntityBehaviour behaviour in this.behaviours)
            {
                if (behaviour is IEntityDispose entityDispose)
                {
                    entityDispose.Dispose(this);
                }
            }


            this.initialized = false;
            this.OnDisposed?.Invoke();

            //Auto unsubscribe events:
            DelegateUtils.Unsubscribe(ref this.OnInitialized);
            DelegateUtils.Unsubscribe(ref this.OnEnabled);
            DelegateUtils.Unsubscribe(ref this.OnDisabled);
            DelegateUtils.Unsubscribe(ref this.OnUpdated);
            DelegateUtils.Unsubscribe(ref this.OnFixedUpdated);
            DelegateUtils.Unsubscribe(ref this.OnLateUpdated);
            DelegateUtils.Unsubscribe(ref this.OnDisposed);
        }

        public void Enable()
        {
            if (this.enabled)
            {
                return;
            }

            if (!this.initialized)
            {
                this.Init();
            }

            this.enabled = true;

            foreach (IEntityBehaviour behaviour in this.behaviours)
            {
                if (behaviour is IEntityEnable entityEnable)
                {
                    entityEnable.Enable(this);
                }

                if (behaviour is IEntityUpdate update)
                {
                    this.updates.Add(update);
                }

                if (behaviour is IEntityFixedUpdate fixedUpdate)
                {
                    this.fixedUpdates.Add(fixedUpdate);
                }

                if (behaviour is IEntityLateUpdate lateUpdate)
                {
                    this.lateUpdates.Add(lateUpdate);
                }
            }

            this.OnEnabled?.Invoke();
        }

        public void Disable()
        {
            if (!this.enabled)
            {
                return;
            }

            foreach (IEntityBehaviour behaviour in this.behaviours)
            {
                if (behaviour is IEntityDisable entityDisable)
                {
                    entityDisable.Disable(this);
                }

                if (behaviour is IEntityUpdate update)
                {
                    this.updates.Remove(update);
                }

                if (behaviour is IEntityFixedUpdate fixedUpdate)
                {
                    this.fixedUpdates.Remove(fixedUpdate);
                }

                if (behaviour is IEntityLateUpdate lateUpdate)
                {
                    this.lateUpdates.Remove(lateUpdate);
                }
            }

            this.enabled = false;
            this.OnDisabled?.Invoke();
        }

        public void OnUpdate(float deltaTime)
        {
            if (!this.enabled)
            {
                return;
            }

            int count = this.updates.Count;
            if (count != 0)
            {
                _updateCache.Clear();
                _updateCache.AddRange(this.updates);

                for (int i = 0; i < count; i++)
                {
                    IEntityUpdate update = _updateCache[i];
                    update.OnUpdate(this, deltaTime);
                }
            }

            this.OnUpdated?.Invoke(deltaTime);
        }

        public void OnFixedUpdate(float deltaTime)
        {
            if (!this.enabled)
            {
                return;
            }

            int count = this.fixedUpdates.Count;
            if (count != 0)
            {
                _fixedUpdateCache.Clear();
                _fixedUpdateCache.AddRange(this.fixedUpdates);

                for (int i = 0; i < count; i++)
                {
                    var fixedUpdate = _fixedUpdateCache[i];
                    fixedUpdate.OnFixedUpdate(this, deltaTime);
                }
            }

            this.OnFixedUpdated?.Invoke(deltaTime);
        }

        public void OnLateUpdate(float deltaTime)
        {
            if (!this.enabled)
            {
                return;
            }

            int count = this.lateUpdates.Count;
            if (count != 0)
            {
                _lateUpdateCache.Clear();
                _lateUpdateCache.AddRange(this.lateUpdates);

                for (int i = 0; i < count; i++)
                {
                    var lateUpdate = _lateUpdateCache[i];
                    lateUpdate.OnLateUpdate(this, deltaTime);
                }
            }

            this.OnLateUpdated?.Invoke(deltaTime);
        }

        #endregion

        #region Tags

        public event Action<IEntity, int> OnTagAdded;
        public event Action<IEntity, int> OnTagDeleted;
        public event Action<IEntity> OnTagsCleared;

        public IReadOnlyCollection<int> Tags => this.tags;

        private readonly HashSet<int> tags;

        public bool DelTag(int tag)
        {
            if (this.tags.Remove(tag))
            {
                this.OnTagDeleted?.Invoke(this, tag);
                return true;
            }

            return false;
        }

        public bool ClearTags()
        {
            if (this.tags.Count == 0)
            {
                return false;
            }

            this.tags.Clear();
            this.OnTagsCleared?.Invoke(this);
            return true;
        }

        public bool HasTag(int tag)
        {
            return this.tags.Contains(tag);
        }

        public bool AddTag(int tag)
        {
            if (this.tags.Add(tag))
            {
                this.OnTagAdded?.Invoke(this, tag);
                return true;
            }

            return false;
        }

        #endregion

        #region Values

        public event Action<IEntity, int, object> OnValueAdded;
        public event Action<IEntity, int, object> OnValueDeleted;
        public event Action<IEntity, int, object> OnValueChanged;
        public event Action<IEntity> OnValuesCleared;

        public IReadOnlyDictionary<int, object> Values => this.values;

        private readonly Dictionary<int, object> values;

        public T GetValue<T>(int id) 
        {
            return (T) this.values[id];
        }

        public bool TryGetValue<T>(int id, out T value)
        {
            if (this.values.TryGetValue(id, out object v))
            {
                value = (T) v;
                return true;
            }

            value = default;
            return false;
        }

        public object GetValue(int id)
        {
            return this.values[id];
        }

        public bool TryGetValue(int id, out object value)
        {
            return this.values.TryGetValue(id, out value);
        }

        public bool HasValue(int id)
        {
            return this.values.ContainsKey(id);
        }

        public void SetValue(int id, object value, out object previous)
        {
            if (this.values.TryGetValue(id, out previous))
            {
                this.values[id] = value;
                this.OnValueChanged?.Invoke(this, id, value);
            }
            else
            {
                this.AddValue(id, value);
            }
        }

        public void SetValue(int id, object value)
        {
            if (this.values.ContainsKey(id))
            {
                this.values[id] = value;
                this.OnValueChanged?.Invoke(this, id, value);
            }
            else
            {
                this.AddValue(id, value);
            }
        }

        public bool AddValue(int id, object value)
        {
            if (this.values.TryAdd(id, value))
            {
                this.OnValueAdded?.Invoke(this, id, value);
                return true;
            }

            return false;
        }

        public bool DelValue(int id)
        {
            if (this.values.Remove(id, out object removed))
            {
                this.OnValueDeleted?.Invoke(this, id, removed);
                return true;
            }

            return false;
        }

        public bool DelValue(int id, out object removed)
        {
            if (this.values.Remove(id, out removed))
            {
                this.OnValueDeleted?.Invoke(this, id, removed);
                return true;
            }

            return false;
        }

        public bool ClearValues()
        {
            if (this.values.Count == 0)
            {
                return false;
            }

            this.values.Clear();
            this.OnValuesCleared?.Invoke(this);
            return true;
        }

        #endregion

        #region Behaviours

        public event Action<IEntity, IEntityBehaviour> OnBehaviourAdded;
        public event Action<IEntity, IEntityBehaviour> OnBehaviourDeleted;
        public event Action<IEntity> OnBehavioursCleared;

        public IReadOnlyCollection<IEntityBehaviour> Behaviours => this.behaviours;

        private readonly HashSet<IEntityBehaviour> behaviours;

        public bool HasBehaviour(IEntityBehaviour behaviour)
        {
            return this.behaviours.Contains(behaviour);
        }

        public bool AddBehaviour(IEntityBehaviour behaviour)
        {
            if (!this.behaviours.Add(behaviour))
            {
                return false;
            }

            if (this.initialized && behaviour is IEntityInit initBehaviour)
            {
                initBehaviour.Init(this);
            }

            if (this.enabled)
            {
                if (behaviour is IEntityEnable enableBehaviour)
                {
                    enableBehaviour.Enable(this);
                }

                if (behaviour is IEntityUpdate update)
                {
                    this.updates.Add(update);
                }

                if (behaviour is IEntityFixedUpdate fixedUpdate)
                {
                    this.fixedUpdates.Add(fixedUpdate);
                }

                if (behaviour is IEntityLateUpdate lateUpdate)
                {
                    this.lateUpdates.Add(lateUpdate);
                }
            }

            this.OnBehaviourAdded?.Invoke(this, behaviour);
            return true;
        }

        public bool DelBehaviour(IEntityBehaviour behaviour)
        {
            if (!this.behaviours.Remove(behaviour))
            {
                return false;
            }

            if (this.enabled)
            {
                if (behaviour is IEntityUpdate update)
                {
                    this.updates.Remove(update);
                }

                if (behaviour is IEntityFixedUpdate fixedUpdate)
                {
                    this.fixedUpdates.Remove(fixedUpdate);
                }

                if (behaviour is IEntityLateUpdate lateUpdate)
                {
                    this.lateUpdates.Remove(lateUpdate);
                }

                if (behaviour is IEntityDisable disable)
                {
                    disable.Disable(this);
                }
            }

            if (this.initialized && behaviour is IEntityDispose dispose)
            {
                dispose.Dispose(this);
            }

            this.OnBehaviourDeleted?.Invoke(this, behaviour);
            return true;
        }

        public bool ClearBehaviours()
        {
            if (this.behaviours.Count == 0)
            {
                return false;
            }

            this.behaviours.Clear();
            this.OnBehavioursCleared?.Invoke(this);
            return true;
        }

        #endregion
    }
}