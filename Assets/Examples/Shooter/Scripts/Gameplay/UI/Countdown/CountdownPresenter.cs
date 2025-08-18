using Atomic.Elements;
using Atomic.Entities;
using TMPro;

namespace ShooterGame.Gameplay
{
    public sealed class CountdownPresenter : IEntitySpawn, IEntityDespawn
    {
        private readonly TMP_Text _view;
        private IReactiveVariable<float> _gameTime;

        public CountdownPresenter(TMP_Text view)
        {
            _view = view;
        }

        public void OnSpawn(IEntity _)
        {
            _gameTime = GameContext.Instance.GetGameTime();
            _gameTime.Observe(this.OnGameTimeChanged);
        }

        public void OnDespawn(IEntity _)
        {
            _gameTime.Unsubscribe(this.OnGameTimeChanged);
        }

        private void OnGameTimeChanged(float time)
        {
            _view.text = $"Game Time: {time:F0}";
        }
    }
}