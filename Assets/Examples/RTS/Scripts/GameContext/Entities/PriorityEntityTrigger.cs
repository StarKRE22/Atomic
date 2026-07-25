using System;
using Atomic.Elements;
using Atomic.Entities;

namespace RTSGame
{
    public sealed class PriorityEntityTrigger : SubscriptionEntityTrigger<IGameEntity, Subscription<EntityUpdatePriority>>
    {
        protected override Subscription<EntityUpdatePriority> Track(IGameEntity entity, Action<IGameEntity> callback) => 
            entity.GetUpdatePriority().Subscribe(_ => callback.Invoke(entity));
    }
}