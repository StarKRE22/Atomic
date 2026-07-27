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
            if (context.GetGameState().Value != GameState.Playing)
                return false;

            context.GetCurrentTurn().Value++;
            context.GetCharacters().ResetTurn();

            IGameEventBus eventBus = context.GetEventBus();
            eventBus.InvokePlayerTurnStarted();
            eventBus.Flush();
            return true;
        }

        public static void EndPlayerTurn(this IGameContext context)
        {
            if (context.GetGameState().Value != GameState.Playing)
                return;

            IGameEventBus eventBus = context.GetEventBus();
            eventBus.InvokePlayerTurnEnded();
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
                gameContext.GetEventBus().Flush();

            return success;
        }

        public static async UniTask<bool> AttackPlayerCharacter(
            this IGameEntity character,
            IGameEntity target,
            IGameContext gameContext
        )
        {
            if (target.GetEntityType().Value != GameEntityType.Enemy)
                return false;

            bool success = await UniTask.RunOnThreadPool(() => character.AttackAsCharacter(target, gameContext));
            if (success)
            {
                gameContext.UpdateGameState();
                gameContext.GetEventBus().Flush();
            }

            return success;
        }
    }
}