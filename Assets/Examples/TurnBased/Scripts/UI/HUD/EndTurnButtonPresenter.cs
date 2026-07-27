using Atomic.Entities;
using Atomic.Events;
using Cysharp.Threading.Tasks;
using Game.Gameplay;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    public sealed class EndTurnButtonPresenter : IUIContextEnable, IUIContextDisable
    {
        private readonly Button _button;
        private readonly IGameContext _gameContext;
        private Subscription _subscription;

        public EndTurnButtonPresenter(Button button, IGameContext gameContext)
        {
            _button = button;
            _gameContext = gameContext;
        }

        public void Enable(IUIContext context)
        {
            _button.onClick.AddListener(this.OnClicked);
            _subscription = _gameContext
                .GetEventBus()
                .SubscribePlayerTurnStarted( this.OnTurnStarted);
        }

        public void Disable(IUIContext context)
        {
            _button.onClick.RemoveListener(this.OnClicked);
            _subscription.Dispose();
        }

        private void OnClicked()
        {
            _button.gameObject.SetActive(false);
            _gameContext.EndPlayerTurn();
        }

        private void OnTurnStarted()
        {
            _button.gameObject.SetActive(true);
        }
    }
}