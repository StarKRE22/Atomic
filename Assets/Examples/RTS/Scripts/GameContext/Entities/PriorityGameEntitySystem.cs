using System;
using Atomic.Entities;
using UnityEngine;

namespace RTSGame
{
    [Serializable]
    public abstract class PriorityGameEntitySystem : PriorityEntitySystem<IGameEntity>
    {
        protected PriorityGameEntitySystem(
            IReadOnlyEntityCollection<IGameEntity> source,
            GamePriorityEntitySystemSettings settings)
            : base(source, settings)
        {
        }

        protected sealed override EntityUpdatePriority EvaluatePriority(IGameEntity entity) =>
            entity.GetUpdatePriority().Value;
    }
}
