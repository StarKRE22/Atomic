using System.Collections.Generic;
using Atomic.Entities;
using Atomic.Events;
using UnityEngine;
// ReSharper disable All

namespace Game.Gameplay
{
    public static class MovementUseCase
    {
        public static bool HasMovesInTurn(this IGameEntity entity) =>
            entity.GetValue(GameEntityAPI.CurrentMovesCount).Value > 0;

        public static void ResetMovesInTurn(this IGameEntity entity) =>
            entity.GetValue(GameEntityAPI.CurrentMovesCount).Value =
                entity.GetValue(GameEntityAPI.MaxMovesPerTurn).Value;
        
        public static bool MoveAsCharacter(this IGameEntity entity, Vector2Int targetPosition, IGameContext context)
        {
            int currentMoves = entity.GetValue(GameEntityAPI.CurrentMovesCount).Value;
            if (currentMoves <= 0)
                return false;

            GameEntityBoard board = context.GetValue(GameContextAPI.GameBoard);
            if (!board.TryGetCellPosition(entity, out Vector2Int currentPosition))
                return false;

            Vector2Int delta = targetPosition - currentPosition;
            int distance = Mathf.Abs(delta.x) + Mathf.Abs(delta.y);

            // проверка: хватает ли очков хода
            if (distance > currentMoves)
                return false;

            if (!board.PlaceEntity(entity, targetPosition))
                return false;

            // списываем ровно столько, сколько прошли
            entity.GetValue(GameEntityAPI.CurrentMovesCount).Value -= distance;

            IGameEventBus eventBus = context.GetValue(GameContextAPI.EventBus);
            eventBus.Invoke(GameEventAPI.EntityMoved, new MoveEventArgs(entity, currentPosition, targetPosition));
            return true;
        }
        
        public static IEnumerable<Vector2Int> GetAvailableMovePositions(
            this IGameEntity entity,
            IGameContext context
        )
        {
            GameEntityBoard board = context.GetValue(GameContextAPI.GameBoard);
            if (!board.TryGetCellPosition(entity, out var currentPosition))
                yield break;

            int currentMoves = entity.GetValue(GameEntityAPI.CurrentMovesCount).Value;
            if (currentMoves <= 0)
                yield break;

            foreach (Vector2Int position in board)
            {
                if (position == currentPosition)
                    continue;

                Vector2Int delta = position - currentPosition;
                int distance = Mathf.Abs(delta.x) + Mathf.Abs(delta.y);

                if (distance <= currentMoves)
                    yield return position;
            }
        }
    }
}