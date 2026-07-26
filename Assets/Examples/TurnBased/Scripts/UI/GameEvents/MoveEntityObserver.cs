using Atomic.Entities;
using Atomic.Events;
using Cysharp.Threading.Tasks;
using Game.Gameplay;
using UnityEngine;
// ReSharper disable ConvertToLambdaExpression

namespace Game.UI
{
    public sealed class MoveEntityObserver : IUIContextEnable, IUIContextDisable
    {
        private readonly struct MoveCommand : IUICommand
        {
            private readonly IUIContext _ui;

            public MoveCommand(IUIContext ui, MoveEventArgs args)
            {
                _ui = ui;
                _args = args;
            }

            private readonly MoveEventArgs _args;
            
            public async UniTask Execute()
            {
                GameEntityView entity = _ui.GetEntityView(_args.entity);
                Vector3 position = _ui.GetWorldPosition(_args.newPosition);
                await entity.RotateAt(position);
                entity.SetAnimatorMoving(true);
                await entity.MoveAt(position);
                entity.SetAnimatorMoving(false);
            }
        }
        
        private readonly IGameContext _gameContext;
        private Subscription<MoveEventArgs> _subscription;
        private IUIContext _ui;

        public MoveEntityObserver(IGameContext gameContext)
        {
            _gameContext = gameContext;
        }

        public void Enable(IUIContext ui)
        {
            _ui = ui;
            
            _subscription = _gameContext
                .GetValue(GameContextAPI.EventBus)
                .Subscribe(GameEventAPI.EntityMoved, this.OnMoved);
        }

        public void Disable(IUIContext context)
        {
            _subscription.Dispose();
        }

        private void OnMoved(MoveEventArgs args)
        {
            _ui.EnqueueCommand(new MoveCommand(_ui, args));
        }
    }
}