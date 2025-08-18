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
        private PlayerSystemInstaller _playerSystemInstaller;

        [SerializeField]
        private TeamViewConfig _teamViewConfig;
        
        public override IGameContext Create()
        {
            var context = new GameContext();
            _gameEntityInstaller.Install(context);
            _playerSystemInstaller.Install(context);
            context.AddTeamViewConfig(_teamViewConfig);
            return context;
        }

      
    }
}