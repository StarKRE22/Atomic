using System;
using System.Collections.Generic;

namespace Atomic.Entities
{
    public interface IEntity
    {
        int InstanceId { get; }
        string Name { get; set; }

        #region Lifecycle
        
        event Action OnInitialized;
        event Action OnDisposed;
        event Action OnEnabled;
        event Action OnDisabled;

        event Action<float> OnUpdated;
        event Action<float> OnFixedUpdated;
        event Action<float> OnLateUpdated; 

        bool Initialized { get; }
        bool Enabled { get; }

        void Init();
        void Enable();
        void Disable();
        void Dispose();
        
        void OnUpdate(float deltaTime);
        void OnFixedUpdate(float deltaTime);
        void OnLateUpdate(float deltaTime);
        
        #endregion

        #region Tags

        event Action<IEntity, int> OnTagAdded;
        event Action<IEntity, int> OnTagDeleted;
        event Action<IEntity> OnTagsCleared;

        IReadOnlyCollection<int> Tags { get; }
        
        bool HasTag(int tag);
        bool AddTag(int tag);
        bool DelTag(int tag);
        bool ClearTags();

        #endregion

        #region Values

        event Action<IEntity, int, object> OnValueAdded;
        event Action<IEntity, int, object> OnValueDeleted;
        event Action<IEntity, int, object> OnValueChanged; 
        event Action<IEntity> OnValuesCleared;

        IReadOnlyDictionary<int, object> Values { get; }

        T GetValue<T>(int id);
        object GetValue(int id);
        bool TryGetValue<T>(int id, out T value);
        bool TryGetValue(int id, out object value);

        bool AddValue(int id, object value);
        bool DelValue(int id);
        bool DelValue(int id, out object removed);
        
        void SetValue(int id, object value);
        void SetValue(int id, object value, out object previous);

        bool HasValue(int id);
        bool ClearValues();

        #endregion

        #region Behaviours

        event Action<IEntity, IEntityBehaviour> OnBehaviourAdded;
        event Action<IEntity, IEntityBehaviour> OnBehaviourDeleted;
        event Action<IEntity> OnBehavioursCleared;

        IReadOnlyCollection<IEntityBehaviour> Behaviours { get; }
        
        bool AddBehaviour(IEntityBehaviour behaviour);
        bool DelBehaviour(IEntityBehaviour behaviour);
        bool HasBehaviour(IEntityBehaviour behaviour);
        bool ClearBehaviours();

        #endregion
    }
}