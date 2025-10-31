using System.Collections.Generic;
using Atomic.Elements;
using Atomic.Entities;
using UnityEngine;

namespace BeginnerGame
{
    public sealed class GameContextInstaller : SceneEntityInstaller
    {
        [SerializeField]
        private Transform _worldTransform;

        [SerializeField]
        private Cooldown _gameCountdown;

        [SerializeField]
        private SpawnInfo _coinSpawnInfo;

        [SerializeField]
        private PlayerInfo[] _players;

        public override void Install(IEntity context)
        {
            // Players
            var players = new Dictionary<TeamType, IEntity>();
            foreach (PlayerInfo playerInfo in _players)
            {
                TeamType team = playerInfo.team;
                SceneEntity character = playerInfo.character;
                players.Add(team, character);
                context.WhenDisable(character.Disable);
            }

            context.AddPlayers(players);

            // Countdown
            context.AddGameCountdown(_gameCountdown);
            context.WhenTick(_gameCountdown.Tick);

            // Coin system
            context.AddBehaviour<CoinSpawnBehaviour>();

            //Game Over
            context.AddGameOverEvent(new BaseEvent());
            context.AddBehaviour<GameOverBehaviour>();
        }
    }
}