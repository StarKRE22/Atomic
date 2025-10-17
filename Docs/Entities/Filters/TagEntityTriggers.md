# 🧩 TagEntityTriggers

A specialized entity trigger that monitors tag-related changes on entities, responding to tag additions and deletions.
It automatically tracks when tags are added to or removed from entities, triggering
callbacks for reactive system updates. Essential for building systems that respond to entity state classification
changes, role updates, or behavioral flag modifications.

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

### 2️⃣ Generic Usage

```csharp
// Track entities whose tags change (addition/removal)
var trigger = new TagEntityTrigger<GameEntity>(added: true, deleted: true);

// Usage with EntityFilter
var filter = new EntityFilter<GameEntity>(
    allEntities,
    e => e.HasTag("Enemy"),
    trigger
);
```

<div id="ex2"></div>

### 3️⃣ Custom Usage

```csharp
var tagTrigger = new TagEntityTrigger<GameEntity>(added: true, deleted: true);
tagTrigger.SetAction(e => Console.WriteLine($"Tag changed on {e.Name}"));
tagTrigger.Track(someEntity);
``` 

---

## 🔍 API Reference

- [TagEntityTrigger](TagEntityTrigger.md) <!-- + -->
- [TagEntityTrigger\<E>](TagEntityTrigger%601.md) <!-- + -->

---

## 📝 Notes

### Selective Monitoring

- Configurable tracking of tag additions only
- Configurable tracking of tag deletions only
- Full monitoring of both operations by default

### Automatic Event Management

- Automatic subscription to entity tag events
- Clean unsubscription for resource management
- Type-safe entity casting in callbacks

### Lightweight Operation

- Minimal overhead per tracked entity
- Event-driven architecture avoids polling
- Efficient for high-frequency tag changes