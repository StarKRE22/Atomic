# 🧩 BaseEvents

Provide **parameterless and generic reactive events** that can be **subscribed to, invoked,
and disposed**. They implement the corresponding [IEvent](IEvents.md) interfaces and allow both reactive tracking and
action-based invocation.

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
var attackEvent = new BaseEvent<string, int>();

// Subscribe to the event
attackEvent.OnEvent += (attacker, damage) =>
{
    Console.WriteLine($"{attacker} hit for {damage} damage.");
};

// Invoke the event
attackEvent.Invoke("Player", 10);

// Dispose all subscriptions
attackEvent.Dispose();
```

---

## 🔍 API Reference

There are several implementations of events, depending on the number of arguments they take:

- [BaseEvent](BaseEvent.md) — Event without parameters.
- [BaseEvent&lt;T&gt;](BaseEvent%601.md) — Event that takes one argument.
- [BaseEvent&lt;T1, T2&gt;](BaseEvent%602.md) — Event that takes two arguments.
- [BaseEvent&lt;T1, T2, T3&gt;](BaseEvent%603.md) — Event that takes three arguments.
- [BaseEvent&lt;T1, T2, T3, T4&gt;](BaseEvent%604.md) — Event that takes four arguments.

---

## 📌 Best Practices

- [Using Events with Entities](../../BestPractices/UsingEvents.md)