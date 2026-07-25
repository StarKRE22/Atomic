using Atomic.Elements;
using Atomic.Entities;
using UnityEngine;

namespace RTSGame
{
    [CreateAssetMenu(
        fileName = "TankFactory",
        menuName = "RTSGame/GameEntities/New TankFactory"
    )]
    public sealed class TankFactory : GameEntityFactory
    {
        [SerializeField]
        private TransformEntityInstaller transformInstaller;

        [SerializeField]
        private MoveUnitInstaller _unitMoveInstaller;

        [SerializeField]
        private LifeEntityInstaller _lifeInstaller;

        [SerializeField]
        private RangeCombatEntityInstaller _combatInstaller;

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
            
            transformInstaller.Install(entity);
            _unitMoveInstaller.Install(entity);
            _lifeInstaller.Install(entity, gameContext);
            _combatInstaller.Install(entity, gameContext);
            _aiInstaller.Install(entity, gameContext);
        }
    }
}