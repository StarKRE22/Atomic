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
        public IGameEntityWorld EntityWorld { private get; set; }
        public TeamType TeamType { private get; set; }

        public override IPlayerContext Create()
        {
            IPlayerContext playerContext = new PlayerContext();
            playerContext.AddTeam(new Const<TeamType>(this.TeamType));
            playerContext.AddEnemyFilter(
                new EntityFilter<IGameEntity>(this.EntityWorld, e => TeamUseCase.IsEnemy(e, this.TeamType))
            );
            return playerContext;
        }
    }
}