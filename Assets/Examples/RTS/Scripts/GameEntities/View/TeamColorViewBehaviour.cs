using System;
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
        private Subscription<TeamType> _subscription;

        public void Init(IGameEntity entity)
        {
            _team = entity.GetTeam();
            _subscription = _team.Observe(this.OnTeamChanged);
        }

        public void Dispose(IEntity entity)
        {
            _subscription.Dispose();
        }

        private void OnTeamChanged(TeamType teamType)
        {
            TeamViewConfig.TeamInfo team = _viewConfig.GetTeam(teamType);
            _renderers.SetMaterial(team.Material);
        }
    }
}