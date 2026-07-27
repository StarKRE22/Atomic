using Atomic.Entities;
using Atomic.Events;
using Game.Gameplay;

namespace Game.UI
{
    public sealed class GameBoardPresenter : IUIContextInit, IUIContextEnable, IUIContextDisable
    {
        private readonly IGameContext _gameContext;
        private IGameEventBus _eventBus;
        private GameBoardView _gameBoardView;
        private Subscription _subscription;

        public GameBoardPresenter(IGameContext gameContext)
        {
            _gameContext = gameContext;
        }

        public void Init(IUIContext ui)
        {
            _eventBus = _gameContext.GetEventBus();
            
            GameEntityBoard gameBoard = _gameContext.GetGameBoard();
            _gameBoardView = ui.GetGameBoardView();
            _gameBoardView.Initialize(gameBoard.Width, gameBoard.Height);
        }

        public void Enable(IUIContext entity)
        {
            _subscription = _eventBus.SubscribePlayerTurnStarted( this.OnTurnStarted);
        }

        public void Disable(IUIContext entity)
        {
            _subscription.Dispose();
        }

        private void OnTurnStarted()
        {
            _gameBoardView.ClearMaterials();

            if (_gameContext.TryGetCurrentWave(out EntitySpawnInfo wave)) 
                _gameBoardView.HighlightCells(wave.points);
        }
    }
}