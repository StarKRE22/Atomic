using Atomic.Elements;
using Atomic.Entities;
using ShooterGame.Gameplay;

namespace ShooterGame.App
{
    public sealed class GameOverObserver : IEntitySpawn<IGameContext>, IEntityDespawn
    {
        private ISignal _gameOverEvent;

        public void OnSpawn(IGameContext context)
        {
            _gameOverEvent = context.GetGameOverEvent();
            _gameOverEvent.Subscribe(this.OnGameOver);
        }

        public void OnDespawn(IEntity entity)
        {
            _gameOverEvent.Unsubscribe(this.OnGameOver);
        }

        private void OnGameOver()
        {
            LevelUseCase.IncrementLevel(AppContext.Instance);
            MenuUseCase.LoadMenu().Forget();
        }
    }
}