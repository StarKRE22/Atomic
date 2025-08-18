// using Atomic.Entities;
// using UnityEngine;
//
// namespace RTSGame
// {
//     public static class AttackUseCase
//     {
//         public static bool AttackTarget(in IEntity entity, in IEntity target, in float deltaTime)
//         {
//             if (target is not {Enabled: true})
//                 return false;
//
//             if (!HealthUseCase.IsAlive(target))
//                 return false;
//
//             Vector3 vector = TransformUseCase.GetVector(entity, target);
//             float attackDistance = entity.GetFireDistance().Value;
//             
//             if (vector.sqrMagnitude > attackDistance * attackDistance)
//                 MoveUseCase.MoveAbstract(entity, vector.normalized, deltaTime);
//             else
//                 FireUseCase.FireAbstract(entity, target);
//
//             return true;
//         }
//     }
// }