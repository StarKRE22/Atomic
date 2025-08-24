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
        private IGameEntityWorld _entityWorld;
        private TeamType _teamType;

        public PlayerContextFactory SetEntityWorld(IGameEntityWorld entityWorld)
        {
            _entityWorld = entityWorld;
            return this;
        }

        public PlayerContextFactory SetTeamType(TeamType teamType)
        {
            _teamType = teamType;
            return this;
        }


        public override IPlayerContext Create()
        {
            IPlayerContext playerContext = new PlayerContext();
            playerContext.AddTeam(new Const<TeamType>(this._teamType));
            playerContext.AddEnemyFilter(
                new EntityFilter<IGameEntity>(this._entityWorld, e => TeamUseCase.IsEnemy(e, this._teamType))
            );
            return playerContext;
        }
    }
}