using Atomic.Entities;
using UnityEngine;

namespace ShooterGame.Gameplay
{
    public sealed class BulletViewInstaller : SceneEntityInstaller<IWorldEntity>
    {
        [SerializeField]
        private Renderer _renderer;

        public override void Install(IWorldEntity entity)
        {
            entity.AddRenderer(_renderer);
            entity.AddBehaviour<TeamColorBehaviour>();
        }
    }
}