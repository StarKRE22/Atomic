# 🧩 EventBus

Default implementation of [IEventBus](IEventBus.md). Stores event delegates in a dictionary keyed by integer event IDs and supports parameterless, single-argument, two-argument, and three-argument events.

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

```csharp
IEventBus eventBus = new EventBus();

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
public class EventBus : IEventBus
```

- **Description:** Default implementation of [IEventBus](IEventBus.md).
- **Inheritance:** [IEventBus](IEventBus.md)
- **Notes:** Not thread-safe. Use [ThreadSafeEventBus](ThreadSafeEventBus.md) for multi-threaded scenarios.
- **See also:** [MonoEventBus](MonoEventBus.md), [ThreadSafeEventBus](ThreadSafeEventBus.md), [Extensions](../Extensions.md)

---

### 🏹 Methods

#### `Subscribe`

```csharp
public Subscription Subscribe(int key, Action action);
public Subscription<T> Subscribe<T>(int key, Action<T> action);
public Subscription<T1, T2> Subscribe<T1, T2>(int key, Action<T1, T2> action);
public Subscription<T1, T2, T3> Subscribe<T1, T2, T3>(int key, Action<T1, T2, T3> action);
```

- **Description:** Registers a callback for the specified event key.
- **Parameter:** `key` – The integer event identifier.
- **Parameter:** `action` – The callback to invoke.
- **Returns:** A disposable subscription that removes the callback when disposed.
- **See also:** [Subscription](../Subscriptions/Subscription.md)

#### `Unsubscribe`

```csharp
public void Unsubscribe(int key, Action action);
public void Unsubscribe<T>(int key, Action<T> action);
public void Unsubscribe<T1, T2>(int key, Action<T1, T2> action);
public void Unsubscribe<T1, T2, T3>(int key, Action<T1, T2, T3> action);
```

- **Description:** Removes a previously registered callback from the event.
- **Parameter:** `key` – The integer event identifier.
- **Parameter:** `action` – The callback to remove.

#### `Invoke`

```csharp
public void Invoke(int key);
public void Invoke<T>(int key, T arg);
public void Invoke<T1, T2>(int key, T1 arg1, T2 arg2);
public void Invoke<T1, T2, T3>(int key, T1 arg1, T2 arg2, T3 arg3);
```

- **Description:** Invokes all callbacks registered for the specified event key.
- **Parameter:** `key` – The integer event identifier.

#### `IsSubscribed`

```csharp
public bool IsSubscribed(int key);
```

- **Description:** Returns whether any callback is registered for the key.
- **Parameter:** `key` – The integer event identifier.
- **Returns:** `true` if the key has subscribers; otherwise `false`.

#### `Dispose`

```csharp
public void Dispose();
public bool Dispose(int key);
```

- **Description:** Removes callbacks from the bus.
- **Returns:** `true` from `Dispose(int)` if the key existed and was removed.
- **Notes:** `Dispose()` clears all events. `Dispose(int)` removes only the specified event.

---

#### `SubscribeUnsafe`

```csharp
public Subscription SubscribeUnsafe(int key, Action action);
public Subscription<T> SubscribeUnsafe<T>(int key, Action<T> action);
public Subscription<T1, T2> SubscribeUnsafe<T1, T2>(int key, Action<T1, T2> action);
public Subscription<T1, T2, T3> SubscribeUnsafe<T1, T2, T3>(int key, Action<T1, T2, T3> action);
```

- **Description:** Registers a callback without runtime validation.
- **Notes:** Unsafe counterpart to `Subscribe`. Intended for performance-critical paths where the key is known to be valid.

#### `UnsubscribeUnsafe`

```csharp
public void UnsubscribeUnsafe(int key, Action action);
public void UnsubscribeUnsafe<T>(int key, Action<T> action);
public void UnsubscribeUnsafe<T1, T2>(int key, Action<T1, T2> action);
public void UnsubscribeUnsafe<T1, T2, T3>(int key, Action<T1, T2, T3> action);
```

- **Description:** Removes a callback without runtime validation.
- **Notes:** Unsafe counterpart to `Unsubscribe`.

#### `InvokeUnsafe`

```csharp
public void InvokeUnsafe(int key);
public void InvokeUnsafe<T>(int key, T arg);
public void InvokeUnsafe<T1, T2>(int key, T1 arg1, T2 arg2);
public void InvokeUnsafe<T1, T2, T3>(int key, T1 arg1, T2 arg2, T3 arg3);
```

- **Description:** Invokes all callbacks for the key without runtime validation.
- **Notes:** Unsafe counterpart to `Invoke`.
