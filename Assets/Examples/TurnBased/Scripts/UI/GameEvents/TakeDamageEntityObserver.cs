using Atomic.Entities;
using Atomic.Events;
using Cysharp.Threading.Tasks;
using Game.Gameplay;

namespace Game.UI
{
    public sealed class TakeDamageEntityObserver : IUIContextEnable, IUIContextDisable
    {
        private readonly struct TakeDamageCommand : IUICommand
        {
            private readonly IUIContext _ui;
            private readonly TakeDamageEventArgs _args;

            public TakeDamageCommand(IUIContext ui, TakeDamageEventArgs args)
            {
                _ui = ui;
                _args = args;
            }

            public UniTask Execute()
            {
                GameEntityView target = _ui.GetEntityView(_args.victim);
                return target.TakeDamage(_args); 
            }
        }
        
        private readonly IGameContext _gameContext;
        private IUIContext _ui;
        private Subscription<TakeDamageEventArgs> _subscription;

        public TakeDamageEntityObserver(IGameContext gameContext)
        {
            _gameContext = gameContext;
        }
        
        public void Enable(IUIContext ui)
        {
            _ui = ui;
            _subscription = _gameContext
                .GetEventBus()
                .SubscribeEntityDamaged( this.OnDamaged);
        }

        public void Disable(IUIContext ui)
        {
            _subscription.Dispose();
        }

        private void OnDamaged(TakeDamageEventArgs args)
        {
            _ui.EnqueueCommand(new TakeDamageCommand(_ui, args));
        }
    }
}