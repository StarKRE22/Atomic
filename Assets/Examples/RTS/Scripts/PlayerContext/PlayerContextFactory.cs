using Atomic.Elements;
using Atomic.Entities;
using UnityEngine;

namespace RTSGame
{
    [CreateAssetMenu(
        fileName = "PlayerContextFactory",
        menuName = "RTSGame/New PlayerContextFactory"
    )]
    public sealed class PlayerContextFactory : ScriptableEntityFactory<IPlayerContext>
    {
        public override IPlayerContext Create()
        {
            var context = new PlayerContext();
            context.AddTeam(new BaseVariable<TeamType>());
            context.AddEnemyFilter(new EntityFilter<IGameEntity>());
            return context;
        }
    }
}