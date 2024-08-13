using Atomic.Contexts;
using GameExample.Engine;
using UnityEngine;

namespace Walkthrough
{
    public sealed class PlayerCameraInstaller : SceneContextInstallerBase
    {
        [SerializeField]
        private CameraData cameraData;

        public override void Install(IContext context)
        {            
            context.AddCameraData(this.cameraData);
            context.AddSystem<CameraFollowSystem>();
        }
    }
}