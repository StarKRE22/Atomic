using UnityEngine;

namespace Atomic.Entities
{
    public abstract class EntityViewPoolAbstract : MonoBehaviour
    {
        public abstract EntityViewBase Rent(string name);

        public abstract void Return(string name, EntityViewBase view);

        public abstract void Clear();
    }
}