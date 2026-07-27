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
    }
}
