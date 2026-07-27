using System.Collections.Generic;
using Atomic.Entities;
using Atomic.Events;
using Cysharp.Threading.Tasks;
using Game.Gameplay;
using UnityEngine;

namespace Game.UI
{
    public sealed class SpawnEntityObserver : IUIContextInit, IUIContextEnable, IUIContextDisable
    {
        private readonly struct SpawnEntityCommand : IUICommand
        {
            private readonly IUIContext _ui;
            private readonly SpawnEventArgs _args;

            public SpawnEntityCommand(IUIContext ui, SpawnEventArgs args)
            {
                _ui = ui;
                _args = args;
            }

            public UniTask Execute()
            {
                return _ui.SpawnEntityView(_args.entity, _args.position);
            }
        }
        
        private readonly IGameContext _gameContext;
        private IUIContext _ui;
        private Subscription<SpawnEventArgs> _subscription;

        public SpawnEntityObserver(IGameContext gameContext)
        {
            _gameContext = gameContext;
        }
        
        public void Init(IUIContext ui)
        {
            GameEntityBoard gameBoard = _gameContext.GetGameBoard();
            foreach (KeyValuePair<IGameEntity, Vector2Int> pair in gameBoard.Entities) 
                ui.SpawnEntityView(pair.Key, pair.Value).Forget();
        }

        public void Enable(IUIContext ui)
        {
            _ui = ui;
            _subscription = _gameContext
                .GetEventBus()
                .SubscribeEntitySpawned( this.OnSpawned);
        }

        public void Disable(IUIContext entity)
        {
            _subscription.Dispose();
        }

        private void OnSpawned(SpawnEventArgs args)
        {
            _ui.EnqueueCommand(new SpawnEntityCommand(_ui, args));
        }
    }
}