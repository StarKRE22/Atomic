# 🧩 IEntityTrigger

```csharp
public interface IEntityTrigger : IEntityTrigger<IEntity>
```

- **Description:** A **non-generic shorthand** for `IEntityTrigger<E>` with the default type `IEntity`.  
  Represents a trigger that reacts to an `IEntity` interaction or condition.
- **Inheritance:** [IEntityTrigger\<E>](IEntityTrigger%601.md)
- **See also:** [IEntity](../Entities/IEntity.md)

> [!NOTE]  
> Use this version when you want to create triggers for **general entities** without binding to a specific type.

---

## 🏹 Methods

#### `SetAction(Action<IEntity>)`

```csharp
public void SetAction(Action<IEntity> action);
```

- **Description:** Assigns a callback invoked when a tracked entity changes and needs re-evaluation.
- **Parameter:** `action` — A delegate that takes an `IEntity` instance and triggers re-evaluation in a filter.

---

#### `Track(IEntity)`

```csharp
public void Track(IEntity entity);
```

- **Description:** Starts tracking the specified entity for relevant state changes or interactions.
- **Parameter:** `entity` — The entity instance to monitor.

---

#### `Untrack(IEntity)`

```csharp
public void Untrack(IEntity entity);
```

- **Description:** Stops tracking the specified entity.
- **Parameter:** `entity` — The entity instance to stop monitoring.

---

## 🗂 Example of Usage

```csharp
// A simple trigger that fires when an entity's "IsActive" flag changes
public class ActiveStateTrigger : IEntityTrigger
{
    private readonly Dictionary<IEntity, Subscription<bool>> _subscriptions = new();
    
    private Action<IEntity> _onChanged;

    public void SetAction(Action<IEntity> action)
    {
        _onChanged = action;
    }

    public void Track(IEntity entity)
    {
        Subscription<bool> subscription = entity
            .GetValue<IReactiveValue<bool>>("IsActive")
            .Subscribe(_ => _onChanged?.Invoke(entity));
        
       _subscriptions.Add(entity, subscription);
    }

    public void Untrack(IEntity entity)
    {
        if (_subscriptions.Remove(entity, out Subscription<int> subscription))
           subscription.Dispose();
    }
}
```

```csharp
// Usage with non-generic EntityFilter
var filter = new EntityFilter(
    allEntities,
    e => e.GetValue<IReactiveValue<bool>>("IsActive").Value,
    new ActiveStateTrigger()
);
```