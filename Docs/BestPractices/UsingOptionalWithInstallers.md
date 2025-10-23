# 📌 Using Optional with EntityInstallers

[Optional\<T>](../Elements/Utils/Optional.md) can be used to define **optional settings** in Unity components or installers, allowing for **flexible entity configuration**. 
This approach makes it easy to **enable or disable features dynamically** without changing the core logic of your entities.

---

## 📑 Table of Contents

- [Examples of Usage](#-examples-of-usage)
    - [Optional State](#1-optional-state)
    - [Optional Visualization](#2-optional-visualization)
    - [Override Entity Installing](#3-override-entity-installing)
- [Conclusion](#-conclusion)
- [Benefits](#-benefits)

---

## 🗂 Examples of Usage

### 1. Optional State

For weapons with optional components, you can connect them using [Optional\<T>](../Elements/Utils/Optional.md) fields:

```csharp
public sealed class WeaponInstaller : SceneEntityInstaller<IWeaponEntity>
{
    [SerializeField] private Optional<ReactiveInt> _ammo;
    [SerializeField] private Optional<Cooldown> _cooldown;

    public void Install(IWeaponEntity entity)
    {
        // Add ammo to weapon if active
        if (_ammo)
            entity.AddAmmo(_ammo);
        
        // Add cooldown to weapon if active
        if (_cooldown)
            entity.AddCooldown(_cooldown);
        
        entity.AddFireRequest(new BaseRequest());
        entity.AddFireEvent(new BaseEvent());
        entity.AddBehaviour<FireProjectileBehaviour>();
    }
}
```

> [!TIP]
> Using `Optional<T>` allows you to **enable or disable features dynamically** without modifying core entity logic.

---

### 2. Optional Visualization

Optional fields can also be used for **visual and audio components**:

```csharp
public sealed class WeaponViewInstaller : SceneEntityInstaller
{
    [SerializeField] private Optional<ParticleSystem> fireVFX;
    [SerializeField] private Optional<AudioSource> fireSFX;
    [SerializeField] private Optional<Animator> animator;

    public void Install(IEntity entity)
    {
        ISignal fireEvent = entity.GetFireEvent();

        // Play VFX if active
        if (fireVFX)
            fireEvent.Subscribe(fireVFX.Value.Play);

        // Play SFX if active
        if (fireSFX)
            fireEvent.Subscribe(fireSFX.Value.Play);

        // Trigger animation if active
        if (animator)
            fireEvent.Subscribe(() => animator.Value.SetTrigger("Fire"));
    }
}
```

> [!TIP] 
> Optional fields let you **configure features in Inspector** without touching entity logic.

---

### 3. Override Entity Installing

For shared installers, you can override default configurations using `Optional<T>`:

```csharp
// Scriptable Object Installer for many enemies
public sealed class EnemyInstaller : ScriptableEntityInstaller<IEnemyEntity>
{
    [SerializeField] private ReactiveInt _health;
    [SerializeField] private ReactiveInt _damage;
    [SerializeField] private ReactiveFloat _speed;

    public void Install(IWeaponEntity entity)
    {
        entity.AddHealth(_health);
        entity.AddDamage(_damage);
        entity.AddSpeed(_speed);
    }
}
```

```csharp
// MonoBehaviour Installer for a specific enemy
public sealed class OverrideEnemyInstaller : SceneEntityInstaller<IEnemyEntity>
{
    [SerializeField] private Optional<ReactiveInt> _health;
    [SerializeField] private Optional<ReactiveInt> _damage;
    [SerializeField] private Optional<ReactiveFloat> _speed;

    public void Install(IWeaponEntity entity)
    {
        // Override health if active
        if (_health)
            entity.SetHealth(_health);
        
        // Override damage if active
        if (_damage)
            entity.SetDamage(_damage);
        
        // Override speed if active
        if (_speed)
            entity.SetSpeed(_speed);
    }
}
```

---

## 🏁 Conclusion

- [Optional\<T>](../Elements/Utils/Optional.md) enables **flexible, conditional configuration** of entities in Unity installers.
- Optional fields allow you to **enable or disable features dynamically** without modifying the core logic.
- Supports both **scriptable and scene-based installers**, making it ideal for shared and specific entity setups.
- Helps maintain **clean, modular, and maintainable entity configurations**.
- Integrates seamlessly with [Atomic.Entities](../Entities/Manual.md), supporting dynamic gameplay behaviors.

---

## ✅ Benefits

- Provides **dynamic configuration** without changing core logic.
- Allows **enabling/disabling features in Inspector** easily.
- Supports **shared and override installers** for flexible scene setup.
- Improves **maintainability** and reduces boilerplate code.
- Encourages **modular design**, keeping optional components isolated and reusable.  
