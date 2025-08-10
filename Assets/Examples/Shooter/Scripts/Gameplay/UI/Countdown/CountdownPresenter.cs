using Atomic.Elements;
using ShooterGame.Gameplay;
using TMPro;
using UnityEngine;

namespace ShooterGame.UI
{
    public sealed class CountdownPresenter : Presenter
    {
        [SerializeField]
        private TMP_Text _text;
        
        private IReactiveVariable<float> _gameTime;

        protected override void OnShow()
        {
            _gameTime = GameContext.Instance.GetGameTime();
            _gameTime.Observe(this.OnGameTimeChanged);
        }

        protected override void OnHide()
        {
            _gameTime.Unsubscribe(this.OnGameTimeChanged);
        }

        private void OnGameTimeChanged(float time)
        {
            _text.text = $"Game Time: {time:F0}";
        }
    }
}