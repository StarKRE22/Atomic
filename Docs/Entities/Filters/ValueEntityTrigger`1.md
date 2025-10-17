# 🧩 ValueEntityTrigger\<E>

A trigger that responds to **value changes** (added, removed, or modified) on entities of type `E`.
Allows an [EntityFilter\<E>](EntityFilter%601.md) to automatically re-evaluate entities when values change.

---

## 📑 Table of Contents

- [Examples of Usage](#-examples-of-usage)
    - [EntityFilter Usage](#ex1)
    - [Custom Monitoring](#ex2)
- [API Reference](#-api-reference)
    - [Type](#-type)
    - [Constructor](#-constructor)
    - [Methods](#-methods)
        - [SetAction(Action\<E>)](#setactionactione)
        - [Track(E)](#tracke)
        - [Untrack(E)](#untracke)

---

## 🗂 Example of Usage

<div id="ex1"></div>

### 1️⃣ EntityFilter Usage

```csharp
// Track entities for value additions, deletions, and modifications
var trigger = new ValueEntityTrigger<GameEntity>(
    added: true,
    deleted: true,
    changed: true
);

// Usage with EntityFilter
var filter = new EntityFilter<GameEntity>(
    allEntities,
    e => e.GetValue<int>("Health") > 0,
    trigger
);
```

<div id="ex2"></div>

### 2️⃣ Custom Monitoring

```csharp
// Monitor all tag changes (additions and deletions)
var valueTrigger = new ValueEntityTrigger<UnitEntity>();
valueTrigger.SetAction(entity => Debug.Log($"Value change detected on entity: {entity.Name}"));

// Track entities
valueTrigger.Track(playerEntity);
valueTrigger.Track(enemyEntity);

// When values change on entities, the trigger automatically responds
playerEntity.AddValue("Inventory", new Inventory());      // Triggers callback
enemyEntity.RemoveValue("Inventory");   // Triggers callback
enemyEntity.SetValue("Health", 25);   // Triggers callback if "Health" value is alreay added!
```

---

## 🔍 API Reference

### 🏛️ Type <div id="-type"></div>

```csharp
public class ValueEntityTrigger<E> : IEntityTrigger<E> where E : IEntity
```

- **Type Parameter:** `E` — The entity type being tracked. Must implement [IEntity](../Entities/IEntity.md).
- **Inheritance:** [IEntityTrigger\<E>](IEntityTrigger%601.md)

---

<div id="-constructor"></div>

### 🏗️ Constructor

```csharp
public ValueEntityTrigger(bool added = true, bool deleted = true, bool changed = true)
```

- **Description:** Creates a new `ValueEntityTrigger` instance with configurable listening behavior for value events.
- **Parameters:**
    - `added` — Whether to react to value additions via the `OnValueAdded` event. Default is `true`.
    - `deleted` — Whether to react to value removals via the `OnValueDeleted` event. Default is `true`.
    - `changed` — Whether to react to value modifications via the `OnValueChanged` event. Default is `true`.

---

### 🏹 Methods

#### `SetAction(Action<E>)`

```csharp
public void SetAction(Action<E> action);
```

- **Description:** Sets the callback action that will be invoked whenever a tracked entity’s values change.
- **Parameter:** `action` — The delegate to invoke on the entity. Cannot be `null`.
- **Exception:** `ArgumentNullException` — Thrown if `action` is `null`.

#### `Track(E)`

```csharp
public void Track(E entity);
```

- **Description:** Starts tracking the specified entity for value changes.
- **Parameter:** `entity` — The entity to track.
- **Note:** Subscribes to `OnValueAdded`, `OnValueDeleted`, and/or `OnValueChanged` events depending on constructor
  parameters.

#### `Untrack(E)`

```csharp
public void Untrack(E entity);
```

- **Description:** Stops tracking the specified entity for value changes.
- **Parameter:** `entity` — The entity to stop monitoring.
- **Note:** Unsubscribes from previously subscribed value events.