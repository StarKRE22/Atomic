# 🧩 EventBus

**EventBus** is the default implementation of [IEventBus](IEventBus.md). It stores event delegates in a dictionary keyed
by integer event IDs and supports parameterless, single-argument, two-argument, and three-argument events.

---

## 📑 Table of Contents

- [Overview](#-overview)
- [Examples of Usage](#-examples-of-usage)
- [API Reference](#-api-reference)
- [Best Practices](#-best-practices)

---

## 🧩 Overview

`EventBus` is a simple, non-thread-safe event bus. It is suitable for single-threaded scenarios such as game logic running
on the main thread.

For multi-threaded scenarios, use [ThreadSafeEventBus](ThreadSafeEventBus.md).

For scene-bound Unity buses, use [MonoEventBus](MonoEventBus.md).

---

## 🗂 Examples of Usage

### Define Event Keys

```csharp
public static class GameEventAPI
{
    public static readonly EventKey<IGameEventBus> PlayerTurnStarted = new(nameof(PlayerTurnStarted));
    public static readonly EventKey<IGameEventBus, IGameEntity> EntityDied = new(nameof(EntityDied));
}
```

### Subscribe and Invoke

```csharp
IEventBus eventBus = new EventBus();

using var subscription = eventBus.Subscribe(GameEventAPI.EntityDied.Id, (IGameEntity entity) =>
{
    Debug.Log($"Entity died: {entity}");
});

eventBus.Invoke(GameEventAPI.EntityDied.Id, enemyEntity);
```

---

## 🔍 API Reference

### 🏛️ Type

```csharp
public class EventBus : IEventBus
```

### 🏹 Methods

Implements all methods from [IEventBus](IEventBus.md):

- `Subscribe(...)` / `Subscribe<T>(...)` / `Subscribe<T1, T2>(...)` / `Subscribe<T1, T2, T3>(...)`
- `Unsubscribe(...)` / `Unsubscribe<T>(...)` / `Unsubscribe<T1, T2>(...)` / `Unsubscribe<T1, T2, T3>(...)`
- `Invoke(...)` / `Invoke<T>(...)` / `Invoke<T1, T2>(...)` / `Invoke<T1, T2, T3>(...)`
- `IsSubscribed(int)`
- `Dispose()` / `Dispose(int)`

---

## 📌 Best Practices

- Use `EventBus` for main-thread-only scenarios.
- Store subscriptions in `using` statements or dispose them manually to prevent leaks.
- Use [EventKey](EventKey.md) instead of raw integer IDs for type safety.
- Use [ThreadSafeEventBus](ThreadSafeEventBus.md) for multi-threaded event dispatch.
