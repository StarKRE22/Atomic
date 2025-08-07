using System.Collections.Generic;
using UnityEngine;

namespace SampleGame
{
    public static class GameOverUseCase
    {
        public static void GameOver(IGameContext context)
        {
            Time.timeScale = 0;
            context.GetWinnerTeam().Value = DefineWinnerTeam(context);
            context.GetGameOverEvent().Invoke();
            Debug.Log("Game Over!");
        }

        private static TeamType DefineWinnerTeam(IGameContext context)
        {
            IDictionary<TeamType, IPlayerContext> players = context.GetPlayers();
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