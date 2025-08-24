using Atomic.Elements;
using Atomic.Entities;
using UnityEngine;

namespace RTSGame
{
    [CreateAssetMenu(
        fileName = "WarriorFactory",
        menuName = "RTSGame/GameEntities/New WarriorFactory"
    )]
    public sealed class WarriorFactory : ScriptableEntityInstaller<IGameEntity>
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

        protected override void Install(IGameEntity entity)
        {
            entity.AddUnitTag();
            entity.AddTeam(new ReactiveVariable<TeamType>());
            
            _transformInstaller.Install(entity);
            _moveInstaller.Install(entity);
            _lifeInstaller.Install(entity);
            _meleeCombatInstaller.Install(entity);
            _aiInstaller.Install(entity);
        }
    }
}