using Atomic.Elements;
using Atomic.Entities;
using UnityEngine;

namespace RTSGame
{
    [CreateAssetMenu(
        fileName = "TankFactory",
        menuName = "RTSGame/GameEntities/New TankFactory"
    )]
    public sealed class TankFactory : UnitFactory
    {
        [SerializeField]
        private TransformEntityInstaller _transformInstaller;

        [SerializeField]
        private MoveEntityInstaller _moveInstaller;

        [SerializeField]
        private LifeEntityInstaller _lifeInstaller;

        [SerializeField]
        private RangeCombatEntityInstaller _combatInstaller;

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
            entity.Install(_combatInstaller);
            entity.Install(_aiInstaller);
        }
    }
}