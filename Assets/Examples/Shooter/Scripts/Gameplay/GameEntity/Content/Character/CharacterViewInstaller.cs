using Atomic.Elements;
using Atomic.Entities;
using UnityEngine;
using UnityEngine.Serialization;

namespace ShooterGame.Gameplay
{
    public sealed class CharacterViewInstaller : SceneEntityInstaller<IGameEntity>
    {
        [SerializeField]
        private Renderer _renderer;

        [SerializeField]
        private Animator _animator;

        [FormerlySerializedAs("_view")]
        [SerializeField]
        private GameObject _viewGO;

        [SerializeField]
        private ParticleSystem _bloodVfx;

        [SerializeField]
        private AudioSource _audioSource;

        [SerializeField]
        private AudioClip _damageClip;

        [SerializeField]
        private AudioClip _deathClip;

        [SerializeField]
        private Transform _canvas;

        [SerializeField]
        private HitPointsView _hitPointsView;

        public override void Install(IGameEntity entity)
        {
            entity.AddRenderer(_renderer);
            entity.AddAnimator(_animator);

            entity.AddBehaviour<TeamColorBehaviour>();
            entity.AddBehaviour(new MoveAnimBehaviour());
            entity.AddBehaviour<TakeDamageAnimBehaviour>();
            entity.AddBehaviour(new DeathAnimBehaviour(_viewGO));
            entity.AddBehaviour<FireAnimBehaviour>();
            entity.AddBehaviour(new TakeDamageBloodBehaviour(_bloodVfx));
            entity.AddBehaviour(new CameraBillboardBehaviour(_canvas));

            entity.AddHitPointsView(_hitPointsView);
            entity.AddBehaviour<HitPointsPresenter>();
            
            entity.GetTakeDamageEvent().Subscribe(_ => _audioSource.PlayOneShot(_damageClip));
            entity.GetTakeDeathEvent().Subscribe(_ => _audioSource.PlayOneShot(_deathClip));
            entity.GetRespawnEvent().Subscribe(() => _viewGO.SetActive(true));
        }
    }
}