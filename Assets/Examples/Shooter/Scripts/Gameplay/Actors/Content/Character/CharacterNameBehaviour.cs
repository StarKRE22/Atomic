using Atomic.Elements;
using Atomic.Entities;

namespace ShooterGame.Gameplay
{
    public sealed class CharacterNameBehaviour : IEntityInit<IActor>, IEntityDispose
    {
        private const string NAME_FORMAT = "Character ({0})";
        
        private IActor _entity;
        private IReactiveValue<TeamType> _teamType;
        
        public void Init(IActor entity)
        {
            _entity = entity;
            _teamType = entity.GetTeamType();
            _teamType.Observe(this.OnTeamChanged);
        }

        public void Dispose(IEntity entity)
        {
            _teamType.Unsubscribe(this.OnTeamChanged);
        }

        private void OnTeamChanged(TeamType teamType)
        {
            _entity.Name = string.Format(NAME_FORMAT, teamType);
        }
    }
}