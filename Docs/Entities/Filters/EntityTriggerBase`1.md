# 🧩 EntityTriggerBase\<E>

```csharp
public abstract class EntityTriggerBase<E> : IEntityTrigger<E> where E : IEntity
```

- **Description:** Provides a **base implementation** for `IEntityTrigger<E>` that monitors entity state changes and
  invokes a configured action when such changes occur.
- **Type Parameter:** `E` — The type of entity being tracked. Must implement [IEntity](../Entities/IEntity.md).
- **Inheritance:** [IEntityTrigger\<E>](IEntityTrigger%601.md)

> [!NOTE]  
> This class implements common functionality (like `SetAction` and action invocation), allowing you to create custom
> triggers by inheriting and implementing `Track` / `Untrack`.

---

## 🏹 Methods

#### `SetAction(Action<E>)`

```csharp
public void SetAction(Action<E> action);
```

- **Description:** Configures the callback action that will be invoked when the trigger detects a relevant change.
- **Parameter:** `action` — A delegate to invoke for re-evaluating the entity. Cannot be `null`.
- **Exception:** `ArgumentNullException` — Thrown if `action` is `null`.

---

#### `Track(E)`

```csharp
public abstract void Track(E entity);
```

- **Description:** Starts tracking the specified entity for changes.
- **Parameter:** `entity` — The entity to begin monitoring.
- **Note:** Must be implemented by derived classes. Typically, involves subscribing to entity events.

---

#### `Untrack(E)`

```csharp
public abstract void Untrack(E entity);
```

- **Description:** Stops tracking the specified entity.
- **Parameters:** `entity` — The entity to stop monitoring.
- **Note:** Must be implemented by derived classes. Typically, involves unsubscribing from entity events.

---

#### `InvokeAction(E)`

```csharp
protected void InvokeAction(E entity);
```

- **Description:** Invokes the configured action for the given entity, if any.
- **Parameter:** `entity` — The entity to pass to the callback action.
- **Notes:** Intended for use by derived classes when a relevant change is detected.

---

## 🗂 Example of Usage

```csharp
// Custom trigger that monitors a "HealthChanged" event
public class HealthChangedTrigger : EntityTriggerBase<GameEntity>
{
    public override void Track(GameEntity entity)
    {
        entity
            .GetValue<IReactiveValue<int>>("Health")
            .OnEvent += this.OnHealthChanged;
    }

    public override void Untrack(GameEntity entity)
    {
        entity
            .GetValue<IReactiveValue<int>>("Health")
            .OnEvent -= this.OnHealthChanged;
    }

    private void HandleHealthChanged(GameEntity entity)
    {
        // Notify the filter to re-evaluate this entity
        InvokeAction(entity);
    }
}
```

```csharp
// Example usage with EntityFilter
var filter = new EntityFilter<GameEntity>(
    allEntities,
    e => e.GetValue<IReactiveValue<int>>("Health").Value > 0,
    new HealthChangedTrigger()
);
```