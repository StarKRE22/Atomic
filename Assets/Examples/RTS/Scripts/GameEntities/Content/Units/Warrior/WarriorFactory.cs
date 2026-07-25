using Atomic.Elements;
using Atomic.Entities;
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
        private MoveUnitInstaller _unitMoveInstaller;

        [SerializeField]
        private LifeEntityInstaller _lifeInstaller;

        [SerializeField]
        private MeleeCombatEntityInstaller _meleeCombatInstaller;

        [SerializeField]
        private AIEntityInstaller _aiInstaller;
        
        protected override void Install(IGameEntity entity, IGameContext gameContext)
        {
            entity.AddUnitTag();
            entity.AddDetectorTag();
            entity.AddAttackerTag();
           
            entity.AddTeam(new ReactiveVariable<TeamType>());
            entity.AddBehaviour(new SpatialGridBehaviour(gameContext));
            entity.AddUpdatePriority(new ReactiveVariable<EntityUpdatePriority>(EntityUpdatePriority.High));
            
            _transformInstaller.Install(entity);
            _unitMoveInstaller.Install(entity);
            _lifeInstaller.Install(entity, gameContext);
            _meleeCombatInstaller.Install(entity);
            _aiInstaller.Install(entity, gameContext);
        }
    }
}