using Atomic.Elements;
using Atomic.Entities;
using UnityEngine;
using Event = Atomic.Elements.Event;

namespace ShooterGame.Gameplay
{
    public sealed class ProjectileWeaponInstaller : WeaponInstaller
    {
        [SerializeField]
        private GameEntity _owner;

        [SerializeField]
        private Transform _firePoint;

        [SerializeField]
        private ReactiveVariable<int> _ammo = 100;

        [SerializeField]
        private Cooldown _cooldown = 0.5f;

        public override void Install(IWeapon weapon)
        {
            weapon.AddFireEvent(new Event());
            weapon.AddFireAction(new InlineAction(() =>
            {
                if (_ammo.Value <= 0 || !_cooldown.IsCompleted())
                    return;

                _ammo.Value--;
                
                BulletUseCase.Spawn(
                    GameContext.Instance,
                    _firePoint.position,
                    _firePoint.rotation,
                    _owner.GetTeamType().Value
                );
                
                _cooldown.ResetTime();
                weapon.GetFireEvent().Invoke();
            }));
            weapon.WhenFixedTick(_cooldown.Tick);
        }
    }
}