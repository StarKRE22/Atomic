using System.Collections.Generic;
using Atomic.Elements;
using Atomic.Entities;
using UnityEngine;

namespace RTSGame
{
    public sealed class TeamColorBehaviour : IEntityInit<IGameEntity>, IEntityDispose
    {
        private readonly IGameContext _gameContext;
        private readonly IEnumerable<Renderer> _renderers;

        private IReactiveValue<TeamType> _team;
        private TeamViewConfig _viewConfig;

        public TeamColorBehaviour(IEnumerable<Renderer> renderers, IGameContext context)
        {
            _renderers = renderers;
            _gameContext = context;
        }

        public void Init(IGameEntity entity)
        {
            _viewConfig = _gameContext.GetTeamViewConfig();
            _team = entity.GetTeam();
            _team.Observe(this.OnTeamChanged);
        }

        public void Dispose(IEntity entity)
        {
            _team.Unsubscribe(this.OnTeamChanged);
        }

        private void OnTeamChanged(TeamType teamType)
        {
            TeamViewConfig.TeamInfo team = _viewConfig.GetTeam(teamType);
            RendererUseCase.SetMaterial(_renderers, team.Material);
        }
    }
}