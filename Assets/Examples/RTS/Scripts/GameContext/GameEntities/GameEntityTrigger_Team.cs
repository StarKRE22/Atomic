using System;
using Atomic.Entities;

namespace RTSGame
{
    public sealed class GameEntityTrigger_Team : EntityTriggerDisposable<IGameEntity>
    {
        protected override IDisposable ProvideSubscription(IGameEntity entity, Action<IGameEntity> action) => 
            entity.GetTeam().Subscribe(_ => action(entity));
    }
}