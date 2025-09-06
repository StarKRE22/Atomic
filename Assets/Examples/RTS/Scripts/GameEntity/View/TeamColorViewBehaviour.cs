using System;
using System.Collections.Generic;
using Atomic.Elements;
using Atomic.Entities;
using UnityEngine;

namespace RTSGame
{
    [Serializable]
    public sealed class TeamColorViewBehaviour : IEntityInit<IGameEntity>, IEntityDispose
    {
        [SerializeField]
        private Renderer[] _renderers;

        [SerializeField]
        private TeamViewConfig _viewConfig;
        
        private IReactiveValue<TeamType> _team;

        public void Init(IGameEntity entity)
        {
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