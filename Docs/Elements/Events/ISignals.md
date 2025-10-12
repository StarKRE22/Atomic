# 🧩 ISignals

Define a family of contracts for **reactive event sources**. It provides a lightweight abstraction for subscribing to
notifications and reacting to events, optionally with arguments.

---

## 📑 Table of Contents

- [Examples of Usage](#-examples-of-usage)
    - [Non-generic Signal](#ex-1)
    - [Signal with single argument](#ex-2)
    - [Signal with three arguments](#ex-2)
- [API Reference](#-api-reference)

---

## 🗂 Examples of Usage

### 1️⃣ Non-generic Signal <div id="ex-1"></div>

```csharp
// Assume we have an instance of "player died event"
ISignal playerDiedEvent = ...

// Subscribe to the event
playerDiedEvent.OnEvent += () => Console.WriteLine("Player died event triggered.");
```

### 2️⃣ Signal with single argument <div id="ex-2"></div>

```csharp
// Assume we have an instance of "health changed event"
ISignal<int> healthChangedEvent = ...

// Subscribe to the event
healthChangedEvent.OnEvent += health => Console.WriteLine($"Health changed to: {health}");
```

### 3️⃣ Signal with three arguments <div id="ex-3"></div>

```csharp
ISignal<string, int, bool> attackEvent = ...

// Subscribe to the event
attackEvent.OnEvent += (attacker, damage, critical) =>
{
    if (critical)
        Console.WriteLine($"{attacker} performed a CRITICAL hit for {damage} damage!");
    else
        Console.WriteLine($"{attacker} hit for {damage} damage.");
};
```

---

## 🔍 API Reference

There are several interfaces of signals, depending on the number of arguments they take:

- [ISignal](ISignal.md) — Signal without parameters.
- [ISignal&lt;T&gt;](ISignal%601.md) — Signal that takes one argument.
- [ISignal&lt;T1, T2&gt;](ISignal%602.md) — Signal that takes two arguments.
- [ISignal&lt;T1, T2, T3&gt;](ISignal%603.md) — Signal that takes three arguments.
- [ISignal&lt;T1, T2, T3, T4&gt;](ISignal%604.md) — Signal that takes four arguments.