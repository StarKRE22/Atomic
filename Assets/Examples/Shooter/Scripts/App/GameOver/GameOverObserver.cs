using Atomic.Elements;
using ShooterGame.Gameplay;

namespace ShooterGame.App
{
    public sealed class GameOverObserver : IGameContextInit, IGameContextDispose
    {
        private readonly IAppContext _appContext;
        private ISignal _gameOverEvent;

        public GameOverObserver(IAppContext appContext)
        {
            _appContext = appContext;
        }

        public void Init(IGameContext context)
        {
            _gameOverEvent = context.GetGameOverEvent();
            _gameOverEvent.Subscribe(this.OnGameOver);
        }

        public void Dispose(IGameContext entity)
        {
            _gameOverEvent.Unsubscribe(this.OnGameOver);
        }

        private void OnGameOver()
        {
            LevelsUseCase.IncrementLevel(_appContext);
            MenuUseCase.LoadMenu().Forget();
        }
    }
}