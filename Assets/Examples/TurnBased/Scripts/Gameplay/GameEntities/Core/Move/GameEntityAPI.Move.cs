using Atomic.Elements;
using Atomic.Entities;

namespace Game.Gameplay
{
    public static partial class GameEntityAPI
    {
        public static readonly ValueKey<IGameEntity, IValue<int>> MaxMovesPerTurn =
            new(nameof(MaxMovesPerTurn));
        
        public static readonly ValueKey<IGameEntity, IReactiveVariable<int>> CurrentMovesCount =
            new(nameof(CurrentMovesCount));
    }
}