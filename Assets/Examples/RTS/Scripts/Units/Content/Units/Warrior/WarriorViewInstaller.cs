using Atomic.Entities;
using UnityEngine;

namespace RTSGame
{
    public sealed class WarriorViewInstaller : SceneEntityInstaller<IUnit>
    {
        [SerializeField]
        private TakeDamageViewBehaviour _takeDamageBehaviour;

        [SerializeField]
        private PositionViewBehaviour _positionBehaviour;

        [SerializeField]
        private RotationViewBehaviour _rotationBehaviour;

        [SerializeField]
        private TeamColorViewBehaviour _teamColorBehaviour;

        [SerializeField]
        private WeaponRecoilViewBehaviour _weaponRecoilBehaviour;
        
        public override void Install(IUnit entity)
        {
            entity.AddBehaviour(_takeDamageBehaviour);
            entity.AddBehaviour(_positionBehaviour);
            entity.AddBehaviour(_rotationBehaviour);
            entity.AddBehaviour(_teamColorBehaviour);
            entity.AddBehaviour(_weaponRecoilBehaviour);
        }

        public override void Uninstall(IUnit entity)
        {
            entity.DelBehaviour(_takeDamageBehaviour);
            entity.DelBehaviour(_positionBehaviour);
            entity.DelBehaviour(_rotationBehaviour);
            entity.DelBehaviour(_teamColorBehaviour);
            entity.DelBehaviour(_weaponRecoilBehaviour);
        }
    }
}