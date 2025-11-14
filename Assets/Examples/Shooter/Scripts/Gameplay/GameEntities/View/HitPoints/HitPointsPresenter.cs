using Atomic.Elements;

namespace ShooterGame.Gameplay
{
    public sealed class HitPointsPresenter : IGameEntityInit, IGameEntityEnable, IGameEntityDisable
    {
        private HitPointsView _view;
        private Health _health;
        private TeamCatalog _teamConfig;
        private IValue<TeamType> _teamType;

        public void Init(IGameEntity entity)
        {
            _view = entity.GetHitPointsView();
            _health = entity.GetHealth();
            _teamType = entity.GetTeamType();
            _teamConfig = GameContext.Instance.GetTeamCatalog();
        }

        public void Enable(IGameEntity entity)
        {
            _health.OnStateChanged += this.OnHealthChanged;
            _view.Hide();
        }

        public void Disable(IGameEntity entity)
        {
            _health.OnStateChanged -= this.OnHealthChanged;
        }

        private void OnHealthChanged()
        {
            _view.SetColor(_teamConfig.GetInfo(_teamType.Value).Material.color);
            _view.SetProgress(_health.GetPercent());
            _view.SetText($"{_health.GetCurrent()}/{_health.GetMax()}");
            _view.Show();
        }
    }
}