using Atomic.Elements;
using Atomic.Entities;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Game.Gameplay
{
    [CreateAssetMenu(
        fileName = "Enemy",
        menuName = "Game/Gameplay/GameEntities/Enemy"
    )]
    public sealed class EnemyFactory : GameEntityFactory
    {
        [SerializeField]
        private HealthEntityInstaller _healthInstaller;

        [SerializeField]
        private MoveEntityInstaller _moveInstaller;

        [SerializeField]
        private AttackEntityInstaller _attackInstaller;

        [SerializeField]
        private PushEntityInstaller _pushInstaller;

        protected override void Install(IGameEntity entity, Args<IGameContext> args)
        {
            entity
                .Install(_healthInstaller)
                .Install(_moveInstaller)
                .Install(_attackInstaller)
                .Install(_pushInstaller);

            entity.AddMakeTurnAction( new InlineFunction<IGameContext, UniTask>(entity.ExecuteTurn));
            
            entity.AddResetTurnAction( new CompositeAction(
                entity.ResetMovesInTurn,
                entity.ResetAttacksInTurn
            ));

            entity.AddRespawnAction( new InlineAction(entity.AssignMaxHealth));
        }
    }
}