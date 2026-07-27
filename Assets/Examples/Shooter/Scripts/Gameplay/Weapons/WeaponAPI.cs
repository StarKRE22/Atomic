using Atomic.Elements;
using Atomic.Entities;

namespace ShooterGame.Gameplay
{
    [EntityAPI]
    public static partial class WeaponAPI
    {
        public static readonly ValueKey<IWeapon, IAction> FireAction = new(nameof(FireAction));
        public static readonly ValueKey<IWeapon, IEvent> FireEvent = new(nameof(FireEvent));
        public static readonly ValueKey<IWeapon, IReactiveVariable<int>> Ammo = new(nameof(Ammo));
        public static readonly ValueKey<IWeapon, Cooldown> Cooldown = new(nameof(Cooldown));
    }
}
