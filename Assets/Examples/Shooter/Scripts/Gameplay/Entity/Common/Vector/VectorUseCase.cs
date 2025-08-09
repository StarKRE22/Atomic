// using Atomic.Entities;
// using UnityEngine;
//
// namespace ShooterGame.Gameplay
// {
//     public static class VectorUseCase
//     {
//         public static Vector3 GetDirectionAt(in IEntity entity, in IEntity target)
//         {
//             Vector3 currentPosition = entity.GetTransform().position;
//             Vector3 targetPosition = target.GetTransform().position;
//             Vector3 vector = targetPosition - currentPosition;
//             vector.y = 0;
//             
//             return vector.normalized;
//         }
//     }
// }