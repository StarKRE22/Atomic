# 🧩 IEntityTrigger\<E>

```csharp
public interface IEntityTrigger<E> where E : IEntity
```

- **Description:** Represents a **trigger mechanism** that monitors specific aspects of an entity’s state and signals
  when the entity should be re-evaluated by an [EntityFilter\<E>](EntityFilter%601.md).
- **Type Parameter:** `E` — The type of entity being tracked. Must implement [IEntity](../Entities/IEntity.md).
- **Note:** Attach triggers to a filter to automatically update the filtered set when entities mutate (e.g., health
  changes, status flags toggle).

---

## 🏹 Methods

#### `SetAction(Action<E>)`

```csharp
void SetAction(Action<E> action);
```

- **Description:** Assigns a callback that will be invoked when the tracked entity changes in a way that may affect
  filter inclusion.
- **Parameters:**
    - `action` — A delegate that takes the entity as a parameter and re-evaluates it in the filter.
- **Notes:**
    - Usually set once by the filter itself.

---

#### `Track(E)`

```csharp
void Track(E entity);
```

- **Description:** Starts tracking the specified entity for relevant state changes.
- **Parameters:**
    - `entity` — The entity instance to start monitoring.
- **Notes:**
    - The trigger should subscribe to internal events or state changes of the entity.

---

#### `Untrack(E)`

```csharp
void Untrack(E entity);
```

- **Description:** Stops tracking the specified entity.
- **Parameters:**
    - `entity` — The entity instance to stop monitoring.
- **Notes:**
    - The trigger should unsubscribe from any previously registered callbacks.

---

## 🗂 Example of Usage

```csharp
// A trigger that listens to Health changes
public class HealthChangedTrigger : IEntityTrigger<GameEntity>
{
private Action<GameEntity> _onChanged;

    public void SetAction(Action<GameEntity> action)
    {
        _onChanged = action;
    }

    public void Track(GameEntity entity)
    {
        entity.OnHealthChanged += HandleHealthChanged;
    }

    public void Untrack(GameEntity entity)
    {
        entity.OnHealthChanged -= HandleHealthChanged;
    }

    private void HandleHealthChanged(GameEntity entity)
    {
        // Notify filter to re-evaluate this entity
        _onChanged?.Invoke(entity);
    }
}

// Usage with EntityFilter
var filter = new EntityFilter<GameEntity>(
source: allEntities,
predicate: e => e.Health > 0,
new HealthChangedTrigger()
);
```
