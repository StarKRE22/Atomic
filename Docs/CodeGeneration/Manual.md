# 🧬 Code Generation

**Atomic.CodeGeneration** provides Roslyn source generators and analyzers that turn declarative API classes into
strongly-typed extension methods. Instead of maintaining `.yaml` files, `.atomic` configs, or relying on an IDE plugin,
you declare keys as static fields in a partial class and the compiler generates the rest automatically.

---

## 📑 Table of Contents

- [Overview](#-overview)
- [Generators](#-generators)
- [Analyzers](#-analyzers)
- [Setup](#-setup)
- [Quick Example](#-quick-example)
- [Source Repository](#-source-repository)

---

## 🧩 Overview

The framework ships with two source generators and two diagnostic analyzers:

| Tool | Purpose | Marker Attribute |
|------|---------|------------------|
| [Entity API Generator](EntityAPI/EntityAPIGenerator.md) | Generate entity tag/value extension methods | `[EntityAPI]` |
| [Event API Generator](EventAPI/EventAPIGenerator.md) | Generate event-bus extension methods | `[EventAPI]` |
| [Entity API Analyzer](EntityAPI/EntityAPIAnalyzer.md) | Validate `[EntityAPI]` key initializers | — |
| [Event API Analyzer](EventAPI/EventAPIAnalyzer.md) | Validate `[EventAPI]` key initializers | — |

All generators are **compile-time only**. They do not add runtime overhead and do not ship in player builds.

---

## 🔍 Generators

### [Entity API Generator](EntityAPI/EntityAPIGenerator.md)

Turns `ValueKey<>` and `TagKey<>` fields into extension methods such as:

```csharp
entity.AddHealth(100);
int health = entity.GetHealth();
entity.DelPlayerTag();
```

### [Event API Generator](EventAPI/EventAPIGenerator.md)

Turns `EventKey<>` fields into bus extension methods such as:

```csharp
bus.InvokePlayerTurnStarted();
bus.SubscribeEntityDamaged(args => { /* ... */ });
```

---

## 🔍 Analyzers

Analyzers ship with code fixes and report build errors when a key field is missing an initializer or is initialized with
`new()` / `default`:

| Rule | Severity | Description |
|------|----------|-------------|
| `EAPI0001` | Error | Key field has no initializer. |
| `EAPI0002` | Error | Key field is initialized with `new()` / `default`. |

---

## ⚙️ Setup

See [Setup.md](Setup.md) for a single, shared guide on how to add the generator/analyzer DLLs to a Unity project.

---

## 🗂 Quick Example

### Entity API

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
```

---

## 📦 Source Repository

The generator and analyzer source code lives in a separate repository:

**https://github.com/dre0dru/Atomic.SourceGenerators**

The sections above document how to use the compiled DLLs in a Unity project. If you want to modify, debug, or build the
generators yourself, follow the instructions in the repository.
