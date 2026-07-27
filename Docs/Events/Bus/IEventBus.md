# 🧩 IEventBus

**IEventBus** is the core interface for publish-subscribe event messaging in the Atomic.Events module. It supports
parameterless and parameterized events, subscriptions, unsubscriptions, and event disposal.

---

## 📑 Table of Contents

- [Overview](#-overview)
- [Examples of Usage](#-examples-of-usage)
- [API Reference](#-api-reference)
  - [Methods](#-methods)
- [See Also](#-see-also)

---

## 🧩 Overview

An event bus decouples event publishers from subscribers. Subscribers register callbacks for integer event keys;
publishers invoke those keys. The bus supports up to three event arguments.

The framework also provides strongly-typed [EventKey](EventKey.md) wrappers and extension methods for working with string
names instead of raw IDs.

---

## 🗂 Examples of Usage

### Basic Parameterless Event

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

---

### 🏹 Methods

#### Subscribe

```csharp
Subscription Subscribe(int key, Action action);
Subscription<T> Subscribe<T>(int key, Action<T> action);
Subscription<T1, T2> Subscribe<T1, T2>(int key, Action<T1, T2> action);
Subscription<T1, T2, T3> Subscribe<T1, T2, T3>(int key, Action<T1, T2, T3> action);
```

- **Description:** Registers a callback for the specified event key.
- **Returns:** A disposable `Subscription` that unsubscribes when disposed.

#### Unsubscribe

```csharp
void Unsubscribe(int key, Action action);
void Unsubscribe<T>(int key, Action<T> action);
void Unsubscribe<T1, T2>(int key, Action<T1, T2> action);
void Unsubscribe<T1, T2, T3>(int key, Action<T1, T2, T3> action);
```

- **Description:** Removes a previously registered callback.

#### Invoke

```csharp
void Invoke(int key);
void Invoke<T>(int key, T arg);
void Invoke<T1, T2>(int key, T1 arg1, T2 arg2);
void Invoke<T1, T2, T3>(int key, T1 arg1, T2 arg2, T3 arg3);
```

- **Description:** Invokes all callbacks registered for the specified event key.

#### IsSubscribed

```csharp
bool IsSubscribed(int key);
```

- **Description:** Returns whether any callback is registered for the key.

#### Dispose(int)

```csharp
bool Dispose(int key);
```

- **Description:** Removes all callbacks for the specified key.
- **Returns:** `true` if the key existed and was removed.

#### Dispose()

```csharp
void Dispose();
```

- **Description:** Clears all events from the bus.

---

## 🔗 See Also

- [EventBus](EventBus.md)
- [ThreadSafeEventBus](ThreadSafeEventBus.md)
- [MonoEventBus](MonoEventBus.md)
- [EventKey](EventKey.md)
- [EventKeyStore](EventKeyStore.md)
- [EventBus Extensions](Extensions.md)
