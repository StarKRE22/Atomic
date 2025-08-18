using Atomic.Elements;
using Atomic.Entities;

namespace ShooterGame.Gameplay
{
    public sealed class HitPointsPresenter : IEntitySpawn<IGameEntity>, IEntityActivate, IEntityDeactivate
    {
        private HitPointsView _view;
        private Health _health;
        private TeamCatalog _teamConfig;
        private IValue<TeamType> _teamType;

        public void OnSpawn(IGameEntity entity)
        {
            _view = entity.GetHitPointsView();
            _health = entity.GetHealth();
            _teamType = entity.GetTeamType();
            _teamConfig = GameContext.Instance.GetTeamCatalog();
        }

        public void OnActivate(IEntity entity)
        {
            _health.OnStateChanged += this.OnHealthChanged;
            _view.Hide();
        }

        public void OnDeactivate(IEntity entity)
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