using Atomic.Entities;
using UnityEngine;

namespace ShooterGame.Gameplay
{
    public sealed class BulletViewInstaller : SceneEntityInstaller<IGameEntity>
    {
        [SerializeField]
        private Renderer _renderer;

        public override void Install(IGameEntity entity)
        {
            entity.AddRenderer(_renderer);
            entity.AddBehaviour<TeamColorBehaviour>();
        }
    }
}