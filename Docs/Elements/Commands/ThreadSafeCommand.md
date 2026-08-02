# 🧩 ThreadSafeCommand

A family of **thread-safe command implementations** that protect their internal state with a `lock`. Actions run on the calling thread, while `OnEvent` signals are deferred to the main thread via `MainThreadDispatcher`.

---

## 📑 Table of Contents

- [Example of Usage](#-example-of-usage)
- [API Reference](#-api-reference)
  - [ThreadSafeCommand](#-threadsafecommand)
    - [Type](#type)
    - [Events](#events)
    - [Methods](#methods)
  - [ThreadSafeCommand&lt;T&gt;](#-threadsafecommandt)
    - [Type](#type-1)
    - [Events](#events-1)
    - [Methods](#methods-1)
  - [ThreadSafeCommand&lt;T1, T2&gt;](#-threadsafecommandt1-t2)
    - [Type](#type-2)
    - [Events](#events-2)
    - [Methods](#methods-2)
  - [ThreadSafeCommand&lt;T1, T2, T3&gt;](#-threadsafecommandt1-t2-t3)
    - [Type](#type-3)
    - [Events](#events-3)
    - [Methods](#methods-3)
  - [ThreadSafeCommand&lt;T1, T2, T3, T4&gt;](#-threadsafecommandt1-t2-t3-t4)
    - [Type](#type-4)
    - [Events](#events-4)
    - [Methods](#methods-4)

---

## 🔍 API Reference

### 🏛️ ThreadSafeCommand

#### Type

```csharp
public sealed class ThreadSafeCommand : ICommand, MainThreadDispatcher.IFlushable
```

- **Description:** Thread-safe parameterless command.
- **Inheritance:** [ICommand](ICommand.md), `MainThreadDispatcher.IFlushable`
- **Type Parameters:** None
- **Notes:** All mutating methods are protected by a lock. `OnEvent` is flushed on the main thread.
- **See also:** [Command](Command.md), [ICommand](ICommand.md)

#### Events

##### `OnEvent`

```csharp
public event Action OnEvent;
```

- **Description:** Raised on the main thread after the action is successfully invoked.
- **See also:** [ISignal](../Events/ISignal.md)

#### Methods

##### `CanInvoke()`

```csharp
public bool CanInvoke();
```

- **Description:** Returns `true` only if all registered conditions pass.
- **Returns:** `true` when the command can be invoked; otherwise `false`.
- **Thread Safety:** Reads the condition array under a lock.

##### `TryInvoke()`

```csharp
public bool TryInvoke();
```

- **Description:** Invokes the action if all conditions pass, then schedules `OnEvent` for the main thread.
- **Returns:** `true` if the command executed; otherwise `false`.
- **Thread Safety:** Reads the action under a lock; the action runs on the calling thread.

##### `Invoke()`

```csharp
public void Invoke();
```

- **Description:** Invokes the action if all conditions pass.
- **Thread Safety:** Reads the action under a lock; the action runs on the calling thread.

##### `AddCondition(Func<bool>)`

```csharp
public ICommand AddCondition(Func<bool> condition);
```

- **Description:** Adds a parameterless condition.
- **Parameter:** `condition` — Predicate that must return `true` for execution.
- **Returns:** The command instance for chaining.
- **Thread Safety:** Mutates the condition array under a lock.

##### `RemoveCondition(Func<bool>)`

```csharp
public ICommand RemoveCondition(Func<bool> condition);
```

- **Description:** Removes a previously added condition.
- **Parameter:** `condition` — The condition delegate to remove.
- **Returns:** The command instance for chaining.
- **Thread Safety:** Mutates the condition array under a lock.

##### `AddAction(Action)`

```csharp
public ICommand AddAction(Action action);
```

- **Description:** Adds an action to invoke when conditions pass.
- **Parameter:** `action` — The action delegate to add.
- **Returns:** The command instance for chaining.
- **Thread Safety:** Mutates the action delegate under a lock.

##### `RemoveAction(Action)`

```csharp
public ICommand RemoveAction(Action action);
```

- **Description:** Removes a previously added action.
- **Parameter:** `action` — The action delegate to remove.
- **Returns:** The command instance for chaining.
- **Thread Safety:** Mutates the action delegate under a lock.

---

### 🏛️ ThreadSafeCommand&lt;T&gt;

#### Type

```csharp
public sealed class ThreadSafeCommand<T> : ICommand<T>, MainThreadDispatcher.IFlushable
```

- **Description:** Thread-safe command that accepts one argument.
- **Inheritance:** [ICommand&lt;T&gt;](ICommand.md), `MainThreadDispatcher.IFlushable`
- **Type Parameters:** `T` — The argument type.
- **Notes:** All mutating methods are protected by a lock. `OnEvent` is flushed on the main thread with the captured argument.
- **See also:** [Command&lt;T&gt;](Command.md), [ICommand&lt;T&gt;](ICommand.md)

#### Events

##### `OnEvent`

```csharp
public event Action<T> OnEvent;
```

- **Description:** Raised on the main thread after the action is successfully invoked, passing the argument.
- **See also:** [ISignal&lt;T&gt;](../Events/ISignal%601.md)

#### Methods

##### `CanInvoke(T)`

```csharp
#if ODIN_INSPECTOR
[Button]
#endif
public bool CanInvoke(T arg);
```

- **Description:** Returns `true` only if all registered conditions pass for `arg`.
- **Parameter:** `arg` — The argument to evaluate.
- **Returns:** `true` when the command can be invoked; otherwise `false`.
- **Thread Safety:** Reads the condition array under a lock.

##### `TryInvoke(T)`

```csharp
#if ODIN_INSPECTOR
[Button]
#endif
public bool TryInvoke(T arg);
```

- **Description:** Invokes the action if all conditions pass, then schedules `OnEvent` for the main thread.
- **Parameter:** `arg` — The argument to pass.
- **Returns:** `true` if the command executed; otherwise `false`.
- **Thread Safety:** Reads the action and conditions under a lock; the action runs on the calling thread.

##### `Invoke(T)`

```csharp
public void Invoke(T arg);
```

- **Description:** Invokes the action if all conditions pass.
- **Parameter:** `arg` — The argument to pass.
- **Thread Safety:** Reads the action and conditions under a lock; the action runs on the calling thread.

##### `Flush()`

```csharp
void MainThreadDispatcher.IFlushable.Flush();
```

- **Description:** Explicit interface implementation that raises `OnEvent` on the main thread with the captured argument.
- **Notes:** Called automatically by `MainThreadDispatcher`; do not invoke directly.

##### `AddCondition(Func<T, bool>)`

```csharp
public ICommand<T> AddCondition(Func<T, bool> condition);
```

- **Description:** Adds a single-argument condition.
- **Parameter:** `condition` — Predicate that returns `true` for execution.
- **Returns:** The command instance for chaining.
- **Thread Safety:** Mutates the condition array under a lock.

##### `RemoveCondition(Func<T, bool>)`

```csharp
public ICommand<T> RemoveCondition(Func<T, bool> condition);
```

- **Description:** Removes a previously added condition.
- **Parameter:** `condition` — The condition delegate to remove.
- **Returns:** The command instance for chaining.
- **Thread Safety:** Mutates the condition array under a lock.

##### `AddAction(Action<T>)`

```csharp
public ICommand<T> AddAction(Action<T> action);
```

- **Description:** Adds an action to invoke when conditions pass.
- **Parameter:** `action` — The action delegate to add.
- **Returns:** The command instance for chaining.
- **Thread Safety:** Mutates the action delegate under a lock.

##### `RemoveAction(Action<T>)`

```csharp
public ICommand<T> RemoveAction(Action<T> action);
```

- **Description:** Removes a previously added action.
- **Parameter:** `action` — The action delegate to remove.
- **Returns:** The command instance for chaining.
- **Thread Safety:** Mutates the action delegate under a lock.

---

### 🏛️ ThreadSafeCommand&lt;T1, T2&gt;

#### Type

```csharp
public sealed class ThreadSafeCommand<T1, T2> : ICommand<T1, T2>, MainThreadDispatcher.IFlushable
```

- **Description:** Thread-safe command that accepts two arguments.
- **Inheritance:** [ICommand&lt;T1, T2&gt;](ICommand.md), `MainThreadDispatcher.IFlushable`
- **Type Parameters:**
  - `T1` — The first argument type.
  - `T2` — The second argument type.
- **Notes:** All mutating methods are protected by a lock. `OnEvent` is flushed on the main thread with the captured arguments.
- **See also:** [Command&lt;T1, T2&gt;](Command.md)

#### Events

##### `OnEvent`

```csharp
public event Action<T1, T2> OnEvent;
```

- **Description:** Raised on the main thread after the action is successfully invoked, passing both arguments.

#### Methods

##### `CanInvoke(T1, T2)`

```csharp
public bool CanInvoke(T1 arg1, T2 arg2);
```

- **Description:** Returns `true` only if all registered conditions pass.
- **Parameters:**
  - `arg1` — The first argument.
  - `arg2` — The second argument.
- **Returns:** `true` when the command can be invoked; otherwise `false`.
- **Thread Safety:** Reads the condition array under a lock.

##### `TryInvoke(T1, T2)`

```csharp
public bool TryInvoke(T1 arg1, T2 arg2);
```

- **Description:** Invokes the action if all conditions pass, then schedules `OnEvent` for the main thread.
- **Parameters:**
  - `arg1` — The first argument.
  - `arg2` — The second argument.
- **Returns:** `true` if the command executed; otherwise `false`.
- **Thread Safety:** Reads the action and conditions under a lock; the action runs on the calling thread.

##### `Invoke(T1, T2)`

```csharp
public void Invoke(T1 arg1, T2 arg2);
```

- **Description:** Invokes the action if all conditions pass.
- **Parameters:**
  - `arg1` — The first argument.
  - `arg2` — The second argument.
- **Thread Safety:** Reads the action and conditions under a lock; the action runs on the calling thread.

##### `AddCondition(Func<T1, T2, bool>)`

```csharp
public ICommand<T1, T2> AddCondition(Func<T1, T2, bool> condition);
```

- **Description:** Adds a two-argument condition.
- **Parameter:** `condition` — Predicate that returns `true` for execution.
- **Returns:** The command instance for chaining.
- **Thread Safety:** Mutates the condition array under a lock.

##### `RemoveCondition(Func<T1, T2, bool>)`

```csharp
public ICommand<T1, T2> RemoveCondition(Func<T1, T2, bool> condition);
```

- **Description:** Removes a previously added condition.
- **Parameter:** `condition` — The condition delegate to remove.
- **Returns:** The command instance for chaining.
- **Thread Safety:** Mutates the condition array under a lock.

##### `AddAction(Action<T1, T2>)`

```csharp
public ICommand<T1, T2> AddAction(Action<T1, T2> action);
```

- **Description:** Adds an action to invoke when conditions pass.
- **Parameter:** `action` — The action delegate to add.
- **Returns:** The command instance for chaining.
- **Thread Safety:** Mutates the action delegate under a lock.

##### `RemoveAction(Action<T1, T2>)`

```csharp
public ICommand<T1, T2> RemoveAction(Action<T1, T2> action);
```

- **Description:** Removes a previously added action.
- **Parameter:** `action` — The action delegate to remove.
- **Returns:** The command instance for chaining.
- **Thread Safety:** Mutates the action delegate under a lock.

---

### 🏛️ ThreadSafeCommand&lt;T1, T2, T3&gt;

#### Type

```csharp
public sealed class ThreadSafeCommand<T1, T2, T3> : ICommand<T1, T2, T3>, MainThreadDispatcher.IFlushable
```

- **Description:** Thread-safe command that accepts three arguments.
- **Inheritance:** [ICommand&lt;T1, T2, T3&gt;](ICommand.md), `MainThreadDispatcher.IFlushable`
- **Type Parameters:**
  - `T1` — The first argument type.
  - `T2` — The second argument type.
  - `T3` — The third argument type.
- **Notes:** All mutating methods are protected by a lock. `OnEvent` is flushed on the main thread with the captured arguments.
- **See also:** [Command&lt;T1, T2, T3&gt;](Command.md)

#### Events

##### `OnEvent`

```csharp
public event Action<T1, T2, T3> OnEvent;
```

- **Description:** Raised on the main thread after the action is successfully invoked, passing all arguments.

#### Methods

##### `CanInvoke(T1, T2, T3)`

```csharp
public bool CanInvoke(T1 arg1, T2 arg2, T3 arg3);
```

- **Description:** Returns `true` only if all registered conditions pass.
- **Parameters:**
  - `arg1` — The first argument.
  - `arg2` — The second argument.
  - `arg3` — The third argument.
- **Returns:** `true` when the command can be invoked; otherwise `false`.
- **Thread Safety:** Reads the condition array under a lock.

##### `TryInvoke(T1, T2, T3)`

```csharp
public bool TryInvoke(T1 arg1, T2 arg2, T3 arg3);
```

- **Description:** Invokes the action if all conditions pass, then schedules `OnEvent` for the main thread.
- **Parameters:**
  - `arg1` — The first argument.
  - `arg2` — The second argument.
  - `arg3` — The third argument.
- **Returns:** `true` if the command executed; otherwise `false`.
- **Thread Safety:** Reads the action and conditions under a lock; the action runs on the calling thread.

##### `Invoke(T1, T2, T3)`

```csharp
public void Invoke(T1 arg1, T2 arg2, T3 arg3);
```

- **Description:** Invokes the action if all conditions pass.
- **Parameters:**
  - `arg1` — The first argument.
  - `arg2` — The second argument.
  - `arg3` — The third argument.
- **Thread Safety:** Reads the action and conditions under a lock; the action runs on the calling thread.

##### `Flush()`

```csharp
void MainThreadDispatcher.IFlushable.Flush();
```

- **Description:** Explicit interface implementation that raises `OnEvent` on the main thread with the captured arguments.
- **Notes:** Called automatically by `MainThreadDispatcher`; do not invoke directly.

##### `AddCondition(Func<T1, T2, T3, bool>)`

```csharp
public ICommand<T1, T2, T3> AddCondition(Func<T1, T2, T3, bool> condition);
```

- **Description:** Adds a three-argument condition.
- **Parameter:** `condition` — Predicate that returns `true` for execution.
- **Returns:** The command instance for chaining.
- **Thread Safety:** Mutates the condition array under a lock.

##### `RemoveCondition(Func<T1, T2, T3, bool>)`

```csharp
public ICommand<T1, T2, T3> RemoveCondition(Func<T1, T2, T3, bool> condition);
```

- **Description:** Removes a previously added condition.
- **Parameter:** `condition` — The condition delegate to remove.
- **Returns:** The command instance for chaining.
- **Thread Safety:** Mutates the condition array under a lock.

##### `AddAction(Action<T1, T2, T3>)`

```csharp
public ICommand<T1, T2, T3> AddAction(Action<T1, T2, T3> action);
```

- **Description:** Adds an action to invoke when conditions pass.
- **Parameter:** `action` — The action delegate to add.
- **Returns:** The command instance for chaining.
- **Thread Safety:** Mutates the action delegate under a lock.

##### `RemoveAction(Action<T1, T2, T3>)`

```csharp
public ICommand<T1, T2, T3> RemoveAction(Action<T1, T2, T3> action);
```

- **Description:** Removes a previously added action.
- **Parameter:** `action` — The action delegate to remove.
- **Returns:** The command instance for chaining.
- **Thread Safety:** Mutates the action delegate under a lock.

---

### 🏛️ ThreadSafeCommand&lt;T1, T2, T3, T4&gt;

#### Type

```csharp
public sealed class ThreadSafeCommand<T1, T2, T3, T4> : ICommand<T1, T2, T3, T4>, MainThreadDispatcher.IFlushable
```

- **Description:** Thread-safe command that accepts four arguments.
- **Inheritance:** [ICommand&lt;T1, T2, T3, T4&gt;](ICommand.md), `MainThreadDispatcher.IFlushable`
- **Type Parameters:**
  - `T1` — The first argument type.
  - `T2` — The second argument type.
  - `T3` — The third argument type.
  - `T4` — The fourth argument type.
- **Notes:** All mutating methods are protected by a lock. `OnEvent` is flushed on the main thread with the captured arguments.
- **See also:** [Command&lt;T1, T2, T3, T4&gt;](Command.md)

#### Events

##### `OnEvent`

```csharp
public event Action<T1, T2, T3, T4> OnEvent;
```

- **Description:** Raised on the main thread after the action is successfully invoked, passing all arguments.

#### Methods

##### `CanInvoke(T1, T2, T3, T4)`

```csharp
public bool CanInvoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4);
```

- **Description:** Returns `true` only if all registered conditions pass.
- **Parameters:**
  - `arg1` — The first argument.
  - `arg2` — The second argument.
  - `arg3` — The third argument.
  - `arg4` — The fourth argument.
- **Returns:** `true` when the command can be invoked; otherwise `false`.
- **Thread Safety:** Reads the condition array under a lock.

##### `TryInvoke(T1, T2, T3, T4)`

```csharp
public bool TryInvoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4);
```

- **Description:** Invokes the action if all conditions pass, then schedules `OnEvent` for the main thread.
- **Parameters:**
  - `arg1` — The first argument.
  - `arg2` — The second argument.
  - `arg3` — The third argument.
  - `arg4` — The fourth argument.
- **Returns:** `true` if the command executed; otherwise `false`.
- **Thread Safety:** Reads the action and conditions under a lock; the action runs on the calling thread.

##### `Invoke(T1, T2, T3, T4)`

```csharp
public void Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4);
```

- **Description:** Invokes the action if all conditions pass.
- **Parameters:**
  - `arg1` — The first argument.
  - `arg2` — The second argument.
  - `arg3` — The third argument.
  - `arg4` — The fourth argument.
- **Thread Safety:** Reads the action and conditions under a lock; the action runs on the calling thread.

##### `AddCondition(Func<T1, T2, T3, T4, bool>)`

```csharp
public ICommand<T1, T2, T3, T4> AddCondition(Func<T1, T2, T3, T4, bool> condition);
```

- **Description:** Adds a four-argument condition.
- **Parameter:** `condition` — Predicate that returns `true` for execution.
- **Returns:** The command instance for chaining.
- **Thread Safety:** Mutates the condition array under a lock.

##### `RemoveCondition(Func<T1, T2, T3, T4, bool>)`

```csharp
public ICommand<T1, T2, T3, T4> RemoveCondition(Func<T1, T2, T3, T4, bool> condition);
```

- **Description:** Removes a previously added condition.
- **Parameter:** `condition` — The condition delegate to remove.
- **Returns:** The command instance for chaining.
- **Thread Safety:** Mutates the condition array under a lock.

##### `AddAction(Action<T1, T2, T3, T4>)`

```csharp
public ICommand<T1, T2, T3, T4> AddAction(Action<T1, T2, T3, T4> action);
```

- **Description:** Adds an action to invoke when conditions pass.
- **Parameter:** `action` — The action delegate to add.
- **Returns:** The command instance for chaining.
- **Thread Safety:** Mutates the action delegate under a lock.

##### `RemoveAction(Action<T1, T2, T3, T4>)`

```csharp
public ICommand<T1, T2, T3, T4> RemoveAction(Action<T1, T2, T3, T4> action);
```

- **Description:** Removes a previously added action.
- **Parameter:** `action` — The action delegate to remove.
- **Returns:** The command instance for chaining.
- **Thread Safety:** Mutates the action delegate under a lock.
