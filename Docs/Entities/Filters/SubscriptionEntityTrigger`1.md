# 🧩 SubscriptionEntityTrigger<E, S>

```csharp
public abstract class SubscriptionEntityTrigger<E, S> : IEntityTrigger<E>
    where E : IEntity
    where S : IDisposable
```

- **Description:** An abstract base class for entity triggers that rely on **subscriptions**. It manages the lifecycle
  of `IDisposable` subscription objects for each tracked entity, ensuring proper cleanup when entities are untracked.

- **Type Parameters:**
    - `E` — The entity type being tracked. Must implement [IEntity](../Entities/IEntity.md).
    - `S` — The subscription type, which must implement `IDisposable`

- **Inheritance:** [IEntityTrigger\<E>](IEntityTrigger%601.md)

---

## 🏹 Methods

#### `SetAction(Action<E>)`

```csharp
public void SetAction(Action<E> action);
```

- **Description:** Sets the callback action that will be invoked whenever the subscription detects a change in the
  entity.
- **Parameter:** `action` — The delegate to invoke. Cannot be `null`.
- **Exception:** `ArgumentNullException` — Thrown if `action` is `null`.

---

#### `Track(E)`

```csharp
public void Track(E entity);
```

- **Description:** Starts tracking the specified entity by creating a new subscription via the abstract
  `Track(E, Action<E>)` method.
- **Parameter:** `entity` — The entity to track.
- **Behavior:**
    - Creates a subscription if the entity is not already tracked.
    - Stores the resulting `IDisposable` object in an internal dictionary for later disposal.

---

#### `Untrack(E)`

```csharp
public void Untrack(E entity);
```

- **Description:** Stops tracking the specified entity.
- **Parameter:** `entity` — The entity to stop tracking.
- **Behavior:**
    - Removes the associated subscription from the internal dictionary.
    - Calls `Dispose()` on the subscription to release resources.

---

#### `Track(E, Action<E>)`

```csharp
protected abstract S Track(E entity, Action<E> callback);
```

- **Description:** Defines the logic for creating a subscription for a specific entity.
- **Parameters:**
    - `entity` — The entity to track.
    - `callback` — The callback action to invoke when the entity changes or requires re-evaluation.
- **Returns:** An `IDisposable` representing the active subscription for the entity.
- **Note:** Implementations must ensure that the returned subscription will eventually invoke the callback as needed.

---

## 🗂 Example of Usage

```csharp
public sealed class TeamEntityTrigger : SubscriptionEntityTrigger<IUnitEntity, Subscription<TeamType>>
{
    protected override Subscription<TeamType> Track(IUnitEntity entity, Action<IUnitEntity> callback) 
    {
        IReactiveValue<TeamType> teamType = entity.GetValue<IReactiveValue<TeamType>>("Team");
        Subscription<TeamType> subscription = teamType.Subscribe(_ => callback.Invoke(entity));
        return subscription;
    } 
}
```