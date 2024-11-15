using UnityEngine.Scripting.APIUpdating;

namespace Atomic.Contexts
{
    [MovedFrom(true, null, null, "IFixedUpdateSystem")]
    public interface IContextFixedUpdate : IContextSystem
    {
        void FixedUpdate(IContext context, float deltaTime);
    }
}