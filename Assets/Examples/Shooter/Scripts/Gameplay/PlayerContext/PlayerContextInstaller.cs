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

        protected override void Install(IPlayerContext context)
        {
            GameContext gameContext = GameContext.Instance;
            gameContext.GetLeaderboard().Add(_teamType, 0);
            gameContext.GetPlayers().Add(_teamType, context);
            gameContext.WhenDeactivate(context.Deactivate);

            context.AddTeamType(_teamType);
            context.AddInputMap(_inputMap);

            _characterInstaller.Install(context);
            _cameraInstaller.Install(context);
        }
    }
}