using System;
using System.Collections.Generic;

namespace Atomic.Entities
{
    public partial interface IEntity
    {
        event Action<IEntity, IBehaviour> OnBehaviourAdded;
        event Action<IEntity, IBehaviour> OnBehaviourDeleted;

        int BehaviourCount { get; }

        void AddBehaviour(in IBehaviour behaviour);
        T GetBehaviour<T>() where T : IBehaviour;
        bool TryGetBehaviour<T>(out T behaviour) where T : IBehaviour;

        bool HasBehaviour(in IBehaviour behaviour);
        bool HasBehaviour<T>() where T : IBehaviour;
        
        bool DelBehaviour(in IBehaviour behaviour);
        bool DelBehaviour<T>() where T : IBehaviour;

        void ClearBehaviours();

        IBehaviour[] GetBehaviours();
        int GetBehaviours(in IBehaviour[] results);
        IEnumerator<IBehaviour> BehaviourEnumerator();
    }
}