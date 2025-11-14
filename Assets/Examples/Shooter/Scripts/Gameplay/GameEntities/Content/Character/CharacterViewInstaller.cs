using Atomic.Elements;
using Atomic.Entities;
using UnityEngine;
using UnityEngine.Serialization;

namespace ShooterGame.Gameplay
{
    public sealed class CharacterViewInstaller : GameEntityInstaller
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

        private readonly DisposableComposite _disposables = new();

        public override void Install(IGameEntity entity)
        {
            GameContext.TryGetInstance(out GameContext gameContext);
            
            entity.AddRenderer(_renderer);
            entity.AddAnimator(_animator);

            entity.AddBehaviour(new TeamColorBehaviour(gameContext));
            entity.AddBehaviour(new MoveAnimBehaviour());
            entity.AddBehaviour<TakeDamageAnimBehaviour>();
            entity.AddBehaviour(new DeathAnimBehaviour(_viewGO));
            entity.AddBehaviour<FireAnimBehaviour>();
            entity.AddBehaviour(new TakeDamageBloodBehaviour(gameContext, _bloodVfx));
            entity.AddBehaviour(new CameraBillboardBehaviour(gameContext, _canvas));

            entity.AddHitPointsView(_hitPointsView);
            entity.AddBehaviour<HitPointsPresenter>();
            
            entity.GetTakeDamageEvent().Subscribe(_ => _audioSource.PlayOneShot(_damageClip)).AddTo(_disposables);
            entity.GetTakeDeathEvent().Subscribe(_ => _audioSource.PlayOneShot(_deathClip)).AddTo(_disposables);
            entity.GetRespawnEvent().Subscribe(() => _viewGO.SetActive(true)).AddTo(_disposables);
        }

        public override void Uninstall(IGameEntity entity)
        {
            _disposables.Dispose();
        }
    }
}