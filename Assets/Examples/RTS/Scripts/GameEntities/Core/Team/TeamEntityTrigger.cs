using System;
using Atomic.Elements;
using Atomic.Entities;

namespace RTSGame
{
    public sealed class TeamEntityTrigger : SubscriptionEntityTrigger<IGameEntity, Subscription<TeamType>>
    {
        protected override Subscription<TeamType> Track(IGameEntity entity, Action<IGameEntity> callback)
        {
            IReactiveVariable<TeamType> team = entity.GetTeam();
            Subscription<TeamType> subscription = team.Subscribe(_ => callback.Invoke(entity));
            return subscription;
        }
    }
}