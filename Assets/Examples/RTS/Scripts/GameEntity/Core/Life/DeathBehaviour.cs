using Atomic.Entities;

namespace RTSGame
{
    public sealed class DeathBehaviour : IEntitySpawn<IGameEntity>, IEntityDespawn
    {
        private readonly IGameContext _gameContext;

        private Health _health;
        private IGameEntity _entity;

        public DeathBehaviour(IGameContext gameContext)
        {
            _gameContext = gameContext;
        }

        public void OnSpawn(IGameEntity entity)
        {
            _entity = entity;

            _health = entity.GetHealth();
            _health.OnHealthEmpty += this.OnHealthEmpty;
        }

        public void OnDespawn(IEntity entity)
        {
            _health.OnHealthEmpty -= this.OnHealthEmpty;
        }

        private void OnHealthEmpty() => GameEntityUseCase.Despawn(_gameContext, _entity);
    }
}