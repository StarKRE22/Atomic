using Atomic.Elements;
using Atomic.Entities;
using Atomic.Events;
using UnityEngine;

namespace Game.Gameplay
{
    public static class PushUseCase
    {
        public static void PushTowards(this IGameEntity target, Vector2Int direction, IGameContext gameContext)
        {
            if (direction == Vector2Int.zero)
                return;

            direction = new Vector2Int(
                Mathf.Clamp(direction.x, -1, 1),
                Mathf.Clamp(direction.y, -1, 1)
            );

            GameEntityBoard board = gameContext.GetValue(GameContextAPI.GameBoard);
            if (!board.TryGetCellPosition(target, out var position))
                return;

            var eventBus = gameContext.GetValue(GameContextAPI.EventBus);
            var nextPosition = position + direction;

            // 1. Вылетел за границы
            if (!board.InBounds(nextPosition))
            {
                target.AssignZeroHealth();
                board.RemoveEntity(target);

                eventBus.Invoke(GameEventAPI.EntityPushedOut,
                    new PushOutEventArgs(target, position, direction));

                return;
            }

            // 2. Если кто-то стоит — пушим его
            if (board.TryGetEntity(nextPosition, out var blocker))
            {
                int pushDamage = target.GetValue(GameEntityAPI.PushDamage).Value;
                eventBus.Invoke(GameEventAPI.EntityPushedTarget,
                    new PushTargetEventArgs(target, blocker, position, nextPosition, direction));

                target.DealDamage(blocker, pushDamage, gameContext);

                // рекурсивный пуш
                blocker.PushTowards(direction, gameContext);
                return;
            }

            // 3. Двигаем target
            if (board.PlaceEntity(target, nextPosition))
            {
                eventBus.Invoke(GameEventAPI.EntityPushed,
                    new PushedEventArgs(target, position, nextPosition, direction));
            }
        }
    }
}