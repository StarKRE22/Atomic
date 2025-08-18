// using System;
// using System.Collections.Generic;
// using Atomic.Elements;
// using Atomic.Entities;
// using UnityEngine;
//
// namespace RTSGame
// {
//     public static class EntitiesUseCase
//     {
//         private static readonly Predicate<IEntity> TruePredicate = _ => true;
//
//         public static IEntity SpawnEntity(
//             in GameContext gameContext,
//             in string name,
//             in Vector3 position,
//             in Quaternion rotation,
//             in TeamType team
//         )
//         {
//             IEntity entity = gameContext.GetEntityPool().Rent(name);
//             entity.AddPosition(new ReactiveVector3(position));
//             entity.AddRotation(new ReactiveQuaternion(rotation));
//             entity.AddTeam(new ReactiveVariable<TeamType>(team));
//             gameContext.GetEntityWorld().Add(entity);
//
//             return entity;
//         }
//
//         public static bool UnspawnEntity(
//             in GameContext gameContext,
//             in IEntity entity
//         )
//         {
//             if (!gameContext.GetEntityWorld().Del(entity))
//                 return false;
//
//             entity.DelPosition();
//             entity.DelRotation();
//             entity.DelTeam();
//
//             gameContext.GetEntityPool().Return(entity);
//             return true;
//         }
//
//         public static IEntity FindClosest(in IEnumerable<IEntity> entities, in Vector3 center)
//         {
//             IEntity result = null;
//
//             float minDistance = float.MaxValue;
//             foreach (IEntity entity in entities)
//             {
//                 Vector3 position = entity.GetPosition().Value;
//                 float distance = Vector3.SqrMagnitude(position - center);
//                 if (distance >= minDistance)
//                     continue;
//
//                 result = entity;
//                 minDistance = distance;
//             }
//
//             return result;
//         }
//
//
//         public static IEntity FindClosest(in IEnumerator<IEntity> entities, in Vector3 center)
//         {
//             IEntity result = null;
//
//             float minDistance = float.MaxValue;
//             while (entities.MoveNext())
//             {
//                 IEntity entity = entities.Current;
//                 Vector3 position = entity.GetPosition().Value;
//                 float distance = Vector3.SqrMagnitude(position - center);
//                 if (distance >= minDistance)
//                     continue;
//
//                 result = entity;
//                 minDistance = distance;
//             }
//
//             return result;
//         }
//
//         public static bool FindClosest(
//             in GameContext context,
//             in Vector3 center,
//             out IEntity result,
//             Predicate<IEntity> predicate = null
//         )
//         {
//             predicate ??= TruePredicate;
//
//             result = null;
//             IEntityWorld world = context.GetEntityWorld();
//
//             float minDistance = float.MaxValue;
//             foreach (IEntity entity in world.All)
//             {
//                 if (!predicate.Invoke(entity))
//                     continue;
//
//                 Vector3 position = entity.GetPosition().Value;
//                 float distance = Vector3.SqrMagnitude(position - center);
//                 if (distance >= minDistance)
//                     continue;
//
//                 result = entity;
//                 minDistance = distance;
//             }
//
//             return result != null;
//         }
//     }
// }