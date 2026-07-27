# 🧩 Atomic.Events

**Atomic.Events** provides a lightweight, strongly-typed event bus system for Unity and C#. It supports parameterless
and parameterized events, subscriptions, thread-safe dispatch, and Unity scene-bound buses.

---

## 📑 Table of Contents

- [Overview](#-overview)
- [Bus Implementations](#-bus-implementations)
- [Event Keys](#-event-keys)
- [Examples of Usage](#-examples-of-usage)
- [Best Practices](#-best-practices)

---

## 🧩 Overview

The event system decouples publishers from subscribers using integer event keys. The framework provides:

- [IEventBus](Bus/IEventBus.md) — core interface
- [EventBus](Bus/EventBus.md) — default implementation
- [ThreadSafeEventBus](Bus/ThreadSafeEventBus.md) — thread-safe wrapper with main-thread flushing
- [MonoEventBus](Bus/MonoEventBus.md) — Unity `MonoBehaviour` bus
- [MonoEventBusSingleton](Bus/MonoEventBusSingleton.md) — singleton scene/global bus
- [EventKey](Keys/EventKey.md) — strongly-typed event identifiers
- [EventKeyStore](Keys/EventKeyStore.md) — name-to-ID mapping

---

## 🔍 Bus Implementations

- [IEventBus](Bus/IEventBus.md)
- [EventBus](Bus/EventBus.md)
- [ThreadSafeEventBus](Bus/ThreadSafeEventBus.md)
- [MonoEventBus](Bus/MonoEventBus.md)
- [MonoEventBusSingleton](Bus/MonoEventBusSingleton.md)

## 🔍 Event Keys

- [EventKey](Keys/EventKey.md)
- [EventKeyStore](Keys/EventKeyStore.md)
- [IEventKeyAlgorithm](Keys/IEventKeyAlgorithm.md)
- [SequentialEventKeyAlgorithm](Keys/SequentialEventKeyAlgorithm.md)
- [Fnv1AEventKeyAlgorithm](Keys/Fnv1AEventKeyAlgorithm.md)
- [SHA256EventKeyAlgorithm](Keys/SHA256EventKeyAlgorithm.md)

## 🔍 Subscriptions

- [Subscription](Subscriptions/Subscription.md)

## 🔍 Extensions

- [EventBus Extensions](Extensions.md)

---

## 🗂 Examples of Usage

### Define Events

```csharp
public static class GameEventAPI
{
    public static readonly EventKey<IGameEventBus> PlayerTurnStarted = new(nameof(PlayerTurnStarted));
    public static readonly EventKey<IGameEventBus, SpawnEventArgs> EntitySpawned = new(nameof(EntitySpawned));
    public static readonly EventKey<IGameEventBus, IGameEntity> EntityDied = new(nameof(EntityDied));
}
```

### Subscribe and Invoke

```csharp
IGameEventBus eventBus = context.GetValue(GameContextAPI.EventBus);

using var subscription = eventBus.Subscribe(GameEventAPI.EntityDied, (IGameEntity entity) =>
{
    Debug.Log($"Entity died: {entity}");
});

eventBus.Invoke(GameEventAPI.EntityDied, enemyEntity);
eventBus.Flush(); // for ThreadSafeEventBus
```

---

## 📌 Best Practices

- Define event keys in a single static API class.
- Use `EventKey<TBus>` for compile-time bus type safety.
- Dispose subscriptions to avoid leaks.
- Use `ThreadSafeEventBus` for background thread event dispatch.
- Call `Flush()` once per frame on the main thread for `ThreadSafeEventBus`.
- Keep event callbacks fast and side-effect free where possible.
