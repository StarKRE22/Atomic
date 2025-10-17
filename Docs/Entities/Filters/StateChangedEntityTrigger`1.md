# 🧩 StateChangedEntityTrigger\<E>

A trigger that responds to **state changes** on entities of type `E`. Allows an [EntityFilter\<E>](EntityFilter%601.md)
to automatically re-evaluate entities when their state changes.

---

## 📑 Table of Contents

- [Examples of Usage](#-examples-of-usage)
    - [Filter Usage](#ex1)
    - [Custom Usage](#ex2)
- [API Reference](#-api-reference)
    - [Type](#-type)
    - [Constructor](#-constructor)
    - [Methods](#-methods)
        - [SetAction(Action\<IEntity>)](#setactionactione)
        - [Track(IEntity)](#tracke)
        - [Untrack(IEntity)](#untracke)

---

## 🗂 Examples of Usage

<div id="ex1"></div>

### 1️⃣ Filter Usage

```csharp
// Track entities for state changes
var trigger = new StateChangedEntityTrigger<GameEntity>();

// Usage with EntityFilter
var filter = new EntityFilter<GameEntity>(
    allEntities,
    e => e.GetValue<bool>("IsAlive"),
    trigger
);
```

<div id="ex2"></div>

### 2️⃣ Custom Usage

```csharp
var stateTrigger = new StateChangedEntityTrigger<UnitEntity>();
stateTrigger.SetAction(entity =>
Console.WriteLine($"State changed on entity: {entity.Name}"));

// Track entities
stateTrigger.Track(playerEntity);
stateTrigger.Track(enemyEntity);

// Changing state triggers the action
playerEntity.AddTag("Frozen");
enemyEntity.AddValue("Target", playerEntity);
```

---

## 🔍 API Reference

### 🏛️ Type <div id="-type"></div>

```csharp
public class StateChangedEntityTrigger<E> : IEntityTrigger<E> where E : IEntity
```

- **Type Parameter:** `E` — The entity type being tracked. Must implement [IEntity](../Entities/IEntity.md).
- **Inheritance:** [IEntityTrigger\<E>](IEntityTrigger%601.md)

---

<div id="-constructor"></div>

### 🏗️ Constructor

```csharp
public StateChangedEntityTrigger();
```

- **Description:** Initializes a new instance of the `StateChangedEntityTrigger<E>`.
- **Note:** No parameters are needed; the trigger automatically hooks into the entity’s `OnStateChanged` event.

---

### 🏹 Methods

#### `SetAction(Action<E>)`

```csharp
public void SetAction(Action<E> action);
```

- **Description:** Sets the callback action that will be invoked whenever a tracked entity’s state changes.
- **Parameter:** `action` — The delegate to invoke on the entity. Cannot be `null`.
- **Exception:** `ArgumentNullException` — Thrown if `action` is `null`.

#### `Track(E)`

```csharp
public void Track(E entity);
```

- **Description:** Starts tracking the specified entity for state changes.
- **Parameter:** `entity` — The entity to track.
- **Note:** Subscribes to the entity's `OnStateChanged` event.

#### `Untrack(E)`

```csharp
public void Untrack(E entity);
```

- **Description:** Stops tracking the specified entity for state changes.
- **Parameter:** `entity` — The entity to stop monitoring.
- **Note:** Unsubscribes from the entity's `OnStateChanged` event.