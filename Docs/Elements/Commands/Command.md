# 🧩 Command

**Command** is a sealed implementation of [ICommand](ICommand.md). It provides a lightweight, composable way to
define invocable logic guarded by conditions. Multiple generic variants are available for commands that accept up to
four arguments.

---

## 📑 Table of Contents

- [Overview](#-overview)
- [Variants](#-variants)
- [API Reference](#-api-reference)
  - [Command](#command)
  - [Command\<T\>](#commandt)
  - [Command\<T1, T2\>](#commandt1-t2)
  - [Command\<T1, T2, T3\>](#commandt1-t2-t3)
  - [Command\<T1, T2, T3, T4\>](#commandt1-t2-t3-t4)
- [Examples of Usage](#-examples-of-usage)
- [Best Practices](#-best-practices)

---

## 🧩 Overview

`Command` is the concrete implementation of the command pattern in the Atomic Elements framework.
Each command stores:

- A list of **conditions** that must all pass before execution
- A single **action** delegate invoked when conditions pass
- An **OnEvent** signal raised after successful execution

The command is **fluent**: `AddCondition`, `RemoveCondition`, `AddAction`, and `RemoveAction` all return the command
instance, allowing chaining.

---

## 🔍 Variants

| Class | Interface | Arguments |
|-------|-----------|-----------|
| `Command` | `ICommand` | 0 |
| `Command<T>` | `ICommand<T>` | 1 |
| `Command<T1, T2>` | `ICommand<T1, T2>` | 2 |
| `Command<T1, T2, T3>` | `ICommand<T1, T2, T3>` | 3 |
| `Command<T1, T2, T3, T4>` | `ICommand<T1, T2, T3, T4>` | 4 |

---

## 🔍 API Reference

### Command

```csharp
public sealed class Command : ICommand
```

#### `CanInvoke()`

```csharp
public bool CanInvoke();
```

- **Description:** Returns `true` if all registered conditions pass.

#### `TryInvoke()`

```csharp
public bool TryInvoke();
```

- **Description:** Invokes the action and raises `OnEvent` if all conditions pass.
- **Returns:** `true` if the command executed, `false` otherwise.

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
- **Parameter:** `condition` — Predicate that must return `true` for the command to execute.
- **Returns:** The command instance for chaining.

#### `RemoveCondition(Func<bool>)`

```csharp
public ICommand RemoveCondition(Func<bool> condition);
```

- **Description:** Removes a previously added condition.
- **Returns:** The command instance for chaining.

#### `AddAction(Action)`

```csharp
public ICommand AddAction(Action action);
```

- **Description:** Adds an action to invoke when conditions pass.
- **Returns:** The command instance for chaining.

#### `RemoveAction(Action)`

```csharp
public ICommand RemoveAction(Action action);
```

- **Description:** Removes a previously added action.
- **Returns:** The command instance for chaining.

### Command\<T\>

```csharp
public sealed class Command<T> : ICommand<T>
```

Same API as `Command`, but methods accept one argument:

- `bool CanInvoke(T arg)`
- `bool TryInvoke(T arg)`
- `void Invoke(T arg)`
- `ICommand<T> AddCondition(Func<T, bool> condition)`
- `ICommand<T> AddAction(Action<T> action)`

### Command\<T1, T2\>

```csharp
public sealed class Command<T1, T2> : ICommand<T1, T2>
```

Accepts two arguments. Methods:

- `bool CanInvoke(T1 arg1, T2 arg2)`
- `bool TryInvoke(T1 arg1, T2 arg2)`
- `void Invoke(T1 arg1, T2 arg2)`

### Command\<T1, T2, T3\>

```csharp
public sealed class Command<T1, T2, T3> : ICommand<T1, T2, T3>
```

Accepts three arguments.

### Command\<T1, T2, T3, T4\>

```csharp
public sealed class Command<T1, T2, T3, T4> : ICommand<T1, T2, T3, T4>
```

Accepts four arguments.

---

## 🗂 Examples of Usage

### Chained Command Setup

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

### Command with Argument

```csharp
ICommand<Vector3> moveCommand = new Command<Vector3>()
    .AddCondition(destination => player.CanMove)
    .AddAction(destination => player.MoveTo(destination));

bool moved = moveCommand.TryInvoke(new Vector3(10, 0, 0));
```

---

## 📌 Best Practices

- Prefer `TryInvoke()` over `Invoke()` when the result matters.
- Use `CanInvoke()` for UI state updates (e.g., graying out a button).
- Avoid side effects in conditions.
- Remove conditions and actions when the owning object is disposed to prevent leaks.
- For thread-safe scenarios, use [ThreadSafeCommand](ThreadSafeCommand.md).
