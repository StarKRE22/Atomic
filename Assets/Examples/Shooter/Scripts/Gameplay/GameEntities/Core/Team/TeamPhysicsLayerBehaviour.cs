using Atomic.Elements;

namespace ShooterGame.Gameplay
{
    public sealed class TeamPhysicsLayerBehaviour : IGameEntityInit, IGameEntityDispose
    {
        private readonly IGameContext _gameContext;
        
        private IVariable<int> _physicsLayer;
        private TeamCatalog _teamCatalog;
        private IReactiveValue<TeamType> _team;

        public TeamPhysicsLayerBehaviour(IGameContext gameContext)
        {
            _gameContext = gameContext;
        }

        public void Init(IGameEntity entity)
        {
            _physicsLayer = entity.GetPhysicsLayer();
            _teamCatalog = _gameContext.GetTeamCatalog();
            
            _team = entity.GetTeamType();
            _team.Observe(this.OnTeamChanged);
        }

        public void Dispose(IGameEntity entity)
        {
            _team.OnEvent -= this.OnTeamChanged;
        }

        private void OnTeamChanged(TeamType teamType)
        {
            _physicsLayer.Value = _teamCatalog.GetInfo(teamType).PhysicsLayer;
        }
    }
}