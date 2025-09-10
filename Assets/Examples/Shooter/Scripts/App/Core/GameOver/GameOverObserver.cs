using Atomic.Elements;
using Atomic.Entities;
using ShooterGame.Gameplay;

namespace ShooterGame.App
{
    public sealed class GameOverObserver : IEntityInit<IGameContext>, IEntityDispose
    {
        private ISignal _gameOverEvent;

        public void Init(IGameContext context)
        {
            _gameOverEvent = context.GetGameOverEvent();
            _gameOverEvent.Subscribe(this.OnGameOver);
        }

        public void Dispose(IEntity entity)
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