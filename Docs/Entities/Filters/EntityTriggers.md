# 🧩 Entity Triggers

**Entity Triggers** are reactive tools that allow systems to **respond automatically** to changes in entity state, tags,
values, behaviours, or custom conditions.  
They are a core part of a **reactive entity management system**, often used together with [Entity Filters](Manual.md) to
maintain dynamic subsets of entities.

- **Interfaces**
    - [IEntityTrigger](IEntityTrigger.md) <!-- + -->
    - [IEntityTrigger\<E>](IEntityTrigger%601.md) <!-- + -->
- **TagTriggers**
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

## 🗂 Examples of Usage

### 1️⃣ Tag Trigger

```csharp
var tagTrigger = new TagEntityTrigger<GameEntity>(added: true, deleted: true);
tagTrigger.SetAction(e => Console.WriteLine($"Tag changed on {e.Name}"));
tagTrigger.Track(someEntity);
`````

---

### 2️⃣ Value Trigger

```csharp
var valueTrigger = new ValueEntityTrigger<GameEntity>(added: true, deleted: true, changed: true);
valueTrigger.SetAction(e => Console.WriteLine($"{e.Name}'s value changed"));
valueTrigger.Track(someEntity);
````

---

### 3️⃣ Inline Trigger

```csharp
var inlineTrigger = new InlineEntityTrigger<GameEntity>(
    track: (e, cb) => e.OnTagAdded += _ => cb(e),
    untrack: (e, cb) => e.OnTagAdded -= _ => cb(e)
);
inlineTrigger.SetAction(e => Console.WriteLine($"Custom trigger fired for {e.Name}"));
inlineTrigger.Track(someEntity);
````

---

### 4️⃣ State Change Trigger

```csharp
var stateTrigger = new StateChangedEntityTrigger<GameEntity>();
stateTrigger.SetAction(e => Console.WriteLine($"State changed: {e.Name}"));
stateTrigger.Track(someEntity);
````



<!--

## Classes


## Trigger Types

### TagEntityTrigger

- Fires when specified tag is added/removed
- Use for state-based filtering

### ValueEntityTrigger

- Fires when specified value changes
- Use for data-based filtering

### SubscriptionEntityTrigger

- Custom trigger with manual control
- Use for complex conditions

# 🧩 IEntityTrigger

The `IEntityTrigger` interface defines a mechanism for monitoring specific aspects of an entity's state and signaling
when an entity should be re-evaluated by a filter. It comes in two forms:

* **Non-generic** version (`IEntityTrigger`) for working with `IEntity`
* **Generic** version (`IEntityTrigger<E>`) for specific entity types

---

## Key Features

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

## Usage Examples

### Non-Generic Usage

```csharp
public class PlayerTrigger : EntityTriggerBase
{
    public override void Track(IEntity entity)
    {
        // Subscribe to custom events and call "InvokeAction(entity)" in a certain place
    }

    public override void Untrack(IEntity entity)
    {
        // Unsubscribe from custom events
    }
}
```

### Generic Usage

```csharp
public class PlayerTrigger : EntityTriggerBase<PlayerEntity>
{
    public override void Track(PlayerEntity entity)
    {
        // Subscribe to custom events and call "InvokeAction(entity)" in a certain place
    }

    public override void Untrack(PlayerEntity entity)
    {
        // Unsubscribe from custom events
    }
}
```
-->