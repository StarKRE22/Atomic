using Atomic.Entities;
using Atomic.Events;
using Cysharp.Threading.Tasks;
using Game.Gameplay;

namespace Game.UI
{
    public sealed class EnemyTurnEndObserver : IUIContextEnable, IUIContextDisable
    {
        private readonly IGameContext _gameContext;
        private UICommandQueue _commandQueue;
        private Subscription _subscription;

        public EnemyTurnEndObserver(IGameContext gameContext) => 
            _gameContext = gameContext;
        
        public void Enable(IUIContext ui)
        {
            _commandQueue = ui.GetCommandQueue();
            _subscription = _gameContext
                .GetEventBus()
                .SubscribeEnemyTurnEnded( () => this.OnTurnEnded().Forget());
        }

        public void Disable(IUIContext ui) => 
            _subscription.Dispose();

        private async UniTaskVoid OnTurnEnded()
        {
            await _commandQueue.Execute();
            _gameContext.StartPlayerTurn();
            _gameContext.GetEventBus().Flush();
        }
    }
}