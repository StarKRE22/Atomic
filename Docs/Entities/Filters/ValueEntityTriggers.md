# 🧩 ValueEntityTriggers

A specialized entity trigger that monitors **value-related changes** on entities, responding to additions, removals, and
modifications. It automatically tracks when values are added, removed, or changed on entities, triggering callbacks
for reactive system updates. This is essential for building systems that respond to runtime data changes, such as stats,
attributes, or custom properties.

---

## 📑 Table of Contents

- [Examples of Usage](#-examples-of-usage)
    - [Non-generic Usage](#ex1)
    - [Generic Usage](#ex2)
    - [Custom Usage](#ex3)
- [API Reference](#-api-reference)
- [Notes](#-notes)

---

## 🗂 Examples of Usage

<div id="ex1"></div>

### 1️⃣ Non-generic Usage

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

<div id="ex2"></div>

### 2️⃣ Generic Usage

```csharp
// Track entities for value additions, deletions, and modifications
var trigger = new ValueEntityTrigger<GameEntity>(
    added: true,
    deleted: true,
    changed: true
);

// Usage with EntityFilter
var filter = new EntityFilter<GameEntity>(
    allEntities,
    e => e.GetValue<int>("Health") > 0,
    trigger
);
```

<div id="ex3"></div>

### 3️⃣ Custom Usage

```csharp
var valueTrigger = new ValueEntityTrigger<GameEntity>(added: true, deleted: true, changed: true);
valueTrigger.SetAction(e => Console.WriteLine($"{e.Name}'s value changed"));
valueTrigger.Track(someEntity);
```

---

## 🔍 API Reference

- [ValueEntityTrigger](ValueEntityTrigger.md) <!-- + -->
- [ValueEntityTrigger\<E>](ValueEntityTrigger%601.md) <!-- + -->

---

## 📝 Notes

### Selective Monitoring

- Configurable tracking of **value additions** (`OnValueAdded`)
- Configurable tracking of **value removals** (`OnValueDeleted`)
- Configurable tracking of **value modifications** (`OnValueChanged`)
- Full monitoring of all operations by default

### Automatic Event Management

- Automatic subscription to entity value events
- Clean unsubscription for resource management
- Type-safe entity casting in callbacks

### Lightweight Operation

- Minimal overhead per tracked entity
- Event-driven architecture avoids polling
- Efficient for high-frequency value changes