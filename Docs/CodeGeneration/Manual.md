# 🧬 Code Generation

**Atomic.CodeGeneration** provides Roslyn source generators and analyzers that turn declarative API classes into
strongly-typed extension methods. Instead of maintaining `.yaml` files, `.atomic` configs, or relying on an IDE plugin,
you declare keys as static fields in a partial class and the compiler generates the rest automatically.

All generators are **compile-time only**. They do not add runtime overhead and do not ship in player builds.

---

## 📑 Table of Contents

- [Examples of Usage](#-examples-of-usage)
  - [Entity API](#entity-api)
  - [Event API](#event-api)
- [API Reference](#-api-reference)
  - [Generators](#generators)
  - [Analyzers](#analyzers)
  - [Setup](#setup)
- [Best Practices](#-best-practices)
- [Source Repository](#-source-repository)

---

## 🗂 Examples of Usage

### Entity API

Declare a static partial class, mark it with `[EntityAPI]`, and add `TagKey<>` and `ValueKey<,>` fields:

```csharp
using Atomic.Entities;

[EntityAPI]
public static partial class PlayerAPI
{
    public static readonly TagKey<IEntity> Alive = new(nameof(Alive));
    public static readonly ValueKey<IEntity, int> Health = new(nameof(Health));
}
```

Generated usage:

```csharp
IEntity entity = new Entity();

entity.AddAliveTag();
entity.AddHealth(100);
int health = entity.GetHealth();
```

### Event API

Declare a static partial class, mark it with `[EventAPI]`, and add `EventKey<>` fields:

```csharp
using Atomic.Events;

[EventAPI]
public static partial class GameEventAPI
{
    public static readonly EventKey<IEventBus> PlayerTurnStarted = new(nameof(PlayerTurnStarted));
    public static readonly EventKey<IEventBus, int> DamageDealt = new(nameof(DamageDealt));
}
```

Generated usage:

```csharp
IEventBus bus = new EventBus();

bus.InvokePlayerTurnStarted();
bus.InvokeDamageDealt(10);

using var subscription = bus.SubscribeDamageDealt(amount => Debug.Log($"Damage: {amount}"));
```

---

## 🔍 API Reference

### Generators

| Generator | Purpose | Marker Attribute |
|-----------|---------|------------------|
| [Entity API Generator](EntityAPI/EntityAPIGenerator.md) | Generate entity tag/value extension methods | `[EntityAPI]` |
| [Event API Generator](EventAPI/EventAPIGenerator.md) | Generate event-bus extension methods | `[EventAPI]` |

### Analyzers

Analyzers ship with code fixes and report build errors when a key field is missing an initializer or is initialized with
`new()` / `default`:

| Analyzer | Description |
|----------|-------------|
| [Entity API Analyzer](EntityAPI/EntityAPIAnalyzer.md) | Validates `[EntityAPI]` key initializers. |
| [Event API Analyzer](EventAPI/EventAPIAnalyzer.md) | Validates `[EventAPI]` key initializers. |

### Setup

- [Setup.md](Setup.md) — a single, shared guide on how to add the generator/analyzer DLLs to a Unity project.

---

## 📌 Best Practices

- Keep API classes `static`, `partial`, and marked with `[EntityAPI]` or `[EventAPI]`.
- Initialize every key field; analyzers report uninitialized keys as build errors.
- Define keys once and reuse them across behaviours, systems, UI, and installers.
- Prefer source-generated extension methods over hand-written ones.
- Use `[EntityAPI(Unsafe = true)]` only when unsafe direct value access is required.

---

## 📦 Source Repository

The generator and analyzer source code lives in a separate repository:

**https://github.com/dre0dru/Atomic.SourceGenerators**

The sections above document how to use the compiled DLLs in a Unity project. If you want to modify, debug, or build the
generators yourself, follow the instructions in the repository.
