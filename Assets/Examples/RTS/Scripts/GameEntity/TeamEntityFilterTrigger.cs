// using System;
// using System.Collections.Generic;
// using Atomic.Elements;
// using Atomic.Entities;
//
// namespace RTSGame
// {
//     public sealed class TeamEntityFilterTrigger : IEntityFilter.ITrigger
//     {
//         private readonly Dictionary<IEntity, Subscription<TeamType>> _subscriptions = new();
//
//         public void Subscribe(IEntity entity, Action<IEntity> callback)
//         {
//             _subscriptions.Add(entity, new Subscription<TeamType>(entity.GetTeam(), _ => callback.Invoke(entity)));
//         }
//
//         public void Unsubscribe(IEntity entity, Action<IEntity> callback)
//         {
//             if (_subscriptions.Remove(entity, out var subscription)) 
//                 subscription.Dispose();
//         }
//     }
// }