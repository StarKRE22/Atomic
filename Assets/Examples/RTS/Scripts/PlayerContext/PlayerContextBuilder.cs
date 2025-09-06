using System;
using Atomic.Elements;
using Atomic.Entities;
using UnityEngine;

namespace RTSGame
{
    [CreateAssetMenu(
        fileName = "PlayerContextBuilder",
        menuName = "RTSGame/New PlayerContextBuilder"
    )]
    public sealed class PlayerContextBuilder : ScriptableEntityFactory<IPlayerContext>
    {
        private const string PLAYER_CONTEXT_NAME_FORMAT = "PlayerContext {0}";

        private EntityWorld<IGameEntity> _entityWorld;
        private TeamType _teamType;

        public PlayerContextBuilder SetEntityWorld(EntityWorld<IGameEntity> entityWorld)
        {
            _entityWorld = entityWorld;
            return this;
        }

        public PlayerContextBuilder SetTeamType(TeamType teamType)
        {
            _teamType = teamType;
            return this;
        }

        public override IPlayerContext Create()
        {
            if (_entityWorld == null)
                throw new InvalidOperationException("EntityWorld must be set before creating PlayerContext!");  
            
            var playerContext = new PlayerContext(
                string.Format(PLAYER_CONTEXT_NAME_FORMAT, _teamType),
                this.InitialTagCount,
                this.InitialValueCount,
                this.InitialBehaviourCount
            );

            playerContext.AddTeam(new Const<TeamType>(_teamType));
            playerContext.AddFreeEnemyFilter(this.CreateFreeEnemyFilter(playerContext));
            return playerContext;
        }

        private EntityFilter<IGameEntity> CreateFreeEnemyFilter(IPlayerContext playerContext) => new(
            _entityWorld,
            entity => TeamUseCase.IsFreeEnemyUnit(playerContext, entity),
            new TeamEntityTrigger(), new TagEntityTrigger<IGameEntity>()
        );
    }
}