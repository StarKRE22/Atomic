using Atomic.Entities;

namespace RTSGame
{
    public sealed class LifeBehaviour : IEntityInit<IUnit>, IEntityDispose
    {
        private readonly IGameContext _gameContext;

        private Health _health;
        private IUnit _entity;

        public LifeBehaviour(IGameContext gameContext)
        {
            _gameContext = gameContext;
        }

        public void Init(IUnit entity)
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