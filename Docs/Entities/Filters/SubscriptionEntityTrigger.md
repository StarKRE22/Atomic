# 🧩 SubscriptionEntityTrigger

A base trigger for working with **entities using subscriptions**. Provides infrastructure for tracking entities and managing disposable resources associated with each entity.

---

## Overview

`SubscriptionEntityTrigger` is designed for reactive systems where entity changes are tracked via **subscriptions** (`IDisposable`). It automatically manages creation and disposal of subscriptions for each tracked entity.

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

### Generic version
