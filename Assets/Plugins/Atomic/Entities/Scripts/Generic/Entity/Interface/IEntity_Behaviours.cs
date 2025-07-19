using System;
using System.Collections.Generic;

namespace Atomic.Entities
{
    ///Represents a collection of behaviours (<see cref="IBehaviour"/>) for modular logic
    public partial interface IEntity<E>
    {
        /// <summary>
        /// Event triggered when a behaviour is added.
        /// </summary>
        event Action<E, IBehaviour<E>> OnBehaviourAdded;

        /// <summary>
        /// Event triggered when a behaviour is deleted.
        /// </summary>
        event Action<E, IBehaviour<E>> OnBehaviourDeleted;

        /// <summary>
        /// Number of behaviours attached to this entity.
        /// </summary>
        int BehaviourCount { get; }

        /// <summary>
        /// Adds a behaviour to the entity.
        /// </summary>
        void AddBehaviour(IBehaviour<E> behaviour);

        /// <summary>
        /// Gets the first behaviour of the specified type.
        /// </summary>
        T GetBehaviour<T>() where T : IBehaviour<E>;

        /// <summary>
        /// Tries to get a behaviour of the specified type.
        /// </summary>
        bool TryGetBehaviour<T>(out T behaviour) where T : IBehaviour<E>;

        /// <summary>
        /// Checks if a specific behaviour exists.
        /// </summary>
        bool HasBehaviour(IBehaviour<E> behaviour);

        /// <summary>
        /// Checks if a behaviour of the specified type exists.
        /// </summary>
        bool HasBehaviour<T>() where T : IBehaviour<E>;

        /// <summary>
        /// Removes a specific behaviour.
        /// </summary>
        bool DelBehaviour(IBehaviour<E> behaviour);

        /// <summary>
        /// Removes a behaviour of the specified type.
        /// </summary>
        bool DelBehaviour<T>() where T : IBehaviour<E>;

        /// <summary>
        /// Clears all behaviours from the entity.
        /// </summary>
        void ClearBehaviours();

        /// <summary>
        /// Returns all behaviours attached to the entity.
        /// </summary>
        IBehaviour<E>[] GetBehaviours();

        /// <summary>
        /// Copies behaviours into the provided array.
        /// </summary>
        int GetBehaviours(IBehaviour<E>[] results);

        /// <summary>
        /// Enumerates all behaviours.
        /// </summary>
        IEnumerator<IBehaviour<E>> GetBehaviourEnumerator();
    }
}