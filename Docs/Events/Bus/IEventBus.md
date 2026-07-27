# 🧩 IEventBus

Core interface for publish-subscribe event messaging in the Atomic.Events module. Supports parameterless and parameterized events, subscriptions, unsubscriptions, and event disposal.

---

## 📑 Table of Contents

- [Example of Usage](#-example-of-usage)
- [API Reference](#-api-reference)
  - [Type](#-type)
  - [Methods](#-methods)
    - [Subscribe](#subscribe)
    - [Unsubscribe](#unsubscribe)
    - [Invoke](#invoke)
    - [IsSubscribed](#issubscribed)
    - [Dispose](#dispose)

---

## 🗂 Example of Usage

### Parameterless Event

```csharp
IEventBus eventBus = new EventBus();

using var subscription = eventBus.Subscribe(GameEventAPI.PlayerTurnStarted.Id, () =>
{
    Debug.Log("Player turn started");
});

eventBus.Invoke(GameEventAPI.PlayerTurnStarted.Id);
```

### Parameterized Event

```csharp
using var subscription = eventBus.Subscribe(GameEventAPI.EntityDamaged.Id, (TakeDamageEventArgs args) =>
{
    Debug.Log($"Entity took {args.Damage} damage");
});

eventBus.Invoke(GameEventAPI.EntityDamaged.Id, new TakeDamageEventArgs(entity, 25));
```

---

## 🔍 API Reference

### 🏛️ Type

```csharp
public interface IEventBus : IDisposable
```

- **Description:** Core interface for publish-subscribe event messaging.
- **Inheritance:** `IDisposable`
- **Notes:** Implementations store callbacks by integer event ID. The framework also provides strongly-typed [EventKey](../Keys/EventKey.md) wrappers and [Extensions](../Extensions.md) for string names.
- **See also:** [EventBus](EventBus.md), [ThreadSafeEventBus](ThreadSafeEventBus.md), [MonoEventBus](MonoEventBus.md), [EventKey](../Keys/EventKey.md)

---

### 🏹 Methods

#### `Subscribe`

```csharp
Subscription Subscribe(int key, Action action);
Subscription<T> Subscribe<T>(int key, Action<T> action);
Subscription<T1, T2> Subscribe<T1, T2>(int key, Action<T1, T2> action);
Subscription<T1, T2, T3> Subscribe<T1, T2, T3>(int key, Action<T1, T2, T3> action);
```

- **Description:** Registers a callback for the specified event key.
- **Parameter:** `key` – The integer event identifier.
- **Parameter:** `action` – The callback to invoke.
- **Returns:** A disposable subscription that unsubscribes when disposed.
- **See also:** [Subscription](../Subscriptions/Subscription.md)

#### `Unsubscribe`

```csharp
void Unsubscribe(int key, Action action);
void Unsubscribe<T>(int key, Action<T> action);
void Unsubscribe<T1, T2>(int key, Action<T1, T2> action);
void Unsubscribe<T1, T2, T3>(int key, Action<T1, T2, T3> action);
```

- **Description:** Removes a previously registered callback from the event.

#### `Invoke`

```csharp
void Invoke(int key);
void Invoke<T>(int key, T arg);
void Invoke<T1, T2>(int key, T1 arg1, T2 arg2);
void Invoke<T1, T2, T3>(int key, T1 arg1, T2 arg2, T3 arg3);
```

- **Description:** Invokes all callbacks registered for the specified event key.

#### `IsSubscribed`

```csharp
bool IsSubscribed(int key);
```

- **Description:** Returns whether any callback is registered for the key.
- **Returns:** `true` if the key has subscribers; otherwise `false`.

#### `Dispose`

```csharp
bool Dispose(int key);
```

- **Description:** Removes all callbacks for the specified key.
- **Returns:** `true` if the key existed and was removed.
- **Notes:** `IDisposable.Dispose()` clears all events from the bus.
