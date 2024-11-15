using UnityEngine.Scripting.APIUpdating;

namespace Atomic.Contexts
{
    [MovedFrom(true, null, null, "IDisableSystem")]
    public interface IContextDisable : IContextSystem
    {
        void Disable(IContext context);
    }
}