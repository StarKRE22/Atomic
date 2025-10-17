# 🧩 SubscriptionEntityTrigger\<S>

```csharp
public abstract class SubscriptionEntityTrigger<S> : SubscriptionEntityTrigger<IEntity, S>
    where S : IDisposable
```

- **Description:** A **non-generic shortcut** for [SubscriptionEntityTrigger<E, S>](SubscriptionEntityTrigger%601.md),  
  specialized for working with the base [IEntity](../Entities/IEntity.md) type.  
  Provides infrastructure for tracking entities via **disposable subscriptions**, without needing to specify an entity
  type parameter.
- **Type Parameter:** `S` — The subscription type, which must implement `IDisposable`.
- **Inheritance:** [SubscriptionEntityTrigger<E, S>](SubscriptionEntityTrigger%601.md)

---

## 🏹 Methods

#### `SetAction(Action<IEntity>)`

```csharp
public void SetAction(Action<IEntity> action);
```

- **Description:** Sets the callback action that will be invoked whenever the subscription detects a change in the
  entity.
- **Parameter:** `action` — The delegate to invoke. Cannot be `null`.
- **Exception:** `ArgumentNullException` — Thrown if `action` is `null`.

---

#### `Track(IEntity)`

```csharp
public void Track(IEntity entity);
```

- **Description:** Starts tracking the specified entity by creating a new subscription via the abstract  
  `Track(IEntity, Action<IEntity>)` method.
- **Parameter:** `entity` — The entity to track.

---

#### `Untrack(IEntity)`

```csharp
public void Untrack(IEntity entity);
```

- **Description:** Stops tracking the specified entity.
- **Parameter:** `entity` — The entity to stop tracking.

---

#### `Track(IEntity, Action<IEntity>)`

```csharp
protected abstract S Track(IEntity entity, Action<IEntity> callback);
```

- **Description:** Defines the logic for creating a subscription for a specific entity.
- **Parameters:**
    - `entity` — The entity to track.
    - `callback` — The callback action to invoke when the entity changes or requires re-evaluation.
- **Returns:** An `IDisposable` representing the active subscription for the entity.
- **Note:** Must be implemented in derived classes.

---

## 🗂 Example of Usage

```csharp
public sealed class HealthTrigger : SubscriptionEntityTrigger<Subscription<int>>
{
    protected override Subscription<int> Track(IEntity entity, Action<IEntity> callback)
    {
        var health = entity.GetValue<IReactiveValue<int>>("Health");
        return health.Subscribe(_ => callback.Invoke(entity));
    }
}
```

<!-- 

# 🧩 SubscriptionEntityTrigger


---

## Overview


- **Non-generic version:** `SubscriptionEntityTrigger` — works with plain `IEntity`.
- **Generic version:** `SubscriptionEntityTrigger<E>` — works with specific entity types.

---

## SubscriptionEntityTrigger

- Non-generic shortcut for `SubscriptionEntityTrigger<IEntity>`.
```csharp
public abstract class SubscriptionEntityTrigger : SubscriptionEntityTrigger<IEntity>
```

---

## SubscriptionEntityTrigger<E>

- Generic base for triggers that maintain **subscriptions** per entity.
- Inherits from `EntityTriggerBase<E>`.
- Automatically tracks and disposes subscriptions for each entity.

```csharp
public abstract class SubscriptionEntityTrigger<E> : EntityTriggerBase<E> where E : IEntity
```

---

## Methods

### Track
```csharp
public sealed override void Track(E entity)
```
- Starts tracking the specified entity.
- Creates a subscription via `Track(E, Action<E>)` and stores it for later disposal.
- If the entity is already tracked, does nothing.

### Untrack
```csharp
public sealed override void Untrack(E entity)
```
- Stops tracking the specified entity.
- Disposes the associated subscription to release resources.

### Track (abstract)
```csharp
protected abstract IDisposable Track(E entity, Action<E> callback)
```
- Defines logic for creating a subscription for a specific entity.
- Must return an `IDisposable` representing the subscription.
- The returned subscription is stored and disposed when `Untrack(E)` is called.
- **Parameters:**
    - `entity` — The entity to track.
    - `callback` — The action to invoke when the entity changes or needs re-evaluation.
- **Returns:** `IDisposable` representing the subscription.

---

## Example Usage
**Trigger for player teams**

### Non-Generic version
```csharp
public sealed class TeamEntityTrigger : SubscriptionEntityTrigger
{
    protected override IDisposable Track(IEntity entity, Action<IEntity> callback) 
    {
        IReactiveVariable<TeamType> teamType = entity.GetValue<IReactiveVariable<TeamType>>();
        IDisposable handle = teamType.Subscribe(_ => callback.Invoke(entity));
        return handle;
    } 
        
}
```

-->