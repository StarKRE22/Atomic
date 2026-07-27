using Atomic.Entities;

namespace Game.Gameplay
{
    public static partial class GameContextAPI
    {
        public static readonly ValueKey<IGameContext, IGameEventBus> EventBus = new(nameof(EventBus));
    }
}
