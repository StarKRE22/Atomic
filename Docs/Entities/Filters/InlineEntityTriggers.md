# 🧩 InlineEntityTriggers

An inline-configurable entity trigger that allows **custom tracking and untracking logic** for entities. Provides both a
**generic** and a **non-generic** version for flexible use. `InlineEntityTrigger` allows you to define **inline logic**
for how entities should be tracked and untracked. Instead of subscribing to predefined events, you can pass **custom
delegates** to handle the entity monitoring.

---

## 📑 Table of Contents

- [Examples of Usage](#-examples-of-usage)
    - [Non-generic Version](#ex1)
    - [Generic Version](#ex2)
- [API Reference](#-api-reference)

---

## 🗂 Examples of Usage

<div id="ex1"></div>

### 1️⃣ Non-generic Version

```csharp
var inlineTrigger = new InlineEntityTrigger(
    track: (e, callback) => e.OnTagAdded += _ => callback(e),
    untrack: (e, callback) => e.OnTagAdded -= _ => callback(e)
);
inlineTrigger.SetAction(e => Console.WriteLine($"Custom trigger fired for {e.Name}"));
inlineTrigger.Track(someEntity);
```

<div id="ex2"></div>

### 2️⃣ Generic Version

```csharp
var inlineTrigger = new InlineEntityTrigger<GameEntity>(
    track: (e, callback) => e.OnTagAdded += _ => callback(e),
    untrack: (e, callback) => e.OnTagAdded -= _ => callback(e)
);
inlineTrigger.SetAction(e => Console.WriteLine($"Custom trigger fired for {e.Name}"));
inlineTrigger.Track(someEntity);
```

--- 

## 🔍 API Reference

- [InlineEntityTrigger](InlineEntityTrigger.md)  — works with basic `IEntity`
- [InlineEntityTrigger\<E>](InlineEntityTrigger%601.md) — works with specific entity types.