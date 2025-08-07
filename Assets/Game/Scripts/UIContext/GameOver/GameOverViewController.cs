using Atomic.Elements;
using Atomic.Entities;

namespace SampleGame
{
    public sealed class GameOverViewController : IEntitySpawn<IUIContext>, IEntityDespawn
    {
        private IUIContext _context;
        private IEvent _gameOverEvent;
        
        public void OnSpawn(IUIContext context)
        {
            _context = context;
            _gameOverEvent = GameContext.Instance.GetGameOverEvent();
            _gameOverEvent.OnEvent += this.OnGameOver;
        }

        public void OnDespawn(IEntity entity)
        {
            _gameOverEvent.OnEvent -= this.OnGameOver;
        }

        private void OnGameOver() => GameOverViewUseCase.ShowPopup(_context);
    }
}