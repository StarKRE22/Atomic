using Atomic.Entities;
using Atomic.Events;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Game.Gameplay
{
    public static class PlayerUseCase
    {
        public static bool StartPlayerTurn(this IGameContext context)
        {
            if (context.GetValue(GameContextAPI.GameState).Value != GameState.Playing)
                return false;

            context.GetValue(GameContextAPI.CurrentTurn).Value++;
            context.GetCharacters().ResetTurn();

            IGameEventBus eventBus = context.GetValue(GameContextAPI.EventBus);
            eventBus.Invoke(GameEventAPI.PlayerTurnStarted);
            eventBus.Flush();
            return true;
        }

        public static void EndPlayerTurn(this IGameContext context)
        {
            if (context.GetValue(GameContextAPI.GameState).Value != GameState.Playing)
                return;

            IGameEventBus eventBus = context.GetValue(GameContextAPI.EventBus);
            eventBus.Invoke(GameEventAPI.PlayerTurnEnded);
            eventBus.Flush();
        }

        public static async UniTask<bool> MovePlayerCharacter(
            this IGameEntity character,
            Vector2Int position,
            IGameContext gameContext
        )
        {
            bool success = await UniTask.RunOnThreadPool(() => character.MoveAsCharacter(position, gameContext));
            if (success)
                gameContext.GetValue(GameContextAPI.EventBus).Flush();

            return success;
        }

        public static async UniTask<bool> AttackPlayerCharacter(
            this IGameEntity character,
            IGameEntity target,
            IGameContext gameContext
        )
        {
            if (target.GetValue(GameEntityAPI.EntityType).Value != GameEntityType.Enemy)
                return false;

            bool success = await UniTask.RunOnThreadPool(() => character.AttackAsCharacter(target, gameContext));
            if (success)
            {
                gameContext.UpdateGameState();
                gameContext.GetValue(GameContextAPI.EventBus).Flush();
            }

            return success;
        }
    }
}