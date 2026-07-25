using System;
using Atomic.Elements;
using Atomic.Entities;

namespace RTSGame
{
    [Serializable]
    public sealed class ProjectileLifetimeSystem : FixedTickPriorityGameEntitySystem
    {
        private readonly IGameContext _gameContext;

        public ProjectileLifetimeSystem(
            IGameContext context,
            GamePriorityEntitySystemSettings settings)
            : base(
                new EntityFilter<IGameEntity>(context.GetEntityWorld(), e => e.HasProjectileTag()),
                settings)
        {
            _gameContext = context;
        }

        protected override void Update(IGameEntity entity, float deltaTime)
        {
            Cooldown lifetime = entity.GetLifetime();
            lifetime.Tick(deltaTime);
            if (lifetime.IsCompleted())
                _gameContext.Despawn(entity);
        }
    }
}
