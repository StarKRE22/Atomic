using System;
using System.Collections.Generic;

namespace Atomic.Entities
{
    public partial class SceneEntity
    {
        public event Action<IEntity, IBehaviour> OnBehaviourAdded
        {
            add => this.Entity.OnBehaviourAdded += value;
            remove => this.Entity.OnBehaviourAdded -= value;
        }

        public event Action<IEntity, IBehaviour> OnBehaviourDeleted
        {
            add => this.Entity.OnBehaviourDeleted += value;
            remove => this.Entity.OnBehaviourDeleted -= value;
        }
        
        public int BehaviourCount => Entity.BehaviourCount;

        public void AddBehaviour(in IBehaviour behaviour) => this.Entity.AddBehaviour(in behaviour);

        public T GetBehaviour<T>() where T : IBehaviour => Entity.GetBehaviour<T>();
        public bool TryGetBehaviour<T>(out T behaviour) where T : IBehaviour => Entity.TryGetBehaviour(out behaviour);

        public IBehaviour[] GetBehaviours() => Entity.GetBehaviours();
        public int GetBehaviours(in IBehaviour[] results) => Entity.GetBehaviours(in results);
        public IBehaviour GetBehaviourAt(in int index) => this.Entity.GetBehaviourAt(index);
        
        public bool DelBehaviour(in IBehaviour behaviour) => this.Entity.DelBehaviour(in behaviour);
        public bool DelBehaviour<T>() where T : IBehaviour => Entity.DelBehaviour<T>();

        public bool HasBehaviour<T>() where T : IBehaviour => Entity.HasBehaviour<T>();
        public bool HasBehaviour(in IBehaviour behaviour) => this.Entity.HasBehaviour(in behaviour);
        public void ClearBehaviours() => this.Entity.ClearBehaviours();
        
        public IEnumerator<IBehaviour> BehaviourEnumerator() => Entity.BehaviourEnumerator();
    }
}