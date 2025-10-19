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
            GameContext gameContext = GameContext.Instance;
            gameContext.GetLeaderboard().TryAdd(_teamType, 0);
            
            if (gameContext.TryGetPlayers(out IDictionary<TeamType, IPlayerContext> players)) 
                players.TryAdd(_teamType, context);

            gameContext.WhenDisable(context.Disable);

            context.AddTeamType(_teamType);
            context.AddInputMap(_inputMap);

            context.Install(_characterInstaller);
            context.Install(_cameraInstaller);
        }
    }
}