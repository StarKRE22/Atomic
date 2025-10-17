# 🧩 SubscriptionEntityTriggers

A base trigger for working with **entities using subscriptions**. Provides infrastructure for tracking entities and
managing disposable resources associated with each entity. The subscription trigger is designed for reactive systems
where entity changes are tracked via **subscriptions** (`IDisposable`). It automatically manages creation and disposal
of subscriptions for each tracked entity.

---

## 📑 Table of Contents

- [Examples of Usage](#-examples-of-usage)
    - [Non-generic Usage](#ex1)
    - [Generic Usage](#ex2)
- [API Reference](#-api-reference)
- [About the Generic Parameter \<S>](#-notes)

---

## 🗂 Examples of Usage

<div id="ex1"></div>

### 1️⃣ Non-generic Entity Usage

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

This example demonstrates **inheritance without specifying a concrete entity type**. `HealthTrigger` inherits from
[SubscriptionEntityTrigger\<S>](SubscriptionEntityTrigger.md) and subscribes to changes of the `"Health"` property
on any entity implementing [IEntity](../Entities/IEntity.md). Whenever the reactive property (`IReactiveValue<int>`)
changes, the provided `callback` is invoked. This approach is suitable for monitoring specific values across all
entities without tying the trigger to a specific entity type.

---

<div id="ex2"></div>

### 2️⃣ Generic Entity Usage

```csharp
public sealed class TeamEntityTrigger : SubscriptionEntityTrigger<IUnitEntity, Subscription<TeamType>>
{
    protected override Subscription<TeamType> Track(IUnitEntity entity, Action<IUnitEntity> callback) 
    {
        IReactiveValue<TeamType> teamType = entity.GetValue<IReactiveValue<TeamType>>("Team");
        Subscription<TeamType> subscription = teamType.Subscribe(_ => callback.Invoke(entity)); //IDisposable
        return subscription;
    } 
}
```

This example demonstrates **inheritance with a specific entity type (`IUnitEntity`)** and a specific subscription type
(`Subscription<TeamType>`). It allows type-safe handling of entities and reactive values without casting. Whenever the
`"Team"` value changes, the `callback` is executed, and the returned `Subscription<TeamType>` manages the lifetime of
the subscription.

---

## 🔍 API Reference

- [SubscriptionEntityTrigger\<S>](SubscriptionEntityTrigger.md)— works with plain [IEntity](../Entities/IEntity.md)
- [SubscriptionEntityTrigger\<E, S>](SubscriptionEntityTrigger%601.md) — works with specific entity types

---

<div id="-notes"></div>

## 📝 About the Generic Parameter \<S>

**`<S>` is the subscription type parameter**, which **must implement `IDisposable`**. This requirement ensures that
`SubscriptionEntityTrigger` can **reliably release resources** (e.g., unsubscribe from events or reactive streams). By
enforcing `where S : IDisposable`, you can:

- use **struct-based subscriptions**,
- avoid **boxing** when working with value-type subscriptions,
- ensure efficient memory management in high-performance systems.
