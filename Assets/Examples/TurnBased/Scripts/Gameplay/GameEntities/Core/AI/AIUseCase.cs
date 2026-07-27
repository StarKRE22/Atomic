using System.Threading;
using Atomic.Entities;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Game.Gameplay
{
    public static class AIUseCase
    {
        public static async UniTask ExecuteTurn(this IGameEntity enemy, IGameContext context)
        {
            var board = context.GetGameBoard();
            var pathFinder = context.GetPathFinder();
            var characters = context.GetCharacters();

            int safety = 16;

            while ((enemy.HasMovesInTurn() || enemy.HasAttacksInTurn()) && safety-- > 0)
            {
                IGameEntity target = await UniTask.RunOnThreadPool(() => enemy.FindBestTarget(characters, context));
                if (target == null)
                    break;

                var enemyPos = board.GetPosition(enemy);
                var targetPos = board.GetPosition(target);

                int range = enemy.GetAttackDistance().Value;
                int attackDistance = GetChebyshevDistance(enemyPos, targetPos);

                // 1. Атака
                if (attackDistance <= range && enemy.HasAttacksInTurn())
                {
                    if (!enemy.AttackAsCharacter(target, context))
                        break;

                    continue;
                }

                // 2. Движение
                if (!enemy.HasMovesInTurn())
                    break;

                var path = pathFinder.FindPath(enemy, target);
                if (path == null || path.Count == 0)
                    break;

                var nextStep = path[0];

                if (!enemy.MoveAsCharacter(nextStep, context))
                    break;
            }
        }

        private static UniTask<IGameEntity> FindBestTarget(
            this IGameEntity enemy,
            IGameEntity[] targets,
            IGameContext context
        )
        {
            // Heavy calculations
            Thread.Sleep(1000);
            
            var board = context.GetGameBoard();
            var pathFinder = context.GetPathFinder();

            IGameEntity bestTarget = null;
            int bestScore = int.MinValue;

            Vector2Int enemyPos = board.GetPosition(enemy);

            int damage = enemy.GetAttackDamage().Value;
            int attackRange = enemy.GetAttackDistance().Value;

            foreach (var target in targets)
            {
                if (target == null || !target.HealthExists())
                    continue;

                if (!enemy.CanAttackTarget(target))
                    continue;

                Vector2Int targetPos = board.GetPosition(target);

                // проверка достижимости
                var path = pathFinder.FindPath(enemy, target);
                if (path == null)
                    continue;

                int score = 0;

                int distance = GetChebyshevDistance(enemyPos, targetPos);

                // 🔥 1. Push kill
                if (distance <= attackRange && CanPushOutOfBoard(enemyPos, targetPos, board))
                {
                    score += 10000;
                }

                int hp = target.GetHealth().Value;

                // 🔥 2. Добивание
                if (distance <= attackRange && hp <= damage)
                {
                    score += 5000;
                }

                // 🔥 3. Низкое HP
                score += Mathf.Clamp(1000 - hp * 10, 0, 1000);

                // 🔥 4. Близость
                score -= distance * 5;

                if (score > bestScore)
                {
                    bestScore = score;
                    bestTarget = target;
                }
            }

            return UniTask.FromResult(bestTarget);
        }

        private static bool CanPushOutOfBoard(
            Vector2Int attackerPos,
            Vector2Int targetPos,
            GameEntityBoard board)
        {
            Vector2Int direction = targetPos - attackerPos;

            direction.x = Mathf.Clamp(direction.x, -1, 1);
            direction.y = Mathf.Clamp(direction.y, -1, 1);

            Vector2Int pushPosition = targetPos + direction;

            return !board.InBounds(pushPosition);
        }

        private static int GetChebyshevDistance(Vector2Int a, Vector2Int b)
        {
            Vector2Int delta = a - b;
            return Mathf.Max(Mathf.Abs(delta.x), Mathf.Abs(delta.y));
        }
    }
}