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
                IFunction<IGameContext, UniTask> function = enemy.GetMakeTurnAction();
                await function.Invoke(context);
            }

            IGameEventBus eventBus = context.GetEventBus();
            eventBus.InvokeEnemyTurnEnded();
            
            context.UpdateGameState();
            eventBus.Flush();
        }

        public static bool HasAliveEnemies(this IGameContext context) =>
            context.GetEnemies().AnyAlive();

        public static IGameEntity[] GetEnemies(this IGameContext context)
        {
            GameEntityBoard entityBoard = context.GetGameBoard();
            return entityBoard.Entities.Keys
                .Where(e => e.GetEntityType().Value == GameEntityType.Enemy)
                .ToArray();
        }
    }
}