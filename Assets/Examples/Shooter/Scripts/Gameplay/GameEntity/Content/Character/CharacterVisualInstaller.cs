// using Atomic.Entities;
// using UnityEngine;
//
// namespace ShooterGame.Gameplay
// {
//     public sealed class CharacterVisualInstaller : SceneEntityInstaller
//     {
//         [SerializeField]
//         private Renderer _meshRenderer;
//
//         [SerializeField]
//         private Animator _animator;
//
//         [SerializeField]
//         private ParticleSystem _bloodVfx;
//
//         [SerializeField]
//         private AudioSource _audioSource;
//
//         [SerializeField]
//         private AudioClip _damageClip;
//
//         [SerializeField]
//         private AudioClip _deathClip;
//
//         public override void Install(IEntity entity)
//         {
//             entity.AddMeshRenderer(_meshRenderer);
//             entity.AddAnimator(_animator);
//             entity.AddBehaviour<TeamColorBehaviour>();
//             entity.AddBehaviour<MoveAnimBehaviour>();
//             entity.AddBehaviour<TakeDamageAnimBehaviour>();
//             entity.AddBehaviour<DeathAnimBehaviour>();
//             entity.AddBehaviour<FireAnimBehaviour>();
//             entity.AddBehaviour(new TakeDamageBloodBehaviour(_bloodVfx));
//
//             entity.GetTakeDamageEvent().Subscribe(_ => _audioSource.PlayOneShot(_damageClip));
//             entity.GetTakeDeathEvent().Subscribe(_ => _audioSource.PlayOneShot(_deathClip));
//         }
//     }
// }