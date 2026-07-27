using Atomic.Elements;
using Atomic.Entities;
using Game.Gameplay;
using TMPro;

namespace Game.UI
{
    public sealed class GameStatePresenter : IUIContextEnable, IUIContextDisable
    {
        private readonly TMP_Text _view;
        private readonly IGameContext _gameContext;

        private IReactiveVariable<GameState> _currentState;
        private Subscription<GameState> _subscription;

        public GameStatePresenter(TMP_Text view, IGameContext gameContext)
        {
            _view = view;
            _gameContext = gameContext;
        }

        public void Enable(IUIContext entity)
        {
            _currentState = _gameContext.GetGameState();
            _subscription = _currentState.Observe(OnStateChanged);
        }

        public void Disable(IUIContext entity)
        {
            _subscription.Dispose();
        }

        private void OnStateChanged(GameState gameState)
        {
            _view.text = $"Game State: {gameState}";
        }
    }
}