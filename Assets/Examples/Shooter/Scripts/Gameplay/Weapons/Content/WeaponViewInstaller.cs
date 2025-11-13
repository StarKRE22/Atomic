using Atomic.Elements;
using UnityEngine;

namespace ShooterGame.Gameplay
{
    public sealed class WeaponViewInstaller : WeaponInstaller
    {
        [SerializeField]
        private ParticleSystem _particleSystem;

        [SerializeField]
        private AudioSource _audioSource;

        private readonly DisposableComposite _disposables = new();

        public override void Install(IWeapon weapon)
        {
            weapon.GetFireEvent().Subscribe(_particleSystem.Play).AddTo(_disposables);
            weapon.GetFireEvent().Subscribe(_audioSource.Play).AddTo(_disposables);
        }

        public override void Uninstall(IWeapon weapon)
        {
            _disposables.Dispose();
        }
    }
}