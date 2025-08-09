// using Atomic.Elements;
// using Atomic.Entities;
// using UnityEngine;
//
// namespace ShooterGame.Gameplay
// {
//     public sealed class TakeDamageAnimBehaviour : IEntityInit, IEntityDispose
//     {
//         private static readonly int TakeDamage = Animator.StringToHash("TakeDamage");
//
//         private IReactive<DamageArgs> _damageEvent;
//         private Animator _animator;
//
//         public void Init(in IEntity entity)
//         {
//             _animator = entity.GetAnimator();
//             _damageEvent = entity.GetTakeDamageEvent();
//             _damageEvent.Subscribe(this.OnDamageTaken);
//         }
//
//         public void Dispose(in IEntity entity)
//         {
//             _damageEvent.Unsubscribe(this.OnDamageTaken);
//         }
//
//         private void OnDamageTaken(DamageArgs obj)
//         {
//             _animator.SetTrigger(TakeDamage);
//         }
//     }
// }