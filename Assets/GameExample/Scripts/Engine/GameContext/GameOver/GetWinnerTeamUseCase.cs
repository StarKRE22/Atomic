using Atomic.Contexts;

namespace GameExample.Engine
{
    public static class GetWinnerTeamUseCase
    {
        public static TeamType GetWinnerPlayerTeam(this IContext playerContext)
        {
            var players = playerContext.GetPlayerMap();
            var redMoney = players[TeamType.RED].GetMoney().Value;
            var blueMoney = players[TeamType.BLUE].GetMoney().Value;
            
            if (redMoney > blueMoney)
            {
                return TeamType.RED;
            }

            if (blueMoney > redMoney)
            {
                return TeamType.BLUE;
            }

            return TeamType.NONE;
        }
    }
}