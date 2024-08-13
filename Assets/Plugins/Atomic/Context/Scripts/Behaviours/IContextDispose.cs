using UnityEngine.Scripting.APIUpdating;

namespace Atomic.Contexts
{
    [MovedFrom(true, null, null, "IDisposeSystem")] 
    public interface IContextDispose : IContextSystem
    {
        void Dispose(IContext context);
    }
}