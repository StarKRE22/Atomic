# 🧩 StateChangedEntityTrigger

A **non-generic shortcut** for [StateChangedEntityTrigger\<E>](StateChangedEntityTrigger%601.md). It allows subscribing
to **state change events** (`OnStateChanged`) directly for basic [IEntity](../Entities/IEntity.md) objects without
specifying a generic type parameter.

---

## 📑 Table of Contents

- [Examples of Usage](#-examples-of-usage)
    - [Filter Usage](#ex1)
    - [Custom Usage](#ex2)
- [API Reference](#-api-reference)
    - [Type](#-type)
    - [Constructor](#-constructor)
    - [Methods](#-methods)
        - [SetAction(Action\<IEntity>)](#setactionactionientity)
        - [Track(IEntity)](#trackientity)
        - [Untrack(IEntity)](#untrackientity)

---

## 🗂 Examples of Usage

<div id="ex1"></div>

### 1️⃣ Filter Usage

```csharp
// Track entities for state changes
var trigger = new StateChangedEntityTrigger();

// Usage with EntityFilter
var filter = new EntityFilter(
    allEntities,
    e => e.GetValue<bool>("IsAlive"),
    trigger
);
```

<div id="ex2"></div>

### 2️⃣ Custom Usage

```csharp
var stateTrigger = new StateChangedEntityTrigger();
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
public class StateChangedEntityTrigger : StateChangedEntityTrigger<IEntity>
```

- **Inheritance:** [StateChangedEntityTrigger\<E>](StateChangedEntityTrigger%601.md)

---

<div id="-constructor"></div>

### 🏗️ Constructor

```csharp
public StateChangedEntityTrigger();
```

- **Description:** Initializes a new instance of the `StateChangedEntityTrigger`.
- **Note:** No parameters are needed; the trigger automatically hooks into the entity’s `OnStateChanged` event.

---

### 🏹 Methods

#### `SetAction(Action<IEntity>)`

```csharp
public void SetAction(Action<IEntity> action);
```

- **Description:** Sets the callback action that will be invoked whenever the tracked entity’s state changes.
- **Parameter:** `action` — The delegate to invoke when the entity state changes. Cannot be `null`.
- **Exception:** `ArgumentNullException` — Thrown if `action` is `null`.

#### `Track(IEntity)`

```csharp
public void Track(IEntity entity);
```

- **Description:** Subscribes to the entity’s `OnStateChanged` event.
- **Parameter:** `entity` — The entity to track.

#### `Untrack(IEntity)`

```csharp
public void Untrack(IEntity entity);
```

- **Description:** Unsubscribes from the entity’s `OnStateChanged` event.
- **Parameter:** `entity` — The entity to stop tracking.