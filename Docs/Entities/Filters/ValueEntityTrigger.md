# 🧩 ValueEntityTrigger

A non-generic shortcut for [ValueEntityTrigger\<E>](ValueEntityTrigger%601.md). Provides value-based tracking for
general [IEntity](../Entities/IEntity.md) instances, including reactions to value additions, removals, and changes.

---

## 📑 Table of Contents

- [Examples of Usage](#examples-of-usage)
    - [EntityFilter Usage](#ex1)
    - [Custom Monitoring](#ex2)
    - [Addition-Only Monitoring](#ex3)
    - [Deletion-Only Monitoring](#ex4)
    - [Changing-Only Monitoring](#ex5)
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

### 1️⃣ EntityFilter Usage

```csharp
// Track general entities for value additions, deletions, and modifications
var trigger = new ValueEntityTrigger(
    added: true,
    deleted: true,
    changed: true
);

// Usage with non-generic EntityFilter
var filter = new EntityFilter(
    allEntities,
    e => e.GetValue<int>("Health") > 0,
    trigger
);
```

<div id="ex2"></div>

### 2️⃣ Custom Monitoring

```csharp
// Monitor all tag changes (additions and deletions)
var valueTrigger = new ValueEntityTrigger();
valueTrigger.SetAction(entity => Debug.Log($"Value change detected on entity: {entity.Name}"));

// Track entities
valueTrigger.Track(playerEntity);
valueTrigger.Track(enemyEntity);

// When values change on entities, the trigger automatically responds
playerEntity.AddValue("Inventory", new Inventory());      // Triggers callback
enemyEntity.RemoveValue("Inventory");   // Triggers callback
enemyEntity.SetValue("Inventory", new Inventory());   // Triggers callback
```

<div id="ex3"></div>

### 3️⃣ Addition-Only Monitoring

```csharp
// Monitor only when values are added
var additionTrigger = new ValueEntityTrigger(added: true, deleted: false, changed: false);
additionTrigger.SetAction(entity => Debug.Log($"New tag added to entity: {entity.Name}"));

// This will only trigger when values are added, not removed
additionTrigger.Track(playerEntity);

// When values added on entities, the trigger automatically responds
playerEntity.AddValue("Inventory", new Inventory());      // Triggers callback
```

<div id="ex4"></div>

### 4️⃣ Deletion-Only Monitoring

```csharp
// Monitor only when values are removed
var deletionTrigger = new ValueEntityTrigger(added: false, deleted: true, changed: false);
deletionTrigger.SetAction(entity => Debug.Log($"Value removed from entity: {entity.Name}"));

deletionTrigger.Track(npcEntity);

// When values removed from entities, the trigger automatically responds
enemyEntity.RemoveValue("Inventory");   // Triggers callback
```

<div id="ex5"></div>

### 5️⃣ Changing-Only Monitoring

```csharp
// Monitor only when values are removed
var deletionTrigger = new ValueEntityTrigger(added: false, deleted: false, changed: true);
deletionTrigger.SetAction(entity => Debug.Log($"Value removed from entity: {entity.Name}"));

deletionTrigger.Track(npcEntity);

// When values removed from entities, the trigger automatically responds
enemyEntity.SetValue("Health", 10);   // Triggers callback if "Health" value is already added
```

---

## 🔍 API Reference

### 🏛️ Type <div id="-type"></div>

```csharp
public class ValueEntityTrigger : ValueEntityTrigger<IEntity>
```

- **Inheritance:** [ValueEntityTrigger\<E>](ValueEntityTrigger%601.md)

---

<div id="-constructor"></div>

### 🏗️ Constructor

```csharp
public ValueEntityTrigger(bool added = true, bool deleted = true, bool changed = true)
```

- **Description:** Creates a new non-generic `ValueEntityTrigger` instance with configurable listening behavior.
- **Parameters:**
    - `added` — Whether to react to value additions via the `OnValueAdded` event. Default is `true`.
    - `deleted` — Whether to react to value removals via the `OnValueDeleted` event. Default is `true`.
    - `changed` — Whether to react to value modifications via the `OnValueChanged` event. Default is `true`.

---

### 🏹 Methods

#### `SetAction(Action<IEntity>)`

```csharp
public void SetAction(Action<IEntity> action);
```

- **Description:** Sets the callback action that will be invoked whenever a tracked entity’s values change.
- **Parameter:** `action` — The delegate to invoke on the entity. Cannot be `null`.
- **Exception:** `ArgumentNullException` — Thrown if `action` is `null`.

#### `Track(IEntity)`

```csharp
public void Track(IEntity entity);
```

- **Description:** Starts tracking the specified entity for value changes.
- **Parameter:** `entity` — The entity to track.
- **Note:** Subscribes to `OnValueAdded`, `OnValueDeleted`, and/or `OnValueChanged` events depending on constructor
  parameters.

#### `Untrack(IEntity)`

```csharp
public void Untrack(IEntity entity);
```

- **Description:** Stops tracking the specified entity for value changes.
- **Parameter:** `entity` — The entity to stop monitoring.
- **Note:** Unsubscribes from previously subscribed value events.