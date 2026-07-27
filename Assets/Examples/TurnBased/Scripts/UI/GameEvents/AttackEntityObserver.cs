using Atomic.Elements;
using Atomic.Entities;
using Atomic.Events;
using Cysharp.Threading.Tasks;
using Game.Gameplay;
using UnityEngine;

namespace Game.UI
{
    public sealed class AttackEntityObserver : IUIContextEnable, IUIContextDisable
    {
        private readonly struct StartAttackCommand : IUICommand
        {
            private readonly IUIContext _ui;
            private readonly AttackEventArgs _args;

            public StartAttackCommand(IUIContext ui, AttackEventArgs args)
            {
                _ui = ui;
                _args = args;
            }

            public UniTask Execute()
            {
                GameEntityView source = _ui.GetEntityView(_args.instigator);
                GameEntityView target = _ui.GetEntityView(_args.victim);
                return source.StartAttack(target);
            }
        }
        
        private readonly struct EndAttackCommand : IUICommand
        {
            private readonly IUIContext _ui;
            private readonly AttackEventArgs _args;
            
            public EndAttackCommand(IUIContext ui, AttackEventArgs args)
            {
                _ui = ui;
                _args = args;
            }
            
            public UniTask Execute()
            {
                GameEntityView attacker = _ui.GetEntityView(_args.instigator);
                Vector3 position = _ui.GetWorldPosition(_args.sourcePosition);
                return attacker.EndAttack(position);
            }
        }
        
        private readonly IGameContext _gameContext;
        private readonly DisposableComposite _disposables = new();
        private IUIContext _ui;

        public AttackEntityObserver(IGameContext gameContext)
        {
            _gameContext = gameContext;
        }

        public void Enable(IUIContext ui)
        {
            _ui = ui;
            IGameEventBus eventBus = _gameContext.GetEventBus();
            eventBus.SubscribeEntityAttackStarted( this.OnAttackStarted).AddTo(_disposables);
            eventBus.SubscribeEntityAttackEnded( this.OnAttackEnded).AddTo(_disposables);
        }

        public void Disable(IUIContext ui)
        {
            _disposables.Dispose();
        }

        private void OnAttackStarted(AttackEventArgs args) => 
            _ui.EnqueueCommand(new StartAttackCommand(_ui, args));

        private void OnAttackEnded(AttackEventArgs args) => 
            _ui.EnqueueCommand(new EndAttackCommand(_ui, args));
    }
}