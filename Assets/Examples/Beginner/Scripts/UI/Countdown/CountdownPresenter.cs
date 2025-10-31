using Atomic.Elements;
using UnityEngine;

namespace BeginnerGame
{
    public sealed class CountdownPresenter : MonoBehaviour
    {
        [SerializeField]
        private CountdownView _view;

        private ICooldown _countdown;
        
        private void Start()
        {
            _countdown = GameContext.Instance.GetGameCountdown();
            _countdown.OnTimeChanged += this.OnTimeChanged;
        }

        private void OnDestroy()
        {
            if (_countdown != null)
                _countdown.OnTimeChanged -= this.OnTimeChanged;
        }

        private void OnTimeChanged(float time)
        {
            int minutes = Mathf.FloorToInt(time / 60f);
            int seconds = Mathf.FloorToInt(time % 60f);
            _view.SetTime($"{minutes:00}:{seconds:00}");
        }
    }
}