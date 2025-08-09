// using Atomic.Elements;
// using Atomic.Entities;
//
// namespace ShooterGame.Gameplay
// {
//     public static class LeaderboardUseCase
//     {
//         public static bool AddScore(in IGameContext gameContext, in KillArgs args)
//         {
//             IEntity instigator = args.instigator;
//             IEntity victim = args.victim;
//
//             if (instigator == null || victim == null || instigator.Equals(victim))
//                 return false;
//
//             TeamType instigatorTeam = instigator.GetTeam().Value;
//             TeamType victimTeam = victim.GetTeam().Value;
//             if (instigatorTeam == victimTeam)
//                 return false; 
//
//             AddScore(gameContext, instigatorTeam);
//             return true;
//         }
//
//         public static void AddScore(in IGameContext gameContext, in TeamType teamType, int score = 1)
//         {
//             IReactiveDictionary<TeamType,int> leaderboard = gameContext.GetLeaderboard();
//             leaderboard[teamType] += score;
//         }
//     }
// }