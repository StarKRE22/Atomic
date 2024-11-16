using UnityEngine;

namespace Atomic.Entities
{
    public abstract class ScriptableEntityInstaller : ScriptableObject
    {
        public abstract void Install(IEntity entity);
    }
}