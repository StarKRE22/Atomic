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
public void SetAction(Action<E> action);
```

- **Description:** Assigns a callback that will be invoked when the tracked entity changes in a way that may affect
  filter inclusion.
- **Parameter:** `action` — A delegate that takes the entity as a parameter and re-evaluates it in the filter.
- **Note:** Usually set once by the filter itself.

#### `Track(E)`

```csharp
public void Track(E entity);
```

- **Description:** Starts tracking the specified entity for relevant state changes.
- **Parameter:** `entity` — The entity instance to start monitoring.
- **Note:** The trigger should subscribe to internal events or state changes of the entity.

#### `Untrack(E)`

```csharp
public void Untrack(E entity);
```

- **Description:** Stops tracking the specified entity.
- **Parameter:** `entity` — The entity instance to stop monitoring.
- **Note:** The trigger should unsubscribe from any previously registered callbacks.

---

## 🗂 Example of Usage

```csharp
// A trigger that listens to Health changes
public class HealthChangedTrigger : IEntityTrigger<GameEntity>
{
    private readonly Dictionary<GameEntity, Subscription<int>> _subscriptions = new();
    
    private Action<GameEntity> _onChanged;

    public void SetAction(Action<GameEntity> action)
    {
        _onChanged = action;
    }

    public void Track(GameEntity entity)
    {
       Subscription<int> subscription = entity
            .GetValue<IReactiveValue<int>>("Health")
            .Subscribe(_ => _onChanged?.Invoke(entity));
        
       _subscriptions.Add(entity, subscription);
    }

    public void Untrack(GameEntity entity)
    {
        if (_subscriptions.Remove(entity, out Subscription<int> subscription))
              subscription.Dispose();
    }
}
```

```csharp
// Usage with EntityFilter
var filter = new EntityFilter<GameEntity>(
    source: allEntities,
    predicate: e => e.GetValue<IReactiveValue>("Health").Value > 0,
    new HealthChangedTrigger()
);
```
