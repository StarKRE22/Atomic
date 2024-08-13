using UnityEngine;

namespace Atomic.Contexts
{
    public abstract class ScriptableContextInstallerBase : ScriptableObject
    {
        public abstract void Install(IContext context);
    }
}