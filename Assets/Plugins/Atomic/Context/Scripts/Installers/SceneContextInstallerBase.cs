using UnityEngine;

namespace Atomic.Contexts
{
    public abstract class SceneContextInstallerBase : MonoBehaviour, IContextInstaller
    {
        public abstract void Install(IContext context);
    }
}