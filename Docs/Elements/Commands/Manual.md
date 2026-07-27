# 🧩 Commands

**Commands** provide a conditional action pattern. Unlike plain actions, commands check whether they can be invoked
before executing. They support dynamic conditions, actions, and signals, making them useful for gameplay logic such as
abilities, UI interactions, and state-gated behaviors.

---

## 📑 Table of Contents

- [Examples of Usage](#-examples-of-usage)
  - [Parameterless Command](#parameterless-command)
  - [Parameterized Command](#parameterized-command)
- [API Reference](#-api-reference)
- [Best Practices](#-best-practices)

---

## 🗂 Examples of Usage

### Parameterless Command

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

### Parameterized Command

```csharp
ICommand<int> healCommand = new Command<int>();

healCommand
    .AddCondition(amount => player.IsAlive)
    .AddAction(amount => player.Health += amount);

bool healed = healCommand.TryInvoke(25);
```

---

## 🔍 API Reference

### Interfaces

- [ICommand](ICommand.md) — overview of all command interfaces

### Implementations

- [Command](Command.md) — standard command implementation
- [ThreadSafeCommand](ThreadSafeCommand.md) — thread-safe command implementation

---

## 📌 Best Practices

- Use `TryInvoke()` when you need to know whether the command executed.
- Use `CanInvoke()` to drive UI state such as button enable/disable.
- Keep conditions side-effect free.
- Clean up conditions and actions when disposing to avoid leaks.
- Use `ThreadSafeCommand` for multi-threaded access.
