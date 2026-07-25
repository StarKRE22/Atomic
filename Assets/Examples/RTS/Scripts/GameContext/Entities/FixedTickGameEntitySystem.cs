using System;
using Atomic.Entities;
using UnityEngine;

namespace RTSGame
{
    [Serializable]
    public abstract class FixedTickGameEntitySystem : EntitySystem<IGameEntity>
    {
        protected FixedTickGameEntitySystem(
            IReadOnlyEntityCollection<IGameEntity> source,
            GameEntitySystemSettings settings)
            : base(source, settings)
        {
        }
    }
}
