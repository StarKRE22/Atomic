using Atomic.Elements;
using ShooterGame.Gameplay;
using TMPro;

namespace ShooterGame.UI
{
    public sealed class CountdownPresenter : IGameUIInit, IGameUIDispose
    {
        private readonly TMP_Text _view;
        private readonly IGameContext _gameContext;
        
        private Subscription<float> _subscription;

        public CountdownPresenter(TMP_Text view, IGameContext gameContext)
        {
            _view = view;
            _gameContext = gameContext;
        }

        public void Init(IGameUI entity)
        {
            _subscription = _gameContext
                .GetGameTime()
                .Observe(this.OnGameTimeChanged);
        }
        
        public void Dispose(IGameUI entity)
        {
            _subscription.Dispose();
        }

        private void OnGameTimeChanged(float time)
        {
            _view.text = $"Game Time: {time:F0}";
        }
    }
}