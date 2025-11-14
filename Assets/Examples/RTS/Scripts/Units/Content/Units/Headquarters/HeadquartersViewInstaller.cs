using Atomic.Entities;
using UnityEngine;

namespace RTSGame
{
    public sealed class HeadquartersViewInstaller : SceneEntityInstaller<IUnit>
    {
        [SerializeField]
        private TakeDamageViewBehaviour _takeDamageBehaviour;

        [SerializeField]
        private PositionViewBehaviour _positionBehaviour;

        [SerializeField]
        private RotationViewBehaviour _rotationBehaviour;

        [SerializeField]
        private TeamColorViewBehaviour _teamColorBehaviour;

        public override void Install(IUnit entity)
        {
            entity.AddBehaviour(_takeDamageBehaviour);
            entity.AddBehaviour(_positionBehaviour);
            entity.AddBehaviour(_rotationBehaviour);
            entity.AddBehaviour(_teamColorBehaviour);
        }

        public override void Uninstall(IUnit entity)
        {
            entity.DelBehaviour(_takeDamageBehaviour);
            entity.DelBehaviour(_positionBehaviour);
            entity.DelBehaviour(_rotationBehaviour);
            entity.DelBehaviour(_teamColorBehaviour);
        }
    }
}