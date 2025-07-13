using System;

namespace Atomic.Entities
{
    public partial interface IEntityWorld
    {
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
    }
}