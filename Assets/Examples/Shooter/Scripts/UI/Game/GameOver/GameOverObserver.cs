using Atomic.Elements;
using ShooterGame.App;
using ShooterGame.Gameplay;

namespace ShooterGame.UI
{
    public sealed class GameOverObserver : IGameUIInit, IGameUIDispose
    {
        private readonly IGameContext _gameContext;
        private readonly IAppContext _appContext;
        
        private IGameUI _uiContext;
        private Subscription _subscription;

        public GameOverObserver(IGameContext gameContext, IAppContext appContext)
        {
            _gameContext = gameContext;
            _appContext = appContext;
        }

        public void Init(IGameUI gameUI)
        {
            _uiContext = gameUI;
            _subscription = _gameContext.GetGameOverEvent().Subscribe(this.OnGameFinished);
        }

        public void Dispose(IGameUI gameUI)
        {
            _subscription.Dispose();
        }

        private void OnGameFinished() => GameOverUseCase.ShowPopup(_uiContext, _gameContext, _appContext);
    }
}