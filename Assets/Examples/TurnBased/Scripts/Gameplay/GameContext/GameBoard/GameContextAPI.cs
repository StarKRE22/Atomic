using Atomic.Entities;

namespace Game.Gameplay
{
    public static partial class GameContextAPI
    {
        public static readonly ValueKey<IGameContext, GameEntityBoard> GameBoard = new(nameof(GameBoard));
        public static readonly ValueKey<IGameContext, GameBoardPathFinder> PathFinder = new(nameof(PathFinder));
    }
}