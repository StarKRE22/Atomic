# 🧩 BehaviourEntityTrigger

A non-generic shortcut for [BehaviourEntityTrigger\<E>](BehaviourEntityTrigger%601.md).  
Subscribes to behaviour-related events (`OnBehaviourAdded`, `OnBehaviourRemoved`) on
general [IEntity](../Entities/IEntity.md) instances.

---

## 📑 Table of Contents

- [Examples of Usage](#examples-of-usage)
    - [Custom Usage](#ex1)
    - [Only Added Monitoring](#ex2)
    - [Only Removed Monitoring](#ex3)
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

### 1️⃣ Custom Usage

```csharp
var behaviourTrigger = new BehaviourEntityTrigger();
behaviourTrigger.SetAction(entity => Console.WriteLine($"Behaviour changed on entity: {entity.Name}"));

// Track entities
behaviourTrigger.Track(playerEntity);
behaviourTrigger.Track(enemyEntity);

// Adding a behaviour triggers the action
playerEntity.AddBehaviour(new MovementBehaviour());

// Removing a behaviour triggers the action
enemyEntity.RemoveBehaviour(new HealthBehaviour());
```

<div id="ex2"></div>

### 2️⃣ Only Added Monitoring

```csharp
var addedTrigger = new BehaviourEntityTrigger(added: true, removed: false);
addedTrigger.SetAction(entity =>
    Console.WriteLine($"New behaviour added to entity: {entity.Name}"));

// Track entities
addedTrigger.Track(playerEntity);

// Only additions trigger the callback
playerEntity.AddBehaviour(new MovementBehaviour());  // Triggers callback
playerEntity.RemoveBehaviour(new HealthBehaviour()); // Does NOT trigger
```

<div id="ex3"></div>

### 3️⃣ Only Removed Monitoring

```csharp
var removedTrigger = new BehaviourEntityTrigger(added: false, removed: true);
removedTrigger.SetAction(entity =>
    Console.WriteLine($"Behaviour removed from entity: {entity.Name}"));

// Track entities
removedTrigger.Track(enemyEntity);

// Only removals trigger the callback
enemyEntity.AddBehaviour(new MovementBehaviour());    // Does NOT trigger
enemyEntity.RemoveBehaviour(new HealthBehaviour());  // Triggers callback
```

---

## 🔍 API Reference

### 🏛️ Type <div id="-type"></div>

```csharp
public class BehaviourEntityTrigger : BehaviourEntityTrigger<IEntity>
```

- **Inheritance:** [BehaviourEntityTrigger\<E>](BehaviourEntityTrigger%601.md)

---

<div id="-constructor"></div>

### 🏗️ Constructor

```csharp
public BehaviourEntityTrigger(bool added = true, bool removed = true)
```

- **Description:** Creates a new non-generic `BehaviourEntityTrigger` instance.
- **Parameters:**
    - `added` — Whether to react to behaviour additions via the `OnBehaviourAdded` event. Default is `true`.
    - `removed` — Whether to react to behaviour removals via the `OnBehaviourRemoved` event. Default is `true`.

---

### 🏹 Methods

#### `SetAction(Action<IEntity>)`

```csharp
public void SetAction(Action<IEntity> action);
```

- **Description:** Sets the callback action that will be invoked whenever a tracked entity’s behaviours change.
- **Parameter:** `action` — The delegate to invoke on the entity. Cannot be `null`.
- **Exception:** `ArgumentNullException` — Thrown if `action` is `null`.

#### `Track(IEntity)`

```csharp
public void Track(IEntity entity);
```

- **Description:** Starts tracking the specified entity for behaviour changes.
- **Parameter:** `entity` — The entity to track.
- **Note:** Subscribes to `OnBehaviourAdded` and/or `OnBehaviourRemoved` events depending on constructor parameters.

#### `Untrack(IEntity)`

```csharp
public void Untrack(IEntity entity);
```

- **Description:** Stops tracking the specified entity for behaviour changes.
- **Parameter:** `entity` — The entity to stop monitoring.
- **Note:** Unsubscribes from previously subscribed behaviour events.