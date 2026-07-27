# 🧬 EntityAPIAttribute

Marks a static class as an **Entity API definition** for the Entity API Generator. The generator reads `TagKey<>` and
`ValueKey<>` fields and emits strongly-typed extension methods for entity tags and values.

---

## 📑 Table of Contents

- [Example of Usage](#-example-of-usage)
- [API Reference](#-api-reference)
  - [Type](#-type)
  - [Constructors](#-constructors)
    - [EntityAPIAttribute()](#entityapiattribute)
  - [Properties](#-properties)
    - [Unsafe](#unsafe)
    - [AggressiveInlining](#aggressiveinlining)

---

## 🗂 Example of Usage

Define keys in a `public static partial` class:

```csharp
using Atomic.Entities;
using UnityEngine;

[EntityAPI]
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

---

## 🔍 API Reference

### 🏛️ Type <div id="-type"></div>

```csharp
[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
public sealed class EntityAPIAttribute : Attribute
```

- **Description:** Marks a static class as an Entity API definition for source generation.
- **Inheritance:** `Attribute`
- **Notes:**
  - The target class must be `public static partial`.
  - The generator reads static fields of type [TagKey&lt;E&gt;](../../Entities/KeyStore/TagKey.md) or
    [ValueKey&lt;E, T&gt;](../../Entities/KeyStore/ValueKey.md) from the `Atomic.Entities` namespace.
  - Generated methods include `Has{Name}Tag`, `Add{Name}Tag`, `Del{Name}Tag` for tags, and `Get{Name}`, `Set{Name}`,
    `Add{Name}`, `Has{Name}`, `Del{Name}`, `TryGet{Name}` for values.
  - Unsafe mode emits `Ref{Name}` methods that bypass runtime checks.
- **See also:** [EntityAPIAnalyzer](EntityAPIAnalyzer.md), [Setup](../Setup.md), [Code Generation Manual](../Manual.md)

---

### 🏗️ Constructors <div id="-constructors"></div>

#### `EntityAPIAttribute()`

```csharp
public EntityAPIAttribute()
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
  - When `true`, all value fields generate `GetValueUnsafe<T>` and `Ref{Name}` methods instead of `GetValue<T>`.
  - Individual fields can override this with `[Unsafe]`.
  - Unsafe methods can crash or return undefined data if the value is not present.

#### `AggressiveInlining`

```csharp
public bool AggressiveInlining { get; set; } = true;
```

- **Description:** Gets or sets whether generated methods are decorated with aggressive inlining.
- **Access:** Read-write
- **Notes:**
  - Default value is `true`.
  - Set to `false` for debugging or profiling scenarios.
