// using System;
// using Atomic.Contexts;
// using Atomic.Elements;
//
// namespace ShooterGame.Gameplay
// {
//     [Serializable]
//     public sealed class LeaderboardInstaller : IContextInstaller<IGameContext>
//     {
//         public void Install(IGameContext context)
//         {
//             context.AddLeaderboard(new ReactiveDictionary<TeamType, int>());
//             context.AddController<LeaderboardController>();
//         }
//     }
// }