#if UNITY_5_3_OR_NEWER
using System;
using System.Collections.Generic;
using UnityEngine;

#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

namespace Atomic.Entities
{
    /// <summary>
    /// Non-generic proxy component for exposing and interacting with a <see cref="SceneEntity"/> in the Unity scene.
    /// </summary>
    /// <remarks>
    /// This component serves as a non-generic version of <see cref="SceneEntityProxy{E}"/> and is intended
    /// for convenience when working with base <see cref="SceneEntity"/> types.
    /// </remarks>
    [AddComponentMenu("Atomic/Entities/Entity Proxy")]
    [DisallowMultipleComponent]
    public class SceneEntityProxy : SceneEntityProxy<SceneEntity>
    {
    }

    /// <summary>
    /// A Unity MonoBehaviour proxy that forwards all <see cref="IEntity"/> calls to an underlying <typeparamref name="E"/> source entity.
    /// </summary>
    /// <typeparam name="E">The type of the source entity, must inherit from <see cref="SceneEntity"/>.</typeparam>
    /// <remarks>
    /// This proxy allows interaction with an entity instance inside the Unity scene while decoupling logic from the GameObject.
    /// It acts as a transparent forwarder for all <see cref="IEntity"/> functionality, including tags, values, lifecycle, and behaviours.
    ///
    /// Use this component to expose scene-level access to the underlying entity while maintaining modularity.
    ///
    /// ⚠️ **Collider Interaction Note**:
    /// If your entity consists of multiple child colliders (e.g., hitboxes, triggers),
    /// and you want to detect which entity was interacted with (e.g., on hit or raycast),
    /// you can place <c>SceneEntityProxy</c> on each child and reference the same source <see cref="SceneEntity"/>.
    /// This allows unified access to the logical entity regardless of which physical collider was hit.
    ///
    /// <example>
    /// Example: Detecting hits from any collider on the entity:
    /// <code>
    /// void OnTriggerEnter(Collider other)
    /// {
    ///     if (other.TryGetComponent(out IEntity proxy)) // concrete type is SceneEntityProxy
    ///     {
    ///         Debug.Log($"Hit entity: {entity.Name}");
    ///     }
    /// }
    /// </code>
    /// </example>
    /// </remarks>
    public abstract class SceneEntityProxy<E> : MonoBehaviour, IEntity where E : SceneEntity
    {
        public event Action OnStateChanged
        {
            add => _source.OnStateChanged += value;
            remove => _source.OnStateChanged -= value;
        }

        public E Source
        {
            get => _source;
            set => _source = value;
        }

        public int InstanceID => _source.InstanceID;

        public string Name
        {
            get => _source.Name;
            set => _source.Name = value;
        }

        [Tooltip("Reference to the actual scene entity object that this proxy wraps")]
        [SerializeField]
        private E _source;

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
        public int CopyTags(int[] results) => _source.CopyTags(results);
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
        public int CopyValues(KeyValuePair<int, object>[] results) => _source.CopyValues(results);
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
        public void DelBehaviours<T>() where T : IEntityBehaviour => _source.DelBehaviours<T>();

        public void ClearBehaviours() => _source.ClearBehaviours();

        public int CopyBehaviours(IEntityBehaviour[] results) => _source.CopyBehaviours(results);
        public int CopyBehaviours<T>(T[] results) where T : IEntityBehaviour => _source.CopyBehaviours(results);

        public T[] GetBehaviours<T>() where T : IEntityBehaviour => _source.GetBehaviours<T>();
        public IEntityBehaviour[] GetBehaviours() => _source.GetBehaviours();
        public IEnumerator<IEntityBehaviour> GetBehaviourEnumerator() => _source.GetBehaviourEnumerator();

        #endregion

        #region Lifecycle

        public event Action OnSpawned
        {
            add => _source.OnSpawned += value;
            remove => _source.OnSpawned -= value;
        }

        public event Action OnActivated
        {
            add => _source.OnActivated += value;
            remove => _source.OnActivated -= value;
        }

        public event Action OnDeactivated
        {
            add => _source.OnDeactivated += value;
            remove => _source.OnDeactivated -= value;
        }

        public event Action OnDespawned
        {
            add => _source.OnDespawned += value;
            remove => _source.OnDespawned -= value;
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

        public bool IsSpawned => _source.IsSpawned;
        public bool IsActive => _source.IsActive;

        public void Spawn() => _source.Spawn();
        public void Activate() => _source.Activate();
        public void Deactivate() => _source.Deactivate();
        public void Despawn() => _source.Despawn();
        public void OnUpdate(float deltaTime) => _source.OnUpdate(deltaTime);
        public void OnFixedUpdate(float deltaTime) => _source.OnFixedUpdate(deltaTime);
        public void OnLateUpdate(float deltaTime) => _source.OnLateUpdate(deltaTime);

        #endregion
    }
}
#endif