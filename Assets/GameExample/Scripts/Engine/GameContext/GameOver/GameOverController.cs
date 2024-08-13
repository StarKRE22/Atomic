using Atomic.Contexts;
using Atomic.Elements;
using UnityEngine;

namespace GameExample.Engine
{
    public sealed class GameOverController : IContextInit, IContextEnable, IContextDisable
    {
        private Countdown _gameCountdown;

        public void Init(IContext context)
        {
            _gameCountdown = context.GetGameCountdown();
        }

        public void Enable(IContext context)
        {
            _gameCountdown.OnEnded += this.OnCountdownFinished;
        }

        public void Disable(IContext context)
        {
            _gameCountdown.OnEnded -= this.OnCountdownFinished;
        }

        private void OnCountdownFinished()
        {
            Time.timeScale = 0;
            Debug.Log("Game Over!");
        }
    }
}