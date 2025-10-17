# 🧩 BehaviourEntityTrigger\<E>

A trigger that responds to **behaviour changes** (added or removed) on entities of type `E`. Allows
an [EntityFilter\<E>](EntityFilter%601.md) to automatically re-evaluate entities when behaviours are added or
removed.

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
// Track specific entities for behaviour additions and deletions
var trigger = new BehaviourEntityTrigger<PlayerEntity>(
    added: true,
    removed: true,
);

// Usage with non-generic EntityFilter
var filter = new EntityFilter<PlayerEntity>(
    allEntities,
    e => e.GetValue<int>("Health") > 0,
    trigger
);
```

---

## 🔍 API Reference

### 🏛️ Type <div id="-type"></div>

```csharp
public class BehaviourEntityTrigger<E> : IEntityTrigger<E> where E : IEntity
```

- **Type Parameter:** `E` — The entity type being tracked. Must implement [IEntity](../Entities/IEntity.md).
- **Inheritance:** [IEntityTrigger\<E>](IEntityTrigger%601.md)

---

<div id="-constructor"></div>

### 🏗️ Constructor

```csharp
public BehaviourEntityTrigger(bool added = true, bool removed = true)
```

- **Description:** Creates a new `BehaviourEntityTrigger` instance.
- **Parameters:**
    - `added` — Whether to react to behaviour additions via the `OnBehaviourAdded` event. Default is `true`.
    - `removed` — Whether to react to behaviour removals via the `OnBehaviourRemoved` event. Default is `true`.

---

### 🏹 Methods

#### `SetAction(Action<E>)`

```csharp
public void SetAction(Action<E> action);
```

- **Description:** Sets the callback action that will be invoked whenever a tracked entity’s behaviours change.
- **Parameter:** `action` — The delegate to invoke on the entity. Cannot be `null`.
- **Exception:** `ArgumentNullException` — Thrown if `action` is `null`.

#### `Track(E)`

```csharp
public void Track(E entity);
```

- **Description:** Starts tracking the specified entity for behaviour changes.
- **Parameter:** `entity` — The entity to track.
- **Note:** Subscribes to `OnBehaviourAdded` and/or `OnBehaviourRemoved` events depending on constructor parameters.

#### `Untrack(E)`

```csharp
public void Untrack(E entity);
```

- **Description:** Stops tracking the specified entity for behaviour changes.
- **Parameter:** `entity` — The entity to stop monitoring.
- **Note:** Unsubscribes from previously subscribed behaviour events.