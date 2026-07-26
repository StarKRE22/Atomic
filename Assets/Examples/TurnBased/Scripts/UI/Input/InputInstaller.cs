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
            ui.AddValue(UIContextAPI.InputCondition, new InlineFunction<bool>(() =>
            {
                UICommandQueue commandQueue = ui.GetValue(UIContextAPI.CommandQueue);
                IReactiveVariable<GameState> gameState = gameContext.GetValue(GameContextAPI.GameState);
                return !commandQueue.IsActive && gameState.Value == GameState.Playing;
            }));
            ui.AddBehaviour(new AttackInputController(gameContext));
            ui.AddBehaviour(new MoveInputController(gameContext));
            ui.AddBehaviour(new SelectionInputController());
        }
    }
}