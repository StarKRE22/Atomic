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
        private IEntityWorld<IGameEntity> _entityWorld;
        private TeamType _teamType;

        public PlayerContextFactory SetEntityWorld(IEntityWorld<IGameEntity> entityWorld)
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
            playerContext.AddTeam(new Const<TeamType>(_teamType));
            
            EntityFilter<IGameEntity> filter = new EntityFilter<IGameEntity>(_entityWorld,
                e => TeamUseCase.IsEnemyUnit(e, _teamType));
         
            playerContext.AddEnemyFilter(filter);
            return playerContext;
        }
    }
}