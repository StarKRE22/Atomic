using UnityEngine;

namespace Atomic.Events
{
    public abstract class SceneEventBusInstaller : MonoBehaviour, IEventBusInstaller
    {
        public abstract void Install(IEventBus bus);
    }
}