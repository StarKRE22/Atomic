// using Atomic.Elements;
// using Atomic.Entities;
// using UnityEngine;
//
// namespace SampleGame
// {
//     public sealed class FireAnimBehaviour : IEntityInit, IEntityDispose
//     {
//         private static readonly int Fire = Animator.StringToHash("Fire");
//
//         private Animator _animator;
//         private IReactive _fireEvent;
//
//         public void Init(in IEntity entity)
//         {
//             _animator = entity.GetAnimator();
//             _fireEvent = entity.GetFireEvent();
//             _fireEvent.Subscribe(this.OnFire);
//         }
//
//         public void Dispose(in IEntity entity)
//         {
//             _fireEvent.Unsubscribe(this.OnFire);
//         }
//
//         private void OnFire()
//         {
//             _animator.SetTrigger(Fire);
//         }
//     }
// }