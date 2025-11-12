using System;
using Atomic.Elements;
using Atomic.Entities;

namespace ShooterGame.Gameplay
{
    [Serializable]
    public sealed class LeaderboardInstaller : IGameContextInstaller
    {
        public void Install(IGameContext context)
        {
            context.AddLeaderboard(new ReactiveDictionary<TeamType, int>());
            context.AddBehaviour<LeaderboardController>();
        }
    }
}