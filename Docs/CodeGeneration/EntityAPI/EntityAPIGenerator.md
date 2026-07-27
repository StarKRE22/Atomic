# 🧩 Entity API Generator

The **Entity API Generator** is a Roslyn incremental source generator that reads a static class marked with `[EntityAPI]`
and emits strongly-typed extension methods for entity tags and values. It removes magic constants and gives you IDE
autocomplete for entity operations.

---

## 📑 Table of Contents

- [Overview](#-overview)
- [Requirements](#-requirements)
- [Setup](#-setup)
- [Declaring an API Class](#-declaring-an-api-class)
  - [Supported Key Types](#-supported-key-types)
  - [Entity Type Resolution](#-entity-type-resolution)
- [Generated Methods](#-generated-methods)
- [Unsafe Mode](#-unsafe-mode)
- [Aggressive Inlining](#-aggressive-inlining)
- [Analyzer](#-analyzer)
- [Examples of Usage](#-examples-of-usage)
- [Troubleshooting](#-troubleshooting)

---

## 🧩 Overview

Write the keys as static fields:

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
    public static readonly ValueKey<IPlayerContext, Camera> Camera = new(nameof(Camera));
}
```

After the first compilation, the following methods become available automatically:

```csharp
IEntity entity = new Entity();

entity.AddAliveTag();
int health = entity.GetHealth();
entity.SetSpeed(5.5f);
```

---

## 📝 Requirements

- Unity 6 (6000.0 LTS or newer)
- The project must reference the **Atomic.Entities** runtime assembly
- The `EntityAPIGenerator.dll` must be loaded as a Roslyn analyzer

See [Setup.md](../Setup.md) for the shared setup instructions.

---

## ⚙️ Setup

See [Setup.md](../Setup.md) for how to add the generator DLL to your Unity project.

The generator is deployed at:

```
Assets/Plugins/Atomic/SourceGenerators/EntityAPIGenerator.dll
```

---

## 🗂 Declaring an API Class

An API class must be:

- `public static partial class`
- Decorated with `[EntityAPI]`
- Contain static fields of supported key types

```csharp
[EntityAPI]
public static partial class PlayerAPI
{
    public static readonly TagKey<IEntity> Alive = new(nameof(Alive));
    public static readonly ValueKey<IEntity, int> Health = new(nameof(Health));
}
```

Every key field must be initialized with a non-default constructor. The analyzer reports `new()` or `default` as an error.

---

## 🔍 Supported Key Types

| Declaration | Namespace | Entity Type | Generated As |
|-------------|-----------|-------------|--------------|
| `TagKey<E> Name` | `Atomic.Entities` | `E` | Tag methods |
| `TagKey Name` | `Atomic.Entities` | `IEntity` | Tag methods |
| `ValueKey<E, T> Name` | `Atomic.Entities` | `E` | Value methods of type `T` |
| `ValueKey<T> Name` | `Atomic.Entities` | `IEntity` | Value methods of type `T` |

> ⚠️ Only `ValueKey<>` and `TagKey<>` from the `Atomic.Entities` namespace are recognized. Plain types and the legacy `Tag` struct are not supported.

---

## 🔍 Entity Type Resolution

The generator reads the entity type from the key's generic arguments:

- `TagKey<E>` and `ValueKey<E, T>` extend `E`
- `TagKey` and `ValueKey<T>` extend `IEntity`

This lets a single API class target different entity interfaces if needed:

```csharp
[EntityAPI]
public static partial class MixedAPI
{
    public static readonly ValueKey<IEntity, int> Health = new(nameof(Health));       // extends IEntity
    public static readonly ValueKey<IPlayer, Weapon> Weapon = new(nameof(Weapon));  // extends IPlayer
}
```

---

## 🔍 Generated Methods

### Tags

For each `TagKey` field the generator creates:

| Method | Description |
|--------|-------------|
| `bool Has{Name}Tag(this E entity)` | Returns `true` if the tag is present. |
| `bool Add{Name}Tag(this E entity)` | Adds the tag. |
| `bool Del{Name}Tag(this E entity)` | Removes the tag. |

### Values

For each `ValueKey` field the generator creates:

| Method | Description |
|--------|-------------|
| `T Get{Name}(this E entity)` | Returns the stored value. |
| `bool TryGet{Name}(this E entity, out T value)` | Safely retrieves the value. |
| `void Add{Name}(this E entity, T value)` | Adds the value to the entity. |
| `bool Has{Name}(this E entity)` | Returns `true` if the value exists. |
| `bool Del{Name}(this E entity)` | Removes the value. |
| `void Set{Name}(this E entity, T value)` | Sets the value. |

In **unsafe mode**, the generator also emits:

| Method | Description |
|--------|-------------|
| `ref T Ref{Name}(this E entity)` | Returns a direct reference to the value. |

---

## 🔥 Unsafe Mode

Unsafe mode removes runtime checks and uses `GetValueUnsafe<T>` for maximum performance.

Enable it for the whole class:

```csharp
[EntityAPI(Unsafe = true)]
public static partial class PlayerAPI
{
    public static readonly ValueKey<IEntity, int> Health = new(nameof(Health));
    public static readonly ValueKey<IEntity, float> Speed = new(nameof(Speed));
}
```

When unsafe mode is off, you can still mark individual fields as unsafe by adding `[Unsafe]`:

```csharp
[EntityAPI]
public static partial class PlayerAPI
{
    public static readonly ValueKey<IEntity, int> Health = new(nameof(Health)); // safe

    [Unsafe]
    public static readonly ValueKey<IEntity, float> Speed = new(nameof(Speed)); // unsafe
}
```

> ⚠️ `[Unsafe]` is recognized by its presence. If the class-level `Unsafe` flag is `true`, every value field is generated as unsafe.
> ⚠️ Unsafe methods can crash or return undefined data if the value is not present. Only use them in verified, hot paths.

---

## ⚡ Aggressive Inlining

By default, every generated method is decorated with `[MethodImpl(MethodImplOptions.AggressiveInlining)]`.
Disable it per class for debugging or profiling:

```csharp
[EntityAPI(AggressiveInlining = false)]
public static partial class DebugPlayerAPI
{
    public static readonly ValueKey<IEntity, int> Health = new(nameof(Health));
}
```

---

## 🔬 Analyzer

Deploy the [Entity API Analyzer](EntityAPIAnalyzer.md) alongside the generator. It reports two errors:

| Rule | Description |
|------|-------------|
| `EAPI0001` | Key field has no initializer. |
| `EAPI0002` | Key field is initialized with `new()` or `default`. |

Both diagnostics come with a code fix that inserts `= new(nameof(FieldName))`.

---

## 🗂 Examples of Usage

### Definition

```csharp
using Atomic.Entities;
using UnityEngine;

[EntityAPI]
public static partial class PlayerAPI
{
    public static readonly TagKey<IEntity> Alive = new(nameof(Alive));
    public static readonly ValueKey<IEntity, int> Health = new(nameof(Health));
    public static readonly ValueKey<IEntity, float> Speed = new(nameof(Speed));
}
```

### Usage

```csharp
public sealed class PlayerSpawnUseCase : IEntityInit
{
    public void Init(IEntity entity)
    {
        entity.AddAliveTag();
        entity.AddHealth(100);
        entity.AddSpeed(5.0f);
    }
}
```

### System that reads the value

```csharp
public sealed class PlayerDeathSystem : IEntityTick
{
    public void Tick(IEntity entity, float deltaTime)
    {
        if (entity.GetHealth() <= 0)
        {
            entity.DelAliveTag();
            entity.AddDeadTag();
        }
    }
}
```

---

## 🔧 Troubleshooting

### Generated methods do not appear

1. Confirm `EntityAPIGenerator.dll` is in `Assets/Plugins/Atomic/SourceGenerators/`.
2. Confirm the DLL has the `RoslynAnalyzer` asset label.
3. Confirm all platforms are unchecked in the DLL import settings.
4. Restart Unity or run `Assets → Reimport All`.

### Build errors about missing initializers

Every field must be initialized with a non-default constructor, e.g.:

```csharp
public static readonly ValueKey<IEntity, int> Health = new(nameof(Health));
```

`new()` and `default` are reported as `EAPI0002`.

### Generated file is not written to disk

The generator works in-memory. To dump generated files, add the scripting define symbol:

```
ATOMIC_OUTPUT_SOURCEGEN_FILES
```

and look in `Temp/GeneratedCode/`.

---

## 📦 Source Repository

The generator source code is available at:

**https://github.com/dre0dru/Atomic.SourceGenerators**
