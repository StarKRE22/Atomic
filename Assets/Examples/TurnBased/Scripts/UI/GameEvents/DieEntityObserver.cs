using Atomic.Entities;
using Atomic.Events;
using Cysharp.Threading.Tasks;
using Game.Gameplay;

namespace Game.UI
{
    public sealed class DieEntityObserver : IUIContextEnable, IUIContextDisable
    {
        private readonly IGameContext _gameContext;
        private Subscription<IGameEntity> _subscription;
        private UICommandQueue _commandQueue;
        private IUIContext _ui;

        public DieEntityObserver(IGameContext gameContext) => 
            _gameContext = gameContext;

        public void Enable(IUIContext ui)
        {
            _ui = ui;
            _commandQueue = ui.GetCommandQueue();
            _subscription = _gameContext
                .GetEventBus()
                .SubscribeEntityDied( this.OnDied);
        }

        public void Disable(IUIContext ui) => 
            _subscription.Dispose();

        private void OnDied(IGameEntity target) => 
            _commandQueue.Enqueue(new DieCommand(_ui, target));


        public readonly struct DieCommand : IUICommand
        {
            private readonly IUIContext _ui;
            private readonly IGameEntity _target;

            public DieCommand(IUIContext ui, IGameEntity target)
            {
                _ui = ui;
                _target = target;
            }

            public async UniTask Execute()
            {
                GameEntityView entityView = _ui.GetEntityView(_target);
                await entityView.Die(_ui);
            }
        }
    }
}