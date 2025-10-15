# 📌 Using Inline Actions with Entities

**InlineAction** is ideal for creating actions for specific game objects using **lambda expressions**, making it easy to
define custom behavior inline for events, commands, or reactive systems.

---

## 🗂 Example of Usage

Below is an example of creating a weapon that shoots bullets, manages ammo, and triggers a cooldown using
inline action:

```csharp
public sealed class WeaponInstaller : SceneEntityInstaller<IWeapon>
{
    [SerializeField] private GameEntity _owner;
    [SerializeField] private Transform _firePoint;
    [SerializeField] private ReactiveVariable<int> _ammo = 100;
    [SerializeField] private Cooldown _cooldown = 0.5f;

    public override void Install(IWeapon weapon)
    {
        weapon.AddFireEvent(new BaseEvent());
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
    }
}
```