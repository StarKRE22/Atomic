using Atomic.Elements;
using Atomic.Entities;
using Game.Gameplay;
using TMPro;

namespace Game.UI
{
    public sealed class CurrentTurnPresenter : IUIContextEnable, IUIContextDisable
    {
        private readonly TMP_Text _view;
        private readonly IGameContext _gameContext;

        private IReactiveVariable<int> _turn;
        private Subscription<int> _subscription;

        public CurrentTurnPresenter(TMP_Text view, IGameContext context)
        {
            _view = view;
            _gameContext = context;
        }

        public void Enable(IUIContext entity)
        {
            _turn = _gameContext.GetCurrentTurn();
            _subscription = _turn.Observe(this.OnTurnChanged);
        }

        public void Disable(IUIContext entity)
        {
            _subscription.Dispose();
        }

        private void OnTurnChanged(int turn) => _view.text = $"Turn: {turn}";
    }
}