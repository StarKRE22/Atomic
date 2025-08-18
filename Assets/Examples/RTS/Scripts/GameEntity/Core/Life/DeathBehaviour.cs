using Atomic.Entities;

namespace RTSGame
{
    public sealed class DeathBehaviour : IEntitySpawn<IGameEntity>, IEntityDespawn
    {
        private Health _health;
        private IEntity _entity;
        private GameContext _gameContext;

        public void Init(in IEntity entity)
        {
            _entity = entity;
            _gameContext = GameContext.Instance;
            
            _health = entity.GetHealth();
            _health.OnHealthEmpty += this.OnHealthEmpty;
        }

        public void Dispose(in IEntity entity)
        {
            _health.OnHealthEmpty -= this.OnHealthEmpty;
        }

        private void OnHealthEmpty()
        {
            EntitiesUseCase.UnspawnEntity(_gameContext, _entity);
        }

        public void OnSpawn(IGameEntity entity)
        {
            
        }

        public void OnDespawn(IEntity entity)
        {
        }
    }
}