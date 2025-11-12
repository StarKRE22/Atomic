using Atomic.Elements;
using ShooterGame.Gameplay;

namespace ShooterGame.App
{
    public sealed class GameOverObserver : IGameContextInit, IGameContextDispose
    {
        private ISignal _gameOverEvent;

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
            LevelsUseCase.IncrementLevel(AppContext.Instance);
            MenuUseCase.LoadMenu().Forget();
        }
    }
}