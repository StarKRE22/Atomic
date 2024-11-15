using UnityEngine.Scripting.APIUpdating;

namespace Atomic.Contexts
{
    [MovedFrom(true, null, null, "ILateUpdateSystem")] 
    public interface IContextLateUpdate : IContextSystem
    {
        void LateUpdate(IContext context, float deltaTime);
    }
}