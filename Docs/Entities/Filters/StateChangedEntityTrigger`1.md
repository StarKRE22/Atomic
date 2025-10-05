# 🧩 StateChangedEntityTrigger\<E>

```csharp
public class StateChangedEntityTrigger<E> : IEntityTrigger<E> where E : IEntity
```

- **Description:** A trigger that responds to **state changes** on entities of type `E`.  
  Allows an [EntityFilter\<E>](EntityFilter%601.md) to automatically re-evaluate entities when their state changes.
- **Type Parameter:** `E` — The entity type being tracked. Must implement [IEntity](../Entities/IEntity.md).
- **Inheritance:** [IEntityTrigger\<E>](IEntityTrigger%601.md)

---

## 🏹 Methods

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

---

## 🗂 Example of Usage

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
