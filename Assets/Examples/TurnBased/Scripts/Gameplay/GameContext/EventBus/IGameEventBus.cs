using Atomic.Events;

namespace Game.Gameplay
{
    public interface IGameEventBus : IEventBus
    {
        void Flush();
    }
}