using Atomic.Elements;
using Atomic.Entities;

namespace RTSGame
{
    [GenerateEntityExtensionsAPI(Unsafe = true)]
    public static partial class PlayerContextAPI
    {
        public static readonly ValueKey<IPlayerContext, IValue<TeamType>> Team = new(nameof(Team));
        public static readonly ValueKey<IPlayerContext, EntityFilter<IGameEntity>> Enemies = new(nameof(Enemies));
    }
}
