using Atomic.Elements;
using Atomic.Entities;

namespace RTSGame
{
    public sealed class ProjectileLifetimeBehaviour : IEntityInit<IGameEntity>, IEntityEnable, IEntityFixedTick
    {
        private readonly IGameContext _gameContext;
        private IGameEntity _entity;
        private Cooldown _lifetime;

        public ProjectileLifetimeBehaviour(IGameContext gameContext)
        {
            _gameContext = gameContext;
        }

        public void Init(IGameEntity entity)
        {
            _entity = entity;
            _lifetime = entity.GetLifetime();
        }

        public void Enable(IEntity entity)
        {
            _lifetime.ResetTime();
        }

        public void FixedTick(IEntity entity, float deltaTime)
        {
            _lifetime.Tick(deltaTime);
            if (_lifetime.IsCompleted())
                GameEntitiesUseCase.Despawn(_gameContext, _entity);
        }
    }
}