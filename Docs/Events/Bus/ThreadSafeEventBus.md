# 🧩 ThreadSafeEventBus

Thread-safe wrapper around an [IEventBus](IEventBus.md). Subscribe and unsubscribe operations are delegated directly to the inner bus, while `Invoke` calls are queued and executed later on the main thread via `Flush()`.

---

## 📑 Table of Contents

- [Example of Usage](#-example-of-usage)
- [API Reference](#-api-reference)
  - [Type](#-type)
  - [Constructors](#-constructors)
    - [ThreadSafeEventBus()](#threadsafeeventbus)
    - [ThreadSafeEventBus(IEventBus)](#threadsafeeventbusieventbus)
  - [Methods](#-methods)
    - [Subscribe](#subscribe)
    - [Unsubscribe](#unsubscribe)
    - [Invoke](#invoke)
    - [IsSubscribed](#issubscribed)
    - [Dispose](#dispose)
    - [Flush](#flush)

---

## 🗂 Example of Usage

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

---

## 🔍 API Reference

### 🏛️ Type

```csharp
public class ThreadSafeEventBus : IEventBus
```

- **Description:** Thread-safe wrapper around an [IEventBus](IEventBus.md).
- **Inheritance:** [IEventBus](IEventBus.md)
- **Notes:** Invocations are queued on a `ConcurrentQueue<Action>` and must be flushed on the main thread.
- **See also:** [EventBus](EventBus.md), [MonoEventBus](MonoEventBus.md)

---

### 🏗️ Constructors

#### `ThreadSafeEventBus()`

```csharp
public ThreadSafeEventBus()
```

- **Description:** Creates a thread-safe bus wrapping a new [EventBus](EventBus.md).

#### `ThreadSafeEventBus(IEventBus)`

```csharp
public ThreadSafeEventBus(IEventBus inner)
```

- **Description:** Wraps an existing event bus.
- **Parameter:** `inner` – The inner bus to delegate subscriptions and queued invokes to.

---

### 🏹 Methods

#### `Subscribe`

```csharp
public Subscription Subscribe(int key, Action action);
public Subscription<T> Subscribe<T>(int key, Action<T> action);
public Subscription<T1, T2> Subscribe<T1, T2>(int key, Action<T1, T2> action);
public Subscription<T1, T2, T3> Subscribe<T1, T2, T3>(int key, Action<T1, T2, T3> action);
```

- **Description:** Registers a callback on the inner bus.
- **See also:** [Subscription](../Subscriptions/Subscription.md)

#### `Unsubscribe`

```csharp
public void Unsubscribe(int key, Action action);
public void Unsubscribe<T>(int key, Action<T> action);
public void Unsubscribe<T1, T2>(int key, Action<T1, T2> action);
public void Unsubscribe<T1, T2, T3>(int key, Action<T1, T2, T3> action);
```

- **Description:** Removes a callback from the inner bus.

#### `Invoke`

```csharp
public void Invoke(int key);
public void Invoke<T>(int key, T arg);
public void Invoke<T1, T2>(int key, T1 arg1, T2 arg2);
public void Invoke<T1, T2, T3>(int key, T1 arg1, T2 arg2, T3 arg3);
```

- **Description:** Enqueues an action that invokes the event on the inner bus.
- **Notes:** The event is not raised immediately; call `Flush()` on the main thread to execute queued actions.

#### `IsSubscribed`

```csharp
public bool IsSubscribed(int key);
```

- **Description:** Returns whether any callback is registered for the key on the inner bus.

#### `Dispose`

```csharp
public bool Dispose(int key);
public void Dispose();
```

- **Description:** Removes callbacks from the inner bus and clears the invoke queue.

#### `Flush`

```csharp
public void Flush()
```

- **Description:** Dequeues and executes all pending invokes on the main thread.
- **Notes:** Should be called on the main thread. Exceptions thrown by subscribers are caught and logged, and processing continues.
