# 🧩 ICommand

A family of **interfaces** that represent conditional invocable actions. Unlike plain [IAction](../Actions/IAction.md), a command checks whether it can be invoked before executing, and raises an `OnEvent` signal on success.

---

## 📑 Table of Contents

- [Example of Usage](#-example-of-usage)
- [API Reference](#-api-reference)
  - [ICommand](#-icommand)
    - [Type](#type)
    - [Events](#events)
    - [Methods](#methods)
  - [ICommand&lt;T&gt;](#-icommandt)
    - [Type](#type-1)
    - [Events](#events-1)
    - [Methods](#methods-1)
  - [ICommand&lt;T1, T2&gt;](#-icommandt1-t2)
    - [Type](#type-2)
    - [Events](#events-2)
    - [Methods](#methods-2)
  - [ICommand&lt;T1, T2, T3&gt;](#-icommandt1-t2-t3)
    - [Type](#type-3)
    - [Events](#events-3)
    - [Methods](#methods-3)
  - [ICommand&lt;T1, T2, T3, T4&gt;](#-icommandt1-t2-t3-t4)
    - [Type](#type-4)
    - [Events](#events-4)
    - [Methods](#methods-4)

---

## 🗂 Example of Usage

### Parameterless command

```csharp
ICommand attackCommand = new Command();

attackCommand
    .AddCondition(() => player.Stamina >= 10)
    .AddAction(() =>
    {
        player.Stamina -= 10;
        enemy.Health -= 20;
    });

bool success = attackCommand.TryInvoke();
```

### Parameterized command

```csharp
ICommand<int> healCommand = new Command<int>();

healCommand
    .AddCondition(amount => player.IsAlive)
    .AddAction(amount =>
    {
        player.Health = Mathf.Min(player.MaxHealth, player.Health + amount);
    });

bool healed = healCommand.TryInvoke(25);
```

---

## 🔍 API Reference

### 🏛️ ICommand

#### Type

```csharp
public interface ICommand : IAction, ISignal
```

- **Description:** Parameterless command interface.
- **Inheritance:** [IAction](../Actions/IAction.md), [ISignal](../Events/ISignal.md)
- **Type Parameters:** None
- **Notes:** Implementations must evaluate all registered conditions before invoking the action.
- **See also:** [Command](Command.md), [ThreadSafeCommand](ThreadSafeCommand.md)

#### Events

##### `OnEvent`

```csharp
public event Action OnEvent;
```

- **Description:** Raised after the command is successfully invoked.
- **See also:** [ISignal](../Events/ISignal.md)

#### Methods

##### `CanInvoke()`

```csharp
public bool CanInvoke();
```

- **Description:** Returns `true` only if all registered conditions pass.
- **Returns:** `true` when the command can be invoked; otherwise `false`.

##### `TryInvoke()`

```csharp
public bool TryInvoke();
```

- **Description:** Invokes the action and raises `OnEvent` if all conditions pass.
- **Returns:** `true` if the command executed; otherwise `false`.

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

### 🏛️ ICommand&lt;T&gt;

#### Type

```csharp
public interface ICommand<T> : IAction<T>, ISignal<T>
```

- **Description:** Command interface that accepts one argument.
- **Inheritance:** [IAction&lt;T&gt;](../Actions/IAction%601.md), [ISignal&lt;T&gt;](../Events/ISignal%601.md)
- **Type Parameters:** `T` — The argument type.
- **See also:** [Command&lt;T&gt;](Command.md), [ThreadSafeCommand&lt;T&gt;](ThreadSafeCommand.md)

#### Events

##### `OnEvent`

```csharp
public event Action<T> OnEvent;
```

- **Description:** Raised after the command is successfully invoked, passing the argument.
- **See also:** [ISignal&lt;T&gt;](../Events/ISignal%601.md)

#### Methods

##### `CanInvoke(T)`

```csharp
public bool CanInvoke(T arg);
```

- **Description:** Returns `true` only if all registered conditions pass for `arg`.
- **Parameter:** `arg` — The argument to evaluate.
- **Returns:** `true` when the command can be invoked; otherwise `false`.

##### `TryInvoke(T)`

```csharp
public bool TryInvoke(T arg);
```

- **Description:** Invokes the action and raises `OnEvent` if all conditions pass.
- **Parameter:** `arg` — The argument to pass.
- **Returns:** `true` if the command executed; otherwise `false`.

##### `AddCondition(Func<T, bool>)`

```csharp
public ICommand<T> AddCondition(Func<T, bool> condition);
```

- **Description:** Adds a single-argument condition.
- **Parameter:** `condition` — Predicate that returns `true` for execution.
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

### 🏛️ ICommand&lt;T1, T2&gt;

#### Type

```csharp
public interface ICommand<T1, T2> : IAction<T1, T2>, ISignal<T1, T2>
```

- **Description:** Command interface that accepts two arguments.
- **Inheritance:** [IAction&lt;T1, T2&gt;](../Actions/IAction%602.md), [ISignal&lt;T1, T2&gt;](../Events/ISignal%602.md)
- **Type Parameters:**
  - `T1` — The first argument type.
  - `T2` — The second argument type.
- **See also:** [Command&lt;T1, T2&gt;](Command.md)

#### Events

##### `OnEvent`

```csharp
public event Action<T1, T2> OnEvent;
```

- **Description:** Raised after the command is successfully invoked, passing both arguments.

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

##### `TryInvoke(T1, T2)`

```csharp
public bool TryInvoke(T1 arg1, T2 arg2);
```

- **Description:** Invokes the action and raises `OnEvent` if all conditions pass.
- **Parameters:**
  - `arg1` — The first argument.
  - `arg2` — The second argument.
- **Returns:** `true` if the command executed; otherwise `false`.

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

### 🏛️ ICommand&lt;T1, T2, T3&gt;

#### Type

```csharp
public interface ICommand<T1, T2, T3> : IAction<T1, T2, T3>, ISignal<T1, T2, T3>
```

- **Description:** Command interface that accepts three arguments.
- **Inheritance:** [IAction&lt;T1, T2, T3&gt;](../Actions/IAction%603.md), [ISignal&lt;T1, T2, T3&gt;](../Events/ISignal%603.md)
- **Type Parameters:**
  - `T1` — The first argument type.
  - `T2` — The second argument type.
  - `T3` — The third argument type.
- **See also:** [Command&lt;T1, T2, T3&gt;](Command.md)

#### Events

##### `OnEvent`

```csharp
public event Action<T1, T2, T3> OnEvent;
```

- **Description:** Raised after the command is successfully invoked, passing all arguments.

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

##### `TryInvoke(T1, T2, T3)`

```csharp
public bool TryInvoke(T1 arg1, T2 arg2, T3 arg3);
```

- **Description:** Invokes the action and raises `OnEvent` if all conditions pass.
- **Parameters:**
  - `arg1` — The first argument.
  - `arg2` — The second argument.
  - `arg3` — The third argument.
- **Returns:** `true` if the command executed; otherwise `false`.

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

### 🏛️ ICommand&lt;T1, T2, T3, T4&gt;

#### Type

```csharp
public interface ICommand<T1, T2, T3, T4> : IAction<T1, T2, T3, T4>, ISignal<T1, T2, T3, T4>
```

- **Description:** Command interface that accepts four arguments.
- **Inheritance:** [IAction&lt;T1, T2, T3, T4&gt;](../Actions/IAction%604.md), [ISignal&lt;T1, T2, T3, T4&gt;](../Events/ISignal%604.md)
- **Type Parameters:**
  - `T1` — The first argument type.
  - `T2` — The second argument type.
  - `T3` — The third argument type.
  - `T4` — The fourth argument type.
- **See also:** [Command&lt;T1, T2, T3, T4&gt;](Command.md)

#### Events

##### `OnEvent`

```csharp
public event Action<T1, T2, T3, T4> OnEvent;
```

- **Description:** Raised after the command is successfully invoked, passing all arguments.

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

##### `TryInvoke(T1, T2, T3, T4)`

```csharp
public bool TryInvoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4);
```

- **Description:** Invokes the action and raises `OnEvent` if all conditions pass.
- **Parameters:**
  - `arg1` — The first argument.
  - `arg2` — The second argument.
  - `arg3` — The third argument.
  - `arg4` — The fourth argument.
- **Returns:** `true` if the command executed; otherwise `false`.

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
