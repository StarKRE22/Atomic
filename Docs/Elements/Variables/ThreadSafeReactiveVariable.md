# 🧩 ThreadSafeReactiveVariable&lt;T&gt;

A **thread-safe reactive variable** that combines locking for concurrent access with main-thread event dispatching via `MainThreadDispatcher`. Changes from background threads are safely queued and flushed on the main thread.

---

## 📑 Table of Contents

- [Example of Usage](#-example-of-usage)
- [API Reference](#-api-reference)
  - [Type](#-type)
  - [Constructors](#-constructors)
    - [ThreadSafeReactiveVariable()](#threadsafereactivevariable)
    - [ThreadSafeReactiveVariable(T)](#threadsafereactivevariablet)
  - [Events](#-events)
    - [OnEvent](#onevent)
  - [Properties](#-properties)
    - [Value](#value)
  - [Methods](#-methods)
    - [Dispose()](#dispose)
    - [ToString()](#tostring)
- [Thread Safety](#-thread-safety)
- [Main Thread Dispatching](#-main-thread-dispatching)

---

## 🗂 Example of Usage

### Background Thread with Main Thread Callback

```csharp
// Create a thread-safe reactive variable
var health = new ThreadSafeReactiveVariable<int>(100);

// Subscribe to changes (will be called on main thread)
health.OnEvent += newHealth => 
{
    Debug.Log($"Health changed to: {newHealth}");
    UpdateHealthUI(newHealth);
};

// Update from background thread
Task.Run(() =>
{
    health.Value -= 20; // Safe from background thread
    // OnEvent will be raised on the main thread via MainThreadDispatcher
});

// Later, on main thread (e.g., Update)
// MainThreadDispatcher automatically flushes pending events
```

### Using as Interface

```csharp
// ThreadSafeReactiveVariable implements IReactiveVariable<T>
IReactiveVariable<int> score = new ThreadSafeReactiveVariable<int>(0);

// Subscribe to changes
score.OnEvent += newScore => UpdateScoreDisplay(newScore);

// Thread-safe value updates
score.Value = 100; // Safe from any thread
```

---

## 🔍 API Reference

### 🏛️ Type <div id="-type"></div>

```csharp
public class ThreadSafeReactiveVariable<T> : IReactiveVariable<T>, IDisposable, MainThreadDispatcher.IFlushable
```

- **Description:** A **thread-safe reactive variable** that combines locking for concurrent access with main-thread event dispatching via `MainThreadDispatcher`.
- **Inheritance:** [IReactiveVariable&lt;T&gt;](IReactiveVariable.md), `IDisposable`, `MainThreadDispatcher.IFlushable`
- **Type Parameter:** `T` – The type of the value to store.
- **Notes:** Uses `lock` for thread safety. Events are deferred to the main thread via `MainThreadDispatcher`. Not serializable and does not support Unity serialization or Odin Inspector.
- **See also:** [ReactiveVariable&lt;T&gt;](ReactiveVariable.md), [ThreadSafeVariable&lt;T&gt;](ThreadSafeVariable.md)

---

<div id="-constructors"></div>

### 🏗️ Constructors

#### `ThreadSafeReactiveVariable()`

```csharp
public ThreadSafeReactiveVariable()
```

- **Description:** Initializes a new instance with the default value of `T`.

#### `ThreadSafeReactiveVariable(T)`

```csharp
public ThreadSafeReactiveVariable(T value)
```

- **Description:** Initializes a new instance with a specified initial value `value`.
- **Parameter:** `value` – The initial value to initialize the instance with.

---

### ⚡ Events

#### `OnEvent`

```csharp
public event Action<T> OnEvent;
```

- **Description:** Raised on the main thread when the value changes.
- **Parameter:** `T` – The new value after the change.
- **Thread Safety:** Event handlers are always invoked on the main thread via `MainThreadDispatcher`.

---

### 🔑 Properties

#### `Value`

```csharp
public T Value { get; set; }
```

- **Description:** Gets or sets the current value in a thread-safe manner.
- **Access:** Read-write
- **Thread Safety:** Both getter and setter are protected by a `lock`. The setter also performs equality checks to avoid unnecessary updates and marks the variable as dirty for main-thread event dispatch.

---

### 🏹 Methods

#### `Dispose()`

```csharp
public void Dispose()
```

- **Description:** Clears all event subscribers and releases resources.
- **Thread Safety:** Uses `Interlocked.Exchange` for thread-safe event cleanup.

#### `ToString()`

```csharp
public override string ToString();
```

- **Description:** Returns a string that represents the current value in a thread-safe manner.
- **Returns:** A string representation of the current value.

---

## 🔒 Thread Safety

This class is designed for scenarios where a reactive variable may be accessed from multiple threads simultaneously. All public members are protected by a `lock` to prevent race conditions.

**Key characteristics:**
- **Atomic operations:** Both read and write operations are atomic with respect to other locked operations.
- **Equality checking:** The setter uses `EqualityComparer<T>.Default` to avoid unnecessary updates when the value hasn't changed.
- **Blocking:** Calls to `Value` will block if another thread holds the lock. Use with caution to avoid deadlocks.
- **Not serializable:** This class is not marked `[Serializable]` and does not support Unity serialization.

**When to use:**
- Shared reactive state between background threads and the main thread
- Scenarios where you need change notifications from multiple threads
- When `ReactiveVariable<T>` is not thread-safe enough but you still need reactivity

**When not to use:**
- Single-threaded scenarios (use `ReactiveVariable<T>` instead)
- When you don't need change notifications (use `ThreadSafeVariable<T>`)
- When you need Unity serialization (use `ReactiveVariable<T>`)

---

## 🔄 Main Thread Dispatching

`ThreadSafeReactiveVariable<T>` implements `MainThreadDispatcher.IFlushable` to ensure that `OnEvent` handlers are always invoked on the main thread, even when the value is updated from a background thread.

**How it works:**
1. When `Value` is set from any thread, the variable is marked as dirty via `MainThreadDispatcher.MarkDirty(this)`.
2. `MainThreadDispatcher` (a Unity `MonoBehaviour`) checks for dirty objects in its `Update()` loop.
3. During `Update()`, it calls `Flush()` on each dirty object.
4. `Flush()` reads the current value under a lock and invokes `OnEvent` on the main thread.

**Important notes:**
- `MainThreadDispatcher` is automatically created at runtime (before scene load).
- Events are flushed once per frame during `Update()`.
- Multiple rapid changes may be batched into a single flush.
- Do not call `Flush()` directly; it is managed automatically by `MainThreadDispatcher`.
