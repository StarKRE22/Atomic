using Atomic.Entities;
using UnityEngine;

namespace ShooterGame.Gameplay
{
    public sealed class BulletViewInstaller : SceneEntityInstaller<IActor>
    {
        [SerializeField]
        private Renderer _renderer;

        public override void Install(IActor entity)
        {
            entity.AddRenderer(_renderer);
            entity.AddBehaviour<TeamColorBehaviour>();
        }
    }
}