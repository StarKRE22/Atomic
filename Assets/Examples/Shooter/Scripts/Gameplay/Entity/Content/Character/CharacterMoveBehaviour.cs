// using Atomic.Elements;
// using Atomic.Entities;
// using UnityEngine;
//
// namespace ShooterGame.Gameplay
// {
//     public sealed class CharacterMoveBehaviour : IEntityInit, IEntityFixedUpdate
//     {
//         private IValue<bool> _moveCondition;
//         private IValue<Vector3> _moveDirection;
//
//         public void Init(in IEntity entity)
//         {
//             _moveCondition = entity.GetMoveCondition();
//             _moveDirection = entity.GetMoveDirection();
//         }
//
//         public void OnFixedUpdate(in IEntity entity, in float deltaTime)
//         {
//             if (!_moveCondition.Value) 
//                 return;
//             
//             Vector3 direction = _moveDirection.Value;
//             MoveUseCase.MoveTowards(entity, direction, deltaTime);
//             RotateUseCase.RotateTowards(entity, direction, deltaTime);
//         }
//     }
// }