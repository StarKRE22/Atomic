using System.Collections.Generic;
using Atomic.Elements;
using UnityEngine;

namespace BeginnerGame
{
    public sealed class GameOverController : IGameContextInit, IGameContextDispose
    {
        private ICooldown _countdown;
        private GameContext _gameContext;

        public void Init(GameContext context)
        {
            _gameContext = context;
            _countdown = context.GetGameCountdown();
            _countdown.OnCompleted += this.OnGameTimeFinished;
        }

        public void Dispose(GameContext entity)
        {
            _countdown.OnCompleted -= this.OnGameTimeFinished;
        }

        private void OnGameTimeFinished()
        {
            Debug.Log($"{this.GetWinnerTeam()} win!");
            _gameContext.Disable();
        }

        public TeamType GetWinnerTeam()
        {
            IDictionary<TeamType, PlayerContext> players = _gameContext.GetPlayers();
            int redMoney = players[TeamType.RED].GetMoney().Value;
            int blueMoney = players[TeamType.BLUE].GetMoney().Value;

            return redMoney > blueMoney
                ? TeamType.RED
                : blueMoney > redMoney
                    ? TeamType.BLUE
                    : TeamType.NONE;
        }
    }
}