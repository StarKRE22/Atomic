# 🧩 EventKey

A strongly-typed identifier for events on a specific [IEventBus](../Bus/IEventBus.md). It wraps an integer ID
resolved by [EventKeyStore](EventKeyStore.md), providing type safety and avoiding repeated string lookups.

---

## 📑 Table of Contents

- [Example of Usage](#-example-of-usage)
- [API Reference](#-api-reference)
  - [Type](#-type)
    - [EventKey&lt;TBus&gt;](#eventkeytbus)
    - [EventKey&lt;TBus, TArg&gt;](#eventkeytbus-targ)
    - [EventKey&lt;TBus, TArg1, TArg2&gt;](#eventkeytbus-targ1-targ2)
    - [EventKey&lt;TBus, TArg1, TArg2, TArg3&gt;](#eventkeytbus-targ1-targ2-targ3)
  - [Constructors](#-constructors)
    - [EventKey(string)](#eventkeystring)
    - [EventKey(int)](#eventkeyint)
  - [Methods](#-methods)
    - [ToString()](#tostring)
    - [Equals(EventKey&lt;...&gt;)](#equalseventkey)
    - [Equals(object)](#equalsobject)
    - [GetHashCode()](#gethashcode)
  - [Operators](#-operators)
    - [operator ==](#operator-)
    - [operator !=](#operator--1)

---

## 🗂 Example of Usage

Define event keys once in a static API class and reuse them:

```csharp
public static class GameEventAPI
{
    public static readonly EventKey<IGameEventBus> PlayerTurnStarted = new(nameof(PlayerTurnStarted));
    public static readonly EventKey<IGameEventBus, SpawnEventArgs> EntitySpawned = new(nameof(EntitySpawned));
    public static readonly EventKey<IGameEventBus, IGameEntity, int> EntityDamaged = new(nameof(EntityDamaged));
}
```

Subscribe and invoke through the bus:

```csharp
IGameEventBus eventBus = context.GetValue(GameContextAPI.EventBus);

using var subscription = eventBus.Subscribe(GameEventAPI.PlayerTurnStarted, () =>
{
    Debug.Log("Player turn started");
});

eventBus.Invoke(GameEventAPI.PlayerTurnStarted);
```

---

## 🔍 API Reference

### 🏛️ Type

#### `EventKey<TBus>`

```csharp
public readonly struct EventKey<TBus> : IEquatable<EventKey<TBus>> where TBus : IEventBus
```

- **Description:** Identifies a parameterless event on bus type `TBus`.
- **Inheritance:** `IEquatable<EventKey<TBus>>`
- **Type Parameters:**
  - `TBus` – The event bus type. Must implement [IEventBus](../Bus/IEventBus.md).
- **Notes:** The ID is resolved lazily through [EventKeyStore](EventKeyStore.md).
- **See also:** [EventKeyStore](EventKeyStore.md)

#### `EventKey<TBus, TArg>`

```csharp
public readonly struct EventKey<TBus, TArg> : IEquatable<EventKey<TBus, TArg>> where TBus : IEventBus
```

- **Description:** Identifies a single-argument event on bus type `TBus`.
- **Inheritance:** `IEquatable<EventKey<TBus, TArg>>`
- **Type Parameters:**
  - `TBus` – The event bus type. Must implement [IEventBus](../Bus/IEventBus.md).
  - `TArg` – The type of the event argument.

#### `EventKey<TBus, TArg1, TArg2>`

```csharp
public readonly struct EventKey<TBus, TArg1, TArg2> : IEquatable<EventKey<TBus, TArg1, TArg2>>
    where TBus : IEventBus
```

- **Description:** Identifies a two-argument event on bus type `TBus`.
- **Inheritance:** `IEquatable<EventKey<TBus, TArg1, TArg2>>`
- **Type Parameters:**
  - `TBus` – The event bus type. Must implement [IEventBus](../Bus/IEventBus.md).
  - `TArg1` – The type of the first event argument.
  - `TArg2` – The type of the second event argument.

#### `EventKey<TBus, TArg1, TArg2, TArg3>`

```csharp
public readonly struct EventKey<TBus, TArg1, TArg2, TArg3> : IEquatable<EventKey<TBus, TArg1, TArg2, TArg3>>
    where TBus : IEventBus
```

- **Description:** Identifies a three-argument event on bus type `TBus`.
- **Inheritance:** `IEquatable<EventKey<TBus, TArg1, TArg2, TArg3>>`
- **Type Parameters:**
  - `TBus` – The event bus type. Must implement [IEventBus](../Bus/IEventBus.md).
  - `TArg1` – The type of the first event argument.
  - `TArg2` – The type of the second event argument.
  - `TArg3` – The type of the third event argument.

---

### 🏗️ Constructors

#### `EventKey(string)`

```csharp
public EventKey(string name)
```

- **Description:** Creates an event key from a string name, resolving it to an ID through [EventKeyStore](EventKeyStore.md).
- **Parameter:** `name` – The event name. Must not be `null`.
- **Throws:** `ArgumentNullException` if `name` is `null`.

#### `EventKey(int)`

```csharp
public EventKey(int id)
```

- **Description:** Creates an event key from an existing integer ID.
- **Parameter:** `id` – The numeric event identifier.

---

### 🏹 Methods

#### `ToString()`

```csharp
public override string ToString()
```

- **Description:** Returns the original event name registered for this ID.
- **Returns:** The event name, or `#Unknown:{id}` if the ID is not registered.

#### `Equals(EventKey<...>)`

```csharp
public bool Equals(EventKey<TBus> other)
```

- **Description:** Compares two event keys by their IDs.
- **Parameter:** `other` – The other event key.
- **Returns:** `true` if both keys have the same ID; otherwise `false`.

#### `Equals(object)`

```csharp
public override bool Equals(object obj)
```

- **Description:** Compares this event key to another object.
- **Parameter:** `obj` – The object to compare.
- **Returns:** `true` if `obj` is an equivalent event key; otherwise `false`.

#### `GetHashCode()`

```csharp
public override int GetHashCode()
```

- **Description:** Returns the hash code for this event key.
- **Returns:** The underlying integer ID.

---

### 🪄 Operators

#### `operator ==`

```csharp
public static bool operator ==(EventKey<TBus> left, EventKey<TBus> right)
```

- **Description:** Compares two event keys for equality.
- **Parameters:**
  - `left` – The first event key.
  - `right` – The second event key.
- **Returns:** `true` if the keys are equal; otherwise `false`.

#### `operator !=`

```csharp
public static bool operator !=(EventKey<TBus> left, EventKey<TBus> right)
```

- **Description:** Compares two event keys for inequality.
- **Parameters:**
  - `left` – The first event key.
  - `right` – The second event key.
- **Returns:** `true` if the keys are not equal; otherwise `false`.
