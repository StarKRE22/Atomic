using System.Collections.Generic;
using Atomic.Elements;
using Atomic.Entities;
using UnityEngine;

namespace BeginnerGame
{
    public sealed class GameOverBehaviour : IEntityInit, IEntityDispose
    {
        private ICooldown _countdown;
        private IEntity _gameContext;
        private IDictionary<TeamType, IEntity> _players;

        public void Init(IEntity entity)
        {
            _gameContext = entity;
            _players = entity.GetPlayers();
            _countdown = entity.GetGameCountdown();
            _countdown.OnCompleted += this.OnGameTimeFinished;
        }

        public void Dispose(IEntity entity)
        {
            _countdown.OnCompleted -= this.OnGameTimeFinished;
        }

        private void OnGameTimeFinished()
        {
            Debug.Log($"{this.GetWinnerTeam()} is win!");
            _gameContext.Disable();
        }

        public TeamType GetWinnerTeam()
        {
            int redMoney = this.GetMoney(TeamType.RED);
            int blueMoney = this.GetMoney(TeamType.BLUE);

            return redMoney > blueMoney
                ? TeamType.RED
                : blueMoney > redMoney
                    ? TeamType.BLUE
                    : TeamType.NONE;
        }

        private int GetMoney(TeamType team)
        {
            IEntity player = _players[team];
            return player.GetMoney().Value;
        }
    }
}