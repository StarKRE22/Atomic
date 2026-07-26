using Atomic.Events;

namespace Game.Gameplay
{
    public sealed class GameEventBus : ThreadSafeEventBus, IGameEventBus
    {
    }
}