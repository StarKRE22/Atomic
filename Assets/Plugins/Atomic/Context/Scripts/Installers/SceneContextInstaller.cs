using UnityEngine;

namespace Atomic.Contexts
{
    public abstract class SceneContextInstaller : MonoBehaviour, IContextInstaller
    {
        public abstract void Install(IContext context);
    }
}