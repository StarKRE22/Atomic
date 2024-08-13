using UnityEngine.Scripting.APIUpdating;

namespace Atomic.Contexts
{
    [MovedFrom(true, null, null, "IInitSystem")]
    public interface IContextInit : IContextSystem
    {
        void Init(IContext context);
    }
}