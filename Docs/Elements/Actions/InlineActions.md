# 🧩 InlineActions

The **InlineAction** classes provide wrappers around standard `System.Action` delegates.
They implement the corresponding [IAction](IActions.md) interfaces and allow invoking actions directly, optionally with
parameters.

There are several implementations of inline actions, depending on the number of arguments the actions take:


- [InlineAction](InlineAction.md) — Non-generic version; works without parameters.
- [InlineAction&lt;T&gt;](InlineAction%601.md) — Inline action that takes one argument.
- [InlineAction&lt;T1, T2&gt;](InlineAction%602.md) — Inline action that takes two arguments.
- [InlineAction&lt;T1, T2, T3&gt;](InlineAction%603.md) — Inline action that takes three arguments.
- [InlineAction&lt;T1, T2, T3, T4&gt;](InlineAction%604.md) — Inline action that takes four arguments.

---

## 📌 Best Practice

**InlineAction** is ideal for creating actions for specific game objects using **lambda expressions**, making it easy to
define custom behavior inline for events, commands, or reactive systems.

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