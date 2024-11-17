using UnityEngine;

namespace Atomic.Contexts
{
    public abstract class ScriptableContextInstaller : ScriptableObject
    {
        public abstract void Install(IContext context);
    }
}