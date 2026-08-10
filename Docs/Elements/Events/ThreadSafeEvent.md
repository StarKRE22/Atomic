# 🧩 ThreadSafeEvent

A family of **thread-safe event implementations** that protect their internal state with a `lock` and defer
subscriber notifications to the main thread via `MainThreadDispatcher`. Allows events to be raised safely from any
thread.

---

## 📑 Table of Contents

- [Example of Usage](#-example-of-usage)
- [API Reference](#-api-reference)
  - [ThreadSafeEvent](#-threadsafeevent)
  - [ThreadSafeEvent&lt;T&gt;](#-threadsafeeventt)
  - [ThreadSafeEvent&lt;T1, T2&gt;](#-threadsafeeventt1-t2)
  - [ThreadSafeEvent&lt;T1, T2, T3&gt;](#-threadsafeeventt1-t2-t3)
  - [ThreadSafeEvent&lt;T1, T2, T3, T4&gt;](#-threadsafeeventt1-t2-t3-t4)
- [Thread Safety](#-thread-safety)

---

## 🗂 Example of Usage

```csharp
// Parameterless event
ThreadSafeEvent playerDiedEvent = new ThreadSafeEvent();
playerDiedEvent.OnEvent += () => Debug.Log("Player died!");

// Invoke safely from a background thread
Task.Run(() => playerDiedEvent.Invoke());

// Flush scheduled events on the main thread
MainThreadDispatcher.Flush();
```

```csharp
// Event with one argument
ThreadSafeEvent<int> scoreChangedEvent = new ThreadSafeEvent<int>();
scoreChangedEvent.OnEvent += score => Debug.Log($"Score: {score}");

Task.Run(() => scoreChangedEvent.Invoke(100));
MainThreadDispatcher.Flush();
```

---

## 🔍 API Reference

### 🏛️ ThreadSafeEvent

```csharp
public sealed class ThreadSafeEvent : IEvent, IDisposable, MainThreadDispatcher.IFlushable
```

- **Description:** Thread-safe parameterless event.
- **Inheritance:** [IEvent](IEvent.md), `IDisposable`, `MainThreadDispatcher.IFlushable`
- **Notes:** `Invoke()` marks the event dirty; subscribers are notified during `MainThreadDispatcher.Flush()`.
- **See also:** [Event](BaseEvent.md), [IEvent](IEvent.md)

#### ⚡ Events

##### `OnEvent`

```csharp
public event Action OnEvent;
```

- **Description:** Raised on the main thread when the event is flushed.
- **See also:** [ISignal](ISignal.md)

#### 🏹 Methods

##### `Invoke()`

```csharp
public void Invoke();
```

- **Description:** Schedules the event for main-thread notification.
- **Thread Safety:** Safe to call from any thread.

##### `Dispose()`

```csharp
public void Dispose();
```

- **Description:** Releases all event subscribers.
- **Thread Safety:** Uses `Interlocked.Exchange` to clear subscribers safely.

---

### 🏛️ ThreadSafeEvent&lt;T&gt;

```csharp
public sealed class ThreadSafeEvent<T> : IEvent<T>, IDisposable, MainThreadDispatcher.IFlushable
```

- **Description:** Thread-safe event with one argument.
- **Inheritance:** [IEvent&lt;T&gt;](IEvent%601.md), `IDisposable`, `MainThreadDispatcher.IFlushable`
- **Type Parameter:** `T` — The argument type.
- **Notes:** The last invoked argument is captured under a lock and passed to subscribers on the main thread.
- **See also:** [Event&lt;T&gt;](BaseEvent%601.md), [IEvent&lt;T&gt;](IEvent%601.md)

#### ⚡ Events

##### `OnEvent`

```csharp
public event Action<T> OnEvent;
```

- **Description:** Raised on the main thread when the event is flushed, passing the captured argument.
- **See also:** [ISignal&lt;T&gt;](ISignal%601.md)

#### 🏹 Methods

##### `Invoke(T)`

```csharp
public void Invoke(T value);
```

- **Description:** Captures the argument and schedules the event for main-thread notification.
- **Parameter:** `value` — The argument to pass to subscribers.
- **Thread Safety:** Safe to call from any thread.

##### `Dispose()`

```csharp
public void Dispose();
```

- **Description:** Releases all event subscribers.
- **Thread Safety:** Uses `Interlocked.Exchange` to clear subscribers safely.

---

### 🏛️ ThreadSafeEvent&lt;T1, T2&gt;

```csharp
public sealed class ThreadSafeEvent<T1, T2> : IEvent<T1, T2>, IDisposable, MainThreadDispatcher.IFlushable
```

- **Description:** Thread-safe event with two arguments.
- **Inheritance:** [IEvent&lt;T1, T2&gt;](IEvent%602.md), `IDisposable`, `MainThreadDispatcher.IFlushable`
- **Type Parameters:**
  - `T1` — The first argument type.
  - `T2` — The second argument type.
- **Notes:** Both arguments are captured under a lock and passed to subscribers on the main thread.
- **See also:** [Event&lt;T1, T2&gt;](BaseEvent%602.md)

#### ⚡ Events

##### `OnEvent`

```csharp
public event Action<T1, T2> OnEvent;
```

- **Description:** Raised on the main thread when the event is flushed, passing both captured arguments.

#### 🏹 Methods

##### `Invoke(T1, T2)`

```csharp
public void Invoke(T1 v1, T2 v2);
```

- **Description:** Captures both arguments and schedules the event for main-thread notification.
- **Parameters:**
  - `v1` — The first argument.
  - `v2` — The second argument.
- **Thread Safety:** Safe to call from any thread.

##### `Dispose()`

```csharp
public void Dispose();
```

- **Description:** Releases all event subscribers.
- **Thread Safety:** Uses `Interlocked.Exchange` to clear subscribers safely.

---

### 🏛️ ThreadSafeEvent&lt;T1, T2, T3&gt;

```csharp
public sealed class ThreadSafeEvent<T1, T2, T3> : IEvent<T1, T2, T3>, IDisposable, MainThreadDispatcher.IFlushable
```

- **Description:** Thread-safe event with three arguments.
- **Inheritance:** [IEvent&lt;T1, T2, T3&gt;](IEvent%603.md), `IDisposable`, `MainThreadDispatcher.IFlushable`
- **Type Parameters:**
  - `T1` — The first argument type.
  - `T2` — The second argument type.
  - `T3` — The third argument type.
- **Notes:** All arguments are captured under a lock and passed to subscribers on the main thread.
- **See also:** [Event&lt;T1, T2, T3&gt;](BaseEvent%603.md)

#### ⚡ Events

##### `OnEvent`

```csharp
public event Action<T1, T2, T3> OnEvent;
```

- **Description:** Raised on the main thread when the event is flushed, passing all captured arguments.

#### 🏹 Methods

##### `Invoke(T1, T2, T3)`

```csharp
public void Invoke(T1 v1, T2 v2, T3 v3);
```

- **Description:** Captures all arguments and schedules the event for main-thread notification.
- **Parameters:**
  - `v1` — The first argument.
  - `v2` — The second argument.
  - `v3` — The third argument.
- **Thread Safety:** Safe to call from any thread.

##### `Dispose()`

```csharp
public void Dispose();
```

- **Description:** Releases all event subscribers.
- **Thread Safety:** Uses `Interlocked.Exchange` to clear subscribers safely.

---

### 🏛️ ThreadSafeEvent&lt;T1, T2, T3, T4&gt;

```csharp
public sealed class ThreadSafeEvent<T1, T2, T3, T4> : IEvent<T1, T2, T3, T4>, IDisposable, MainThreadDispatcher.IFlushable
```

- **Description:** Thread-safe event with four arguments.
- **Inheritance:** [IEvent&lt;T1, T2, T3, T4&gt;](IEvent%604.md), `IDisposable`, `MainThreadDispatcher.IFlushable`
- **Type Parameters:**
  - `T1` — The first argument type.
  - `T2` — The second argument type.
  - `T3` — The third argument type.
  - `T4` — The fourth argument type.
- **Notes:** All arguments are captured under a lock and passed to subscribers on the main thread.
- **See also:** [Event&lt;T1, T2, T3, T4&gt;](BaseEvent%604.md)

#### ⚡ Events

##### `OnEvent`

```csharp
public event Action<T1, T2, T3, T4> OnEvent;
```

- **Description:** Raised on the main thread when the event is flushed, passing all captured arguments.

#### 🏹 Methods

##### `Invoke(T1, T2, T3, T4)`

```csharp
public void Invoke(T1 v1, T2 v2, T3 v3, T4 v4);
```

- **Description:** Captures all arguments and schedules the event for main-thread notification.
- **Parameters:**
  - `v1` — The first argument.
  - `v2` — The second argument.
  - `v3` — The third argument.
  - `v4` — The fourth argument.
- **Thread Safety:** Safe to call from any thread.

##### `Dispose()`

```csharp
public void Dispose();
```

- **Description:** Releases all event subscribers.
- **Thread Safety:** Uses `Interlocked.Exchange` to clear subscribers safely.

---

## 🔒 Thread Safety

- All `Invoke` overloads are safe to call from background threads.
- For generic versions, the last invoked arguments are captured under a `lock` and flushed once on the main thread.
- Subscribers are cleared atomically with `Interlocked.Exchange` when `Dispose()` is called.
- Remember to call `MainThreadDispatcher.Flush()` on the main thread each frame to deliver queued events.
