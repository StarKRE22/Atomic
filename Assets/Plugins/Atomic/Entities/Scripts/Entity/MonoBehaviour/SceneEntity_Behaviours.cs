using System;
using System.Collections.Generic;

namespace Atomic.Entities
{
    public partial class SceneEntity<E>
    {
        /// <summary>
        /// Occurs when a behaviour is added to the entity.
        /// </summary>
        public event Action<E, IBehaviour<E>> OnBehaviourAdded
        {
            add => this.Entity.OnBehaviourAdded += value;
            remove => this.Entity.OnBehaviourAdded -= value;
        }

        /// <summary>
        /// Occurs when a behaviour is removed from the entity.
        /// </summary>
        public event Action<E, IBehaviour<E>> OnBehaviourDeleted
        {
            add => this.Entity.OnBehaviourDeleted += value;
            remove => this.Entity.OnBehaviourDeleted -= value;
        }
        
        /// <summary>
        /// Gets the number of behaviours attached to the entity.
        /// </summary>
        public int BehaviourCount => this.Entity.BehaviourCount;

        /// <summary>
        /// Adds a behaviour to the entity.
        /// </summary>
        public void AddBehaviour(IBehaviour<E> behaviour) => this.Entity.AddBehaviour(behaviour);

        /// <summary>
        /// Gets a behaviour of the specified type.
        /// </summary>
        public T GetBehaviour<T>() where T : IBehaviour<E> => this.Entity.GetBehaviour<T>();
        
        /// <summary>
        /// Attempts to get a behaviour of the specified type.
        /// </summary>
        public bool TryGetBehaviour<T>(out T behaviour) where T : IBehaviour<E> => this.Entity.TryGetBehaviour(out behaviour);

        /// <summary>
        /// Gets an array of all behaviours attached to the entity.
        /// </summary>
        public IBehaviour<E>[] GetBehaviours() => this.Entity.GetBehaviours();
        
        /// <summary>
        /// Copies all behaviours into the provided array.
        /// </summary>
        public int GetBehaviours(IBehaviour<E>[] results) => this.Entity.GetBehaviours(results);
        
        /// <summary>
        /// Gets the behaviour at the specified index.
        /// </summary>
        public IBehaviour<E> GetBehaviourAt(in int index) => this.Entity.GetBehaviourAt(index);
        
        /// <summary>
        /// Removes the specified behaviour from the entity.
        /// </summary>
        public bool DelBehaviour(IBehaviour<E> behaviour) => this.Entity.DelBehaviour(behaviour);
        
        /// <summary>
        /// Removes the behaviour of the specified type from the entity.
        /// </summary>
        public bool DelBehaviour<T>() where T : IBehaviour<E> => this.Entity.DelBehaviour<T>();

        /// <summary>
        /// Checks whether the entity contains a behaviour of the specified type.
        /// </summary>
        public bool HasBehaviour<T>() where T : IBehaviour<E> => this.Entity.HasBehaviour<T>();
        
        /// <summary>
        /// Checks whether the entity contains the specified behaviour.
        /// </summary>
        public bool HasBehaviour(IBehaviour<E> behaviour) => this.Entity.HasBehaviour(behaviour);
        
        /// <summary>
        /// Removes all behaviours from the entity.
        /// </summary>
        public void ClearBehaviours() => this.Entity.ClearBehaviours();
        
        /// <summary>
        /// Returns an enumerator over all behaviours attached to the entity.
        /// </summary>
        IEnumerator<IBehaviour<E>> IEntity<E>.GetBehaviourEnumerator() => this.Entity.GetBehaviourEnumerator();

        public Entity<E>.BehaviourEnumerator GetBehaviourEnumerator() => this.Entity.GetBehaviourEnumerator();
    }
}