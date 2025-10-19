using UnityEngine;

namespace Atomic.Entities
{
    public abstract partial class SceneEntityBaker<E> : MonoBehaviour where E : IEntity
    {
        public E Bake()
        {
            E entity = this.Create();
            Destroy(this.gameObject);
            return entity;
        }

        protected abstract E Create();
    }
}