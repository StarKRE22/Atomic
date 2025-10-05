# 🧩 StateChangedEntityTrigger

```csharp
public class StateChangedEntityTrigger : StateChangedEntityTrigger<IEntity>
```

- **Description:** A **non-generic shortcut** for [StateChangedEntityTrigger\<E>](StateChangedEntityTrigger%601.md).  
  It allows subscribing to **state change events** (`OnStateChanged`) directly for
  basic [IEntity](../Entities/IEntity.md) objects without specifying a generic type parameter.
- **Inheritance:** [StateChangedEntityTrigger\<E>](StateChangedEntityTrigger%601.md)

---

## 🏗️ Constructor

```csharp
public StateChangedEntityTrigger();
```

- **Description:** Initializes a new instance of the `StateChangedEntityTrigger`.
- **Note:** No parameters are needed; the trigger automatically hooks into the entity’s `OnStateChanged` event.

---

## 🏹 Methods

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

---

## 🗂 Example of Usage

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

<!--
# 🧩 StateChangedEntityTrigger

A trigger that responds to **state changes** on entities. Provides both a **generic** and a **non-generic** version for flexible use with any entity type.

---

## Overview

`StateChangedEntityTrigger` allows monitoring **state changes** on entities that implement `IEntity`. It invokes a configured action whenever a tracked entity’s state changes.

- **Non-generic version:** `StateChangedEntityTrigger` — works with plain `IEntity`.
- **Generic version:** `StateChangedEntityTrigger<E>` — works with specific entity types.

---

## StateChangedEntityTrigger

- Non-generic shortcut for `StateChangedEntityTrigger<IEntity>`.
- Subscribes to state change events (`OnStateChanged`) on basic `IEntity` instances.
```csharp
public class StateChangedEntityTrigger : StateChangedEntityTrigger<IEntity>
```
---

## StateChangedEntityTrigger<E>

- Generic version for specific entity types `E`.
- Inherits from `EntityTriggerBase<E>`.
- Subscribes to entity state change events.

```csharp
public class StateChangedEntityTrigger<E> : EntityTriggerBase<E> where E : IEntity
```

---

## Methods

### Track
```csharp
public override void Track(E entity)
```
- Subscribes to state change events on the given entity.

### Untrack
```csharp
public override void Untrack(E entity)
```
- Unsubscribes from state change events on the given entity.

### OnStateChanged
```csharp
private void OnStateChanged(IEntity entity)
```
- Called when the entity's state changes.
- Invokes the configured action with the tracked entity.

---

## Usage Example

### Non-Generic Usage

```csharp
var stateTrigger = new StateChangedEntityTrigger();
stateTrigger.SetAction(entity =>
Console.WriteLine($"State changed on entity: {entity.Name}"));

// Track entities
stateTrigger.Track(playerEntity);
stateTrigger.Track(enemyEntity);

// Changing state triggers the action
playerEntity.ChangeState(UnitState.Moving);
enemyEntity.ChangeState(UnitState.Attacking);
```

---

### Generic Usage

```csharp
var stateTrigger = new StateChangedEntityTrigger<UnitEntity>();
stateTrigger.SetAction(entity =>
Console.WriteLine($"State changed on entity: {entity.Name}"));

// Track entities
stateTrigger.Track(playerEntity);
stateTrigger.Track(enemyEntity);

// Changing state triggers the action
playerEntity.ChangeState(UnitState.Moving);
enemyEntity.ChangeState(UnitState.Attacking);
```
-->