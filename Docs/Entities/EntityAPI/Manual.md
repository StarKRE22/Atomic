# 🧩 Entity API

The **Entity API** module provides type-safe extension methods for working with entity tags and values. Instead of using
magic constants or string keys, you declare `ValueKey<>` and `TagKey<>` fields in a static partial class and the
[Entity API Generator](../../CodeGeneration/EntityAPI/EntityAPIGenerator.md) produces extension methods automatically.

---

## 📑 Table of Contents

- [The Problem](#the-problem)
- [How It Works](#how-it-works)
- [Declaring Keys](#declaring-keys)
- [Using Generated Extensions](#using-generated-extensions)
- [Configuration](#configuration)
- [Analyzer](#analyzer)
- [Setup](#setup)
- [See Also](#see-also)

---

## The Problem

Without code generation, entity tags and values are accessed by integer ids or strings:

```csharp
const int PlayerTag = 1;
const int Health = 1;

entity.AddTag(PlayerTag);
entity.AddValue(Health, 100);
int health = entity.GetValue<int>(Health);
```

This is error-prone, hard to refactor, and difficult to validate at compile time.

---

## How It Works

Add the `[EntityAPI]` attribute to a static partial class and declare key fields:

```csharp
using Atomic.Entities;

[EntityAPI]
public static partial class PlayerAPI
{
    public static readonly TagKey<IEntity> Alive = new(nameof(Alive));
    public static readonly ValueKey<IEntity, int> Health = new(nameof(Health));
    public static readonly ValueKey<IEntity, float> Speed = new(nameof(Speed));
}
```

After compilation, the generator adds extension methods such as:

```csharp
entity.AddAliveTag();
entity.AddHealth(100);
int health = entity.GetHealth();
entity.SetSpeed(5.5f);
```

The generated class has the same name and namespace as the declared class. You do not edit it.

---

## Declaring Keys

Supported field types:

| Type | Generated As |
|------|--------------|
| `TagKey<E>` | Tag methods extending `E` |
| `TagKey` | Tag methods extending `IEntity` |
| `ValueKey<E, T>` | Value methods of type `T` extending `E` |
| `ValueKey<T>` | Value methods of type `T` extending `IEntity` |

Every field must be initialized with a non-default constructor, for example `new(nameof(FieldName))`.

---

## Using Generated Extensions

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

## Configuration

The `[EntityAPI]` attribute supports two properties:

| Property | Default | Description |
|----------|---------|-------------|
| `Unsafe` | `false` | Generate unsafe value accessors and `Ref{Name}` methods. |
| `AggressiveInlining` | `true` | Add `[MethodImpl(MethodImplOptions.AggressiveInlining)]` to every method. |

Apply `[Unsafe]` to individual value fields to force unsafe accessors for those fields when the class-level `Unsafe`
flag is `false`.

For full details, see the generator documentation.

---

## 🔬 Analyzer

The [Entity API Analyzer](../../CodeGeneration/EntityAPI/EntityAPIAnalyzer.md) validates key initializers:

| Rule | Description |
|------|-------------|
| `EAPI0001` | Key field has no initializer. |
| `EAPI0002` | Key field is initialized with `new()` or `default`. |

Both diagnostics include a code fix that inserts `= new(nameof(FieldName))`.

---

## ⚙️ Setup

See [Code Generation Setup](../../CodeGeneration/Setup.md) for how to add the generator and analyzer DLLs to a Unity
project.

The source code for the generators is available at:

**https://github.com/dre0dru/Atomic.SourceGenerators**

---

## See Also

- [Entity API Generator](../../CodeGeneration/EntityAPI/EntityAPIGenerator.md)
- [Entity API Analyzer](../../CodeGeneration/EntityAPI/EntityAPIAnalyzer.md)
- [Code Generation Setup](../../CodeGeneration/Setup.md)
- [TagKey](../../Entities/KeyStore/TagKey.md)
- [ValueKey](../../Entities/KeyStore/ValueKey.md)
