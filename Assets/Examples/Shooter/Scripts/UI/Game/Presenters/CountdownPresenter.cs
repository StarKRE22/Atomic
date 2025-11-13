using Atomic.Elements;
using TMPro;

namespace ShooterGame.Gameplay
{
    public sealed class CountdownPresenter : IGameUIInit, IGameUIDispose
    {
        private readonly TMP_Text _view;
        private IReactiveVariable<float> _gameTime;

        public CountdownPresenter(TMP_Text view)
        {
            _view = view;
        }

        public void Init(IGameUI entity)
        {
            _gameTime = GameContext.Instance.GetGameTime();
            _gameTime.Observe(this.OnGameTimeChanged);
        }
        
        public void Dispose(IGameUI entity)
        {
            _gameTime.Unsubscribe(this.OnGameTimeChanged);
        }

        private void OnGameTimeChanged(float time)
        {
            _view.text = $"Game Time: {time:F0}";
        }
    }
}