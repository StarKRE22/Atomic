using System;
using Atomic.Elements;
using Atomic.Entities;

namespace RTSGame
{
    [Serializable]
    public sealed class DetectTargetSystem : PriorityGameEntitySystem
    {
        private readonly IEntityWorld<IGameEntity> _entityWorld;
        private readonly IGameContext _gameContext;

        public DetectTargetSystem(
            IGameContext context,
            GamePriorityEntitySystemSettings settings)
            : base(
                new EntityFilter<IGameEntity>(context.GetEntityWorld(), e => e.HasDetectorTag()),
                settings)
        {
            _gameContext = context;
            _entityWorld = context.GetEntityWorld();
        }

        protected override void Update(IGameEntity entity, float deltaTime)
        {
            ICooldown cooldown = entity.GetDetectionCooldown();
            cooldown.Tick(deltaTime);

            var target = entity.GetTarget();
            if (cooldown.IsCompleted() && !_entityWorld.Contains(target.Value))
            {
                target.Value = _gameContext.FindClosestEnemy(entity);
                cooldown.ResetTime();
            }
        }
    }
}