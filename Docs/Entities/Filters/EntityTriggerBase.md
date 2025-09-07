# 🧩 EntityTriggerBase

An abstract class for creating **entity triggers** that monitor **entity state changes** and invoke actions when
relevant changes occur. Provides a convenient starting point for building reactive systems for any type of entity.

## Overview

`EntityTriggerBase` offers a flexible way to track entities and respond to state changes. The abstract base class
handles storing the callback action and defines the core methods for tracking and untracking entities.

For generic scenarios, use `EntityTriggerBase<E>`; for default `IEntity` tracking, use `EntityTriggerBase`.

---

## Key Features

### Flexible Callback System

- Assign a callback action using `SetAction`.
- Action invoked automatically when a tracked entity changes.
- Supports type-safe generic entities.

### Abstract Tracking Interface

- `Track(E entity)` — Start monitoring an entity.
- `Untrack(E entity)` — Stop monitoring an entity.
- Leaves implementation details to derived classes.

### Performance-Friendly

- Uses `[MethodImpl(MethodImplOptions.AggressiveInlining)]` for invoking actions efficiently.

---

## EntityTriggerBase

- A shortcut for `EntityTriggerBase<IEntity>`.
- Useful when no specific entity type is needed.
- Inherits from `EntityTriggerBase<E>`.

```csharp
public abstract class EntityTriggerBase : EntityTriggerBase<IEntity>
{
}
```

---

## EntityTriggerBase<E>

- Generic base for triggers monitoring any entity type.
- Implements `IEntityTrigger<E>` and manages callback invocation.

```csharp
public abstract class EntityTriggerBase<E> : IEntityTrigger<E> where E : IEntity
{
    private protected Action<E> _action;

    public void SetAction(Action<E> action) => _action = action ?? throw new ArgumentNullException(nameof(action));

    public abstract void Track(E entity);

    public abstract void Untrack(E entity);

    protected void InvokeAction(E entity) => _action?.Invoke(entity);
}

```

## Methods

### SetAction

```csharp
public void SetAction(Action<E> action)
```

- **Description:** Assigns the callback to invoke when a tracked entity changes.
- **Parameters:**
    - `action` — The callback action to be executed.

### Track

```csharp
public abstract void Track(E entity)
```

- **Description:** Begins tracking the specified entity.
- **Parameters:**
    - `entity` — The entity to monitor for changes.

### Untrack

```csharp
public abstract void Untrack(E entity)
```

- **Description:** Stops tracking the specified entity.
- **Parameters:**
    - `entity` — The entity to stop monitoring.

### InvokeAction

```csharp
protected void InvokeAction(E entity)
```

- **Description:** Invokes the configured action for the given entity.
- **Parameters:**
    - `entity` — The entity to pass to the callback.

---

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
