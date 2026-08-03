# 🔑 Events Keys

The **Atomic.Events.Keys** namespace provides strongly-typed event keys and name-to-id mapping utilities.
`EventKey<TBus>` wraps an integer event identifier and binds it to a specific bus type, preventing accidental misuse
between different event systems.

---

## 📑 Table of Contents

- [Examples of Usage](#-examples-of-usage)
  - [Create Event Keys](#create-event-keys)
  - [Use with Source Generation](#use-with-source-generation)
  - [Custom Key Algorithm](#custom-key-algorithm)
- [API Reference](#-api-reference)
- [Best Practices](#-best-practices)

---

## 🗂 Examples of Usage

### Create Event Keys

```csharp
EventKey<IEventBus> gameStarted = new(nameof(gameStarted));
EventKey<IEventBus, int> damageDealt = new(nameof(damageDealt));
EventKey<IEventBus, int, Vector3> hit = new(nameof(hit));
```

The keys above resolve string names to integer IDs through `EventKeyStore`.

### Use with Source Generation

The recommended approach is to define keys once in an `[GenerateEventExtensionsAPI]` class and let the generator create extension
methods:

```csharp
[GenerateEventExtensionsAPI]
public static partial class GameEventAPI
{
    public static readonly EventKey<IEventBus> GameStarted = new(nameof(GameStarted));
    public static readonly EventKey<IEventBus, int> DamageDealt = new(nameof(DamageDealt));
}
```

Usage:

```csharp
IEventBus bus = new EventBus();

bus.SubscribeDamageDealt(amount => Debug.Log(amount));
bus.InvokeDamageDealt(10);
```

### Custom Key Algorithm

By default, names are mapped to sequential integer IDs. You can swap the algorithm at application startup:

```csharp
EventKeyStore.SetAlgorithm(new SHA256EventKeyAlgorithm());
```

> [!NOTE]
> Switching the algorithm clears the current cache.

---

## 🔍 API Reference

### Keys

- [EventKey](EventKey.md) — parameterless event key

### Store

- [EventKeyStore](EventKeyStore.md) — string-to-id registry

### Algorithms

- [IEventKeyAlgorithm](IEventKeyAlgorithm.md) — key generation algorithm contract
- [SequentialEventKeyAlgorithm](SequentialEventKeyAlgorithm.md) — sequential integer algorithm
- [Fnv1AEventKeyAlgorithm](Fnv1AEventKeyAlgorithm.md) — FNV-1a hashing algorithm
- [SHA256EventKeyAlgorithm](SHA256EventKeyAlgorithm.md) — SHA-256 hashing algorithm

---

## 📌 Best Practices

- Use `[GenerateEventExtensionsAPI]` source generation for type-safe invoke/subscribe methods.
- Use `EventKey<TBus>` and generic variants instead of raw string or integer IDs.
- Use a deterministic algorithm if you need stable IDs across application restarts.
- Call `EventKeyStore.Reset()` in tests to avoid state leaking between test runs.
