# 🧩 SubscriptionEntityTrigger\<S>

A **non-generic shortcut** for [SubscriptionEntityTrigger<E, S>](SubscriptionEntityTrigger%601.md), specialized for
working with the base [IEntity](../Entities/IEntity.md) type. Provides infrastructure for tracking entities via
**disposable subscriptions**, without needing to specify an entity type parameter.

---

## 📑 Table of Contents

- [Examples of Usage](#-examples-of-usage)
    - [Health Trigger](#ex1)
    - [Team Trigger](#ex2)
- [API Reference](#-api-reference)
    - [Type](#-type)
    - [Methods](#-methods)
        - [Track(IEntity, Action\<IEntity>)](#trackientity-actionientity)
        - [Track(IEntity)](#trackientity)
        - [Untrack(IEntity)](#untrackientity)
        - [SetAction(Action\<IEntity>)](#setactionactionientity)

## 🗂 Examples of Usage

<div id="ex1"></div>

### 1️⃣ Health Trigger

```csharp
public sealed class HealthTrigger : SubscriptionEntityTrigger<Subscription<int>>
{
    protected override Subscription<int> Track(IEntity entity, Action<IEntity> callback)
    {
        IReactiveValue<int> health = entity.GetValue<IReactiveValue<int>>("Health");
        Subscription<int> handle = health.Subscribe(_ => callback.Invoke(entity)); //IDisposable
        return handle;
    }
}
```

<div id="ex2"></div>

### 2️⃣ Team Trigger

```csharp
public sealed class TeamEntityTrigger : SubscriptionEntityTrigger<Subscription<<TeamType>>>
{
    protected override Subscription<<TeamType> Track(IEntity entity, Action<IEntity> callback) 
    {
        IReactiveVariable<TeamType> teamType = entity.GetValue<IReactiveVariable<TeamType>>();
        Subscription<<TeamType> handle = teamType.Subscribe(_ => callback.Invoke(entity)); //IDisposable
        return handle;
    }
}
```

---

## 🔍 API Reference

### 🏛️ Type <div id="-type"></div>

```csharp
public abstract class SubscriptionEntityTrigger<S> : SubscriptionEntityTrigger<IEntity, S>
    where S : IDisposable
```

- **Type Parameter:** `S` — The subscription type, which must implement `IDisposable`.
- **Inheritance:** [SubscriptionEntityTrigger<E, S>](SubscriptionEntityTrigger%601.md)

---

### 🏹 Methods

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

#### `Track(IEntity)`

```csharp
public void Track(IEntity entity);
```

- **Description:** Starts tracking the specified entity by creating a new subscription via the abstract  
  `Track(IEntity, Action<IEntity>)` method.
- **Parameter:** `entity` — The entity to track.

#### `Untrack(IEntity)`

```csharp
public void Untrack(IEntity entity);
```

- **Description:** Stops tracking the specified entity.
- **Parameter:** `entity` — The entity to stop tracking.

#### `SetAction(Action<IEntity>)`

```csharp
public void SetAction(Action<IEntity> action);
```

- **Description:** Sets the callback action that will be invoked whenever the subscription detects a change in the
  entity.
- **Parameter:** `action` — The delegate to invoke. Cannot be `null`.
- **Exception:** `ArgumentNullException` — Thrown if `action` is `null`.