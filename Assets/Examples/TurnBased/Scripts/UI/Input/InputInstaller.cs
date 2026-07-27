using System;
using Atomic.Elements;
using Atomic.Entities;
using Game.Gameplay;

namespace Game.UI
{
    [Serializable]
    public sealed class InputInstaller
    {
        public void Install(IUIContext ui, IGameContext gameContext)
        {
            ui.AddInputCondition( new InlineFunction<bool>(() =>
            {
                UICommandQueue commandQueue = ui.GetCommandQueue();
                IReactiveVariable<GameState> gameState = gameContext.GetGameState();
                return !commandQueue.IsActive && gameState.Value == GameState.Playing;
            }));
            ui.AddBehaviour(new AttackInputController(gameContext));
            ui.AddBehaviour(new MoveInputController(gameContext));
            ui.AddBehaviour(new SelectionInputController());
        }
    }
}