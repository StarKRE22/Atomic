using Atomic.Elements;
using Atomic.Entities;
using System.Collections.Generic;

namespace Game.Gameplay
{
    [EntityAPI]
    public static partial class GameContextAPI
    {
        public static readonly ValueKey<IGameContext, IReactiveVariable<GameState>> GameState = new(nameof(GameState));
        public static readonly ValueKey<IGameContext, IValue<int>> SpawnDamage = new(nameof(SpawnDamage));
        public static readonly ValueKey<IGameContext, IReactiveVariable<bool>> IsWin = new(nameof(IsWin));
        public static readonly ValueKey<IGameContext, IReactiveVariable<int>> CurrentTurn = new(nameof(CurrentTurn));
        public static readonly ValueKey<IGameContext, IList<EntitySpawnInfo>> EnemyWaves = new(nameof(EnemyWaves));

        public static readonly ValueKey<IGameContext, GameEntityBoard> GameBoard = new(nameof(GameBoard));
        public static readonly ValueKey<IGameContext, GameBoardPathFinder> PathFinder = new(nameof(PathFinder));

        public static readonly ValueKey<IGameContext, IGameEventBus> EventBus = new(nameof(EventBus));

        public static readonly ValueKey<IGameContext, IMultiEntityPool<GameEntityType, IGameEntity>> EntityPool =
            new(nameof(EntityPool));

        public static readonly ValueKey<IGameContext, IEntityWorld<IGameEntity>> EntityWorld =
            new(nameof(EntityWorld));

        public static readonly ValueKey<IGameContext, EntitySpawnInfo[]> InitialEntities =
            new(nameof(InitialEntities));
    }
}
