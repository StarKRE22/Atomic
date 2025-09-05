using UnityEngine;

namespace Atomic.Entities
{
    public abstract class SceneEntityAspect : SceneEntityAspect<IEntity>, IEntityAspect
    {
    }

    public abstract class SceneEntityAspect<E> : MonoBehaviour, IEntityAspect<E> where E : IEntity
    {
        public abstract void Apply(E entity);

        public abstract void Discard(E entity);
    }
}