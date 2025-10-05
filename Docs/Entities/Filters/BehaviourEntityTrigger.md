# 🧩 BehaviourEntityTrigger

```csharp
public class BehaviourEntityTrigger : BehaviourEntityTrigger<IEntity>
```

- **Description:** A non-generic shortcut for [BehaviourEntityTrigger\<E>](BehaviourEntityTrigger%601.md).  
  Subscribes to behaviour-related events (`OnBehaviourAdded`, `OnBehaviourRemoved`) on general [IEntity](../Entities/IEntity.md) instances.
- **Inheritance:** [BehaviourEntityTrigger\<E>](BehaviourEntityTrigger%601.md)

---

## 🏗️ Constructor

```csharp
public BehaviourEntityTrigger(bool added = true, bool removed = true)
```

- **Description:** Creates a new non-generic `BehaviourEntityTrigger` instance.
- **Parameters:**
    - `added` — Whether to react to behaviour additions via the `OnBehaviourAdded` event. Default is `true`.
    - `removed` — Whether to react to behaviour removals via the `OnBehaviourRemoved` event. Default is `true`.

---

## 🏹 Methods

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

---

## 🗂 Example of Usage

```csharp
// Track general entities for behaviour additions and removals
var trigger = new BehaviourEntityTrigger(added: true, removed: true);

// Usage with non-generic EntityFilter
var filter = new EntityFilter(
    allEntities,
    e => e.HasBehaviour("Attack"),
    trigger
);
```




<!-- 

# 🧩 BehaviourEntityTrigger

A trigger that responds to **behaviour changes** (added or removed) on entities. Provides both a **generic** and a **non-generic** version for flexible use with any entity type.

---

## Overview

`BehaviourEntityTrigger` allows monitoring **behaviours** added or removed from entities that implement `IEntity`. It invokes a configured action whenever a tracked entity’s behaviours change.

- **Non-generic version:** `BehaviourEntityTrigger` — works with plain `IEntity`.
- **Generic version:** `BehaviourEntityTrigger<E>` — works with specific entity types.

---

## BehaviourEntityTrigger

- Non-generic shortcut for `BehaviourEntityTrigger<IEntity>`.
- Subscribes to behaviour-related events (`OnBehaviourAdded`, `OnBehaviourRemoved`) on basic `IEntity` instances.

```csharp
public class BehaviourEntityTrigger : BehaviourEntityTrigger<IEntity>
```

### Constructor
```csharp
public BehaviourEntityTrigger(bool added = true, bool removed = true)
```
- **Parameters:**
    - `added` — react to behaviour additions (default: true)
    - `removed` — react to behaviour removals (default: true)
---

## BehaviourEntityTrigger<E>
- Generic version for specific entity types `E`.
- Inherits from `EntityTriggerBase<E>`.
- Subscribes to entity behaviour events.
```csharp
public class BehaviourEntityTrigger<E> : EntityTriggerBase<E> where E : IEntity
```

### Constructor
```csharp
public BehaviourEntityTrigger(bool added = true, bool removed = true)
```
- **Parameters:**
    - `added` — react to behaviour additions (default: true)
    - `removed` — react to behaviour removals (default: true)
---

## Methods

### Track
```csharp
public override void Track(E entity)
```
- Subscribes to behaviour-related events on the given entity.

### Untrack
```csharp
public override void Untrack(E entity)
```
- Unsubscribes from behaviour-related events on the given entity.

---

## Usage Example

### Non-Generic Usage

```csharp
var behaviourTrigger = new BehaviourEntityTrigger();
behaviourTrigger.SetAction(entity => 
    Console.WriteLine($"Behaviour changed on entity: {entity.Name}"));

// Track entities
behaviourTrigger.Track(playerEntity);
behaviourTrigger.Track(enemyEntity);

// Adding a behaviour triggers the action
playerEntity.AddBehaviour(new MovementBehaviour());

// Removing a behaviour triggers the action
enemyEntity.RemoveBehaviour(new HealthBehaviour());
```


### Generic Usage

```csharp
var behaviourTrigger = new BehaviourEntityTrigger<UnitEntity>();
behaviourTrigger.SetAction(entity => 
    Console.WriteLine($"Behaviour changed on entity: {entity.Name}"));

// Track entities
behaviourTrigger.Track(playerEntity);
behaviourTrigger.Track(enemyEntity);

// Adding a behaviour triggers the action
playerEntity.AddBehaviour(new MovementBehaviour());

// Removing a behaviour triggers the action
enemyEntity.RemoveBehaviour(new HealthBehaviour());
```

### Only Added Behaviours

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

### Only Removed

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
-->