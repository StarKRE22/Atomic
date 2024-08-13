using UnityEngine.Scripting.APIUpdating;

namespace Atomic.Contexts
{
    [MovedFrom(true, null, null, "IUpdateSystem")] 
    public interface IContextUpdate : IContextSystem
    {
        void Update(IContext context, float deltaTime);
    }
}