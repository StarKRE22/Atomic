# 🧩 InlineEntityTrigger

```csharp
public class InlineEntityTrigger : InlineEntityTrigger<IEntity>
```

- **Description:** A **non-generic shortcut** for [InlineEntityTrigger\<E>](InlineEntityTrigger%601.md).  
  It allows defining **inline tracking and untracking logic** directly for basic [IEntity](../Entities/IEntity.md) objects,  
  without needing to specify a generic type parameter.

- **Inheritance:** [InlineEntityTrigger\<E>](InlineEntityTrigger%601.md)

---

## 🏗️ Constructor

```csharp
public InlineEntityTrigger(
    Action<IEntity, Action<IEntity>> track,
    Action<IEntity, Action<IEntity>> untrack
)
```

- **Description:** Creates a new `InlineEntityTrigger` with inline tracking and untracking delegates.
- **Parameters:**
    - `track` — A delegate that defines how to start tracking the entity (subscribing to events, etc.), given the entity and callback.
    - `untrack` — A delegate that defines how to stop tracking the entity (unsubscribing from events, etc.), given the entity and callback.
- **Exception:** `ArgumentNullException` — Thrown if either `track` or `untrack` is `null`.

---

## 🏹 Methods

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
- **Parameter:** `entity` — The entity to stop tracking.

<!--

# 🧩 InlineEntityTrigger

An inline-configurable entity trigger that allows **custom tracking and untracking logic** for entities.  
Provides both a **generic** and a **non-generic** version for flexible use.

---

## Overview

`InlineEntityTrigger` allows you to define **inline logic** for how entities should be tracked and untracked.  
Instead of subscribing to predefined events, you can pass **custom delegates** to handle the entity monitoring.

- **Non-generic version:** `InlineEntityTrigger` — works with basic `IEntity`.
- **Generic version:** `InlineEntityTrigger<E>` — works with specific entity types.

---

## InlineEntityTrigger

- Non-generic shortcut for `InlineEntityTrigger<IEntity>`.
- Allows inline specification of track and untrack delegates.

```csharp
public class InlineEntityTrigger : InlineEntityTrigger<IEntity>
```

### Constructor

```csharp
public InlineEntityTrigger(
Action<IEntity, Action<IEntity>> track,
Action<IEntity, Action<IEntity>> untrack
)
```
- **Parameters:**
    - `track` — Delegate defining how to start tracking the entity, given an action.
    - `untrack` — Delegate defining how to stop tracking the entity, given an action.
- **Exceptions:**
    - `ArgumentNullException` — Thrown if `track` or `untrack` is null.

---

## InlineEntityTrigger<E>

- Generic version for specific entity types `E`.
- Inherits from `EntityTriggerBase<E>`.
- Uses inline delegates for custom entity monitoring.

```csharp
public class InlineEntityTrigger<E> : EntityTriggerBase<E> where E : IEntity
{
    private readonly Action<E, Action<E>> _track;
    private readonly Action<E, Action<E>> _untrack;

    public InlineEntityTrigger(Action<E, Action<E>> track, Action<E, Action<E>> untrack)
    {
        _track = track ?? throw new ArgumentNullException(nameof(track));
        _untrack = untrack ?? throw new ArgumentNullException(nameof(untrack));
    }

    public override void Track(E entity) => _track.Invoke(entity, _action);

    public override void Untrack(E entity) => _untrack.Invoke(entity, _action);
}
```

### Constructor
```csharp
public InlineEntityTrigger(Action<E, Action<E>> track, Action<E, Action<E>> untrack)
```
- **Parameters:**
    - `track` — Delegate to define how to subscribe or monitor the entity.
    - `untrack` — Delegate to define how to unsubscribe or ignore the entity.
- **Exceptions:**
    - `ArgumentNullException` — Thrown if `track` or `untrack` is null.

---

## Methods

### Track
```csharp
public override void Track(E entity)
```
- Invokes the inline **track delegate** with the entity and configured action.

### Untrack
```csharp
public override void Untrack(E entity)
```
- Invokes the inline **untrack delegate** with the entity and configured action.

---

-->