using Atomic.Elements;
using Atomic.Entities;

namespace ShooterGame.Gameplay
{
    public sealed class CharacterNameBehaviour : IEntitySpawn<IGameEntity>, IEntityDespawn
    {
        private const string NAME_FORMAT = "Character ({0})";
        
        private IGameEntity _entity;
        private IReactiveValue<TeamType> _teamType;
        
        public void OnSpawn(IGameEntity entity)
        {
            _entity = entity;
            _teamType = entity.GetTeamType();
            _teamType.Observe(this.OnTeamChanged);
        }

        public void OnDespawn(IEntity entity)
        {
            _teamType.Unsubscribe(this.OnTeamChanged);
        }

        private void OnTeamChanged(TeamType teamType)
        {
            _entity.Name = string.Format(NAME_FORMAT, teamType);
        }
    }
}