using Atomic.Entities;

namespace RTSGame
{
    public sealed class LifeBehaviour : IEntityInit<IUnitEntity>, IEntityDispose
    {
        private readonly IGameContext _gameContext;

        private Health _health;
        private IUnitEntity _entity;

        public LifeBehaviour(IGameContext gameContext)
        {
            _gameContext = gameContext;
        }

        public void Init(IUnitEntity entity)
        {
            _entity = entity;

            _health = entity.GetHealth();
            _health.OnHealthEmpty += this.OnHealthEmpty;
        }

        public void Dispose(IEntity entity)
        {
            _health.OnHealthEmpty -= this.OnHealthEmpty;
        }

        private void OnHealthEmpty() => UnitsUseCase.Despawn(_gameContext, _entity);
    }
}