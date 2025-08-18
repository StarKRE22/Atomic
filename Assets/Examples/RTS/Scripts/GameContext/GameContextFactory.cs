using System.Collections.Generic;
using Atomic.Elements;
using Atomic.Entities;
using UnityEngine;

namespace RTSGame
{
    [CreateAssetMenu(
        fileName = "GameContextFactory",
        menuName = "RTSGame/New GameContextFactory"
    )]
    public sealed class GameContextFactory : ScriptableEntityFactory<IGameContext>
    {
        [SerializeField]
        private GameEntitySystemInstaller _gameEntityInstaller;

        [SerializeField]
        private TeamViewConfig _teamViewConfig;

        public override IGameContext Create()
        {
            var context = new GameContext();
            _gameEntityInstaller.Install(context);

            context.AddTeamViewConfig(_teamViewConfig);

            IGameEntityWorld entityWorld = context.GetEntityWorld();
            context.AddPlayers(new Dictionary<TeamType, IPlayerContext>
            {
                {TeamType.BLUE, this.CreatePlayerContext(TeamType.BLUE, entityWorld)},
                {TeamType.RED, this.CreatePlayerContext(TeamType.RED, entityWorld)}
            });
            return context;
        }

        private IPlayerContext CreatePlayerContext(TeamType type, IGameEntityWorld entityWorld)
        {
            IPlayerContext playerContext = new PlayerContext();
            playerContext.AddTeam(new Const<TeamType>(type));
            playerContext.AddEnemyFilter(new EntityFilter<IGameEntity>(entityWorld,
                e => TeamUseCase.IsEnemy(e, type)));
            return playerContext;
        }
    }
}