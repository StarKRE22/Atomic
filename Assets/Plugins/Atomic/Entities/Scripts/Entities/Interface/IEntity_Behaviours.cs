using System;
using System.Collections.Generic;

namespace Atomic.Entities
{
    ///Represents a collection of behaviours <see cref="IEntityBehaviour"/> for modular logic
    public partial interface IEntity
    {
        /// <summary>
        /// Event triggered when a behaviour is added.
        /// </summary>
        event Action<IEntity, IEntityBehaviour> OnBehaviourAdded;

        /// <summary>
        /// Event triggered when a behaviour is deleted.
        /// </summary>
        event Action<IEntity, IEntityBehaviour> OnBehaviourDeleted;

        /// <summary>
        /// Number of behaviours attached to this entity.
        /// </summary>
        int BehaviourCount { get; }

        /// <summary>
        /// Adds a behaviour to the entity.
        /// </summary>
        void AddBehaviour(IEntityBehaviour behaviour);

        /// <summary>
        /// Gets the first behaviour of the specified type.
        /// </summary>
        T GetBehaviour<T>() where T : IEntityBehaviour;

        /// <summary>
        /// Returns the behaviour instance at the given index.
        /// </summary>
        IEntityBehaviour GetBehaviourAt(int index);
        
        /// <summary>
        /// Tries to get a behaviour of the specified type.
        /// </summary>
        bool TryGetBehaviour<T>(out T behaviour) where T : IEntityBehaviour;

        /// <summary>
        /// Checks if a specific behaviour exists.
        /// </summary>
        bool HasBehaviour(IEntityBehaviour behaviour);

        /// <summary>
        /// Checks if a behaviour of the specified type exists.
        /// </summary>
        bool HasBehaviour<T>() where T : IEntityBehaviour;

        /// <summary>
        /// Removes a specific behaviour.
        /// </summary>
        bool DelBehaviour(IEntityBehaviour behaviour);

        /// <summary>
        /// Removes a behaviour of the specified type.
        /// </summary>
        bool DelBehaviour<T>() where T : IEntityBehaviour;

        /// <summary>
        /// Removes all behaviour of the specified type.
        /// </summary>
        void DelBehaviours<T>() where T : IEntityBehaviour;

        /// <summary>
        /// Clears all behaviours from the entity.
        /// </summary>
        void ClearBehaviours();

        /// <summary>
        /// Returns all behaviours attached to the entity.
        /// </summary>
        IEntityBehaviour[] GetBehaviours();
        
        /// <summary>
        /// Returns all behaviours of type T that attached to the entity.
        /// </summary>
        T[] GetBehaviours<T>() where T : IEntityBehaviour;

        /// <summary>
        /// Copies behaviours into the provided array.
        /// </summary>
        int CopyBehaviours(IEntityBehaviour[] results);
        
        /// <summary>
        /// Copies behaviours of type T into the provided array.
        /// </summary>
        int CopyBehaviours<T>(T[] results) where T : IEntityBehaviour; 

        /// <summary>
        /// Enumerates all behaviours.
        /// </summary>
        IEnumerator<IEntityBehaviour> GetBehaviourEnumerator();
    }
}