using System;
using Atomic.Elements;
using Atomic.Entities;

namespace RTSGame
{
    [Serializable]
    public sealed class DetectTargetSystem : FixedTickPriorityGameEntitySystem
    {
        private IEntityWorld<IGameEntity> _entityWorld;
        private IGameContext _gameContext;

        protected override IReadOnlyEntityCollection<IGameEntity> ProvideEntityCollection(IGameContext context) => 
            new EntityFilter<IGameEntity>(context.GetEntityWorld(), e => e.HasDetectorTag());

        protected override void OnInit(IGameContext context)
        {
            base.OnInit(context);
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