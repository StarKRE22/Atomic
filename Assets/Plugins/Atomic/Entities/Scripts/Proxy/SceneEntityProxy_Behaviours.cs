using System;
using System.Collections.Generic;

namespace Atomic.Entities
{
    public partial class SceneEntityProxy<E>
    {
        public event Action<IEntity, IBehaviour> OnBehaviourAdded
        {
            add => _source.OnBehaviourAdded += value;
            remove => _source.OnBehaviourAdded -= value;
        }

        public event Action<IEntity, IBehaviour> OnBehaviourDeleted
        {
            add => _source.OnBehaviourDeleted += value;
            remove => _source.OnBehaviourDeleted -= value;
        }

        public int BehaviourCount => _source.BehaviourCount;

        public void AddBehaviour(in IBehaviour behaviour) => _source.AddBehaviour(in behaviour);

        public T GetBehaviour<T>() where T : IBehaviour => _source.GetBehaviour<T>();
        public bool TryGetBehaviour<T>(out T behaviour) where T : IBehaviour => _source.TryGetBehaviour(out behaviour);

        public bool HasBehaviour<T>() where T : IBehaviour => _source.HasBehaviour<T>();
        public bool HasBehaviour(in IBehaviour behaviour) => _source.HasBehaviour(in behaviour);

        public bool DelBehaviour(in IBehaviour behaviour) => _source.DelBehaviour(in behaviour);
        public bool DelBehaviour<T>() where T : IBehaviour => _source.DelBehaviour<T>();

        public void ClearBehaviours() => _source.ClearBehaviours();
        
        public int GetBehaviours(in IBehaviour[] results) => _source.GetBehaviours(in results);
        public IBehaviour[] GetBehaviours() => _source.GetBehaviours();
        public IEnumerator<IBehaviour> BehaviourEnumerator() => _source.BehaviourEnumerator();
    }
}