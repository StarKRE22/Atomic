// using System;
//
// namespace Atomic.Entities
// {
//     /**
//      * Experimental
//      */
//     public partial interface IEntityWorld<E>
//     {
//         /// <summary>
//         /// Event triggered when a system is added.
//         /// </summary>
//         event Action<IEntityWorld<E>, IEntitySystem<E>> OnSystemAdded;
//
//         /// <summary>
//         /// Event triggered when a system is deleted.
//         /// </summary>
//         event Action<IEntityWorld<E>, IEntitySystem<E>> OnSystemDeleted;
//         
//         /// <summary>
//         /// Total number of systems attached to this system.
//         /// </summary>
//         int SystemCount { get; }
//         
//         /// <summary>
//         /// Adds a system to the world.
//         /// </summary>
//         void AddSystem(IEntitySystem<E> system);
//
//         /// <summary>
//         /// Checks if a specific system exists.
//         /// </summary>
//         bool HasSystem(IEntitySystem<E> system);
//
//         /// <summary>
//         /// Checks if a system of the specified type exists.
//         /// </summary>
//         bool HasSystem<T>() where T : IEntitySystem<E>;
//
//         /// <summary>
//         /// Removes a specific system from the world.
//         /// </summary>
//         bool DelSystem(IEntitySystem<E> system);
//
//         /// <summary>
//         /// Removes a system of the specified type.
//         /// </summary>
//         bool DelSystem<T>() where T : IEntitySystem<E>;
//
//         /// <summary>
//         /// Removes all systems of the specified type.
//         /// </summary>
//         void DelSystems<T>() where T : IEntitySystem<E>;
//
//         /// <summary>
//         /// Clears all systems from the entity.
//         /// </summary>
//         void ClearSystems();
//     }
// }