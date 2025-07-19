using UnityEngine;

namespace Atomic.Entities
{
    public abstract class EntityViewInstaller : MonoBehaviour, IEntityViewInstaller
    {
        public abstract void Install(EntityView view);
    }
}