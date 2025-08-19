using Atomic.Elements;
using UnityEngine;

namespace RTSGame
{
    [CreateAssetMenu(
        fileName = "TankFactory",
        menuName = "RTSGame/New TankFactory"
    )]
    public sealed class TankFactory : GameEntityFactory
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

        protected override void Install(IGameEntity entity)
        {
            entity.AddUnitTag();
            entity.AddTeam(new ReactiveVariable<TeamType>());
            
            _transformInstaller.Install(entity);
            _moveInstaller.Install(entity);
            _lifeInstaller.Install(entity);
            _combatInstaller.Install(entity);
            _aiInstaller.Install(entity);
        }
    }
}