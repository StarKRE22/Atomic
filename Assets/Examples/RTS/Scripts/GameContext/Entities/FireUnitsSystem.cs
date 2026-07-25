using System;
using Atomic.Entities;

namespace RTSGame
{
    [Serializable]
    public sealed class FireUnitsSystem : FixedTickPriorityGameEntitySystem
    {
        protected override IReadOnlyEntityCollection<IGameEntity> ProvideEntityCollection(IGameContext context) => 
            new EntityFilter<IGameEntity>(context.GetEntityWorld(), entity => entity.HasFireableTag());
        
        protected override void Update(IGameEntity entity, float deltaTime)
        {
            if (entity.TryGetFireCooldown(out var cooldown)) 
                cooldown.Tick(deltaTime);
            
            if (entity.GetFireRequest().Consume(out IGameEntity target)) 
                entity.GetFireCommand().Invoke(target);
        }
    }
}