using Atomic.Elements;
using Atomic.Entities;
using UnityEngine;

namespace BeginnerGame
{
    [RunInEditMode]
    public sealed class TeamColorBehaviour : IEntitySpawn<IGameEntity>, IEntityDespawn
    {
        private IGameEntity _entity;
        private TeamCatalog _catalog;

        private Renderer _renderer;
        private IReactiveValue<TeamType> _team;

        public void OnSpawn(IGameEntity entity)
        {
            GameContext gameContext = GameContext.Instance;
            if (gameContext) gameContext.TryGetTeamCatalog(out _catalog);
            
            _entity = entity;
            _renderer = entity.GetRenderer();
            
            _team = entity.GetTeamType();
            _team.Observe(this.OnTeamChanged);
        }

        public void OnDespawn(IEntity entity)
        {
            _team.Unsubscribe(this.OnTeamChanged);
        }

        private void OnTeamChanged(TeamType teamType)
        {
            Debug.Log($"ON TEAM CHANGED {teamType} {_entity.Name}");
            if (_catalog)
            {
                Debug.Log("CHANGED MAT!");
                _renderer.material = _catalog.GetInfo(teamType).Material;
            }
        }
    }
}