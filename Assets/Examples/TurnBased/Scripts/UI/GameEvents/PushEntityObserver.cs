using Atomic.Elements;
using Atomic.Entities;
using Atomic.Events;
using Cysharp.Threading.Tasks;
using Game.Gameplay;
using UnityEngine;

namespace Game.UI
{
    public sealed class PushEntityObserver : IUIContextEnable, IUIContextDisable
    {
        private readonly struct PushedCommand : IUICommand
        {
            private readonly IUIContext _ui;
            private readonly PushedEventArgs _args;

            public PushedCommand(IUIContext ui, PushedEventArgs args)
            {
                _ui = ui;
                _args = args;
            }

            public async UniTask Execute()
            {
                GameEntityView entity = _ui.GetEntityView(_args.target);
                // entity.AnimatorHit();
                // entity.UpdateHealthBar();
                // entity.HighlightHit();
                await entity.MoveAt(_ui.GetWorldPosition(_args.endPosition));
            }
        }

        private readonly struct PushInTargetCommand : IUICommand
        {
            private readonly IUIContext _ui;
            private readonly PushTargetEventArgs _args;

            public PushInTargetCommand(IUIContext ui, PushTargetEventArgs args)
            {
                _ui = ui;
                _args = args;
            }

            public UniTask Execute()
            {
                return _ui.PushTarget(_args);
            }
        }
        
        private readonly struct PushedOutCommand : IUICommand
        {
            private readonly IUIContext _ui;
            private readonly PushOutEventArgs _args;

            public PushedOutCommand(IUIContext ui, PushOutEventArgs args)
            {
                _ui = ui;
                _args = args;
            }
            
            public UniTask Execute()
            {
                Vector2Int newPosition = _args.position + _args.direction;
                Vector3 from = _ui.GetWorldPosition(_args.position);
                Vector3 toPosition = _ui.GetWorldPosition(newPosition);
                toPosition.y -= 2f;

                GameEntityView entityView = _ui.GetEntityView(_args.target);
                return entityView.DieFromBounds(from, toPosition, _ui);
            }
        }

        private readonly IGameContext _gameContext;
        private readonly DisposableComposite _disposables = new();
        private IUIContext _ui;

        public PushEntityObserver(IGameContext gameContext)
        {
            _gameContext = gameContext;
        }

        public void Enable(IUIContext ui)
        {
            _ui = ui;

            IGameEventBus eventBus = _gameContext.GetValue(GameContextAPI.EventBus);
            eventBus.Subscribe(GameEventAPI.EntityPushedOut, this.OnPushedOut).AddTo(_disposables);
            eventBus.Subscribe(GameEventAPI.EntityPushedTarget, this.OnPushedInTarget).AddTo(_disposables);
            eventBus.Subscribe(GameEventAPI.EntityPushed, this.OnPushed).AddTo(_disposables);
        }

        public void Disable(IUIContext ui)
        {
            _disposables.Dispose();
        }

        private void OnPushed(PushedEventArgs args) => _ui.EnqueueCommand(new PushedCommand(_ui, args));

        private void OnPushedInTarget(PushTargetEventArgs args) => _ui.EnqueueCommand(new PushInTargetCommand(_ui, args));

        private void OnPushedOut(PushOutEventArgs args) => _ui.EnqueueCommand(new PushedOutCommand(_ui, args));
    }
}