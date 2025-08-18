using System.Collections.Generic;
using Atomic.Elements;
using Atomic.Entities;
using UnityEngine;

namespace RTSGame
{
    public sealed class TeamColorBehaviour : IEntitySpawn<IGameEntity>, IEntityDespawn
    {
        private readonly IGameContext _gameContext;
        private readonly IEnumerable<Renderer> _renderers;

        private IReactiveValue<TeamType> _team;
        private TeamViewConfig _viewConfig;

        public TeamColorBehaviour(IEnumerable<Renderer> renderers)
        {
            _renderers = renderers;
        }

        public void OnSpawn(IGameEntity entity)
        {
            _viewConfig = _gameContext.GetTeamViewConfig();
            _team = entity.GetTeam();
            _team.Observe(this.OnTeamChanged);
        }

        public void OnDespawn(IEntity entity)
        {
            _team.Unsubscribe(this.OnTeamChanged);
        }

        private void OnTeamChanged(TeamType teamType)
        {
            TeamViewConfig.TeamInfo team = _viewConfig.GetTeam(teamType);
            Material material = team.Material;
            foreach (Renderer renderer in _renderers)
                renderer.material = material;
        }
    }
}