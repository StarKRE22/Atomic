using System;
using System.Collections.Generic;

namespace Atomic.Entities
{
    public partial class SceneEntityProxy<E> where E : SceneEntity<E>
    {
        public event Action<E, IBehaviour<E>> OnBehaviourAdded
        {
            add => _source.OnBehaviourAdded += value;
            remove => _source.OnBehaviourAdded -= value;
        }

        public event Action<E, IBehaviour<E>> OnBehaviourDeleted
        {
            add => _source.OnBehaviourDeleted += value;
            remove => _source.OnBehaviourDeleted -= value;
        }

        public int BehaviourCount => _source.BehaviourCount;

        public void AddBehaviour(IBehaviour<E> behaviour) => _source.AddBehaviour(behaviour);

        public T GetBehaviour<T>() where T : IBehaviour<E> => _source.GetBehaviour<T>();
        public bool TryGetBehaviour<T>(out T behaviour) where T : IBehaviour<E> => _source.TryGetBehaviour(out behaviour);

        public bool HasBehaviour<T>() where T : IBehaviour<E> => _source.HasBehaviour<T>();
        public bool HasBehaviour(IBehaviour<E> behaviour) => _source.HasBehaviour(behaviour);

        public bool DelBehaviour(IBehaviour<E> behaviour) => _source.DelBehaviour(behaviour);
        public bool DelBehaviour<T>() where T : IBehaviour<E> => _source.DelBehaviour<T>();

        public void ClearBehaviours() => _source.ClearBehaviours();
        
        public int GetBehaviours(IBehaviour<E>[] results) => _source.GetBehaviours(results);
        public IBehaviour<E>[] GetBehaviours() => _source.GetBehaviours();
        public IEnumerator<IBehaviour<E>> GetBehaviourEnumerator() => _source.GetBehaviourEnumerator();
    }
}