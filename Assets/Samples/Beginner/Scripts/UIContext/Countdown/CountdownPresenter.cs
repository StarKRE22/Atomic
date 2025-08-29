using Atomic.Elements;
using Atomic.Entities;
using UnityEngine;

namespace BeginnerGame
{
    public class CountdownPresenter : IEntityInit<IUIContext>, IEntityDispose
    {
        private ICooldown _countdown;
        private CountdownView _view;
        
        public void Init(IUIContext context)
        {
            _countdown = GameContext.Instance.GetGameCountdown();
            _countdown.OnTimeChanged += this.OnTimeChanged;
            _view = context.GetGameCountdownView();
        }

        public void Dispose(IEntity entity)
        {
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