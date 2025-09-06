using Atomic.Entities;
using UnityEngine;

namespace RTSGame
{
    public sealed class TankViewAspect : SceneEntityAspect<IGameEntity>
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
        
        public override void Apply(IGameEntity entity)
        {
            entity.AddBehaviour(_takeDamageBehaviour);
            entity.AddBehaviour(_positionBehaviour);
            entity.AddBehaviour(_rotationBehaviour);
            entity.AddBehaviour(_teamColorBehaviour);
            entity.AddBehaviour(_weaponRecoilBehaviour);
        }

        public override void Discard(IGameEntity entity)
        {
            entity.DelBehaviour(_takeDamageBehaviour);
            entity.DelBehaviour(_positionBehaviour);
            entity.DelBehaviour(_rotationBehaviour);
            entity.DelBehaviour(_teamColorBehaviour);
            entity.DelBehaviour(_weaponRecoilBehaviour);
        }
    }
}