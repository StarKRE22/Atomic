using System;
using Atomic.Entities;

namespace RTSGame
{
    [Serializable]
    public sealed class AttackTargetSystem : FixedTickPriorityGameEntitySystem
    {
        protected override IReadOnlyEntityCollection<IGameEntity> ProvideEntityCollection(IGameContext context) =>
            new EntityFilter<IGameEntity>(context.GetEntityWorld(), entity => entity.HasAttackerTag());

        protected override void Update(IGameEntity entity, float deltaTime) =>
            entity.Attack();
    }
}