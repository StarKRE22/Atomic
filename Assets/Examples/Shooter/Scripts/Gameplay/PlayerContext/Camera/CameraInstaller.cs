using System;
using Atomic.Entities;
using UnityEngine;

namespace ShooterGame.Gameplay
{
    [Serializable]
    public sealed class CameraInstaller : IEntityInstaller<IPlayerContext>
    {
        [SerializeField]
        private Camera _camera;

        [SerializeField]
        private Vector3 _cameraOffset = new(0, 7, -10);

        public void Install(IPlayerContext context)
        {
            GameContext.TryGetInstance(out GameContext gameContext);
            
            context.AddCamera(_camera);
            context.AddBehaviour(new CameraDisplayController(gameContext));
            context.AddBehaviour(new CameraFollowController(_cameraOffset));
        }
    }
}