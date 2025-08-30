using Atomic.Entities;
using UnityEngine;

namespace ShooterGame.Gameplay
{
    public sealed class CameraDisplayController : IEntityInit<IPlayerContext>
    {
        public void Init(IPlayerContext context)
        {
            Camera camera = context.GetCamera();
            TeamType teamType = context.GetTeamType().Value;
            
            camera.targetDisplay = GameContext.Instance
                .GetTeamCatalog()
                .GetInfo(teamType).CameraDisplay;
        }
    }
}