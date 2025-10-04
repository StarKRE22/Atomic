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