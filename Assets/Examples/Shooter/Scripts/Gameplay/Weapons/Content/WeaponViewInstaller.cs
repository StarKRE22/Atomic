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

        public override void Install(IWeapon weapon)
        {
            weapon.GetFireEvent().Subscribe(_particleSystem.Play);
            weapon.GetFireEvent().Subscribe(_audioSource.Play);
        }
    }
}