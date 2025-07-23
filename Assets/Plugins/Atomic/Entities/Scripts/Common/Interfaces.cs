using System;

namespace Atomic.Entities
{
    public interface ISpawnable
    {
        event Action OnSpawned;

        event Action OnDespawned;
        
        bool Spawned { get; }
        
        void Spawn();
        
        void Despawn();
    }
    
    public interface IActivatable
    {
        event Action OnEnabled;

        event Action OnDisabled;

        bool Enabled { get; }

        void Enable();

        void Disable();
    }
    
    public interface IUpdatable
    {
        /// <summary>
        /// Invoked on every Update tick.
        /// </summary>
        event Action<float> OnUpdated;

        /// <summary>
        /// Invoked on every FixedUpdate tick.
        /// </summary>
        event Action<float> OnFixedUpdated;

        /// <summary>
        /// Invoked on every LateUpdate tick.
        /// </summary>
        event Action<float> OnLateUpdated;
        
        void OnUpdate(float deltaTime);

        void OnFixedUpdate(float deltaTime);
        
        void OnLateUpdate(float deltaTime);
    }
}