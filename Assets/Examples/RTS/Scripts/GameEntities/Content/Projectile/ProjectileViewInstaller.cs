using Atomic.Entities;
using UnityEngine;

namespace RTSGame
{
    public class ProjectileViewInstaller : MonoEntityInstaller<IGameEntity>
    {
        [SerializeField]
        private PositionViewBehaviour _positionBehaviour;

        [SerializeField]
        private RotationViewBehaviour _rotationBehaviour;

        [SerializeField]
        private TeamColorViewBehaviour _teamColorBehaviour;

        [SerializeField]
        private TransformViewBehaviour _transformBehaviour = new ();

        public override void Install(IGameEntity entity)
        {
            // entity.AddBehaviour(_transformBehaviour);
            
            entity.AddBehaviour(_positionBehaviour);
            entity.AddBehaviour(_rotationBehaviour);
            entity.AddBehaviour(_teamColorBehaviour);
        }

        public override void Uninstall(IGameEntity entity)
        {
            // entity.DelBehaviour(_transformBehaviour);
            
            entity.DelBehaviour(_positionBehaviour);
            entity.DelBehaviour(_rotationBehaviour);
            entity.DelBehaviour(_teamColorBehaviour);
        }
    }
}