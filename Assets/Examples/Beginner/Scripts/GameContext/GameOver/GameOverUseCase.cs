using System.Collections.Generic;
using UnityEngine;

namespace BeginnerGame
{
    public static class GameOverUseCase
    {
        public static void GameOver(IGameContext context)
        {
            context.GetWinnerTeam().Value = GetWinnerTeam(context);
            context.GetGameOverEvent().Invoke();
            
            context.Inactivate();
            Debug.Log("Game Over!");
        }

        public static TeamType GetWinnerTeam(IGameContext context)
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