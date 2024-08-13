using System;
using Atomic.Contexts;
using Atomic.Elements;
using Atomic.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

namespace GameExample.Engine
{
    [Serializable]
    public sealed class GameOverPopupShower : IViewInit, IViewEnable, IViewDisable
    {
        [SerializeField]
        private GameObject gameOverPopup;

        private Countdown _gameCountdown; 
        
        public void Init()
        {
            _gameCountdown = GameContext.Instance.GetGameCountdown();
            this.gameOverPopup.SetActive(false);
        }

        public void Enable()
        {
            _gameCountdown.OnEnded += this.OnGameCountdownFinished;
        }

        public void Disable()
        {
            _gameCountdown.OnEnded -= this.OnGameCountdownFinished;
        }

        private void OnGameCountdownFinished()
        {
            this.gameOverPopup.SetActive(true);
        }
    }
}