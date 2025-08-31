# 🧩 Optional<T>

`Optional<T>` is a Unity-friendly wrapper for a value that may or may not be present.  
It was specifically designed to handle optional settings in Unity components, making it easy to define configurable fields that can be enabled or disabled individually.  
It is serializable, visible in the Inspector, and works seamlessly with Unity's serialization system.

---

## Features

- Serializable and Unity Inspector friendly.
- Represents optional settings without relying on null references.
- Supports implicit conversion to and from the underlying type.
- Provides safe access methods and operators to check for presence.

---

## Properties

### `T Value { get; set; }`

Gets or sets the contained value.  
Setting the value automatically activates the optional.

### `bool Active { get; set; }`

Indicates whether the optional is currently active (has a value).

---

## Methods

### `bool TryGetValue(out T value)`

Attempts to get the contained value.

- **Returns:** `true` if active, otherwise `false`.
- **Output:** The current value if active; default if inactive.

### `T GetValueOrDefault(T defaultValue)`

Returns the value if active, otherwise returns the provided `defaultValue`.

---

## Operators

- `implicit operator Optional<T>(T it)` – Converts a value to an `Optional<T>`, activating it if non-null.
- `implicit operator T(Optional<T> it)` – Converts an `Optional<T>` to its contained value.
- `operator true(Optional<T> it)` – Returns `true` if the optional is active.
- `operator false(Optional<T> it)` – Returns `true` if the optional is inactive.

---

## Example Usage

`Optional<T>` can be used to define optional settings in Unity components or installers, allowing for flexible configuration.

```csharp
using UnityEngine;
using Atomic.Elements;

[Serializable]
public sealed class CombatInstaller : IEntityInstaller<IGameEntity>
{
    [SerializeField]
    private Optional<float> _fireCooldown = 1;

    [SerializeField]
    private Optional<float> _fireDistance = 5;

    public void Install(IGameEntity entity)
    {
        if (_fireCooldown) entity.GetFireCooldown().SetDuration(_fireCooldown);
        if (_fireDistance) entity.SetFireDistance(new Const<float>(_fireDistance));
    }
}
```
- `_fireCooldown` and `_fireDistance` are optional settings.
- They can be activated or deactivated individually in the Inspector.
- Simplifies component configuration while keeping fields serialized and visible.