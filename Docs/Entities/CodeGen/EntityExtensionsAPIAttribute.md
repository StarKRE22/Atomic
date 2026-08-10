# 🧩 EntityExtensionsAPIAttribute

Marks a static class as an **Entity API definition** for the Entity API Generator. The generator reads `TagKey<>` and
`ValueKey<>` fields and emits strongly-typed extension methods for entity tags and values.

---

## 📑 Table of Contents

- [Example of Usage](#-example-of-usage)
  - [Unsafe Mode](#unsafe-mode)
- [API Reference](#-api-reference)
  - [Type](#-type)
  - [Constructors](#-constructors)
    - [EntityExtensionsAPIAttribute()](#EntityExtensionsAPIAttribute)
  - [Properties](#-properties)
    - [Unsafe](#unsafe)
    - [AggressiveInlining](#aggressiveinlining)

---

## 🗂 Example of Usage

Define keys in a `public static partial` class:

```csharp
using Atomic.Entities;
using UnityEngine;

[EntityExtensionsAPI]
public static partial class PlayerAPI
{
    public static readonly TagKey<IEntity> Alive = new(nameof(Alive));
    public static readonly TagKey<IEntity> Dead = new(nameof(Dead));

    public static readonly ValueKey<IEntity, int> Health = new(nameof(Health));
    public static readonly ValueKey<IEntity, float> Speed = new(nameof(Speed));
}
```

After compilation, use the generated extension methods:

```csharp
IEntity entity = new Entity();

entity.AddAliveTag();
entity.AddHealth(100);
entity.SetSpeed(5.5f);

int health = entity.GetHealth();
bool isAlive = entity.HasAliveTag();
```

### Unsafe Mode

Enable unsafe accessors for every value field in the class:

```csharp
[EntityExtensionsAPI(Unsafe = true)]
public static partial class PlayerAPI
{
    public static readonly ValueKey<IEntity, int> Health = new(nameof(Health));
    public static readonly ValueKey<IEntity, float> Speed = new(nameof(Speed));
}
```

Generated methods bypass runtime checks:

```csharp
int health = entity.GetHealth();          // calls GetValueUnsafe<int>
ref int healthRef = ref entity.RefHealth(); // direct reference, no validation
```

Mix modes by applying `[Unsafe]` to individual fields:

```csharp
[EntityExtensionsAPI]
public static partial class MixedAPI
{
    public static readonly ValueKey<IEntity, int> Health = new(nameof(Health));      // safe

    [Unsafe]
    public static readonly ValueKey<IEntity, float> Speed = new(nameof(Speed));      // unsafe

    [Unsafe(false)]
    public static readonly ValueKey<IEntity, int> Mana = new(nameof(Mana));          // safe override
}
```

---

## 🔍 API Reference

### 🏛️ Type <div id="-type"></div>

```csharp
[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
public sealed class EntityExtensionsAPIAttribute : Attribute
```

- **Description:** Marks a static class as an Entity API definition for source generation.
- **Inheritance:** `Attribute`
- **Notes:**
  - The target class must be `public static partial`.
  - The generator reads static fields of type [TagKey&lt;E&gt;](../KeyStore/TagKey.md) or
    [ValueKey&lt;E, T&gt;](../KeyStore/ValueKey.md) from the `Atomic.Entities` namespace.
  - Generated methods include `Has{Name}Tag`, `Add{Name}Tag`, `Del{Name}Tag` for tags, and `Get{Name}`, `Set{Name}`,
    `Add{Name}`, `Has{Name}`, `Del{Name}`, `TryGet{Name}` for values.
  - Unsafe mode emits `Ref{Name}` methods that bypass runtime checks.
- **See also:** [UnsafeAttribute](UnsafeAttribute.md), [EntityAPIAnalyzer](EntityAPIAnalyzer.md), [Entity API Source Generation](../Manual.md#-entity-api-source-generation)

---

### 🏗️ Constructors <div id="-constructors"></div>

#### `EntityExtensionsAPIAttribute()`

```csharp
public EntityExtensionsAPIAttribute()
```

- **Description:** Initializes a new instance of the attribute with default settings.
- **Notes:** Default settings are `Unsafe = false` and `AggressiveInlining = true`.

---

### 🔑 Properties

#### `Unsafe`

```csharp
public bool Unsafe { get; set; }
```

- **Description:** Gets or sets whether generated value methods use unsafe direct access.
- **Access:** Read-write
- **Notes:**
  - When `true`, all `ValueKey<>` fields in the class emit `GetValueUnsafe<T>` and `Ref{Name}` methods instead of the
    safe `GetValue<T>` accessor.
  - Tag fields are not affected by this flag.
  - The class-level default can be overridden per field with `[Unsafe]` (force unsafe) or `[Unsafe(false)]` (force safe).
  - Unsafe methods skip runtime existence checks. Calling `Get{Name}` on an entity that does not contain the value, or
    using `Ref{Name}` after the value was removed, can crash or return undefined data.
  - Only enable this flag in performance-critical code paths where the value presence is guaranteed.

#### `AggressiveInlining`

```csharp
public bool AggressiveInlining { get; set; } = true;
```

- **Description:** Gets or sets whether generated methods are decorated with aggressive inlining.
- **Access:** Read-write
- **Notes:**
  - Default value is `true`.
  - Set to `false` for debugging or profiling scenarios.
