using Atomic.Elements;
using Atomic.Entities;
using UnityEngine;

namespace ShooterGame.Gameplay
{
    [RunInEditMode]
    public sealed class TeamColorBehaviour : IGameEntityInit, IGameEntityDispose
    {
        private readonly IGameContext _gameContext;

        private TeamCatalog _catalog;
        private Renderer _renderer;
        private IReactiveValue<TeamType> _team;

        public TeamColorBehaviour(IGameContext gameContext)
        {
            _gameContext = gameContext;
        }

        public void Init(IGameEntity entity)
        {
            _gameContext.TryGetTeamCatalog(out _catalog);
            _renderer = entity.GetRenderer();

            _team = entity.GetTeamType();
            _team.Observe(this.OnTeamChanged);
        }

        public void Dispose(IGameEntity entity)
        {
            _team.OnEvent -= this.OnTeamChanged;
        }

        private void OnTeamChanged(TeamType teamType)
        {
            if (_catalog)
                _renderer.material = _catalog.GetInfo(teamType).Material;
        }
    }
}