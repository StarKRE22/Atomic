
# 📌 Using Optional with Entity Installers

`Optional<T>` can be used to define optional settings in Unity components or installers, allowing for flexible
configuration. Below is an example of configuring optional parameters for an entity in `Atomic.Entities` using the `Optional<T>`

### Example #1: Optional State

If your weapon has optional components, you can connect them using the `Optional<T>` field. This allows you to **enable
or disable features dynamically** without changing the core logic of the weapon.

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

### Example #2: Optional Visualization

Also, if your weapon has optional visualization, you can connect them using the `Optional<T>` field. This allows you to
**enable or disable features in Inspector** without changing the core logic of the weapon.

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

### Example #3: Override entity installing

If you have an installer that configures enemies and is used as a **Shared Entity Installer**, it might look like this:

```csharp
//Scriptable Object Installer for many enemies
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

Later, in the scene, if you need to **override the default enemy configuration**, you can use `Optional<T>` fields:

```csharp
//MonoBehaviour Installer for a specific enemy
public sealed class OverrideEnemyInstaller : SceneEntityInstaller<IEnemyEntity>
{
    [SerializeField] private Optional<ReactiveInt> _health;
    [SerializeField] private Optional<ReactiveInt> _damage;
    [SerializeField] private Optional<ReactiveFloat> _speed;

    public void Install(IWeaponEntity entity)
    {
        //Override health if active
        if (_health)
            entity.SetHealth(_health);
        
        //Override damage if active
        if (_damage)
            entity.SetDamage(_damage);
        
        //Override speed if active
        if (_speed)
            entity.SetSpeed(_speed);
    }
}
```
