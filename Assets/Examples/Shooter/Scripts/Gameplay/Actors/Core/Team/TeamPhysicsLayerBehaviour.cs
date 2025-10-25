using Atomic.Elements;
using Atomic.Entities;

namespace ShooterGame.Gameplay
{
    public sealed class TeamPhysicsLayerBehaviour : IEntityInit<IActor>, IEntityDispose
    {
        private IVariable<int> _physicsLayer;
        private TeamCatalog teamCatalog;
        private IReactiveValue<TeamType> _team;

        public void Init(IActor entity)
        {
            _physicsLayer = entity.GetPhysicsLayer();
            teamCatalog = GameContext.Instance.GetTeamCatalog();
            
            _team = entity.GetTeamType();
            _team.Observe(this.OnTeamChanged);
        }

        public void Dispose(IEntity entity)
        {
            _team.Unsubscribe(this.OnTeamChanged);
        }

        private void OnTeamChanged(TeamType teamType)
        {
            _physicsLayer.Value = teamCatalog.GetInfo(teamType).PhysicsLayer;
        }
    }
}