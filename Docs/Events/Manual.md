# 🧩 Atomic.Events

**Atomic.Events** provides a lightweight, strongly-typed event bus system for Unity and C#. It supports parameterless
and parameterized events, subscriptions, thread-safe dispatch, and Unity scene-bound buses.

The event system decouples publishers from subscribers using integer event keys wrapped in strongly-typed
`EventKey<TBus>` structs.

---

## 📑 Table of Contents

- [Examples of Usage](#-examples-of-usage)
  - [Define Events with Source Generation](#define-events-with-source-generation)
  - [Subscribe and Invoke](#subscribe-and-invoke)
  - [Thread-Safe Dispatch](#thread-safe-dispatch)
- [API Reference](#-api-reference)
  - [Bus Implementations](#bus-implementations)
  - [Event Keys](#event-keys)
  - [Subscriptions](#subscriptions)
  - [Extensions](#extensions)
- [Best Practices](#-best-practices)

---

## 🗂 Examples of Usage

### Define Events with Source Generation

```csharp
using Atomic.Events;

[GenerateEventExtensionsAPI]
public static partial class GameEventAPI
{
    public static readonly EventKey<IEventBus> PlayerTurnStarted = new(nameof(PlayerTurnStarted));
    public static readonly EventKey<IEventBus, int> DamageDealt = new(nameof(DamageDealt));
    public static readonly EventKey<IEventBus, IGameEntity> EntityDied = new(nameof(EntityDied));
}
```

### Subscribe and Invoke

```csharp
IEventBus eventBus = new EventBus();

using var subscription = eventBus.SubscribeEntityDied(entity =>
{
    Debug.Log($"Entity died: {entity}");
});

eventBus.InvokePlayerTurnStarted();
eventBus.InvokeDamageDealt(10);
eventBus.InvokeEntityDied(enemyEntity);
```

### Thread-Safe Dispatch

```csharp
var threadSafeBus = new ThreadSafeEventBus();

// Safe to call from a background thread
threadSafeBus.InvokeDamageDealt(5);

// Call once per frame on the main thread
threadSafeBus.Flush();
```

---

## 🔍 API Reference

### Bus Implementations

- [IEventBus](Bus/IEventBus.md) — core interface
- [EventBus](Bus/EventBus.md) — default implementation
- [ThreadSafeEventBus](Bus/ThreadSafeEventBus.md) — thread-safe wrapper with main-thread flushing
- [MonoEventBus](Bus/MonoEventBus.md) — Unity `MonoBehaviour` bus
- [MonoEventBusSingleton](Bus/MonoEventBusSingleton.md) — singleton scene/global bus
- [Bus Manual](Bus/Manual.md)

### Event Keys

- [EventKey](Keys/EventKey.md) — strongly-typed event identifier
- [EventKeyStore](Keys/EventKeyStore.md) — name-to-ID mapping
- [Keys Manual](Keys/Manual.md)

### Subscriptions

- [Subscription](Subscriptions/Subscription.md) — disposable subscription handle
- [Subscriptions Manual](Subscriptions/Manual.md)

### Extensions

- [EventBus Extensions](Extensions.md)

---

## 📌 Best Practices

- Define event keys in a single `[GenerateEventExtensionsAPI]` class.
- Use generated extension methods for compile-time type safety.
- Dispose subscriptions to avoid leaks; prefer `using` declarations.
- Use `ThreadSafeEventBus` for background thread event dispatch.
- Call `Flush()` once per frame on the main thread for `ThreadSafeEventBus`.
- Keep event callbacks fast and side-effect free where possible.
