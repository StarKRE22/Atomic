using System;
using Atomic.Contexts;
using Atomic.Elements;
using Atomic.UI;
using TMPro;
using UnityEngine;

namespace GameExample.Engine
{
    [Serializable]
    public sealed class GameTimePresenter : IViewInit, IViewEnable, IViewDisable
    {
        [SerializeField]
        private TMP_Text timeText;

        private Countdown _gameTimer;

        public void Init()
        {
            _gameTimer = GameContext.Instance.GetGameCountdown();
        }

        public void Enable()
        {
            _gameTimer.OnCurrentTimeChanged += this.OnTimeChanged;
            this.OnTimeChanged(_gameTimer.CurrentTime);
        }

        public void Disable()
        {
            _gameTimer.OnCurrentTimeChanged -= this.OnTimeChanged;
        }

        private void OnTimeChanged(float time)
        {
            this.timeText.text = $"Remaining time: {time:F0}";
        }
    }
}