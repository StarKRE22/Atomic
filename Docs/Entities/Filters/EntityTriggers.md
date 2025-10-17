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

[TagEntityTrigger](TagEntityTrigger.md) automatically tracks when tags are added to or removed from entities, triggering
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

Usage with non-generic EntityFilter

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
- [TagEntityTriggers](TagEntityTriggers.md)
    - [TagEntityTrigger](TagEntityTrigger.md) <!-- + -->
    - [TagEntityTrigger\<E>](TagEntityTrigger%601.md) <!-- + -->
- **ValueTriggers**
    - [ValueEntityTrigger](ValueEntityTrigger.md) <!-- + -->
    - [ValueEntityTrigger\<E>](ValueEntityTrigger%601.md) <!-- + -->
- **BehaviourTriggers**
    - [BehaviourEntityTrigger](BehaviourEntityTrigger.md) <!-- + -->
    - [BehaviourEntityTrigger\<E>](BehaviourEntityTrigger%601.md) <!-- + -->
- **StateChangeTriggers**
    - [StateChangedEntityTrigger](StateChangedEntityTrigger.md)
    - [StateChangedEntityTrigger\<E>](StateChangedEntityTrigger%601.md) <!-- + -->
- **SubscriptionTriggers**
    - [SubscriptionEntityTrigger\<S>](SubscriptionEntityTrigger.md) <!-- + -->
    - [SubscriptionEntityTrigger\<E, S>](SubscriptionEntityTrigger%601.md) <!-- + -->
- **InlineTriggers**
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

<!--

### 1️⃣ Tag Trigger

```csharp
var tagTrigger = new TagEntityTrigger<GameEntity>(added: true, deleted: true);
tagTrigger.SetAction(e => Console.WriteLine($"Tag changed on {e.Name}"));
tagTrigger.Track(someEntity);
```

---

### 2️⃣ Value Trigger

```csharp
var valueTrigger = new ValueEntityTrigger<GameEntity>(added: true, deleted: true, changed: true);
valueTrigger.SetAction(e => Console.WriteLine($"{e.Name}'s value changed"));
valueTrigger.Track(someEntity);
```

---

### 3️⃣ Inline Trigger

```csharp
var inlineTrigger = new InlineEntityTrigger<GameEntity>(
    track: (e, cb) => e.OnTagAdded += _ => cb(e),
    untrack: (e, cb) => e.OnTagAdded -= _ => cb(e)
);
inlineTrigger.SetAction(e => Console.WriteLine($"Custom trigger fired for {e.Name}"));
inlineTrigger.Track(someEntity);
```

---

### 4️⃣ State Change Trigger

```csharp
var stateTrigger = new StateChangedEntityTrigger<GameEntity>();
stateTrigger.SetAction(e => Console.WriteLine($"State changed: {e.Name}"));
stateTrigger.Track(someEntity);
````

-->

<!--


## Example Usage

```csharp
//Create a simple tag trigger
public class TagEntityTrigger : IEntityTrigger
{
    private Action<IEntity> _callback;

    public void SetAction(Action<IEntity> action) =>
        _callback = action ?? throw new ArgumentNullException(nameof(action));
    
    public void Track(IEntity entity) => 
        entity.OnTagAdded += _callback.Invoke;
    
    public void Untrack(IEntity entity) =>
         entity.OnTagAdded -= _callback.Invoke;
}
```