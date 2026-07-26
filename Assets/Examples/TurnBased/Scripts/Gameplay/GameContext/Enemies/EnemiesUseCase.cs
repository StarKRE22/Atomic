using System.Linq;
using Atomic.Elements;
using Atomic.Entities;
using Atomic.Events;
using Cysharp.Threading.Tasks;

namespace Game.Gameplay
{
    public static class EnemiesUseCase
    {
        public static async UniTask MakeEnemyTurn(this IGameContext context)
        {
            IGameEntity[] enemies = context.GetEnemies();
            enemies.ResetTurn();

            foreach (IGameEntity enemy in enemies)
            {
                IFunction<IGameContext, UniTask> function = enemy.GetValue(GameEntityAPI.MakeTurnAction);
                await function.Invoke(context);
            }

            IGameEventBus eventBus = context.GetValue(GameContextAPI.EventBus);
            eventBus.Invoke(GameEventAPI.EnemyTurnEnded);
            
            context.UpdateGameState();
            eventBus.Flush();
        }

        public static bool HasAliveEnemies(this IGameContext context) =>
            context.GetEnemies().AnyAlive();

        public static IGameEntity[] GetEnemies(this IGameContext context)
        {
            GameEntityBoard entityBoard = context.GetValue(GameContextAPI.GameBoard);
            return entityBoard.Entities.Keys
                .Where(e => e.GetValue(GameEntityAPI.EntityType).Value == GameEntityType.Enemy)
                .ToArray();
        }
    }
}