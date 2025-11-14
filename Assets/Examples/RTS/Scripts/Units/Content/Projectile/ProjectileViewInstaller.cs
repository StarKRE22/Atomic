using Atomic.Entities;
using UnityEngine;

namespace RTSGame
{
    public class ProjectileViewInstaller : SceneEntityInstaller<IUnit>
    {
        [SerializeField]
        private PositionViewBehaviour _positionBehaviour;

        [SerializeField]
        private RotationViewBehaviour _rotationBehaviour;

        [SerializeField]
        private TeamColorViewBehaviour _teamColorBehaviour;

        public override void Install(IUnit entity)
        {
            entity.AddBehaviour(_positionBehaviour);
            entity.AddBehaviour(_rotationBehaviour);
            entity.AddBehaviour(_teamColorBehaviour);
        }

        public override void Uninstall(IUnit entity)
        {
            entity.DelBehaviour(_positionBehaviour);
            entity.DelBehaviour(_rotationBehaviour);
            entity.DelBehaviour(_teamColorBehaviour);
        }
    }
}