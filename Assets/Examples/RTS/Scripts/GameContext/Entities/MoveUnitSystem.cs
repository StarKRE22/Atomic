using System;
using System.Collections.Generic;
using Atomic.Entities;
using UnityEngine;

namespace RTSGame
{
    [Serializable]
    public sealed class MoveUnitSystem : FixedTickPriorityGameEntitySystem
    { 
        protected override IReadOnlyEntityCollection<IGameEntity> ProvideEntityCollection(IGameContext context) =>
            new EntityFilter<IGameEntity>(context.GetEntityWorld(), e => e.HasUnitTag() && e.HasMoveableTag());
        
        protected override void Update(IGameEntity entity, float deltaTime)
        {
            if (entity.GetMoveRequest().Consume(out Vector3 direction))
                entity.GetMoveCommand().Invoke(direction, deltaTime);
        }
    }
}