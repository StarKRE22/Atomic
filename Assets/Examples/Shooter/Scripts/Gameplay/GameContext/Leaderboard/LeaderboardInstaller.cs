using System;
using Atomic.Elements;
using Atomic.Entities;

namespace ShooterGame.Gameplay
{
    [Serializable]
    public sealed class LeaderboardInstaller : IEntityInstaller<IGameContext>
    {
        public void Install(IGameContext context)
        {
            context.AddLeaderboard(new ReactiveDictionary<TeamType, int>());
            context.AddBehaviour<LeaderboardController>();
        }
    }
}