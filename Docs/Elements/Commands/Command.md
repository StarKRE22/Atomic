# 🧩 Command

A family of **sealed command implementations** that guard invocation with configurable conditions. Each variant stores conditions, an action delegate, and an `OnEvent` signal, supporting fluent chaining for gameplay logic.

---

## 📑 Table of Contents

- [Example of Usage](#-example-of-usage)
- [API Reference](#-api-reference)
  - [Command](#-command)
    - [Type](#type)
    - [Events](#events)
    - [Methods](#methods)
  - [Command&lt;T&gt;](#-commandt)
    - [Type](#type-1)
    - [Events](#events-1)
    - [Methods](#methods-1)
  - [Command&lt;T1, T2&gt;](#-commandt1-t2)
    - [Type](#type-2)
    - [Events](#events-2)
    - [Methods](#methods-2)
  - [Command&lt;T1, T2, T3&gt;](#-commandt1-t2-t3)
    - [Type](#type-3)
    - [Events](#events-3)
    - [Methods](#methods-3)
  - [Command&lt;T1, T2, T3, T4&gt;](#-commandt1-t2-t3-t4)
    - [Type](#type-4)
    - [Events](#events-4)
    - [Methods](#methods-4)

---

## 🗂 Example of Usage

### Parameterless command

```csharp
ICommand jumpCommand = new Command()
    .AddCondition(() => player.IsGrounded)
    .AddCondition(() => player.Stamina >= 5)
    .AddAction(() =>
    {
        player.Stamina -= 5;
        player.Jump();
    });

bool jumped = jumpCommand.TryInvoke();
```

### Parameterized command

```csharp
ICommand<Vector3> moveCommand = new Command<Vector3>()
    .AddCondition(destination => player.CanMove)
    .AddAction(destination => player.MoveTo(destination));

bool moved = moveCommand.TryInvoke(new Vector3(10, 0, 0));
```

---

## 🔍 API Reference

### 🏛️ Command

#### Type

```csharp
public sealed class Command : ICommand
```

- **Description:** A parameterless command guarded by conditions.
- **Inheritance:** [ICommand](ICommand.md)
- **Type Parameters:** None
- **Notes:** All mutating methods return the command instance for fluent chaining.
- **See also:** [ICommand](ICommand.md), [ThreadSafeCommand](ThreadSafeCommand.md)

#### Events

##### `OnEvent`

```csharp
public event Action OnEvent;
```

- **Description:** Raised after the action is successfully invoked.
- **See also:** [ISignal](../Events/ISignal.md)

#### Methods

##### `CanInvoke()`

```csharp
#if ODIN_INSPECTOR
[Button]
#endif
public bool CanInvoke();
```

- **Description:** Returns `true` only if all registered conditions pass.
- **Returns:** `true` when the command can be invoked; otherwise `false`.

##### `TryInvoke()`

```csharp
#if ODIN_INSPECTOR
[Button]
#endif
public bool TryInvoke();
```

- **Description:** Invokes the action and raises `OnEvent` if all conditions pass.
- **Returns:** `true` if the command executed; otherwise `false`.

##### `Invoke()`

```csharp
#if ODIN_INSPECTOR
[Button]
#endif
public void Invoke();
```

- **Description:** Invokes the action if all conditions pass.
- **Notes:** Prefer `TryInvoke()` when the result is needed.

##### `AddCondition(Func<bool>)`

```csharp
public ICommand AddCondition(Func<bool> condition);
```

- **Description:** Adds a parameterless condition.
- **Parameter:** `condition` — Predicate that must return `true` for execution.
- **Returns:** The command instance for chaining.

##### `RemoveCondition(Func<bool>)`

```csharp
public ICommand RemoveCondition(Func<bool> condition);
```

- **Description:** Removes a previously added condition.
- **Parameter:** `condition` — The condition delegate to remove.
- **Returns:** The command instance for chaining.

##### `AddAction(Action)`

```csharp
public ICommand AddAction(Action action);
```

- **Description:** Adds an action to invoke when conditions pass.
- **Parameter:** `action` — The action delegate to add.
- **Returns:** The command instance for chaining.

##### `RemoveAction(Action)`

```csharp
public ICommand RemoveAction(Action action);
```

- **Description:** Removes a previously added action.
- **Parameter:** `action` — The action delegate to remove.
- **Returns:** The command instance for chaining.

---

### 🏛️ Command&lt;T&gt;

#### Type

```csharp
public sealed class Command<T> : ICommand<T>
```

- **Description:** A command that accepts one argument.
- **Inheritance:** [ICommand&lt;T&gt;](ICommand.md)
- **Type Parameters:** `T` — The argument type.
- **Notes:** All mutating methods return the command instance for fluent chaining.
- **See also:** [ICommand&lt;T&gt;](ICommand.md)

#### Events

##### `OnEvent`

```csharp
public event Action<T> OnEvent;
```

- **Description:** Raised after the action is successfully invoked, passing the argument.
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

##### `TryInvoke(T)`

```csharp
#if ODIN_INSPECTOR
[Button]
#endif
public bool TryInvoke(T arg);
```

- **Description:** Invokes the action and raises `OnEvent` if all conditions pass.
- **Parameter:** `arg` — The argument to pass.
- **Returns:** `true` if the command executed; otherwise `false`.

##### `Invoke(T)`

```csharp
#if ODIN_INSPECTOR
[Button]
#endif
public void Invoke(T arg);
```

- **Description:** Invokes the action if all conditions pass.
- **Parameter:** `arg` — The argument to pass.

##### `AddCondition(Func<T, bool>)`

```csharp
public ICommand<T> AddCondition(Func<T, bool> condition);
```

- **Description:** Adds a single-argument condition.
- **Parameter:** `condition` — Predicate that receives the argument and returns `true` for execution.
- **Returns:** The command instance for chaining.

##### `RemoveCondition(Func<T, bool>)`

```csharp
public ICommand<T> RemoveCondition(Func<T, bool> condition);
```

- **Description:** Removes a previously added condition.
- **Parameter:** `condition` — The condition delegate to remove.
- **Returns:** The command instance for chaining.

##### `AddAction(Action<T>)`

```csharp
public ICommand<T> AddAction(Action<T> action);
```

- **Description:** Adds an action to invoke when conditions pass.
- **Parameter:** `action` — The action delegate to add.
- **Returns:** The command instance for chaining.

##### `RemoveAction(Action<T>)`

```csharp
public ICommand<T> RemoveAction(Action<T> action);
```

- **Description:** Removes a previously added action.
- **Parameter:** `action` — The action delegate to remove.
- **Returns:** The command instance for chaining.

---

### 🏛️ Command&lt;T1, T2&gt;

#### Type

```csharp
public sealed class Command<T1, T2> : ICommand<T1, T2>
```

- **Description:** A command that accepts two arguments.
- **Inheritance:** [ICommand&lt;T1, T2&gt;](ICommand.md)
- **Type Parameters:**
  - `T1` — The first argument type.
  - `T2` — The second argument type.
- **Notes:** All mutating methods return the command instance for fluent chaining.
- **See also:** [ICommand&lt;T1, T2&gt;](ICommand.md)

#### Events

##### `OnEvent`

```csharp
public event Action<T1, T2> OnEvent;
```

- **Description:** Raised after the action is successfully invoked, passing both arguments.

#### Methods

##### `CanInvoke(T1, T2)`

```csharp
#if ODIN_INSPECTOR
[Button]
#endif
public bool CanInvoke(T1 arg1, T2 arg2);
```

- **Description:** Returns `true` only if all registered conditions pass.
- **Parameters:**
  - `arg1` — The first argument.
  - `arg2` — The second argument.
- **Returns:** `true` when the command can be invoked; otherwise `false`.

##### `TryInvoke(T1, T2)`

```csharp
#if ODIN_INSPECTOR
[Button]
#endif
public bool TryInvoke(T1 arg1, T2 arg2);
```

- **Description:** Invokes the action and raises `OnEvent` if all conditions pass.
- **Parameters:**
  - `arg1` — The first argument.
  - `arg2` — The second argument.
- **Returns:** `true` if the command executed; otherwise `false`.

##### `Invoke(T1, T2)`

```csharp
#if ODIN_INSPECTOR
[Button]
#endif
public void Invoke(T1 arg1, T2 arg2);
```

- **Description:** Invokes the action if all conditions pass.
- **Parameters:**
  - `arg1` — The first argument.
  - `arg2` — The second argument.

##### `AddCondition(Func<T1, T2, bool>)`

```csharp
public ICommand<T1, T2> AddCondition(Func<T1, T2, bool> condition);
```

- **Description:** Adds a two-argument condition.
- **Parameter:** `condition` — Predicate that returns `true` for execution.
- **Returns:** The command instance for chaining.

##### `RemoveCondition(Func<T1, T2, bool>)`

```csharp
public ICommand<T1, T2> RemoveCondition(Func<T1, T2, bool> condition);
```

- **Description:** Removes a previously added condition.
- **Parameter:** `condition` — The condition delegate to remove.
- **Returns:** The command instance for chaining.

##### `AddAction(Action<T1, T2>)`

```csharp
public ICommand<T1, T2> AddAction(Action<T1, T2> action);
```

- **Description:** Adds an action to invoke when conditions pass.
- **Parameter:** `action` — The action delegate to add.
- **Returns:** The command instance for chaining.

##### `RemoveAction(Action<T1, T2>)`

```csharp
public ICommand<T1, T2> RemoveAction(Action<T1, T2> action);
```

- **Description:** Removes a previously added action.
- **Parameter:** `action` — The action delegate to remove.
- **Returns:** The command instance for chaining.

---

### 🏛️ Command&lt;T1, T2, T3&gt;

#### Type

```csharp
public sealed class Command<T1, T2, T3> : ICommand<T1, T2, T3>
```

- **Description:** A command that accepts three arguments.
- **Inheritance:** [ICommand&lt;T1, T2, T3&gt;](ICommand.md)
- **Type Parameters:**
  - `T1` — The first argument type.
  - `T2` — The second argument type.
  - `T3` — The third argument type.
- **Notes:** All mutating methods return the command instance for fluent chaining.
- **See also:** [ICommand&lt;T1, T2, T3&gt;](ICommand.md)

#### Events

##### `OnEvent`

```csharp
public event Action<T1, T2, T3> OnEvent;
```

- **Description:** Raised after the action is successfully invoked, passing all arguments.

#### Methods

##### `CanInvoke(T1, T2, T3)`

```csharp
#if ODIN_INSPECTOR
[Button]
#endif
public bool CanInvoke(T1 arg1, T2 arg2, T3 arg3);
```

- **Description:** Returns `true` only if all registered conditions pass.
- **Parameters:**
  - `arg1` — The first argument.
  - `arg2` — The second argument.
  - `arg3` — The third argument.
- **Returns:** `true` when the command can be invoked; otherwise `false`.

##### `TryInvoke(T1, T2, T3)`

```csharp
#if ODIN_INSPECTOR
[Button]
#endif
public bool TryInvoke(T1 arg1, T2 arg2, T3 arg3);
```

- **Description:** Invokes the action and raises `OnEvent` if all conditions pass.
- **Parameters:**
  - `arg1` — The first argument.
  - `arg2` — The second argument.
  - `arg3` — The third argument.
- **Returns:** `true` if the command executed; otherwise `false`.

##### `Invoke(T1, T2, T3)`

```csharp
#if ODIN_INSPECTOR
[Button]
#endif
public void Invoke(T1 arg1, T2 arg2, T3 arg3);
```

- **Description:** Invokes the action if all conditions pass.
- **Parameters:**
  - `arg1` — The first argument.
  - `arg2` — The second argument.
  - `arg3` — The third argument.

##### `AddCondition(Func<T1, T2, T3, bool>)`

```csharp
public ICommand<T1, T2, T3> AddCondition(Func<T1, T2, T3, bool> condition);
```

- **Description:** Adds a three-argument condition.
- **Parameter:** `condition` — Predicate that returns `true` for execution.
- **Returns:** The command instance for chaining.

##### `RemoveCondition(Func<T1, T2, T3, bool>)`

```csharp
public ICommand<T1, T2, T3> RemoveCondition(Func<T1, T2, T3, bool> condition);
```

- **Description:** Removes a previously added condition.
- **Parameter:** `condition` — The condition delegate to remove.
- **Returns:** The command instance for chaining.

##### `AddAction(Action<T1, T2, T3>)`

```csharp
public ICommand<T1, T2, T3> AddAction(Action<T1, T2, T3> action);
```

- **Description:** Adds an action to invoke when conditions pass.
- **Parameter:** `action` — The action delegate to add.
- **Returns:** The command instance for chaining.

##### `RemoveAction(Action<T1, T2, T3>)`

```csharp
public ICommand<T1, T2, T3> RemoveAction(Action<T1, T2, T3> action);
```

- **Description:** Removes a previously added action.
- **Parameter:** `action` — The action delegate to remove.
- **Returns:** The command instance for chaining.

---

### 🏛️ Command&lt;T1, T2, T3, T4&gt;

#### Type

```csharp
public sealed class Command<T1, T2, T3, T4> : ICommand<T1, T2, T3, T4>
```

- **Description:** A command that accepts four arguments.
- **Inheritance:** [ICommand&lt;T1, T2, T3, T4&gt;](ICommand.md)
- **Type Parameters:**
  - `T1` — The first argument type.
  - `T2` — The second argument type.
  - `T3` — The third argument type.
  - `T4` — The fourth argument type.
- **Notes:** All mutating methods return the command instance for fluent chaining.
- **See also:** [ICommand&lt;T1, T2, T3, T4&gt;](ICommand.md)

#### Events

##### `OnEvent`

```csharp
public event Action<T1, T2, T3, T4> OnEvent;
```

- **Description:** Raised after the action is successfully invoked, passing all arguments.

#### Methods

##### `CanInvoke(T1, T2, T3, T4)`

```csharp
#if ODIN_INSPECTOR
[Button]
#endif
public bool CanInvoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4);
```

- **Description:** Returns `true` only if all registered conditions pass.
- **Parameters:**
  - `arg1` — The first argument.
  - `arg2` — The second argument.
  - `arg3` — The third argument.
  - `arg4` — The fourth argument.
- **Returns:** `true` when the command can be invoked; otherwise `false`.

##### `TryInvoke(T1, T2, T3, T4)`

```csharp
#if ODIN_INSPECTOR
[Button]
#endif
public bool TryInvoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4);
```

- **Description:** Invokes the action and raises `OnEvent` if all conditions pass.
- **Parameters:**
  - `arg1` — The first argument.
  - `arg2` — The second argument.
  - `arg3` — The third argument.
  - `arg4` — The fourth argument.
- **Returns:** `true` if the command executed; otherwise `false`.

##### `Invoke(T1, T2, T3, T4)`

```csharp
#if ODIN_INSPECTOR
[Button]
#endif
public void Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4);
```

- **Description:** Invokes the action if all conditions pass.
- **Parameters:**
  - `arg1` — The first argument.
  - `arg2` — The second argument.
  - `arg3` — The third argument.
  - `arg4` — The fourth argument.

##### `AddCondition(Func<T1, T2, T3, T4, bool>)`

```csharp
public ICommand<T1, T2, T3, T4> AddCondition(Func<T1, T2, T3, T4, bool> condition);
```

- **Description:** Adds a four-argument condition.
- **Parameter:** `condition` — Predicate that returns `true` for execution.
- **Returns:** The command instance for chaining.

##### `RemoveCondition(Func<T1, T2, T3, T4, bool>)`

```csharp
public ICommand<T1, T2, T3, T4> RemoveCondition(Func<T1, T2, T3, T4, bool> condition);
```

- **Description:** Removes a previously added condition.
- **Parameter:** `condition` — The condition delegate to remove.
- **Returns:** The command instance for chaining.

##### `AddAction(Action<T1, T2, T3, T4>)`

```csharp
public ICommand<T1, T2, T3, T4> AddAction(Action<T1, T2, T3, T4> action);
```

- **Description:** Adds an action to invoke when conditions pass.
- **Parameter:** `action` — The action delegate to add.
- **Returns:** The command instance for chaining.

##### `RemoveAction(Action<T1, T2, T3, T4>)`

```csharp
public ICommand<T1, T2, T3, T4> RemoveAction(Action<T1, T2, T3, T4> action);
```

- **Description:** Removes a previously added action.
- **Parameter:** `action` — The action delegate to remove.
- **Returns:** The command instance for chaining.
