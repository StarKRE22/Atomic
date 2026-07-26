using Atomic.Entities;
using Atomic.Events;
using Cysharp.Threading.Tasks;
using Game.Gameplay;

namespace Game.UI
{
    public sealed class EnemyTurnPresenter : IUIContextEnable, IUIContextDisable
    {
        private readonly IGameContext _gameContext;
        private Subscription _subscription;
        private UICommandQueue _commandQueue;

        private TurnView _turnView;

        public EnemyTurnPresenter(IGameContext gameContext)
        {
            _gameContext = gameContext;
        }

        public void Enable(IUIContext ui)
        {
            _commandQueue = ui.GetValue(UIContextAPI.CommandQueue);
            _turnView = ui.GetValue(UIContextAPI.TurnView);
            _subscription = _gameContext
                .GetValue(GameContextAPI.EventBus)
                .Subscribe(GameEventAPI.PlayerTurnEnded, () => this.OnEnemyTurnStarted().Forget());
        }

        public void Disable(IUIContext entity)
        {
            _subscription.Dispose();
        }

        private async UniTaskVoid OnEnemyTurnStarted()
        {
            await _turnView.AnimateEnemyTurn();
            _gameContext.SpawnEnemyWave();
            await _commandQueue.Execute();
            
            await _gameContext.MakeEnemyTurn();
            await _commandQueue.Execute();
        }
    }
}