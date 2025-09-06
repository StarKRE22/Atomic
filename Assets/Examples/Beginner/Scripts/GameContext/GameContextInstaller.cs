using System.Collections.Generic;
using Atomic.Elements;
using Atomic.Entities;
using UnityEngine;

namespace BeginnerGame
{
    public sealed class GameContextInstaller : SceneEntityInstaller<IGameContext>
    {
        [SerializeField]
        private Transform _worldTransform;

        [SerializeField]
        private Cooldown _gameCountdown;

        [SerializeField]
        private TeamCatalog _teamCatalog;

        [Header("Coin System")]
        [SerializeField]
        private CoinPool _coinPool;

        [SerializeField]
        private Cooldown _coinSpawnPeriod = new(2);

        [SerializeField]
        private Bounds _coinSpawnArea = new(Vector3.zero, new Vector3(5, 0, 5));

        public override void Install(IGameContext context)
        {
            context.AddWorldTransform(_worldTransform);
            context.AddPlayers(new Dictionary<TeamType, IPlayerContext>());
            context.AddTeamCatalog(_teamCatalog);

            //Game countdown
            context.AddGameCountdown(_gameCountdown);
            context.AddBehaviour<GameCountdownController>();
            
            //Game Over
            context.AddGameOverEvent(new BaseEvent());
            context.AddWinnerTeam(new ReactiveVariable<TeamType>());

            //Coin system:
            context.AddCoinPool(_coinPool);
            context.AddCoinSpawnArea(_coinSpawnArea);
            context.AddBehaviour(new CoinSpawnController(_coinSpawnPeriod));
            context.AddBehaviour<CoinSpawnAreaGizmos>();
        }
    }
}