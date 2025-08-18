// using Atomic.Entities;
// using UnityEngine;
//
// namespace RTSGame
// {
//     public static class FireUseCase
//     {
//         public static bool FireAbstract(in IEntity entity, in IEntity target)
//         {
//             if (!entity.GetFireCondition().Invoke(target))
//                 return false;
//
//             entity.GetFireAction().Invoke(target);
//             entity.GetFireEvent().Invoke(target);
//             return true;
//         }
//
//         public static IEntity FireProjectile(
//             in IEntity source,
//             in string projectileName,
//             in IEntity target,
//             in GameContext context
//         )
//         {
//             IEntity projectile = FireProjectile(source, projectileName, context);
//             projectile.GetTarget().Value = target;
//             return projectile;
//         }
//
//         public static IEntity FireProjectile(in IEntity source, in string projectileName, in GameContext context)
//         {
//             Vector3 firePosition = source.GetFirePoint().Value;
//             Quaternion fireRotation = source.GetRotation().Value;
//             TeamType team = source.GetTeam().Value;
//             return EntitiesUseCase.SpawnEntity(context, projectileName, firePosition, fireRotation, team);
//         }
//
//         public static Vector3 GetFirePoint(in IEntity entity, Vector3 fireOffset)
//         {
//             return entity.GetPosition().Value + entity.GetRotation().Value * fireOffset;
//         }
//     }
// }