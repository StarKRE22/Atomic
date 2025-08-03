using System;

namespace Atomic.Entities
{
    /// <summary>
    /// Represents an object that can be spawned and despawned within a system,
    /// such as in a game or simulation environment.
    /// </summary>
    public interface ISpawnable
    {
        /// <summary>
        /// Occurs when the object has been spawned.
        /// </summary>
        event Action OnSpawned;

        /// <summary>
        /// Occurs when the object has been despawned.
        /// </summary>
        event Action OnDespawned;

        /// <summary>
        /// Gets a value indicating whether the object is currently spawned.
        /// </summary>
        bool IsSpawned { get; }

        /// <summary>
        /// Spawns the object.
        /// This method should prepare the object to become active in the world.
        /// </summary>
        void Spawn();

        /// <summary>
        /// Despawns the object.
        /// This method should deactivate the object and clean up any necessary state.
        /// </summary>
        void Despawn();
    }
}