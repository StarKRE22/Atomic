# 🧩 TagEntityTrigger\<E>

A trigger that responds to **tag changes** (added or removed) on entities of type `E`. Allows
an [EntityFilter\<E>](EntityFilter%601.md) to automatically re-evaluate entities when tags change.

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
// Track entities whose tags change (addition/removal)
var trigger = new TagEntityTrigger<GameEntity>(added: true, deleted: true);

// Usage with EntityFilter
var filter = new EntityFilter<GameEntity>(
    allEntities,
    e => e.HasTag("Enemy"),
    trigger
);
```

---

## 🔍 API Reference

### 🏛️ Type <div id="-type"></div>

```csharp
public class TagEntityTrigger<E> : IEntityTrigger<E> where E : IEntity
```

- **Type Parameter:** `E` — The entity type being tracked. Must implement [IEntity](../Entities/IEntity.md).
- **Inheritance:** [IEntityTrigger\<E>](IEntityTrigger%601.md)

---

<div id="-constructor"></div>

### 🏗️ Constructor

```csharp
public TagEntityTrigger(bool added = true, bool deleted = true)
```

- **Description:** Creates a new `TagEntityTrigger` instance.
- **Parameters:**
    - `added` — Whether to react to tag additions via the `OnTagAdded` event. Default is `true`.
    - `deleted` — Whether to react to tag removals via the `OnTagDeleted` event. Default is `true`.

---

### 🏹 Methods

#### `SetAction(Action<E>)`

```csharp
public void SetAction(Action<E> action);
```

- **Description:** Sets the callback action that will be invoked whenever a tracked entity’s tags change.
- **Parameter:** `action` — The delegate to invoke on the entity. Cannot be `null`.
- **Exception:** `ArgumentNullException` — Thrown if `action` is `null`.

#### `Track(E)`

```csharp
public void Track(E entity);
```

- **Description:** Starts tracking the specified entity for tag changes.
- **Parameter:** `entity` — The entity to track.
- **Note:** Subscribes to `OnTagAdded` and/or `OnTagDeleted` events depending on constructor parameters.

#### `Untrack(E)`

```csharp
public void Untrack(E entity);
```

- **Description:** Stops tracking the specified entity for tag changes.
- **Parameter:** `entity` — The entity to stop monitoring.
- **Note:** Unsubscribes from previously subscribed tag events.