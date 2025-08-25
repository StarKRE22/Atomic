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
        private const string PLAYER_CONTEXT_NAME_FORMAT = "PlayerContext {0}";

        private EntityWorld<IGameEntity> _entityWorld;
        private TeamType _teamType;

        public PlayerContextFactory SetEntityWorld(EntityWorld<IGameEntity> entityWorld)
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
            var playerContext = new PlayerContext(
                string.Format(PLAYER_CONTEXT_NAME_FORMAT, _teamType),
                this.InitialTagCount,
                this.InitialValueCount,
                this.InitialBehaviourCount
            );

            playerContext.AddTeam(new Const<TeamType>(_teamType));
            playerContext.AddEnemyFilter(this.CreateEnemyFilter(playerContext));
            return playerContext;
        }

        private EntityFilter<IGameEntity> CreateEnemyFilter(IPlayerContext playerContext) => new(
            _entityWorld,
            entity => TeamUseCase.IsEnemyUnit(playerContext, entity),
            new TeamEntityTrigger()
        );
    }
}