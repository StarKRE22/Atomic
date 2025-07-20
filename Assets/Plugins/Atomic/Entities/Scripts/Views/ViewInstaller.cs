using UnityEngine;

namespace Atomic.Entities
{
    public abstract class ViewInstaller<E> : MonoBehaviour, IViewInstaller<E> where E : IEntity<E>
    {
        public abstract void Install(View<E> view);
    }
}