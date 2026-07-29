# 🧩 Subscription

A disposable handle returned when subscribing to an event on an [IEventBus](../Bus/IEventBus.md). Disposing the
subscription unsubscribes the callback.

---

## 📑 Table of Contents

- [Example of Usage](#-example-of-usage)
- [API Reference](#-api-reference)
  - [Type](#-type)
  - [Methods](#-methods)
    - [Dispose()](#dispose)

---

## 🗂 Example of Usage

Subscribe and let the subscription unsubscribe automatically when disposed:

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

### 🏛️ Type

```csharp
public readonly struct Subscription : IDisposable
```

- **Description:** A parameterless event subscription handle.
- **Inheritance:** `IDisposable`
- **Notes:**
  - Instances are created internally by [IEventBus](../Bus/IEventBus.md) subscribe methods.
  - Generic variants exist for events with one, two, and three arguments:
    `Subscription<T>`, `Subscription<T1, T2>`, and `Subscription<T1, T2, T3>`.
- **See also:** [IEventBus](../Bus/IEventBus.md), [EventKey](../Keys/EventKey.md)

---

### 🏹 Methods

#### `Dispose()`

```csharp
public void Dispose()
```

- **Description:** Unsubscribes the callback from the event bus.
- **Remarks:** Calling `Dispose` more than once is safe because the underlying bus ignores duplicate unsubscriptions.
