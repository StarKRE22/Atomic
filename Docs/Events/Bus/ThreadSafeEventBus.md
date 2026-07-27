# 🧩 ThreadSafeEventBus

**ThreadSafeEventBus** is a thread-safe wrapper around an [IEventBus](IEventBus.md). Subscribe and unsubscribe operations
are delegated directly to the inner bus, while `Invoke` calls are queued and executed later on the main thread via `Flush()`.

---

## 📑 Table of Contents

- [Overview](#-overview)
- [Examples of Usage](#-examples-of-usage)
- [API Reference](#-api-reference)
- [Best Practices](#-best-practices)

---

## 🧩 Overview

`ThreadSafeEventBus` is useful when events are raised from background threads (e.g., async tasks, networking, or physics
jobs) but must be handled on the main thread.

- Subscribe / Unsubscribe: safe to call from any thread, delegated to the inner bus.
- Invoke: enqueues an action to be invoked on the inner bus when `Flush()` is called.
- Flush: dequeues and executes all pending invokes. Should be called on the main thread.

---

## 🗂 Examples of Usage

### Background Thread to Main Thread

```csharp
ThreadSafeEventBus eventBus = new ThreadSafeEventBus();

// Subscribe on main thread
using var subscription = eventBus.Subscribe(GameEventAPI.DownloadComplete.Id, (int bytes) =>
{
    Debug.Log($"Downloaded {bytes} bytes");
});

// Invoke from background thread
Task.Run(() =>
{
    int bytes = DownloadFile(url);
    eventBus.Invoke(GameEventAPI.DownloadComplete.Id, bytes);
});

// Later, on main thread (e.g., Update)
eventBus.Flush();
```

### Flush with IGameEventBus

```csharp
public static bool StartPlayerTurn(this IGameContext context)
{
    // ... logic ...
    IGameEventBus eventBus = context.GetValue(GameContextAPI.EventBus);
    eventBus.Invoke(GameEventAPI.PlayerTurnStarted);
    eventBus.Flush();
    return true;
}
```

---

## 🔍 API Reference

### 🏛️ Type

```csharp
public class ThreadSafeEventBus : IEventBus
```

### 🏗️ Constructors

#### `ThreadSafeEventBus()`

```csharp
public ThreadSafeEventBus()
```

- **Description:** Creates a thread-safe bus wrapping a new `EventBus`.

#### `ThreadSafeEventBus(IEventBus)`

```csharp
public ThreadSafeEventBus(IEventBus inner)
```

- **Description:** Wraps an existing event bus.
- **Parameter:** `inner` — The inner bus to which subscriptions are delegated.

### 🏹 Methods

Implements all methods from [IEventBus](IEventBus.md), plus:

#### `Flush()`

```csharp
public void Flush()
```

- **Description:** Executes all queued invokes on the inner bus.
- **Note:** Should be called on the main thread.

---

## 📌 Best Practices

- Call `Flush()` once per frame on the main thread.
- Handle exceptions inside subscribers; `Flush()` catches and logs them but continues processing.
- Dispose the bus when the owning context is destroyed to clear the queue.
- Do not rely on immediate delivery when invoking from background threads.
