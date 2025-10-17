# 🧩 InlineEntityTrigger

A **non-generic shortcut** for [InlineEntityTrigger\<E>](InlineEntityTrigger%601.md). It allows defining **inline
tracking and untracking logic** directly for basic [IEntity](../Entities/IEntity.md) objects, without needing to specify
a generic type parameter.

---

## 📑 Table of Contents

- [Example of Usage](#-example-of-usage)
- [API Reference](#-api-reference)
    - [Type](#-type)
    - [Constructor](#-constructor)
    - [Methods](#-methods)
        - [SetAction(Action\<IEntity>)](#setactionactionientity)
        - [Track(IEntity)](#trackientity)
        - [Untrack(IEntity)](#untrackientity)

---

## 🗂 Example of Usage

```csharp
var inlineTrigger = new InlineEntityTrigger(
    track: (e, callback) => e.OnTagAdded += _ => callback(e),
    untrack: (e, callback) => e.OnTagAdded -= _ => callback(e)
);
inlineTrigger.SetAction(e => Console.WriteLine($"Custom trigger fired for {e.Name}"));
inlineTrigger.Track(someEntity);
```

---

## 🔍 API Reference

### 🏛️ Type <div id="-type"></div>

```csharp
public class InlineEntityTrigger : InlineEntityTrigger<IEntity>
```

- **Inheritance:** [InlineEntityTrigger\<E>](InlineEntityTrigger%601.md)

---

<div id="-constructor"></div>

### 🏗️ Constructor

```csharp
public InlineEntityTrigger(
    Action<IEntity, Action<IEntity>> track,
    Action<IEntity, Action<IEntity>> untrack
)
```

- **Description:** Creates a new `InlineEntityTrigger` with inline tracking and untracking delegates.
- **Parameters:**
    - `track` — A delegate that defines how to start tracking the entity (subscribing to events, etc.), given the entity
      and callback.
    - `untrack` — A delegate that defines how to stop tracking the entity (unsubscribing from events, etc.), given the
      entity and callback.
- **Exception:** `ArgumentNullException` — Thrown if either `track` or `untrack` is `null`.

---

### 🏹 Methods

#### `SetAction(Action<IEntity>)`

```csharp
public void SetAction(Action<IEntity> action);
```

- **Description:** Sets the callback action that will be invoked whenever the tracked entity triggers a change.
- **Parameter:** `action` — The delegate to invoke. Cannot be `null`.
- **Exception:** `ArgumentNullException` — Thrown if `action` is `null`.

---

#### `Track(IEntity)`

```csharp
public void Track(IEntity entity);
```

- **Description:** Starts tracking the specified entity using the inline `track` delegate.
- **Parameter:** `entity` — The entity to track.

#### `Untrack(IEntity)`

```csharp
public void Untrack(IEntity entity);
```

- **Description:** Stops tracking the specified entity using the inline `untrack` delegate.
- **Parameter:** `entity` — The entity to stop tracking. — works with specific entity types.