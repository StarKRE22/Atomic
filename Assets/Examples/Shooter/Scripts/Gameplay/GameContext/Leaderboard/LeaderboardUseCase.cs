using System.Collections.Generic;

namespace ShooterGame.Gameplay
{
    public static class LeaderboardUseCase
    {
        public static bool AddScore(IGameContext gameContext, KillArgs args)
        {
            IGameEntity instigator = args.instigator;
            IGameEntity victim = args.victim;

            if (instigator == null || victim == null || instigator.Equals(victim))
                return false;

            TeamType instigatorTeam = instigator.GetTeamType().Value;
            TeamType victimTeam = victim.GetTeamType().Value;
            if (instigatorTeam == victimTeam)
                return false;

            AddScore(gameContext, instigatorTeam);
            return true;
        }

        public static void AddScore(IGameContext gameContext, TeamType teamType, int score = 1)
        {
            IDictionary<TeamType, int> leaderboard = gameContext.GetLeaderboard();
            leaderboard[teamType] += score;
        }
        
        public static TeamType GetWinner(IGameContext gameContext)
        {
            IDictionary<TeamType, int> leaderboard = gameContext.GetLeaderboard();
            int redMoney = leaderboard[TeamType.RED];
            int blueMoney = leaderboard[TeamType.BLUE];

            return redMoney > blueMoney
                ? TeamType.RED
                : blueMoney > redMoney
                    ? TeamType.BLUE
                    : TeamType.NEUTRAL;
        }
    }
}