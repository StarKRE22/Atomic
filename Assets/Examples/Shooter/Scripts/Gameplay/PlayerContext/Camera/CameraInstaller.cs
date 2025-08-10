using System;
using Atomic.Entities;
using SampleGame;
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
            context.AddCamera(_camera);
            context.AddBehaviour<CameraDisplayController>();
            context.AddBehaviour(new CameraFollowController(_cameraOffset));
        }
    }
}