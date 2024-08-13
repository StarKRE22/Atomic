using UnityEngine;

namespace Atomic.Entities
{
    public abstract class ScriptableEntityInstallerBase : ScriptableObject
    {
        public abstract void Install(IEntity entity);
    }
}