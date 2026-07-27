# 🧩 ThreadSafeCommand

**ThreadSafeCommand** is a thread-safe implementation of [ICommand](ICommand.md). It protects its internal state
with a `lock`, allowing conditions, actions, and event subscriptions to be added or removed from multiple threads.
Events are raised on the main thread via `MainThreadDispatcher`.

---

## 📑 Table of Contents

- [Overview](#-overview)
- [Variants](#-variants)
- [API Reference](#-api-reference)
  - [ThreadSafeCommand](#threadsafecommand)
  - [ThreadSafeCommand\<T\>](#threadsafecommandt)
- [Examples of Usage](#-examples-of-usage)
- [Best Practices](#-best-practices)

---

## 🧩 Overview

`ThreadSafeCommand` behaves like [Command](Command.md), but all mutating operations are protected by a lock. When
invoked, the action runs on the calling thread, but the `OnEvent` signal is deferred to the main thread through
`MainThreadDispatcher.MarkDirty`.

This makes it safe to use in multi-threaded contexts such as:

- Background tasks that report results to the UI
- Async operations that need to trigger gameplay logic
- Systems where commands are configured from worker threads

---

## 🔍 Variants

| Class | Interface | Arguments |
|-------|-----------|-----------|
| `ThreadSafeCommand` | `ICommand` | 0 |
| `ThreadSafeCommand<T>` | `ICommand<T>` | 1 |
| `ThreadSafeCommand<T1, T2>` | `ICommand<T1, T2>` | 2 |
| `ThreadSafeCommand<T1, T2, T3>` | `ICommand<T1, T2, T3>` | 3 |
| `ThreadSafeCommand<T1, T2, T3, T4>` | `ICommand<T1, T2, T3, T4>` | 4 |

---

## 🔍 API Reference

### ThreadSafeCommand

```csharp
public sealed class ThreadSafeCommand : ICommand, MainThreadDispatcher.IFlushable
```

#### `CanInvoke()`

```csharp
public bool CanInvoke();
```

- **Description:** Returns `true` if all registered conditions pass.
- **Thread Safety:** Reads conditions under a lock.

#### `TryInvoke()`

```csharp
public bool TryInvoke();
```

- **Description:** Invokes the action if all conditions pass, then marks the command as dirty for main-thread event dispatch.
- **Returns:** `true` if the command executed, `false` otherwise.
- **Thread Safety:** Reads the action under a lock; action runs on the calling thread; event is deferred.

#### `Invoke()`

```csharp
public void Invoke();
```

- **Description:** Invokes the action if all conditions pass. Does not return a value.

#### `AddCondition(Func<bool>)`

```csharp
public ICommand AddCondition(Func<bool> condition);
```

- **Description:** Adds a parameterless condition.
- **Thread Safety:** Mutates internal arrays under a lock.

#### `RemoveCondition(Func<bool>)`

```csharp
public ICommand RemoveCondition(Func<bool> condition);
```

- **Description:** Removes a previously added condition.
- **Thread Safety:** Mutates internal arrays under a lock.

#### `AddAction(Action)`

```csharp
public ICommand AddAction(Action action);
```

- **Description:** Adds an action to invoke when conditions pass.
- **Thread Safety:** Mutates the action delegate under a lock.

#### `RemoveAction(Action)`

```csharp
public ICommand RemoveAction(Action action);
```

- **Description:** Removes a previously added action.
- **Thread Safety:** Mutates the action delegate under a lock.

### ThreadSafeCommand\<T\>

```csharp
public sealed class ThreadSafeCommand<T> : ICommand<T>, MainThreadDispatcher.IFlushable
```

Same thread-safe behavior as `ThreadSafeCommand`, but accepts one argument:

- `bool CanInvoke(T arg)`
- `bool TryInvoke(T arg)`
- `void Invoke(T arg)`

---

## 🗂 Examples of Usage

### Background Task Result

```csharp
ThreadSafeCommand<int> onDownloadComplete = new ThreadSafeCommand<int>();

onDownloadComplete
    .AddCondition(bytes => bytes > 0)
    .AddAction(bytes => Debug.Log($"Downloaded {bytes} bytes"));

// Invoked from a background thread
Task.Run(() =>
{
    int size = DownloadFileAsync(url).Result;
    onDownloadComplete.TryInvoke(size); // Action runs here; event fires on main thread
});
```

---

## 📌 Best Practices

- Use `ThreadSafeCommand` only when commands are accessed from multiple threads.
- Keep conditions and actions fast to avoid holding the lock for too long.
- Remember that `OnEvent` is deferred to the main thread — subscribers should expect a one-frame delay.
- For single-threaded scenarios, prefer the simpler [Command](Command.md).
