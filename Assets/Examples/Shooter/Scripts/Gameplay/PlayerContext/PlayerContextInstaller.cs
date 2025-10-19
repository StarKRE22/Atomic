using System.Collections.Generic;
using Atomic.Elements;
using Atomic.Entities;
using UnityEngine;

namespace ShooterGame.Gameplay
{
    public sealed class PlayerContextInstaller : SceneEntityInstaller<IPlayerContext>
    {
        [SerializeField]
        private Const<TeamType> _teamType;
        
        [SerializeField]
        private InputMap _inputMap;

        [SerializeField]
        private CharacterSystemInstaller _characterInstaller;

        [SerializeField]
        private CameraInstaller _cameraInstaller;

        public override void Install(IPlayerContext context)
        {
            this.InstallGameContext(context);

            context.AddTeamType(_teamType);
            context.AddInputMap(_inputMap);

            context.Install(_characterInstaller);
            context.Install(_cameraInstaller);
        }

        private void InstallGameContext(IPlayerContext context)
        {
            if (!GameContext.TryGetInstance(out GameContext gameContext)) 
                return;
            
            if (gameContext.TryGetLeaderboard(out IReactiveDictionary<TeamType, int> leaderboard)) 
                leaderboard.TryAdd(_teamType, 0);
            
            if (gameContext.TryGetPlayers(out IDictionary<TeamType, IPlayerContext> players)) 
                players.TryAdd(_teamType, context);

            gameContext.WhenDisable(context.Disable);
        }
    }
}