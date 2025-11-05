using System.Collections.Generic;
using Atomic.Elements;
using Atomic.Entities;
using UnityEngine;

namespace BeginnerGame
{
    /// <summary>
    /// Installs the main <b>game context entity</b>, initializing core systems such as
    /// player management, coin spawning, countdown timers, and game-over logic.
    /// </summary>
    /// <remarks>
    /// This installer acts as the central hub for initializing all global game systems.
    /// It binds together gameplay entities (players, spawners, timers) and behaviors
    /// that define the overall game loop.
    ///
    /// <para>
    /// The <see cref="GameContextInstaller"/> typically exists as a root scene object that 
    /// manages all runtime logic shared between player entities.
    /// </para>
    /// </remarks>
   
    /// <seealso cref="SceneEntityInstaller"/>
    /// <seealso cref="CoinSpawnBehaviour"/>
    /// <seealso cref="GameOverBehaviour"/>
    /// <seealso cref="PlayerInfo"/>
    /// <seealso cref="TeamType"/>
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
            }

            context.AddPlayers(players);

            // Countdown
            context.AddGameCountdown(_gameCountdown);
            context.WhenTick(_gameCountdown.Tick);

            // Coin system
            context.AddCoinSpawnInfo(_coinSpawnInfo);
            context.AddBehaviour<CoinSpawnBehaviour>();

            //Game Over
            context.AddBehaviour<GameOverBehaviour>();
        }
    }
}