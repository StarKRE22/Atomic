using UnityEngine;

namespace Atomic.Entities
{
    public abstract class EntityViewInstaller<E> : MonoBehaviour, IEntityViewInstaller<E> where E : IEntity
    {
        public abstract void Install(EntityView<E> view);
    }
}