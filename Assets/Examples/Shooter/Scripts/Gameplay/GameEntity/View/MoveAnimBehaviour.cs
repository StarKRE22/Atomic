// using Atomic.Elements;
// using Atomic.Entities;
// using UnityEngine;
//
// namespace ShooterGame.Gameplay
// {
//     public sealed class MoveAnimBehaviour : IEntityInit, IEntityLateUpdate
//     {
//         private static readonly int IsMoving = Animator.StringToHash("IsMoving");
//
//         private Animator _animator;
//         private IValue<Vector3> _moveDirection;
//
//         public void Init(in IEntity entity)
//         {
//             _moveDirection = entity.GetMoveDirection();
//             _animator = entity.GetAnimator();
//         }
//
//         public void OnLateUpdate(in IEntity entity, in float deltaTime)
//         {
//             _animator.SetBool(IsMoving, _moveDirection.Value != Vector3.zero);
//         }
//     }
// }