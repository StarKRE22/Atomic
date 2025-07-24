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
}