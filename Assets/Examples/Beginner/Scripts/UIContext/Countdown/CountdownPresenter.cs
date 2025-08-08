using Atomic.Elements;
using Atomic.Entities;

namespace BeginnerGame
{
    public class CountdownPresenter : IEntitySpawn<IUIContext>, IEntityDespawn
    {
        private ICooldown _countdown;
        private CountdownView _view;
        
        public void OnSpawn(IUIContext context)
        {
            _countdown = GameContext.Instance.GetGameCountdown();
            _countdown.OnTimeChanged += this.OnTimeChanged;
            _view = context.GetGameCountdownView();
        }

        public void OnDespawn(IEntity entity)
        {
            _countdown.OnTimeChanged -= this.OnTimeChanged;
        }

        private void OnTimeChanged(float time)
        {
            _view.SetTime($"Remaining time: {time:F0}");
        }
    }
}