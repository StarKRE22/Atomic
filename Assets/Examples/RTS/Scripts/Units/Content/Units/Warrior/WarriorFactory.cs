using Atomic.Elements;
using Atomic.Entities;
using UnityEngine;

namespace RTSGame
{
    [CreateAssetMenu(
        fileName = "WarriorFactory",
        menuName = "RTSGame/GameEntities/New WarriorFactory"
    )]
    public sealed class WarriorFactory : UnitFactory
    {
        [SerializeField]
        private TransformEntityInstaller _transformInstaller;
        
        [SerializeField]
        private MoveEntityInstaller _moveInstaller;

        [SerializeField]
        private LifeEntityInstaller _lifeInstaller;

        [SerializeField]
        private MeleeCombatEntityInstaller _meleeCombatInstaller;

        [SerializeField]
        private AIEntityInstaller _aiInstaller;

        protected override void Install(IUnit entity)
        {
            IGameContext gameContext = GameContext.Instance;
            
            entity.AddUnitTag();
            entity.AddTeam(new ReactiveVariable<TeamType>());
            entity.AddBehaviour(new DynamicSpatialHashBehaviour(gameContext));
            
            entity.Install(_transformInstaller);
            entity.Install(_moveInstaller);
            entity.Install(_lifeInstaller);
            entity.Install(_meleeCombatInstaller);
            entity.Install(_aiInstaller);
        }
    }
}