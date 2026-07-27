using System.Collections.Generic;
using Atomic.Elements;
using Atomic.Entities;
using Atomic.Events;
using UnityEngine;

namespace Game.Gameplay
{
    public static class AttackUseCase
    {
        public static bool HasAttacksInTurn(this IGameEntity entity) =>
            entity.GetCurrentAttacksCount().Value > 0;

        public static void ResetAttacksInTurn(this IGameEntity entity) =>
            entity.GetCurrentAttacksCount().Value =
                entity.GetMaxAttacksPerTurn().Value;

        public static bool AttackAsCharacter(
            this IGameEntity attacker,
            IGameEntity target,
            IGameContext context
        )
        {
            if (attacker == null || target == null)
                return false;

            IReactiveVariable<int> currentAttacksCount = attacker.GetCurrentAttacksCount();
            int currentAttacks = currentAttacksCount.Value;
            if (currentAttacks <= 0)
                return false;

            if (!target.HealthExists())
                return false;

            GameEntityBoard board = context.GetGameBoard();

            if (!board.TryGetCellPosition(attacker, out var attackerPos) ||
                !board.TryGetCellPosition(target, out var targetPos))
                return false;

            if (attackerPos == targetPos)
                return false;

            // 🔥 Chebyshev distance (диагонали учитываются)
            Vector2Int delta = targetPos - attackerPos;
            int distance = Mathf.Max(Mathf.Abs(delta.x), Mathf.Abs(delta.y));

            int range = attacker.GetAttackDistance().Value;
            if (distance > range)
                return false;

            if (!attacker.CanAttackTarget(target))
                return false;

            IGameEventBus eventBus = context.GetEventBus();

            eventBus.InvokeEntityAttackStarted(
                new AttackEventArgs(attacker, target, attackerPos, targetPos));

            int damage = attacker.GetAttackDamage().Value;
            attacker.DealDamage(target, damage, context);
            
            Vector2Int direction = targetPos - attackerPos;
            target.PushTowards(direction, context);
            
            context.DespawnDeadEntities();

            eventBus.InvokeEntityAttackEnded(
                new AttackEventArgs(attacker, target, attackerPos, targetPos));

            currentAttacksCount.Value--;

            return true;
        }
        
        public static IEnumerable<Vector2Int> GetAvailableAttackPositions(
            this IGameEntity entity,
            IGameContext context)
        {
            GameEntityBoard board = context.GetGameBoard();
            if (!board.TryGetCellPosition(entity, out var origin))
                yield break;

            int range = entity.GetAttackDistance().Value;

            foreach (var position in board)
            {
                if (position == origin)
                    continue;

                // 🔥 Chebyshev distance
                Vector2Int delta = position - origin;
                int distance = Mathf.Max(Mathf.Abs(delta.x), Mathf.Abs(delta.y));

                if (distance > range)
                    continue;

                if (!board.TryGetEntity(position, out var target))
                    continue;

                if (!entity.CanAttackTarget(target))
                    continue;

                yield return position;
            }
        }
        
        public static bool CanAttackTarget(this IGameEntity attacker, IGameEntity target)
        {
            if (attacker == null || target == null)
                return false;

            if (!target.HealthExists())
                return false;

            if (attacker == target)
                return false;

            var attackerType = attacker.GetEntityType().Value;
            var targetType = target.GetEntityType().Value;

            // нельзя атаковать своих
            if (attackerType == targetType)
                return false;

            return true;
        }
    }
}