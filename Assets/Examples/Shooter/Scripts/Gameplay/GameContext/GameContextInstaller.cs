// using System.Collections.Generic;
// using Atomic.Contexts;
// using Atomic.Elements;
// using UnityEngine;
//
// namespace ShooterGame.Gameplay
// {
//     public sealed class GameContextInstaller : SceneContextInstaller<IGameContext>
//     {
//         [SerializeField]
//         private SpawnPointsInstaller _spawnPointsInstaller;
//
//         [SerializeField]
//         private GameCycleInstaller _gameCycleInstaller;
//
//         [SerializeField]
//         private BulletSystemInstaller _bulletSystemInstaller;
//
//         [SerializeField]
//         private LeaderboardInstaller _leaderboardInstaller;
//         
//         [SerializeField]
//         private Transform _worldTransform;
//
//         [SerializeField]
//         private TeamConfig _teamConfig;
//         
//         [SerializeField]
//         public float _respawnTime = 3.0f;
//         
//         protected override void Install(IGameContext context)
//         {
//             context.AddPlayers(new Dictionary<TeamType, IPlayerContext>());
//             context.AddWorldTransform(_worldTransform);
//             context.AddTeamConfig(_teamConfig);
//             context.AddKillEvent(new BaseEvent<KillArgs>());
//             context.AddRespawnTime(new Const<float>(_respawnTime));
//
//             _spawnPointsInstaller.Install(context);
//             _gameCycleInstaller.Install(context);
//             _bulletSystemInstaller.Install(context);
//             _leaderboardInstaller.Install(context);
//         }
//     }
// }