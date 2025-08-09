// using Atomic.Entities;
// using UnityEngine;
//
// namespace ShooterGame.Gameplay
// {
//     public sealed class BulletMoveBehaviour : IEntityInit, IEntityFixedUpdate
//     {
//         private Transform _transform;
//
//         public void Init(in IEntity entity)
//         {
//             _transform = entity.GetTransform();
//         }
//
//         public void OnFixedUpdate(in IEntity entity, in float deltaTime)
//         {
//             MoveUseCase.MoveTowards(entity, _transform.forward, deltaTime);
//         }
//     }
// }