using Atomic.Entities;

namespace Game.Gameplay
{
    public static partial class GameContextAPI
    {
        public static readonly ValueKey<IGameContext, IMultiEntityPool<GameEntityType, IGameEntity>> EntityPool =
            new(nameof(EntityPool));

        public static readonly ValueKey<IGameContext, IEntityWorld<IGameEntity>> EntityWorld =
            new(nameof(EntityWorld));

        public static readonly ValueKey<IGameContext, EntitySpawnInfo[]> InitialEntities =
            new(nameof(InitialEntities));
    }
}