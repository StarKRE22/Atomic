using System;
using System.Collections.Generic;

namespace Atomic.Entities
{
    ///Represents a collection of behaviours (<see cref="IBehaviour"/>) for modular logic
    public partial interface IEntity
    {
       /// <summary>
        /// Event triggered when a behaviour is added.
        /// </summary>
        event Action<IEntity, IBehaviour> OnBehaviourAdded;

        /// <summary>
        /// Event triggered when a behaviour is deleted.
        /// </summary>
        event Action<IEntity, IBehaviour> OnBehaviourDeleted;

        /// <summary>
        /// Number of behaviours attached to this entity.
        /// </summary>
        int BehaviourCount { get; }

        /// <summary>
        /// Adds a behaviour to the entity.
        /// </summary>
        void AddBehaviour(in IBehaviour behaviour);

        /// <summary>
        /// Gets the first behaviour of the specified type.
        /// </summary>
        T GetBehaviour<T>() where T : IBehaviour;

        /// <summary>
        /// Tries to get a behaviour of the specified type.
        /// </summary>
        bool TryGetBehaviour<T>(out T behaviour) where T : IBehaviour;

        /// <summary>
        /// Checks if a specific behaviour exists.
        /// </summary>
        bool HasBehaviour(IBehaviour behaviour);

        /// <summary>
        /// Checks if a behaviour of the specified type exists.
        /// </summary>
        bool HasBehaviour<T>() where T : IBehaviour;

        /// <summary>
        /// Removes a specific behaviour.
        /// </summary>
        bool DelBehaviour(IBehaviour behaviour);

        /// <summary>
        /// Removes a behaviour of the specified type.
        /// </summary>
        bool DelBehaviour<T>() where T : IBehaviour;

        /// <summary>
        /// Clears all behaviours from the entity.
        /// </summary>
        void ClearBehaviours();

        /// <summary>
        /// Returns all behaviours attached to the entity.
        /// </summary>
        IBehaviour[] GetBehaviours();

        /// <summary>
        /// Copies behaviours into the provided array.
        /// </summary>
        int GetBehaviours(IBehaviour[] results);

        /// <summary>
        /// Enumerates all behaviours.
        /// </summary>
        IEnumerator<IBehaviour> GetBehaviourEnumerator();
    }
}