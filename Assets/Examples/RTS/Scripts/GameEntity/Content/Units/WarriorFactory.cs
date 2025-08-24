using Atomic.Elements;
using UnityEngine;

namespace RTSGame
{
    [CreateAssetMenu(
        fileName = "WarriorFactory",
        menuName = "RTSGame/GameEntities/New WarriorFactory"
    )]
    public sealed class WarriorFactory : GameEntityFactory
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

        public override IGameEntity Create()
        {
            var entity = base.Create();
            entity.AddUnitTag();
            entity.AddTeam(new ReactiveVariable<TeamType>());
            
            _transformInstaller.Install(entity);
            _moveInstaller.Install(entity);
            _lifeInstaller.Install(entity);
            _meleeCombatInstaller.Install(entity);
            _aiInstaller.Install(entity);
            
            return entity;
        }
    }
}