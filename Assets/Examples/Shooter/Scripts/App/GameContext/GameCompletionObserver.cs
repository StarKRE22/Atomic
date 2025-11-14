using Atomic.Elements;
using ShooterGame.Gameplay;

namespace ShooterGame.App
{
    public sealed class GameCompletionObserver : IGameContextInit, IGameContextDispose
    {
        private readonly IAppContext _appContext;
        private ISignal _gameOverEvent;

        public GameCompletionObserver(IAppContext appContext)
        {
            _appContext = appContext;
        }

        public void Init(IGameContext context)
        {
            _gameOverEvent = context.GetGameOverEvent();
            _gameOverEvent.OnEvent += this.OnGameCompleted;
        }

        public void Dispose(IGameContext entity)
        {
            _gameOverEvent.OnEvent -= this.OnGameCompleted;
        }

        private void OnGameCompleted()
        {
            LevelsUseCase.TryIncrementLevel(_appContext);
        }
    }
}