// using Atomic.Entities;
// using UnityEngine;
//
// namespace ShooterGame.Gameplay
// {
//     public static class RotateUseCase
//     {
//         public static void RotateTowards(in IEntity entity, in Vector3 direction, in float deltaTime)
//         {
//             if (direction == Vector3.zero)
//                 return;
//
//             Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);
//             RotateTowards(entity, targetRotation, deltaTime);
//         }
//
//         public static void RotateTowards(in IEntity entity, in Quaternion targetRotation, in float deltaTime)
//         {
//             float speed = entity.GetAngularSpeed().Value * deltaTime;
//             Transform transform = entity.GetTransform();
//             transform.rotation = RotateTowards(transform.rotation, targetRotation, speed);
//         }
//         
//         public static Quaternion RotateTowards(in Quaternion currentRotation, in Quaternion targetRotation, in float speed)
//         {
//             return Quaternion.Lerp(currentRotation, targetRotation, speed);
//         }
//     }
// }