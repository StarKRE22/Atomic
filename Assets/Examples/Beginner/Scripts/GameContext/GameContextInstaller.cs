using System.Collections.Generic;
using Atomic.Elements;
using Atomic.Entities;
using UnityEngine;

namespace BeginnerGame
{
    public sealed class GameContextInstaller : SceneEntityInstaller<GameContext>
    {
        [SerializeField]
        private Transform _worldTransform;

        [SerializeField]
        private Cooldown _gameCountdown;

        [SerializeField]
        private TeamCatalog _teamCatalog;

        [Header("Coin System")]
        [SerializeField]
        private GameEntity _coinPrefab;

        [SerializeField]
        private Cooldown _coinSpawnPeriod = new(2);

        [SerializeField]
        private Bounds _coinSpawnArea = new(Vector3.zero, new Vector3(5, 0, 5));

        public override void Install(GameContext context)
        {
            context.AddPlayers(new Dictionary<TeamType, PlayerContext>());
            context.AddTeamCatalog(_teamCatalog);

            //Game countdown
            context.AddGameCountdown(_gameCountdown);
            context.WhenTick(_gameCountdown.Tick);
            
            context.AddBehaviour<GameOverController>();

            //Game Over
            context.AddGameOverEvent(new BaseEvent());
            context.AddWinnerTeam(new ReactiveVariable<TeamType>());

            //Coin system:
            context.AddBehaviour(new CoinSpawnController(
                _coinPrefab,
                _worldTransform,
                _coinSpawnArea,
                _coinSpawnPeriod
            ));
        }
    }
}