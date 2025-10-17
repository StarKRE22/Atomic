# 🧩 InlineEntityTrigger\<E>

A flexible, **inline-configurable entity trigger** that allows you to define custom logic for
**tracking** and **untracking** entities without creating a separate trigger class. This is useful for lightweight
scenarios, quick prototyping, or when different entities require different tracking logic.

---

## 📑 Table of Contents

- [Example of Usage](#-example-of-usage)
- [API Reference](#-api-reference)
    - [Type](#-type)
    - [Constructor](#-constructor)
    - [Methods](#-methods)
        - [SetAction(Action\<E>)](#setactionactione)
        - [Track(E)](#tracke)
        - [Untrack(E)](#untracke)

---

## 🗂 Example of Usage

```csharp
var inlineTrigger = new InlineEntityTrigger<PlayerEntity>(
    track: (e, callback) => e.OnTagAdded += _ => callback(e),
    untrack: (e, callback) => e.OnTagAdded -= _ => callback(e)
);

inlineTrigger.SetAction(e => Console.WriteLine($"Custom trigger fired for {e.Name}"));
inlineTrigger.Track(someEntity);
```

---

## 🔍 API Reference

### 🏛️ Type <div id="-type"></div>

```csharp
public class InlineEntityTrigger<E> : IEntityTrigger<E>
    where E : IEntity
```

- **Type Parameter:** `E` — The entity type being tracked. Must implement [IEntity](../Entities/IEntity.md).
- **Inheritance:** [IEntityTrigger\<E>](IEntityTrigger%601.md)

---


<div id="-constructor"></div>

### 🏗️ Constructor

```csharp
public InlineEntityTrigger(Action<E, Action<E>> track, Action<E, Action<E>> untrack)
```

- **Description:** Creates a new inline entity trigger with **custom tracking and untracking logic**.
- **Parameters:**
    - `track` — A delegate that defines how the entity should be tracked (subscribed, monitored, etc.).
    - `untrack` — A delegate that defines how the entity should be untracked (unsubscribed, ignored, etc.).
- **Exception:** `ArgumentNullException` — Thrown if either `track` or `untrack` is `null`.

---

### 🏹 Methods

#### `SetAction(Action<E>)`

```csharp
public void SetAction(Action<E> action);
```

- **Description:** Assigns the action that will be invoked whenever a tracked entity’s state changes (depending on your
  custom logic).
- **Parameter:** `action` — The callback delegate to invoke. Cannot be `null`.
- **Exception:** `ArgumentNullException` — Thrown if `action` is `null`.

#### `Track(E)`

```csharp
public void Track(E entity);
```

- **Description:** Starts tracking the specified entity using the custom `track` logic provided in the constructor.
- **Parameter:** `entity` — The entity to track.

#### `Untrack(E)`

```csharp
public void Untrack(E entity);
```

- **Description:** Stops tracking the specified entity using the custom `untrack` logic provided in the constructor.
- **Parameter:** `entity` — The entity to stop tracking.