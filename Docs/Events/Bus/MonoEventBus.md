# 🧩 MonoEventBus

Unity `MonoBehaviour` implementation of [IEventBus](IEventBus.md). Can be attached to a GameObject in the scene so event handling follows scene lifecycle.

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
    - [OnDestroy](#ondestroy)

---

## 🗂 Example of Usage

### Add to Scene

1. Create an empty GameObject in the scene.
2. Add the **Atomic/Events/Event Bus** component.

### Use in Code

```csharp
public class GameContext : MonoEventBus
{
}

// Subscribe
MonoEventBus eventBus = ...;
using var subscription = eventBus.Subscribe(GameEventAPI.PlayerTurnStarted.Id, () =>
{
    Debug.Log("Player turn started");
});

// Invoke
eventBus.Invoke(GameEventAPI.PlayerTurnStarted.Id);
```

---

## 🔍 API Reference

### 🏛️ Type

```csharp
[AddComponentMenu("Atomic/Events/Event Bus")]
[DisallowMultipleComponent, DefaultExecutionOrder(-1000)]
public partial class MonoEventBus : MonoBehaviour, IEventBus
```

- **Description:** Unity `MonoBehaviour` implementation of [IEventBus](IEventBus.md).
- **Inheritance:** `MonoBehaviour`, [IEventBus](IEventBus.md)
- **Notes:** The internal bus is automatically disposed when the GameObject is destroyed.
- **See also:** [MonoEventBusSingleton](MonoEventBusSingleton.md), [EventBus](EventBus.md)

---

### 🏹 Methods

#### `Subscribe`

```csharp
public Subscription Subscribe(int key, Action action);
public Subscription<T> Subscribe<T>(int key, Action<T> action);
public Subscription<T1, T2> Subscribe<T1, T2>(int key, Action<T1, T2> action);
public Subscription<T1, T2, T3> Subscribe<T1, T2, T3>(int key, Action<T1, T2, T3> action);
```

- **Description:** Registers a callback on the underlying event bus.
- **Parameter:** `key` – The integer event identifier.
- **Parameter:** `action` – The callback to invoke.
- **Returns:** A disposable subscription.
- **See also:** [Subscription](../Subscriptions/Subscription.md)

#### `Unsubscribe`

```csharp
public void Unsubscribe(int key, Action action);
public void Unsubscribe<T>(int key, Action<T> action);
public void Unsubscribe<T1, T2>(int key, Action<T1, T2> action);
public void Unsubscribe<T1, T2, T3>(int key, Action<T1, T2, T3> action);
```

- **Description:** Removes a previously registered callback from the underlying event bus.

#### `Invoke`

```csharp
public void Invoke(int key);
public void Invoke<T>(int key, T arg);
public void Invoke<T1, T2>(int key, T1 arg1, T2 arg2);
public void Invoke<T1, T2, T3>(int key, T1 arg1, T2 arg2, T3 arg3);
```

- **Description:** Raises the event on the underlying event bus.

#### `IsSubscribed`

```csharp
public bool IsSubscribed(int key);
```

- **Description:** Returns whether any callback is registered for the key.
- **Returns:** `true` if the key has subscribers; otherwise `false`.

#### `Dispose`

```csharp
public bool Dispose(int key);
public void Dispose();
```

- **Description:** Removes callbacks from the underlying event bus.
- **Returns:** `true` from `Dispose(int)` if the key existed and was removed.

#### `OnDestroy`

```csharp
protected virtual void OnDestroy();
```

- **Description:** Disposes the internal event bus when the GameObject is destroyed.
- **Notes:** Called by Unity when the component is destroyed.
