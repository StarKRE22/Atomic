using Atomic.Entities;
using Atomic.Events;
using Game.Gameplay;
using Subscription = Atomic.Events.Subscription;

namespace Game.UI
{
    public sealed class PlayerTurnPresenter : IUIContextEnable, IUIContextDisable
    {
        private readonly IGameContext _gameContext;
        private TurnView _turnView;
        private Subscription _subscription;

        public PlayerTurnPresenter(IGameContext gameContext) => 
            _gameContext = gameContext;

        public void Enable(IUIContext ui)
        {
            _turnView = ui.GetValue(UIContextAPI.TurnView);
            _subscription = _gameContext.GetValue(GameContextAPI.EventBus)
                .Subscribe(GameEventAPI.PlayerTurnStarted, this.OnTurnStarted);
        }

        private void OnTurnStarted() => _turnView.AnimatePlayerTurn().Forget();

        public void Disable(IUIContext UI) => _subscription.Dispose();
    }
}