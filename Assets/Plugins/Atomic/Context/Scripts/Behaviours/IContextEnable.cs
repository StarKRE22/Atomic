using UnityEngine.Scripting.APIUpdating;

namespace Atomic.Contexts
{
    [MovedFrom(true, null, null, "IEnableSystem")]
    public interface IContextEnable : IContextSystem
    {
        void Enable(IContext context);
    }
}