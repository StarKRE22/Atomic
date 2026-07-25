using System;
using System.Collections.Generic;
using Atomic.Entities;
using UnityEngine;

namespace RTSGame
{
    [Serializable]
    public abstract class FixedTickPriorityGameEntitySystem : FixedTickPriorityEntitySystem<IGameContext, IGameEntity>
    {
        [SerializeField]
        private PriorityEntityUpdateSettings _settings;

        protected override PriorityEntityUpdateSettings ProvideUpdateSettings(IGameContext context) =>
            _settings;

        protected override IEnumerable<IEntityTrigger<IGameEntity>> ProvidePriorityTriggers(IGameContext context)
        {
            yield return new PriorityEntityTrigger();
        }

        protected override EntityUpdatePriority GetPriority(IGameEntity entity) =>
            entity.GetUpdatePriority().Value;
    }
}