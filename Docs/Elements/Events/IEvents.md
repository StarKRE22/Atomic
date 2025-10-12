# 🧩 IEvents

Define a family of contracts for **reactive events** that can be both **observed** and **invoked**.
They combine the capabilities of [ISignal](ISignals.md) and [IAction](../Actions/IActions.md),
allowing subscription-based reactive tracking and direct action-based invocation.

---

## 📑 Table of Contents

- [Examples of Usage](#-examples-of-usage)
    - [Non-generic Event](#ex-1)
    - [Event with single argument](#ex-2)
    - [Event with two arguments](#ex-3)
- [API Reference](#-api-reference)
- [Best Practices](#-best-practices)

---

## 🗂 Examples of Usage

### 1️⃣ Non-generic Event <div id="ex-1"></div>

```csharp
var playerDiedEvent = new BaseEvent();

// Subscribe to the event
playerDiedEvent.OnEvent += () => Console.WriteLine("Player died event triggered.");

// Invoke the event
playerDiedEvent.Invoke(); // Output: Player died event triggered.

// Dispose all subscriptions
playerDiedEvent.Dispose();
```

### 2️⃣ Event with single argument <div id="ex-2"></div>

```csharp
var healthChangedEvent = new BaseEvent<int>();

// Subscribe to the event
healthChangedEvent.OnEvent += health => Console.WriteLine($"Health changed to: {health}");

// Invoke the event with a value
healthChangedEvent.Invoke(100); // Output: Health changed to: 100
healthChangedEvent.Invoke(75);  // Output: Health changed to: 75

// Dispose all subscriptions
healthChangedEvent.Dispose();
```

---

### 3️⃣ Event with two arguments <div id="ex-3"></div>

```csharp
IEvent<string, int> attackEvent = ...

// Subscribe to the event
attackEvent.OnEvent += (attacker, damage) =>
{
    Console.WriteLine($"{attacker} hit for {damage} damage.");
};

// Invoke the event
attackEvent.Invoke("Player", 10);
```

---

## 🔍 API Reference

There are several interfaces of events, depending on the number of arguments they take:

- [IEvent](IEvent.md) — Event without parameters.
- [IEvent&lt;T&gt;](IEvent%601.md) — Event that takes one argument.
- [IEvent&lt;T1, T2&gt;](IEvent%602.md) — Event that takes two arguments.
- [IEvent&lt;T1, T2, T3&gt;](IEvent%603.md) — Event that takes three arguments.
- [IEvent&lt;T1, T2, T3, T4&gt;](IEvent%604.md) — Event that takes four arguments.

---

## 📌 Best Practices

- [Using Events with Entities](../../BestPractices/UsingEvents.md)