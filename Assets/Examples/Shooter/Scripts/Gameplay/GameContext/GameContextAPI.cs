using Atomic.Elements;
using Atomic.Entities;
using System.Collections.Generic;
using UnityEngine;

namespace ShooterGame.Gameplay
{
    [GenerateEntityExtensionsAPI]
    public static partial class GameContextAPI
    {
        public static readonly ValueKey<IGameContext, IDictionary<TeamType, IPlayerContext>> Players = new(nameof(Players));
        public static readonly ValueKey<IGameContext, IReactiveVariable<float>> GameTime = new(nameof(GameTime));
        public static readonly ValueKey<IGameContext, TeamCatalog> TeamCatalog = new(nameof(TeamCatalog));
        public static readonly ValueKey<IGameContext, IEntityPool<IGameEntity>> BulletPool = new(nameof(BulletPool));
        public static readonly ValueKey<IGameContext, Transform> WorldTransform = new(nameof(WorldTransform));
        public static readonly ValueKey<IGameContext, IReactiveDictionary<TeamType, int>> Leaderboard = new(nameof(Leaderboard));
        public static readonly ValueKey<IGameContext, IEvent<KillArgs>> KillEvent = new(nameof(KillEvent));
        public static readonly ValueKey<IGameContext, IValue<float>> RespawnDelay = new(nameof(RespawnDelay));
        public static readonly ValueKey<IGameContext, IEvent> GameOverEvent = new(nameof(GameOverEvent));
        public static readonly ValueKey<IGameContext, Transform[]> AllSpawnPoints = new(nameof(AllSpawnPoints));
        public static readonly ValueKey<IGameContext, List<Transform>> FreeSpawnPoints = new(nameof(FreeSpawnPoints));
    }
}
