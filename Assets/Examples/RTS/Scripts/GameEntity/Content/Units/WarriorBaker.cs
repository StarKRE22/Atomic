using Atomic.Elements;
using Atomic.Entities;
using UnityEngine;

namespace RTSGame
{
    public sealed class WarriorBaker : GameEntityBaker
    {
        [SerializeField]
        private LifeEntityBaker _lifeBaker; 
        
        [SerializeField]
        private MoveEntityBaker _moveBaker;

        [SerializeField]
        private CombatEntityBaker _combatBaker;

        protected override void Install(IGameEntity entity)
        {
            entity.Install(_moveBaker);
            entity.Install(_lifeBaker);
            entity.Install(_combatBaker);
        }
    }
}