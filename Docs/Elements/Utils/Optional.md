# 🧩 Optional&lt;T&gt;

```csharp
[Serializable]
public struct Optional<T>
```

- **Description:**  Represents a **Unity-friendly optional value** that can be serialized and displayed in the
  Inspector. It supports activation state, implicit conversions, and safe value access.
- **Type Parameter:** `T` – The type of the value to store.
- **Notes:** Support Unity serialization and Odin Inspector

---

## 🔑 Properties

#### `Value`

```csharp
public T Value { get; set; }
```

- **Description:** Accesses the underlying value. Assigning a value automatically sets the optional as active (if not
  `null`).
- **Remarks:** Use this property to modify or read the contained value safely.

#### `Active`

```csharp
public bool Active { get; set; }
```

- **Description:** Shows whether the optional contains a valid value.
- **Remarks:** You can manually activate or deactivate the optional.

---

## 🏹 Methods

#### `TryGetValue(out T)`

```csharp
public bool TryGetValue(out T value)
```

- **Description:** Attempts to retrieve the value.
- **Parameter:** `value` — Output parameter that will hold the optional's value if active.
- **Returns:** `true` if the optional is active; otherwise `false`.

#### `GetValueOrDefault(T)`

```csharp
public T GetValueOrDefault(T defaultValue)
```

- **Description:** Retrieves the value if active; otherwise returns a provided default value.
- **Parameter:** `defaultValue` — The fallback value to return if the optional is inactive.
- **Returns:** The active value or the provided default.

---

## 🪄 Operators

#### `operator Optional<T>(T)`

```csharp
public static implicit operator Optional<T>(T it)
```

- **Description:** Automatically wraps a value of type `T` in an active optional.
- **Remarks:** Useful for clean initialization and assignment.

#### `operator T(Optional<T>)`

```csharp
public static implicit operator T(Optional<T> it)
```

- **Description:** Extracts the underlying value from the optional.
- **Remarks:** Does not check for `Active`; ensure optional is active before using.

#### `operator true(Optional<T>)`

```csharp
public static bool operator true(Optional<T> it)
```

- **Description:** Returns `true` if the optional is active.

#### `operator false(Optional<T>)`

```csharp
public static bool operator false(Optional<T> it)
```

- **Description:** Returns `false` if the optional is not active.

---

## 🗂 Example of Usage

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
            entity.AddCooldown(_ammo);
        
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

---

## 📝 Notes

- Serializable and Unity Inspector friendly.
- Represents optional settings without relying on null references.
- Supports implicit conversion to and from the underlying type.
- Provides safe access methods and operators to check for presence.