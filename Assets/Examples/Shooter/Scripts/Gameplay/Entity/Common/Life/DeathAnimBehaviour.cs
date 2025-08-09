// using Atomic.Entities;
// using Cysharp.Threading.Tasks;
// using UnityEngine;
//
// namespace ShooterGame.Gameplay
// {
//     public sealed class DeathAnimBehaviour : IEntityInit, IEntityDispose
//     {
//         private static readonly int Death = Animator.StringToHash("Death");
//         private const float AnimationTime = 1.5f;
//
//         private Health _health;
//         private Animator _animator;
//         private GameObject _gameObject;
//
//         public void Init(in IEntity entity)
//         {
//             _animator = entity.GetAnimator();
//             _health = entity.GetHealth();
//             _gameObject = entity.GetGameObject();
//             _health.OnHealthEmpty += this.OnDeath;
//         }
//
//         public void Dispose(in IEntity entity)
//         {
//             _health.OnHealthEmpty -= this.OnDeath;
//         }
//
//         private async void OnDeath()
//         {
//             _animator.SetTrigger(Death);
//             
//             await UniTask.WaitForSeconds(AnimationTime);
//             _gameObject.SetActive(false);
//         }
//     }
// }