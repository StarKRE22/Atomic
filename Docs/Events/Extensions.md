# 🧩 Extensions

Provides **extension methods** for [IEventBus](Bus/IEventBus.md) that let you subscribe, invoke, unsubscribe, and dispose events using string names or strongly-typed [EventKey](Keys/EventKey.md) objects.

---

## 📑 Table of Contents

- [Example of Usage](#-example-of-usage)
- [API Reference](#-api-reference)
  - [Type](#-type)
  - [Methods](#-methods)
    - [Subscribe (string)](#subscribe-string)
    - [Invoke (string)](#invoke-string)
    - [Unsubscribe (string)](#unsubscribe-string)
    - [IsSubscribed (string)](#issubscribed-string)
    - [Dispose (string)](#dispose-string)
    - [Subscribe (EventKey)](#subscribe-eventkey)
    - [Invoke (EventKey)](#invoke-eventkey)
    - [Unsubscribe (EventKey)](#unsubscribe-eventkey)
    - [IsSubscribed (EventKey)](#issubscribed-eventkey)
    - [Dispose (EventKey)](#dispose-eventkey)

---

## 🗂 Example of Usage

```csharp
IEventBus eventBus = new EventBus();

// Subscribe by string name
using var subscription = eventBus.Subscribe("PlayerTurnStarted", () =>
{
    Debug.Log("Player turn started");
});

// Invoke by string name
eventBus.Invoke("PlayerTurnStarted");

// Subscribe by strongly-typed EventKey
using var damageSub = eventBus.Subscribe(GameEventAPI.EntityDamaged, (TakeDamageEventArgs args) =>
{
    Debug.Log($"Entity took {args.Damage} damage");
});

eventBus.Invoke(GameEventAPI.EntityDamaged, new TakeDamageEventArgs(entity, 25));
```

---

## 🔍 API Reference

### 🏛️ Type

```csharp
public static class Extensions
```

- **Description:** Provides extension methods for [IEventBus](Bus/IEventBus.md) that convert string names and `EventKey` instances to integer event IDs via [EventKeyStore](Keys/EventKeyStore.md).
- **Notes:** All methods forward to the matching `IEventBus` member after resolving the key.
- **See also:** [IEventBus](Bus/IEventBus.md), [EventKey](Keys/EventKey.md), [EventKeyStore](Keys/EventKeyStore.md)

---

### 🏹 Methods

#### `Subscribe` (string)

```csharp
public static Subscription Subscribe(this IEventBus it, string key, Action action);
public static Subscription<T> Subscribe<T>(this IEventBus it, string key, Action<T> action);
public static Subscription<T1, T2> Subscribe<T1, T2>(this IEventBus it, string key, Action<T1, T2> action);
public static Subscription<T1, T2, T3> Subscribe<T1, T2, T3>(this IEventBus it, string key, Action<T1, T2, T3> action);
```

- **Description:** Registers a callback for the event name resolved by [EventKeyStore.NameToId](Keys/EventKeyStore.md).
- **Parameter:** `key` – The event name.
- **Parameter:** `action` – The callback to invoke when the event is raised.
- **Returns:** A disposable subscription that removes the callback when disposed.

#### `Invoke` (string)

```csharp
public static void Invoke(this IEventBus it, string key);
public static void Invoke<T>(this IEventBus it, string key, T arg);
public static void Invoke<T1, T2>(this IEventBus it, string key, T1 arg1, T2 arg2);
public static void Invoke<T1, T2, T3>(this IEventBus it, string key, T1 arg1, T2 arg2, T3 arg3);
```

- **Description:** Raises the event identified by `key` with the supplied arguments.
- **Parameter:** `key` – The event name.

#### `Unsubscribe` (string)

```csharp
public static void Unsubscribe(this IEventBus it, string key, Action action);
public static void Unsubscribe<T>(this IEventBus it, string key, Action<T> action);
public static void Unsubscribe<T1, T2>(this IEventBus it, string key, Action<T1, T2> action);
public static void Unsubscribe<T1, T2, T3>(this IEventBus it, string key, Action<T1, T2, T3> action);
```

- **Description:** Removes a previously registered callback from the event.

#### `IsSubscribed` (string)

```csharp
public static bool IsSubscribed(this IEventBus it, string key);
```

- **Description:** Returns whether any callback is registered for the named event.
- **Returns:** `true` if the event has subscribers; otherwise `false`.

#### `Dispose` (string)

```csharp
public static bool Dispose(this IEventBus it, string key);
```

- **Description:** Removes all callbacks for the named event.
- **Returns:** `true` if the event existed and was removed.

#### `Subscribe` (EventKey)

```csharp
public static Subscription Subscribe<TBus>(this TBus it, EventKey<TBus> key, Action action)
    where TBus : IEventBus;
public static Subscription<T> Subscribe<TBus, T>(this TBus it, EventKey<TBus, T> key, Action<T> action)
    where TBus : IEventBus;
public static Subscription<T1, T2> Subscribe<TBus, T1, T2>(this TBus it, EventKey<TBus, T1, T2> key, Action<T1, T2> action)
    where TBus : IEventBus;
public static Subscription<T1, T2, T3> Subscribe<TBus, T1, T2, T3>(this TBus it, EventKey<TBus, T1, T2, T3> key, Action<T1, T2, T3> action)
    where TBus : IEventBus;
```

- **Description:** Registers a callback using a strongly-typed event key.
- **Parameter:** `key` – The strongly-typed event key.
- **Parameter:** `action` – The callback to invoke.
- **Returns:** A disposable subscription.
- **Notes:** The `TBus` type parameter ensures the key can only be used on compatible bus implementations.

#### `Invoke` (EventKey)

```csharp
public static void Invoke<TBus>(this TBus it, EventKey<TBus> key)
    where TBus : IEventBus;
public static void Invoke<TBus, T>(this TBus it, EventKey<TBus, T> key, T arg)
    where TBus : IEventBus;
public static void Invoke<TBus, T1, T2>(this TBus it, EventKey<TBus, T1, T2> key, T1 arg1, T2 arg2)
    where TBus : IEventBus;
public static void Invoke<TBus, T1, T2, T3>(this TBus it, EventKey<TBus, T1, T2, T3> key, T1 arg1, T2 arg2, T3 arg3)
    where TBus : IEventBus;
```

- **Description:** Raises the event identified by the strongly-typed key.

#### `Unsubscribe` (EventKey)

```csharp
public static void Unsubscribe<TBus>(this TBus it, EventKey<TBus> key, Action action)
    where TBus : IEventBus;
public static void Unsubscribe<TBus, T>(this TBus it, EventKey<TBus, T> key, Action<T> action)
    where TBus : IEventBus;
public static void Unsubscribe<TBus, T1, T2>(this TBus it, EventKey<TBus, T1, T2> key, Action<T1, T2> action)
    where TBus : IEventBus;
public static void Unsubscribe<TBus, T1, T2, T3>(this TBus it, EventKey<TBus, T1, T2, T3> key, Action<T1, T2, T3> action)
    where TBus : IEventBus;
```

- **Description:** Removes a previously registered callback from the strongly-typed event.

#### `IsSubscribed` (EventKey)

```csharp
public static bool IsSubscribed<TBus>(this TBus it, EventKey<TBus> key)
    where TBus : IEventBus;
public static bool IsSubscribed<TBus, T>(this TBus it, EventKey<TBus, T> key)
    where TBus : IEventBus;
public static bool IsSubscribed<TBus, T1, T2>(this TBus it, EventKey<TBus, T1, T2> key)
    where TBus : IEventBus;
public static bool IsSubscribed<TBus, T1, T2, T3>(this TBus it, EventKey<TBus, T1, T2, T3> key)
    where TBus : IEventBus;
```

- **Description:** Returns whether any callback is registered for the strongly-typed event.

#### `Dispose` (EventKey)

```csharp
public static bool Dispose<TBus>(this TBus it, EventKey<TBus> key)
    where TBus : IEventBus;
public static bool Dispose<TBus, T>(this TBus it, EventKey<TBus, T> key)
    where TBus : IEventBus;
public static bool Dispose<TBus, T1, T2>(this TBus it, EventKey<TBus, T1, T2> key)
    where TBus : IEventBus;
public static bool Dispose<TBus, T1, T2, T3>(this TBus it, EventKey<TBus, T1, T2, T3> key)
    where TBus : IEventBus;
```

- **Description:** Removes all callbacks for the strongly-typed event.
- **Returns:** `true` if the event existed and was removed.
