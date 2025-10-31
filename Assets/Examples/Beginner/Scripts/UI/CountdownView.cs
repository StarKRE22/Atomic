using Atomic.Elements;
using Atomic.Entities;
using TMPro;
using UnityEngine;

namespace BeginnerGame
{
    public sealed class CountdownView : MonoBehaviour
    {
        [SerializeField]
        private SceneEntity _gameContext;

        [SerializeField]
        private CountdownView _view;
        
        [SerializeField]
        private TMP_Text _timeText;

        private ICooldown _gameCooldown;

        private void Start()
        {
            _gameCooldown = _gameContext.GetGameCountdown();
            _gameCooldown.OnTimeChanged += this.OnTimeChanged;
        }

        private void OnDestroy()
        {
            if (_gameCooldown != null)
                _gameCooldown.OnTimeChanged -= this.OnTimeChanged;
        }

        private void OnTimeChanged(float time)
        {
            int minutes = Mathf.FloorToInt(time / 60f);
            int seconds = Mathf.FloorToInt(time % 60f);
            _timeText.text = $"{minutes:00}:{seconds:00}";
        }
    }
}