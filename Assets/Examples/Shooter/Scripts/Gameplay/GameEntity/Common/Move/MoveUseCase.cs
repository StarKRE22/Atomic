// using Atomic.Elements;
// using Atomic.Entities;
// using UnityEngine;
//
// namespace ShooterGame.Gameplay
// {
//     public static class MoveUseCase
//     {
//         public static void MoveTowards(in IEntity entity, in Vector3 direction, in float deltaTime)
//         {
//             Transform transform = entity.GetTransform();
//             IValue<float> speed = entity.GetMoveSpeed();
//             transform.position += direction * (speed.Invoke() * deltaTime);
//         }
//     }
// }