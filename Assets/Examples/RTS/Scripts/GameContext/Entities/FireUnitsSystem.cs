using System;
using Atomic.Entities;

namespace RTSGame
{
    [Serializable]
    public sealed class FireUnitsSystem : FixedTickPriorityGameEntitySystem
    {
        public FireUnitsSystem(
            IGameContext context,
            GamePriorityEntitySystemSettings settings)
            : base(
                new EntityFilter<IGameEntity>(context.GetEntityWorld(), entity => entity.HasFireableTag()),
                settings)
        {
        }

        protected override void Update(IGameEntity entity, float deltaTime)
        {
            if (entity.TryGetFireCooldown(out var cooldown))
                cooldown.Tick(deltaTime);

            if (entity.GetFireRequest().Consume(out IGameEntity target))
                entity.GetFireCommand().Invoke(target);
        }
    }
}
