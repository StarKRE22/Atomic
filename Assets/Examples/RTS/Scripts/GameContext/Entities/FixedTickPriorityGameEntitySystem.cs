using System;
using Atomic.Entities;
using UnityEngine;

namespace RTSGame
{
    [Serializable]
    public abstract class FixedTickPriorityGameEntitySystem : PriorityEntitySystem<IGameEntity>
    {
        protected FixedTickPriorityGameEntitySystem(
            IReadOnlyEntityCollection<IGameEntity> source,
            GamePriorityEntitySystemSettings settings)
            : base(source, settings)
        {
        }

        protected sealed override EntityUpdatePriority EvaluatePriority(IGameEntity entity) =>
            entity.GetUpdatePriority().Value;
    }
}
