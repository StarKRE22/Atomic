using Atomic.Elements;
using Atomic.Entities;
using UnityEngine;

namespace ShooterGame.Gameplay
{
    public sealed class TeamPhysicsLayerBehaviour : IEntityInit, IEntityDispose
    {
        private GameObject _gameObject;
        private TeamConfig _teamConfig;
        private IReactiveValue<TeamType> _team;

        public void Init(in IEntity entity)
        {
            _gameObject = entity.GetGameObject();
            _teamConfig = GameContext.Instance.GetTeamConfig();
            _team = entity.GetTeam();
            _team.Observe(this.OnTeamChanged);
        }

        private void OnTeamChanged(TeamType teamType)
        {
            _gameObject.layer = _teamConfig.GetTeam(teamType).PhysicsLayer;
        }

        public void Dispose(in IEntity entity)
        {
            _team.Unsubscribe(this.OnTeamChanged);
        }
    }
}