using Atomic.Elements;
using Atomic.Entities;
using Unity.Profiling;

namespace RTSGame
{
    public sealed class ProjectileLifetimeBehaviour : IEntityInit<IGameEntity>, IEntityFixedTick
    {
#if ENABLE_PROFILER
        private static readonly ProfilerMarker FixedTickMarker = new("ProjectileLifetime.FixedTick");
#endif

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

        public void FixedTick(IEntity entity, float deltaTime)
        {
#if ENABLE_PROFILER
            using (FixedTickMarker.Auto())
#endif
            {
                _lifetime.Tick(deltaTime);

                if (_lifetime.IsCompleted())
                    _gameContext.Despawn(_entity);
            }
        }
    }
}