using System;
using Atomic.Elements;
using Atomic.Entities;

namespace RTSGame
{
    [Serializable]
    public sealed class ProjectileLifetimeSystem : FixedTickPriorityGameEntitySystem
    {
        private IGameContext _gameContext;
        
        protected override IReadOnlyEntityCollection<IGameEntity> ProvideEntityCollection(IGameContext context) => 
            new EntityFilter<IGameEntity>(context.GetEntityWorld(), e => e.HasProjectileTag());

        protected override void OnInit(IGameContext context)
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