// using Atomic.Entities;
// using UnityEngine;
//
// namespace ShooterGame.Gameplay
// {
//     public sealed class WeaponVisualInstaller : SceneEntityInstaller
//     {
//         [SerializeField]
//         private ParticleSystem _particleSystem;
//
//         [SerializeField]
//         private AudioSource _audioSource;
//         
//         public override void Install(IEntity entity)
//         {
//             entity.GetFireEvent().Subscribe(_particleSystem.Play);
//             entity.GetFireEvent().Subscribe(_audioSource.Play);
//         }
//     }
// }