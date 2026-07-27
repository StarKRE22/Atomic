using Atomic.Entities;
using Modules.SpatialStructures;
using System.Collections.Generic;
using UnityEngine;

namespace RTSGame
{
    [GenerateEntityExtensionsAPI(Unsafe = true)]
    public static partial class GameContextAPI
    {
        public static readonly ValueKey<IGameContext, EntityWorld<IGameEntity>> EntityWorld = new(nameof(EntityWorld));
        public static readonly ValueKey<IGameContext, IMultiEntityPool<GameEntityType, IGameEntity>> EntityPool = new(nameof(EntityPool));
        public static readonly ValueKey<IGameContext, Dictionary<TeamType, IPlayerContext>> Players = new(nameof(Players));
        public static readonly ValueKey<IGameContext, TeamViewConfig> TeamViewConfig = new(nameof(TeamViewConfig));
        public static readonly ValueKey<IGameContext, Transform> PlayerPoint = new(nameof(PlayerPoint));
        public static readonly ValueKey<IGameContext, SpatialGrid2D<IGameEntity>> EntitySpace = new(nameof(EntitySpace));
    }
}
