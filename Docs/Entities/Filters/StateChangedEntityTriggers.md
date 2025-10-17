# 🧩 StateChangedEntityTriggers

A trigger that responds to **state changes** on entities. Provides both a **generic** and a **non-generic** version for
flexible use with any entity type. The state-changed trigger allows monitoring **state changes** on entities that
implement [IEntity](../Entities/IEntity.md). It invokes a configured action whenever a tracked entity’s state changes.

---

## 📑 Table of Contents

- [Examples of Usage](#-examples-of-usage)
    - [Non-generic Usage](#ex1)
    - [Generic Usage](#ex2)
    - [Custom Monitoring](#ex3)
- [API Reference](#-api-reference)

---

## 🗂 Examples of Usage

<div id="ex1"></div>

### 1️⃣ Non-generic Usage

[StateChangedEntityTrigger](StateChangedEntityTrigger.md) works with plain [IEntity](../Entities/IEntity.md)

```csharp
// Track entities for state changes
var trigger = new StateChangedEntityTrigger();

// Usage with EntityFilter
var filter = new EntityFilter(
    allEntities,
    e => e.GetValue<bool>("IsAlive"),
    trigger
);
```

<div id="ex2"></div>

### 2️⃣ Generic Usage

[StateChangedEntityTrigger\<E>](StateChangedEntityTrigger%601.md) works with specific entity types

```csharp
// Track entities for state changes
var trigger = new StateChangedEntityTrigger<GameEntity>();

// Usage with EntityFilter
var filter = new EntityFilter<GameEntity>(
    allEntities,
    e => e.GetValue<bool>("IsAlive"),
    trigger
);
```

<div id="ex3"></div>

### 3️⃣ Custom Monitoring

```csharp
var stateTrigger = new StateChangedEntityTrigger<GameEntity>();
stateTrigger.SetAction(e => Console.WriteLine($"State changed: {e.Name}"));
stateTrigger.Track(someEntity);
````

---

## 🔍 API Reference

- [StateChangedEntityTrigger](StateChangedEntityTrigger.md)
- [StateChangedEntityTrigger\<E>](StateChangedEntityTrigger%601.md) <!-- + -->