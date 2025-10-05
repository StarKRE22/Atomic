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
