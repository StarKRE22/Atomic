# 🧩 Entity Triggers

**Entity Triggers** are reactive tools that allow systems to **respond automatically** to changes in entity state, tags,
values, behaviours, or custom conditions. They are a core part of a **reactive entity management system**, often used
together with [EntityFilters](Manual.md) to maintain dynamic subsets of entities.

---

## 📑 Table of Contents

- [Examples of Usage](#-examples-of-usage)
    - [Tag Trigger](#ex1)
    - [Value Trigger](#ex2)
    - [Custom Trigger](#ex3)
- [API Reference](#-api-reference)
- [Notes](#-notes)

---

## 🗂 Examples of Usage

<div id="ex1"></div>

### 1️⃣ Tag Trigger

[TagEntityTrigger](TagEntityTriggers.md) automatically tracks when tags are added to or removed from entities, triggering
callbacks for reactive system updates. Essential for building systems that respond to entity state classification changes, role
updates, or behavioral flag modifications.

```csharp
// Track general entities for tag additions/removals
var trigger = new TagEntityTrigger(added: true, deleted: true);

// Usage with non-generic EntityFilter
var filter = new EntityFilter(
    allEntities,
    e => e.HasTag("Enemy"),
    trigger
);
```

<div id="ex2"></div>

### 2️⃣ Value Trigger

[ValueEntityTrigger](ValueEntityTriggers.md) automatically tracks when values are added, removed, or changed on entities, triggering callbacks
for reactive system updates.

```csharp
// Track general entities for value additions, deletions, and modifications
var trigger = new ValueEntityTrigger(
    added: true,
    deleted: true,
    changed: true
);

// Usage with non-generic EntityFilter
var filter = new EntityFilter(
    allEntities,
    e => e.GetValue<int>("Health") > 0,
    trigger
);
```

<div id="ex3"></div>

### 3️⃣ Custom Trigger

Also, you can implement your own entity trigger and track entity state manually:

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

And use it with non-generic [EntityFilter](EntityFilter.md):

```csharp
var filter = new EntityFilter(
    allEntities,
    e => e.GetValue<IReactiveValue<bool>>("IsActive").Value,
    new ActiveStateTrigger()
);
```

---

## 🔍 API Reference

Below is a list of available trigger types:

- Interfaces
    - [IEntityTrigger](IEntityTrigger.md) <!-- + -->
    - [IEntityTrigger\<E>](IEntityTrigger%601.md) <!-- + -->
- [TagTriggers](TagEntityTriggers.md)
    - [TagEntityTrigger](TagEntityTrigger.md) <!-- + -->
    - [TagEntityTrigger\<E>](TagEntityTrigger%601.md) <!-- + -->
- [ValueTriggers](ValueEntityTriggers.md)
    - [ValueEntityTrigger](ValueEntityTrigger.md) <!-- + -->
    - [ValueEntityTrigger\<E>](ValueEntityTrigger%601.md) <!-- + -->
- [BehaviourTriggers](BehaviourEntityTriggers.md)
    - [BehaviourEntityTrigger](BehaviourEntityTrigger.md) <!-- + -->
    - [BehaviourEntityTrigger\<E>](BehaviourEntityTrigger%601.md) <!-- + -->
- [StateChangeTriggers](StateChangedEntityTriggers.md)
    - [StateChangedEntityTrigger](StateChangedEntityTrigger.md)
    - [StateChangedEntityTrigger\<E>](StateChangedEntityTrigger%601.md) <!-- + -->
- [SubscriptionTriggers](SubscriptionEntityTriggers.md)
    - [SubscriptionEntityTrigger\<S>](SubscriptionEntityTrigger.md) <!-- + -->
    - [SubscriptionEntityTrigger\<E, S>](SubscriptionEntityTrigger%601.md) <!-- + -->
- [InlineTriggers](InlineEntityTriggers.md)
    - [InlineEntityTrigger](InlineEntityTrigger.md) <!-- + -->
    - [InlineEntityTrigger\<E>](InlineEntityTrigger%601.md) <!-- + -->

---

## 📝 Notes

### Action-Based Callbacks

- Configurable callback system for re-evaluation notifications
- Generic action support for type-safe entity handling
- Flexible trigger response patterns

### Entity Tracking

- Track/untrack lifecycle for entity monitoring
- Multiple entity support per trigger instance
- Clean resource management patterns

### Type Safety

- Generic interface for specific entity types
- Compile-time type checking for callbacks
- Non-generic convenience interface available