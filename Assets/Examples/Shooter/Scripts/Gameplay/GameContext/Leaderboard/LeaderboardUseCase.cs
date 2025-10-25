using Atomic.Elements;
using Atomic.Entities;

namespace ShooterGame.Gameplay
{
    public static class LeaderboardUseCase
    {
        public static bool AddScore(IGameContext gameContext, KillArgs args)
        {
            IActor instigator = args.instigator;
            IActor victim = args.victim;

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
            IReactiveDictionary<TeamType,int> leaderboard = gameContext.GetLeaderboard();
            leaderboard[teamType] += score;
        }
    }
}