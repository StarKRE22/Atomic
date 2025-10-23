# 📌 Using  InlineActions with Entities

[InlineAction](../Elements/Actions/InlineActions.md) is ideal for creating **inline gameplay logic** using **lambda
expressions**. 
It allows developers to define **custom, context-specific behaviors** directly within entity setup — perfect for events,
commands, or reactive systems.

---

## 📑 Table of Contents

- [Example of Usage](#-example-of-usage)
    - [Creating a Weapon with InlineAction](#1-creating-a-weapon-with-inlineaction)
    - [Result](#2-result)
- [Conclusion](#-conclusion)
- [Benefits](#-benefits)

---

## 🗂 Example of Usage

### 1. Creating a Weapon with InlineAction

Below is an example of creating a weapon that shoots bullets, manages ammo, and triggers a cooldown using an inline
action:

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

---

### 2. Result

By defining actions inline, you can **encapsulate gameplay logic directly within the entity’s initialization**, making
it easier to link firing logic, cooldowns, and events without extra classes or boilerplate code.

This approach creates a **concise, readable, and highly maintainable** entity setup.

---

## 🏁 Conclusion

- [InlineAction](../Elements/Actions/InlineAction.md) enables developers to **define behavior inline** using simple
  lambda expressions.
- Inline actions integrate seamlessly with the [Atomic.Entities](../Entities/Manual.md) framework, allowing quick
  composition of gameplay logic.
- They are especially useful for **lightweight event-driven systems**, such as shooting, item use, or cooldown triggers.
- This pattern promotes **rapid prototyping**, reduces complexity, and keeps related logic close to the entities
  themselves.
- Combined with events and expressions, `InlineAction` becomes a **core building block** for dynamic gameplay systems.

---

## ✅ Benefits

- Enables **concise, inline gameplay logic** using C# lambda expressions.
- Reduces **boilerplate** by avoiding extra classes for simple behaviors.
- Keeps **logic modular and maintainable**, directly tied to entity setup.
- Perfect for **event reactions**, **conditional actions**, or **cooldown handling**.
- Encourages **rapid iteration** and clearer gameplay flow definitions.

<!--

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

-->