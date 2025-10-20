using Atomic.Entities;
using UnityEngine;

namespace RTSGame
{
    public sealed class TankBaker : UnitBaker
    {
        [SerializeField]
        private LifeEntityBaker _lifeBaker; 
        
        [SerializeField]
        private MoveEntityBaker _moveBaker;

        [SerializeField]
        private CombatEntityBaker _combatBaker;
        
        [SerializeField]
        private TeamEntityBaker _teamBaker;

        [SerializeField]
        private TransformEntityBaker _transformBaker;

        protected override void Install(IUnitEntity entity)
        {
            entity.Install(_moveBaker);
            entity.Install(_lifeBaker);
            entity.Install(_combatBaker);
            entity.Install(_teamBaker);
            entity.Install(_transformBaker);
        }
        
        private void OnValidate()
        {
            _teamBaker.OnValidate();
        }
    }
}