using UnityEngine;

namespace ShooterGame.Gameplay
{
    public sealed class CameraDisplayController : IPlayerContextInit
    {
        private readonly IGameContext _gameContext;

        public CameraDisplayController(IGameContext gameContext)
        {
            _gameContext = gameContext;
        }

        public void Init(IPlayerContext context)
        {
            Camera camera = context.GetCamera();
            TeamType teamType = context.GetTeamType().Value;
            
            camera.targetDisplay = _gameContext
                .GetTeamCatalog()
                .GetInfo(teamType).CameraDisplay;
        }
    }
}