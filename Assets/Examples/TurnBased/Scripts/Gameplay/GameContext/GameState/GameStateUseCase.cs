using Atomic.Elements;
using Atomic.Entities;

namespace Game.Gameplay
{
    public static class GameStateUseCase
    {
        public static void StartGame(this IGameContext context)
        {
            context.GetValue(GameContextAPI.GameState).Value = GameState.Playing;
            context.StartPlayerTurn();
        }

        public static void UpdateGameState(this IGameContext context)
        {
            IReactiveVariable<GameState> currentState = context.GetValue(GameContextAPI.GameState);
            if (context.IsLose())
            {
                currentState.Value = GameState.Finished;
            }
            else if (context.IsWin())
            {
                currentState.Value = GameState.Finished;
                context.GetValue(GameContextAPI.IsWin).Value = true;
            }
            else
            {
                currentState.Value = GameState.Playing;
            }
        }

        public static bool IsWin(this IGameContext context) => 
            context.IsLastWave() && context.HasAliveEnemies() == false;

        public static bool IsLose(this IGameContext context) => 
            !context.AnyAliveCharacters();
    }
}