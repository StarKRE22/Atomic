using System;
using Atomic.Entities;
using UnityEngine;

namespace RTSGame
{
    [Serializable]
    public sealed class MoveUnitSystem : FixedTickPriorityGameEntitySystem
    {
        public MoveUnitSystem(
            IGameContext context,
            GamePriorityEntitySystemSettings settings)
            : base(
                new EntityFilter<IGameEntity>(context.GetEntityWorld(), e => e.HasUnitTag() && e.HasMoveableTag()),
                settings)
        {
        }

        protected override void Update(IGameEntity entity, float deltaTime)
        {
            if (entity.GetMoveRequest().Consume(out Vector3 direction))
                entity.GetMoveCommand().Invoke(direction, deltaTime);
        }
    }
}
