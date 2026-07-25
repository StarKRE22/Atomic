using System;
using Atomic.Entities;

namespace RTSGame
{
    [Serializable]
    public sealed class AttackTargetSystem : FixedTickPriorityGameEntitySystem
    {
        public AttackTargetSystem(
            IGameContext context,
            GamePriorityEntitySystemSettings settings)
            : base(
                new EntityFilter<IGameEntity>(context.GetEntityWorld(), entity => entity.HasAttackerTag()),
                settings)
        {
        }

        protected override void Update(IGameEntity entity, float deltaTime) =>
            entity.Attack();
    }
}