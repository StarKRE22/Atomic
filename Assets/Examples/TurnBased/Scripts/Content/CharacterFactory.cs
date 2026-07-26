using Atomic.Elements;
using Atomic.Entities;
using UnityEngine;

namespace Game.Gameplay
{
    [CreateAssetMenu(
        fileName = "Character",
        menuName = "Game/Gameplay/GameEntities/Character"
    )]
    public sealed class CharacterFactory : GameEntityFactory
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

            entity.AddValue(GameEntityAPI.ResetTurnAction, new CompositeAction(
                entity.ResetMovesInTurn,
                entity.ResetAttacksInTurn
            ));

            entity.AddValue(GameEntityAPI.RespawnAction, new InlineAction(
                entity.AssignMaxHealth
            ));
        }
    }
}