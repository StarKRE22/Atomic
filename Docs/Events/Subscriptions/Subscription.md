# 🧩 Subscription

**Subscription** is a disposable handle returned when subscribing to an event on an [IEventBus](../Bus/IEventBus.md).
Disposing the subscription unsubscribes the callback.

---

## 📑 Table of Contents

- [Overview](#-overview)
- [Variants](#-variants)
- [Examples of Usage](#-examples-of-usage)
- [API Reference](#-api-reference)

---

## 🧩 Overview

Subscriptions provide a safe, explicit way to manage event listener lifetime. Instead of calling `Unsubscribe` manually,
you can dispose the subscription struct.

---

## 🔍 Variants

| Struct | Description |
|--------|-------------|
| `Subscription` | Parameterless event subscription. |
| `Subscription<T>` | Single-argument event subscription. |
| `Subscription<T1, T2>` | Two-argument event subscription. |
| `Subscription<T1, T2, T3>` | Three-argument event subscription. |

---

## 🗂 Examples of Usage

```csharp
IEventBus eventBus = new EventBus();

using (var subscription = eventBus.Subscribe(GameEventAPI.PlayerTurnStarted.Id, () =>
{
    Debug.Log("Player turn started");
}))
{
    eventBus.Invoke(GameEventAPI.PlayerTurnStarted.Id);
} // automatically unsubscribed here
```

---

## 🔍 API Reference

### Subscription

```csharp
public readonly struct Subscription : IDisposable
```

#### `Dispose()`

```csharp
public void Dispose()
```

- **Description:** Unsubscribes the callback from the event bus.

### Subscription\<T\>

```csharp
public readonly struct Subscription<T> : IDisposable
```

Same API as `Subscription`, but stores a single-argument callback.
