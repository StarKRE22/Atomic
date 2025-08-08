using System;
using System.Collections.Generic;

namespace Atomic.Entities
{
    public class EntityStub : IEntity
    {
        public event Action OnSpawned;
        public event Action OnDespawned;
        public bool IsSpawned { get; }

        public void Spawn()
        {
            throw new NotImplementedException();
        }

        public void Despawn()
        {
            throw new NotImplementedException();
        }

        public event Action OnActivated;
        public event Action OnInactivated;
        public bool IsActive { get; }

        public void Activate()
        {
            throw new NotImplementedException();
        }

        public void Inactivate()
        {
            throw new NotImplementedException();
        }

        public event Action<float> OnUpdated;
        public event Action<float> OnFixedUpdated;
        public event Action<float> OnLateUpdated;

        public void OnUpdate(float deltaTime)
        {
            throw new NotImplementedException();
        }

        public void OnFixedUpdate(float deltaTime)
        {
            throw new NotImplementedException();
        }

        public void OnLateUpdate(float deltaTime)
        {
            throw new NotImplementedException();
        }

        public event Action OnStateChanged;
        public int InstanceID { get; }
        public string Name { get; set; }
        public event Action<IEntity, int> OnTagAdded;
        public event Action<IEntity, int> OnTagDeleted;
        public int TagCount { get; }

        public bool HasTag(int key)
        {
            throw new NotImplementedException();
        }

        public bool AddTag(int key)
        {
            throw new NotImplementedException();
        }

        public bool DelTag(int key)
        {
            throw new NotImplementedException();
        }

        public void ClearTags()
        {
            throw new NotImplementedException();
        }

        public int[] GetTags()
        {
            throw new NotImplementedException();
        }

        public int CopyTags(int[] results)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<int> GetTagEnumerator()
        {
            throw new NotImplementedException();
        }

        public event Action<IEntity, IEntityBehaviour> OnBehaviourAdded;
        public event Action<IEntity, IEntityBehaviour> OnBehaviourDeleted;
        public int BehaviourCount { get; }

        public void AddBehaviour(IEntityBehaviour behaviour)
        {
            throw new NotImplementedException();
        }

        public T GetBehaviour<T>() where T : IEntityBehaviour
        {
            throw new NotImplementedException();
        }

        public bool TryGetBehaviour<T>(out T behaviour) where T : IEntityBehaviour
        {
            throw new NotImplementedException();
        }

        public bool HasBehaviour(IEntityBehaviour behaviour)
        {
            throw new NotImplementedException();
        }

        public bool HasBehaviour<T>() where T : IEntityBehaviour
        {
            throw new NotImplementedException();
        }

        public bool DelBehaviour(IEntityBehaviour behaviour)
        {
            throw new NotImplementedException();
        }

        public bool DelBehaviour<T>() where T : IEntityBehaviour
        {
            throw new NotImplementedException();
        }

        public void DelAllBehaviours<T>() where T : IEntityBehaviour
        {
            throw new NotImplementedException();
        }

        public void ClearBehaviours()
        {
            throw new NotImplementedException();
        }

        public IEntityBehaviour[] GetBehaviours()
        {
            throw new NotImplementedException();
        }

        public T[] GetBehaviours<T>() where T : IEntityBehaviour
        {
            throw new NotImplementedException();
        }

        public int CopyBehaviours(IEntityBehaviour[] results)
        {
            throw new NotImplementedException();
        }

        public int CopyBehaviours<T>(T[] results) where T : IEntityBehaviour
        {
            throw new NotImplementedException();
        }

        public IEnumerator<IEntityBehaviour> GetBehaviourEnumerator()
        {
            throw new NotImplementedException();
        }

        public event Action<IEntity, int> OnValueAdded;
        public event Action<IEntity, int> OnValueDeleted;
        public event Action<IEntity, int> OnValueChanged;
        public int ValueCount { get; }

        public T GetValue<T>(int key)
        {
            throw new NotImplementedException();
        }

        public ref T GetValueUnsafe<T>(int key)
        {
            throw new NotImplementedException();
        }

        public object GetValue(int key)
        {
            throw new NotImplementedException();
        }

        public bool TryGetValue<T>(int key, out T value)
        {
            throw new NotImplementedException();
        }

        public bool TryGetValueUnsafe<T>(int key, out T value)
        {
            throw new NotImplementedException();
        }

        public bool TryGetValue(int key, out object value)
        {
            throw new NotImplementedException();
        }

        public void SetValue(int key, object value)
        {
            throw new NotImplementedException();
        }

        public void SetValue<T>(int key, T value) where T : struct
        {
            throw new NotImplementedException();
        }

        public bool HasValue(int key)
        {
            throw new NotImplementedException();
        }

        public void AddValue(int key, object value)
        {
            throw new NotImplementedException();
        }

        public void AddValue<T>(int key, T value) where T : struct
        {
            throw new NotImplementedException();
        }

        public bool DelValue(int key)
        {
            throw new NotImplementedException();
        }

        public void ClearValues()
        {
            throw new NotImplementedException();
        }

        public KeyValuePair<int, object>[] GetValues()
        {
            throw new NotImplementedException();
        }

        public int CopyValues(KeyValuePair<int, object>[] results)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<KeyValuePair<int, object>> GetValueEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}