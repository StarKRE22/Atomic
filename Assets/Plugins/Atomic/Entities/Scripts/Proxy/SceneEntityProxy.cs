using System;
using System.Collections.Generic;
using UnityEngine;

namespace Atomic.Entities
{
    [AddComponentMenu("Atomic/Entities/Entity Proxy")]
    [DisallowMultipleComponent]
    public class SceneEntityProxy : SceneEntityProxy<SceneEntity>
    {
    }

    public abstract class SceneEntityProxy<E> : MonoBehaviour, IEntity where E : SceneEntity
    {
        public event Action OnStateChanged
        {
            add => _source.OnStateChanged += value;
            remove => _source.OnStateChanged -= value;
        }

        public E Source => _source;

        [SerializeField]
        private E _source;

        public int InstanceID => _source.InstanceID;

        public string Name
        {
            get => _source.Name;
            set => _source.Name = value;
        }

        public void Clear() => _source.Clear();

        public override bool Equals(object obj) => obj is IEntity entity && _source.InstanceID == entity.InstanceID;

        public override int GetHashCode() => _source.GetHashCode();

        private void Reset() => _source = this.GetComponentInParent<E>();

        #region Tags

        public event Action<IEntity, int> OnTagAdded
        {
            add => _source.OnTagAdded += value;
            remove => _source.OnTagAdded -= value;
        }

        public event Action<IEntity, int> OnTagDeleted
        {
            add => _source.OnTagDeleted += value;
            remove => _source.OnTagDeleted -= value;
        }

        public int TagCount => _source.TagCount;

        public bool HasTag(int key) => _source.HasTag(key);
        public bool AddTag(int key) => _source.AddTag(key);
        public bool DelTag(int key) => _source.DelTag(key);
        public void ClearTags() => _source.ClearTags();

        public int[] GetTags() => _source.GetTags();
        public int GetTags(int[] results) => _source.GetTags(results);
        public IEnumerator<int> GetTagEnumerator() => _source.GetTagEnumerator();

        #endregion

        #region Values

        public event Action<IEntity, int> OnValueAdded
        {
            add => _source.OnValueAdded += value;
            remove => _source.OnValueAdded -= value;
        }

        public event Action<IEntity, int> OnValueDeleted
        {
            add => _source.OnValueDeleted += value;
            remove => _source.OnValueDeleted -= value;
        }

        public event Action<IEntity, int> OnValueChanged
        {
            add => _source.OnValueChanged += value;
            remove => _source.OnValueChanged -= value;
        }

        public int ValueCount => _source.ValueCount;

        public T GetValue<T>(int key) => _source.GetValue<T>(key);
        public ref T GetValueUnsafe<T>(int key) => ref _source.GetValueUnsafe<T>(key);
        public object GetValue(int key) => _source.GetValue(key);

        public bool TryGetValue<T>(int key, out T value) => _source.TryGetValue(key, out value);
        public bool TryGetValueUnsafe<T>(int key, out T value) => _source.TryGetValueUnsafe(key, out value);
        public bool TryGetValue(int key, out object value) => _source.TryGetValue(key, out value);

        public void AddValue(int key, object value) => _source.AddValue(key, value);
        public void AddValue<T>(int key, T value) where T : struct => _source.AddValue(key, value);

        public void SetValue(int key, object value) => _source.SetValue(key, value);
        public void SetValue<T>(int key, T value) where T : struct => _source.SetValue(key, value);

        public bool DelValue(int key) => _source.DelValue(key);
        public bool HasValue(int key) => _source.HasValue(key);
        public void ClearValues() => _source.ClearValues();

        public KeyValuePair<int, object>[] GetValues() => _source.GetValues();
        public int GetValues(KeyValuePair<int, object>[] results) => _source.GetValues(results);
        public IEnumerator<KeyValuePair<int, object>> GetValueEnumerator() => _source.GetValueEnumerator();

        #endregion

        #region Behaviours

        public event Action<IEntity, IEntityBehaviour> OnBehaviourAdded
        {
            add => _source.OnBehaviourAdded += value;
            remove => _source.OnBehaviourAdded -= value;
        }

        public event Action<IEntity, IEntityBehaviour> OnBehaviourDeleted
        {
            add => _source.OnBehaviourDeleted += value;
            remove => _source.OnBehaviourDeleted -= value;
        }

        public int BehaviourCount => _source.BehaviourCount;

        public void AddBehaviour(IEntityBehaviour behaviour) => _source.AddBehaviour(behaviour);

        public T GetBehaviour<T>() where T : IEntityBehaviour => _source.GetBehaviour<T>();

        public bool TryGetBehaviour<T>(out T behaviour) where T : IEntityBehaviour =>
            _source.TryGetBehaviour(out behaviour);

        public bool HasBehaviour<T>() where T : IEntityBehaviour => _source.HasBehaviour<T>();
        public bool HasBehaviour(IEntityBehaviour behaviour) => _source.HasBehaviour(behaviour);

        public bool DelBehaviour(IEntityBehaviour behaviour) => _source.DelBehaviour(behaviour);
        public bool DelBehaviour<T>() where T : IEntityBehaviour => _source.DelBehaviour<T>();

        public void ClearBehaviours() => _source.ClearBehaviours();

        public int GetBehaviours(IEntityBehaviour[] results) => _source.GetBehaviours(results);
        public IEntityBehaviour[] GetBehaviours() => _source.GetBehaviours();
        public IEnumerator<IEntityBehaviour> GetBehaviourEnumerator() => _source.GetBehaviourEnumerator();

        #endregion

        #region Lifecycle

        public event Action OnInitialized
        {
            add => _source.OnInitialized += value;
            remove => _source.OnInitialized -= value;
        }

        public event Action OnEnabled
        {
            add => _source.OnEnabled += value;
            remove => _source.OnEnabled -= value;
        }

        public event Action OnDisabled
        {
            add => _source.OnDisabled += value;
            remove => _source.OnDisabled -= value;
        }

        public event Action OnDisposed
        {
            add => _source.OnDisposed += value;
            remove => _source.OnDisposed -= value;
        }

        public event Action<float> OnUpdated
        {
            add => _source.OnUpdated += value;
            remove => _source.OnUpdated -= value;
        }

        public event Action<float> OnFixedUpdated
        {
            add => _source.OnFixedUpdated += value;
            remove => _source.OnFixedUpdated -= value;
        }

        public event Action<float> OnLateUpdated
        {
            add => _source.OnLateUpdated += value;
            remove => _source.OnLateUpdated -= value;
        }

        public bool Initialized => _source.Initialized;
        public bool Enabled => _source.Enabled;

        public void Init() => _source.Init();
        public void Enable() => _source.Enable();
        public void Disable() => _source.Disable();
        public void Dispose() => _source.Dispose();
        public void OnUpdate(float deltaTime) => _source.OnUpdate(deltaTime);
        public void OnFixedUpdate(float deltaTime) => _source.OnFixedUpdate(deltaTime);
        public void OnLateUpdate(float deltaTime) => _source.OnLateUpdate(deltaTime);

        #endregion
    }
}