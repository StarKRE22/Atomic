# 🧩 Atomic.Events.Subscriptions

The **Atomic.Events.Subscriptions** namespace provides disposable subscription handles returned by event buses.
A subscription captures the bus, event key, and delegate so that unsubscribing is as simple as calling `Dispose()`.

---

## 📑 Table of Contents

- [Examples of Usage](#-examples-of-usage)
  - [Using Disposable Subscriptions](#using-disposable-subscriptions)
  - [Manual Unsubscribe](#manual-unsubscribe)
- [API Reference](#-api-reference)
- [Best Practices](#-best-practices)

---

## 🗂 Examples of Usage

### Using Disposable Subscriptions

```csharp
IEventBus bus = new EventBus();

using (bus.Subscribe(1, () => Debug.Log("Hello!")))
{
    bus.Invoke(1); // Output: Hello!
}

bus.Invoke(1); // No output: subscription was disposed
```

With source-generated keys:

```csharp
using var subscription = bus.SubscribeDamageDealt(amount => Debug.Log($"Damage: {amount}"));
bus.InvokeDamageDealt(10);
// subscription is disposed automatically at the end of the scope
```

### Manual Unsubscribe

```csharp
var subscription = bus.SubscribeDamageDealt(amount => Debug.Log(amount));

bus.InvokeDamageDealt(5); // Output: 5

subscription.Dispose();

bus.InvokeDamageDealt(5); // No output
```

---

## 🔍 API Reference

- [Subscription](Subscription.md) — parameterless event subscription

> Generic variants (`Subscription<T>`, `Subscription<T1, T2>`, `Subscription<T1, T2, T3>`) are also available for
> parameterized events.

---

## 📌 Best Practices

- Always dispose subscriptions when the listener is destroyed or no longer needs events.
- Prefer `using` declarations for scoped listeners.
- Dispose in `OnDisable`, `Dispose`, or scene-unload handlers to avoid memory leaks.
- Avoid capturing heavy state in anonymous subscription callbacks.
