using System.Collections.Generic;
using Atomic.Elements;
using Atomic.Entities;
using UnityEngine;
using Event = Atomic.Elements.Event;

namespace ShooterGame.Gameplay
{
    public sealed class GameContextInstaller : SceneEntityInstaller<IGameContext>
    {
        private const string WORLD_TRANSFORM_NAME = "[World]";

        [SerializeField]
        private SpawnPointsInstaller _spawnPointsInstaller;

        [SerializeField]
        private GameCycleInstaller _gameCycleInstaller;

        [SerializeField]
        private LeaderboardInstaller _leaderboardInstaller;

        [SerializeField]
        private TeamCatalog _teamCatalog;

        [SerializeField]
        private Const<float> _respawnTime = 3.0f;

        [SerializeField]
        private GameEntityPool _bulletPool;

        public override void Install(IGameContext context)
        {
            context.AddPlayers(new Dictionary<TeamType, IPlayerContext>());
            context.AddWorldTransform(GameObject.Find(WORLD_TRANSFORM_NAME).transform);
            context.AddTeamCatalog(_teamCatalog);
            context.AddKillEvent(new Event<KillArgs>());
            context.AddRespawnDelay(_respawnTime);
            context.AddBulletPool(_bulletPool);
            context.AddGameOverEvent(new Event());

            context.Install(_spawnPointsInstaller);
            context.Install(_gameCycleInstaller);
            context.Install(_leaderboardInstaller);
        }
    }
}