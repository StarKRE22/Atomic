using System;
using System.Collections.Generic;

namespace Atomic.Contexts
{
    public interface IContext
    {
        #region Main

        string Name { get; set; }
        IContext Parent { get; set; }

        #endregion
        
        #region Lifecycle

        event Action OnInitiazized;
        event Action OnEnabled;
        event Action OnDisabled;
        event Action OnDisposed;
        
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

        #region Values

        event Action<int, object> OnValueAdded;
        event Action<int, object> OnValueDeleted;
        event Action<int, object> OnValueChanged;

        IReadOnlyDictionary<int, object> Values { get; }

        bool AddValue(int key, object value);
        void SetValue(int key, object value);
        bool DelValue(int key);
        bool DelValue(int key, out object removed);
        bool HasValue(int key);

        T GetValue<T>(int key);
        object GetValue(int key);
        bool TryGetValue<T>(int id, out T value);
        bool TryGetValue(int id, out object value);
        
        #endregion

        #region Systems

        event Action<IContextSystem> OnSystemAdded;
        event Action<IContextSystem> OnSystemRemoved;
        
        IReadOnlyCollection<IContextSystem> Systems { get; }
        
        T GetSystem<T>() where T : IContextSystem;
        bool TryGetSystem<T>(out T result) where T : IContextSystem;

        bool AddSystem(IContextSystem system);
        bool AddSystem<T>() where T : IContextSystem, new();
        
        bool DelSystem(IContextSystem system);
        bool DelSystem<T>() where T : IContextSystem;
        
        bool HasSystem(IContextSystem system);
        bool HasSystem<T>() where T : IContextSystem;
        
        #endregion

        void Clear();
    }
}