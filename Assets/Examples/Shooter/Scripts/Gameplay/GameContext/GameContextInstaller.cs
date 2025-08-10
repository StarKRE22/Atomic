using System.Collections.Generic;
using Atomic.Elements;
using Atomic.Entities;
using UnityEngine;

namespace ShooterGame.Gameplay
{
    public sealed class GameContextInstaller : SceneEntityInstaller<IGameContext>
    {
        [SerializeField]
        private SpawnPointsInstaller _spawnPointsInstaller;

        [SerializeField]
        private GameCycleInstaller _gameCycleInstaller;

        [SerializeField]
        private LeaderboardInstaller _leaderboardInstaller;
        
        [SerializeField]
        private Transform _worldTransform;

        [SerializeField]
        private TeamCatalog _teamCatalog;
        
        [SerializeField]
        private float _respawnTime = 3.0f;

        [SerializeField]
        private GameEntityPool _bulletPool;
        
        protected override void Install(IGameContext context)
        {
            context.AddPlayers(new Dictionary<TeamType, IPlayerContext>());
            context.AddWorldTransform(_worldTransform);
            context.AddTeamCatalog(_teamCatalog);
            context.AddKillEvent(new BaseEvent<KillArgs>());
            context.AddRespawnTime(new Const<float>(_respawnTime));
            context.AddBulletPool(_bulletPool);

            _spawnPointsInstaller.Install(context);
            _gameCycleInstaller.Install(context);
            _leaderboardInstaller.Install(context);
        }
    }
}