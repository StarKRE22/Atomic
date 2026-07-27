# 🧩 EventBus Extensions

**Extensions** provide convenient overloads for subscribing to, unsubscribing from, invoking, and disposing events using
string names or strongly-typed [EventKey](Keys/EventKey.md) objects.

---

## 📑 Table of Contents

- [Overview](#-overview)
- [String-Based Extensions](#-string-based-extensions)
- [EventKey Extensions](#-eventkey-extensions)
- [Examples of Usage](#-examples-of-usage)
- [Best Practices](#-best-practices)

---

## 🧩 Overview

The `Atomic.Events.Extensions` class adds extension methods to [IEventBus](Bus/IEventBus.md). These methods convert
string names or `EventKey` objects to integer IDs via [EventKeyStore](Keys/EventKeyStore.md), making event code more
readable.

---

## 🔠 String-Based Extensions

```csharp
public static class Extensions
{
    // Subscribe
    public static Subscription Subscribe(this IEventBus it, string key, Action action);
    public static Subscription<T> Subscribe<T>(this IEventBus it, string key, Action<T> action);
    public static Subscription<T1, T2> Subscribe<T1, T2>(this IEventBus it, string key, Action<T1, T2> action);
    public static Subscription<T1, T2, T3> Subscribe<T1, T2, T3>(this IEventBus it, string key, Action<T1, T2, T3> action);

    // Invoke
    public static void Invoke(this IEventBus it, string key);
    public static void Invoke<T>(this IEventBus it, string key, T arg);
    public static void Invoke<T1, T2>(this IEventBus it, string key, T1 arg1, T2 arg2);
    public static void Invoke<T1, T2, T3>(this IEventBus it, string key, T1 arg1, T2 arg2, T3 arg3);

    // Unsubscribe
    public static void Unsubscribe(this IEventBus it, string key, Action action);
    public static void Unsubscribe<T>(this IEventBus it, string key, Action<T> action);
    public static void Unsubscribe<T1, T2>(this IEventBus it, string key, Action<T1, T2> action);
    public static void Unsubscribe<T1, T2, T3>(this IEventBus it, string key, Action<T1, T2, T3> action);

    // Other
    public static bool IsSubscribed(this IEventBus it, string key);
    public static bool Dispose(this IEventBus it, string key);
}
```

## 🔑 EventKey Extensions

```csharp
public static class Extensions
{
    // Subscribe
    public static Subscription Subscribe<TBus>(this TBus it, EventKey<TBus> key, Action action) where TBus : IEventBus;
    public static Subscription<T> Subscribe<TBus, T>(this TBus it, EventKey<TBus, T> key, Action<T> action) where TBus : IEventBus;
    public static Subscription<T1, T2> Subscribe<TBus, T1, T2>(this TBus it, EventKey<TBus, T1, T2> key, Action<T1, T2> action) where TBus : IEventBus;
    public static Subscription<T1, T2, T3> Subscribe<TBus, T1, T2, T3>(this TBus it, EventKey<TBus, T1, T2, T3> key, Action<T1, T2, T3> action) where TBus : IEventBus;

    // Invoke
    public static void Invoke<TBus>(this TBus it, EventKey<TBus> key) where TBus : IEventBus;
    public static void Invoke<TBus, T>(this TBus it, EventKey<TBus, T> key, T arg) where TBus : IEventBus;
    public static void Invoke<TBus, T1, T2>(this TBus it, EventKey<TBus, T1, T2> key, T1 arg1, T2 arg2) where TBus : IEventBus;
    public static void Invoke<TBus, T1, T2, T3>(this TBus it, EventKey<TBus, T1, T2, T3> key, T1 arg1, T2 arg2, T3 arg3) where TBus : IEventBus;

    // Unsubscribe
    public static void Unsubscribe<TBus>(this TBus it, EventKey<TBus> key, Action action) where TBus : IEventBus;
    public static void Unsubscribe<TBus, T>(this TBus it, EventKey<TBus, T> key, Action<T> action) where TBus : IEventBus;
    public static void Unsubscribe<TBus, T1, T2>(this TBus it, EventKey<TBus, T1, T2> key, Action<T1, T2> action) where TBus : IEventBus;
    public static void Unsubscribe<TBus, T1, T2, T3>(this TBus it, EventKey<TBus, T1, T2, T3> key, Action<T1, T2, T3> action) where TBus : IEventBus;

    // Other
    public static bool IsSubscribed<TBus>(this TBus it, EventKey<TBus> key) where TBus : IEventBus;
    public static bool IsSubscribed<TBus, T>(this TBus it, EventKey<TBus, T> key) where TBus : IEventBus;
    public static bool Dispose<TBus>(this TBus it, EventKey<TBus> key) where TBus : IEventBus;
    public static bool Dispose<TBus, T>(this TBus it, EventKey<TBus, T> key) where TBus : IEventBus;
}
```

---

## 🗂 Examples of Usage

### Using EventKey

```csharp
public static class GameEventAPI
{
    public static readonly EventKey<IGameEventBus> PlayerTurnStarted = new(nameof(PlayerTurnStarted));
    public static readonly EventKey<IGameEventBus, SpawnEventArgs> EntitySpawned = new(nameof(EntitySpawned));
    public static readonly EventKey<IGameEventBus, IGameEntity> EntityDied = new(nameof(EntityDied));
}

public static class PlayerUseCase
{
    public static bool StartPlayerTurn(this IGameContext context)
    {
        if (context.GetValue(GameContextAPI.GameState).Value != GameState.Playing)
            return false;

        context.GetValue(GameContextAPI.CurrentTurn).Value++;

        IGameEventBus eventBus = context.GetValue(GameContextAPI.EventBus);
        eventBus.Invoke(GameEventAPI.PlayerTurnStarted);
        eventBus.Flush();
        return true;
    }
}
```

### Spawning and Notifying

```csharp
public static bool Spawn(this IGameContext context, GameEntityType type, Vector2Int position, out IGameEntity entity, bool notify = true)
{
    // ... spawn logic ...

    if (notify)
    {
        IGameEventBus eventBus = context.GetValue(GameContextAPI.EventBus);
        eventBus.Invoke(GameEventAPI.EntitySpawned, new SpawnEventArgs(entity, position));
    }

    return true;
}
```

---

## 📌 Best Practices

- Use `EventKey` extensions for compile-time type safety.
- Prefer `EventKey<TBus>` over string names to avoid typos.
- Dispose subscriptions returned by `Subscribe` to prevent leaks.
- For `ThreadSafeEventBus`, call `Flush()` after invoking from background threads.
