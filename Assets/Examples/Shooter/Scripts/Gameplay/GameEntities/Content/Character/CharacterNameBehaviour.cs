using Atomic.Elements;

namespace ShooterGame.Gameplay
{
    public sealed class CharacterNameBehaviour : IGameEntityInit, IGameEntityDispose
    {
        private const string NAME_FORMAT = "Character ({0})";
        
        private IGameEntity _entity;
        private IReactiveValue<TeamType> _teamType;
        
        public void Init(IGameEntity entity)
        {
            _entity = entity;
            _teamType = entity.GetTeamType();
            _teamType.Observe(this.OnTeamChanged);
        }

        public void Dispose(IGameEntity entity)
        {
            _teamType.OnEvent -= this.OnTeamChanged;
        }

        private void OnTeamChanged(TeamType teamType)
        {
            _entity.Name = string.Format(NAME_FORMAT, teamType);
        }
    }
}