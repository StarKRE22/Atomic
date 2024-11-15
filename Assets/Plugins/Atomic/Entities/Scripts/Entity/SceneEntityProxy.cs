using System;
using System.Collections.Generic;
using UnityEngine;

namespace Atomic.Entities
{
    [AddComponentMenu("Atomic/Entities/Entity Proxy")]
    [DisallowMultipleComponent]
    public sealed class SceneEntityProxy : MonoBehaviour, IEntity
    {
        [SerializeField]
        public SceneEntity source;

        private void Reset()
        {
            this.source = this.GetComponentInParent<SceneEntity>();
        }

        public event Action OnDisposed
        {
            add => source.OnDisposed += value;
            remove => source.OnDisposed -= value;
        }

        public event Action OnEnabled
        {
            add => source.OnEnabled += value;
            remove => source.OnEnabled -= value;
        }

        public event Action OnDisabled
        {
            add => source.OnDisabled += value;
            remove => source.OnDisabled -= value;
        }

        public event Action<float> OnUpdated
        {
            add => source.OnUpdated += value;
            remove => source.OnUpdated -= value;
        }

        public event Action<float> OnFixedUpdated
        {
            add => source.OnFixedUpdated += value;
            remove => source.OnFixedUpdated -= value;
        }

        public event Action<float> OnLateUpdated
        {
            add => source.OnLateUpdated += value;
            remove => source.OnLateUpdated -= value;
        }

        public void OnLateUpdate(float deltaTime)
        {
            source.OnLateUpdate(deltaTime);
        }

        public event Action<IEntity, int> OnTagAdded
        {
            add => source.OnTagAdded += value;
            remove => source.OnTagAdded -= value;
        }

        public event Action<IEntity, int> OnTagDeleted
        {
            add => source.OnTagDeleted += value;
            remove => source.OnTagDeleted -= value;
        }

        public event Action<IEntity> OnTagsCleared
        {
            add => source.OnTagsCleared += value;
            remove => source.OnTagsCleared -= value;
        }

        public bool ClearTags()
        {
            return source.ClearTags();
        }

        public event Action<IEntity, int, object> OnValueAdded
        {
            add => source.OnValueAdded += value;
            remove => source.OnValueAdded -= value;
        }

        public event Action<IEntity, int, object> OnValueDeleted
        {
            add => source.OnValueDeleted += value;
            remove => source.OnValueDeleted -= value;
        }

        public event Action<IEntity, int, object> OnValueChanged
        {
            add => source.OnValueChanged += value;
            remove => source.OnValueChanged -= value;
        }

        public event Action<IEntity> OnValuesCleared
        {
            add => source.OnValuesCleared += value;
            remove => source.OnValuesCleared -= value;
        }

        public bool ClearValues()
        {
            return source.ClearValues();
        }

        public event Action<IEntity, IEntityBehaviour> OnBehaviourAdded
        {
            add => source.OnBehaviourAdded += value;
            remove => source.OnBehaviourAdded -= value;
        }

        public event Action<IEntity, IEntityBehaviour> OnBehaviourDeleted
        {
            add => source.OnBehaviourDeleted += value;
            remove => source.OnBehaviourDeleted -= value;
        }

        public event Action<IEntity> OnBehavioursCleared
        {
            add => source.OnBehavioursCleared += value;
            remove => source.OnBehavioursCleared -= value;
        }

        public int InstanceId
        {
            get { return source.InstanceId; }
        }

        public string Name
        {
            get => source.Name;
            set => source.Name = value;
        }

        public event Action OnInitialized
        {
            add => source.OnInitialized += value;
            remove => source.OnInitialized -= value;
        }

        public bool Enabled
        {
            get => source.Enabled;
            set => source.Enabled = value;
        }

        public void Init()
        {
            source.Init();
        }

        public void Enable()
        {
            source.Enable();
        }

        public void Disable()
        {
            source.Disable();
        }

        public void Dispose()
        {
            source.Dispose();
        }

        public void OnUpdate(float deltaTime)
        {
            source.OnUpdate(deltaTime);
        }

        public void OnFixedUpdate(float deltaTime)
        {
            source.OnFixedUpdate(deltaTime);
        }

        public bool Initialized
        {
            get => source.Initialized;
        }

        public IReadOnlyCollection<int> Tags => source.Tags;

        public bool HasTag(int tag)
        {
            return source.HasTag(tag);
        }

        public bool AddTag(int tag)
        {
            return source.AddTag(tag);
        }

        public bool DelTag(int tag)
        {
            return source.DelTag(tag);
        }

        public IReadOnlyDictionary<int, object> Values => source.Values;

        public T GetValue<T>(int id)
        {
            return source.GetValue<T>(id);
        }

        public object GetValue(int id)
        {
            return source.GetValue(id);
        }

        public bool TryGetValue<T>(int id, out T value)
        {
            return source.TryGetValue(id, out value);
        }

        public bool TryGetValue(int id, out object value)
        {
            return source.TryGetValue(id, out value);
        }

        public bool AddValue(int id, object value)
        {
            return source.AddValue(id, value);
        }

        public void SetValue(int id, object value)
        {
            source.SetValue(id, value);
        }

        public void SetValue(int id, object value, out object previous)
        {
            source.SetValue(id, value, out previous);
        }

        public bool DelValue(int id)
        {
            return source.DelValue(id);
        }

        public bool DelValue(int id, out object removed)
        {
            return source.DelValue(id, out removed);
        }

        public bool HasValue(int id)
        {
            return source.HasValue(id);
        }

        public IReadOnlyCollection<IEntityBehaviour> Behaviours => source.Behaviours;

        public bool AddBehaviour(IEntityBehaviour behaviour)
        {
            return source.AddBehaviour(behaviour);
        }

        public bool DelBehaviour(IEntityBehaviour behaviour)
        {
            return source.DelBehaviour(behaviour);
        }
        
        public bool HasBehaviour(IEntityBehaviour behaviour)
        {
            return source.HasBehaviour(behaviour);
        }

        public bool ClearBehaviours()
        {
            return source.ClearBehaviours();
        }

        private bool Equals(SceneEntityProxy other)
        {
            return Equals(source, other.source);
        }

        public override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj) || obj is SceneEntityProxy other && Equals(other);
        }

        public override int GetHashCode()
        {
            return this.source.GetHashCode();
        }
    }
}