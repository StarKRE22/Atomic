using Atomic.Elements;
using Atomic.Entities;
using UnityEngine;

namespace ShooterGame.Gameplay
{
    [RunInEditMode]
    public sealed class TeamColorBehaviour : IEntityInit<IWorldEntity>, IEntityDispose
    {
        private TeamCatalog _catalog;
        private Renderer _renderer;
        private IReactiveValue<TeamType> _team;

        public void Init(IWorldEntity entity)
        {
            if (GameContext.TryGetInstance(out GameContext gameContext))
                gameContext.TryGetTeamCatalog(out _catalog);

            _renderer = entity.GetRenderer();

            _team = entity.GetTeamType();
            _team.Observe(this.OnTeamChanged);
        }

        public void Dispose(IEntity entity)
        {
            _team.Unsubscribe(this.OnTeamChanged);
        }

        private void OnTeamChanged(TeamType teamType)
        {
            if (_catalog)
                _renderer.material = _catalog.GetInfo(teamType).Material;
        }
    }
}