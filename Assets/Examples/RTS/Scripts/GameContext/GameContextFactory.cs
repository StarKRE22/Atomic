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

        [SerializeField]
        private PlayerContextFactory _playerFactory;
        
        public override IGameContext Create()
        {
            var context = new GameContext();
            _gameEntityInstaller.Install(context);
            context.AddTeamViewConfig(_teamViewConfig);
            context.AddPlayers(new Dictionary<TeamType, IPlayerContext>
            {
                {TeamType.BLUE, this.CreatePlayerContext(TeamType.BLUE)},
                {TeamType.RED, this.CreatePlayerContext(TeamType.RED)}
            });
            return context;
        }

        private IPlayerContext CreatePlayerContext(TeamType type)
        {
            IPlayerContext playerContext = _playerFactory.Create();
            playerContext.AddTeam(new Const<TeamType>(type));
            return playerContext;
        }
    }
}