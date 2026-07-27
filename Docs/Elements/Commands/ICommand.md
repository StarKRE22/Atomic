# 🧩 ICommand

**ICommand** is a family of interfaces that represent an invocable action guarded by conditions. Unlike a plain
[IAction](../Actions/IAction.md), a command checks whether it **can** be invoked, and only then executes its
action. Commands support dynamic conditions and actions, making them ideal for gameplay logic such as abilities,
interactions, or UI buttons.

---

## 📑 Table of Contents

- [Overview](#-overview)
- [Command Interfaces](#-command-interfaces)
  - [ICommand](#icommand)
  - [ICommand\<T\>](#icommandt)
  - [ICommand\<T1, T2\>](#icommandt1-t2)
  - [ICommand\<T1, T2, T3\>](#icommandt1-t2-t3)
  - [ICommand\<T1, T2, T3, T4\>](#icommandt1-t2-t3-t4)
- [Examples of Usage](#-examples-of-usage)
  - [Parameterless Command](#parameterless-command)
  - [Parameterized Command](#parameterized-command)
- [Best Practices](#-best-practices)

---

## 🧩 Overview

A command combines:

- **Conditions** — predicates that must all return `true` for the command to execute
- **Actions** — delegates invoked when all conditions pass
- **Signals** — events (`OnEvent`) raised after successful invocation

A command inherits from [IAction](../Actions/IAction.md) for invocation and [ISignal](../Events/ISignal.md) for event raising.

Commands provide two main invocation methods:

| Method | Behavior |
|--------|----------|
| `CanInvoke(...)` | Returns `true` only if all conditions pass. Does **not** execute the action. |
| `TryInvoke(...)` | Checks conditions, executes the action and raises `OnEvent` if they pass, then returns `true`. Returns `false` otherwise. |
| `Invoke(...)` | Checks conditions and executes the action if they pass. Does **not** return a value. |

`TryInvoke()` is the safest way to invoke a command because it tells you whether execution actually happened.

---

## 🔍 Command Interfaces

### ICommand

```csharp
public interface ICommand : IAction, ISignal
```

Parameterless command.

| Member | Description |
|--------|-------------|
| `bool CanInvoke()` | Checks all registered conditions. |
| `bool TryInvoke()` | Invokes the action if conditions pass; returns success. |
| `ICommand AddCondition(Func<bool> condition)` | Adds a parameterless condition. |
| `ICommand RemoveCondition(Func<bool> condition)` | Removes a previously added condition. |
| `ICommand AddAction(Action action)` | Adds an action to invoke on success. |
| `ICommand RemoveAction(Action action)` | Removes a previously added action. |

### ICommand\<T\>

```csharp
public interface ICommand<T> : IAction<T>, ISignal<T>
```

Command that accepts one argument.

| Member | Description |
|--------|-------------|
| `bool CanInvoke(T arg)` | Checks all registered conditions with `arg`. |
| `bool TryInvoke(T arg)` | Invokes the action if conditions pass; returns success. |
| `ICommand<T> AddCondition(Func<T, bool> condition)` | Adds a single-argument condition. |
| `ICommand<T> AddAction(Action<T> action)` | Adds a single-argument action. |

### ICommand\<T1, T2\>

```csharp
public interface ICommand<T1, T2> : IAction<T1, T2>, ISignal<T1, T2>
```

Command that accepts two arguments.

### ICommand\<T1, T2, T3\>

```csharp
public interface ICommand<T1, T2, T3> : IAction<T1, T2, T3>, ISignal<T1, T2, T3>
```

Command that accepts three arguments.

### ICommand\<T1, T2, T3, T4\>

```csharp
public interface ICommand<T1, T2, T3, T4> : IAction<T1, T2, T3, T4>, ISignal<T1, T2, T3, T4>
```

Command that accepts four arguments.

---

## 🗂 Examples of Usage

### Parameterless Command

```csharp
ICommand attackCommand = new Command();

// Add a condition: can only attack when stamina is sufficient
attackCommand.AddCondition(() => player.Stamina >= 10);

// Add the action to perform
attackCommand.AddAction(() =>
{
    player.Stamina -= 10;
    enemy.Health -= 20;
});

// Try to invoke
bool success = attackCommand.TryInvoke();
if (!success)
    Debug.Log("Not enough stamina!");
```

### Parameterized Command

```csharp
ICommand<int> healCommand = new Command<int>();

// Condition: can heal only when alive
healCommand.AddCondition(amount => player.IsAlive);

// Action: restore health
healCommand.AddAction(amount =>
{
    player.Health = Mathf.Min(player.MaxHealth, player.Health + amount);
});

// Try to heal for 25 HP
bool healed = healCommand.TryInvoke(25);
```

---

## 📌 Best Practices

- Use `TryInvoke()` when you need to know whether the command actually executed.
- Use `CanInvoke()` to enable or disable UI buttons based on command availability.
- Keep conditions side-effect free — they should only read state.
- Use `RemoveCondition` and `RemoveAction` to avoid memory leaks when disposing commands.
- For multi-threaded scenarios, use [ThreadSafeCommand](ThreadSafeCommand.md).
- For Unity scene components, see [MonoAction](../Actions/MonoAction.md) and [MonoActionConfigurable](../Actions/MonoActionConfigurable.md).
