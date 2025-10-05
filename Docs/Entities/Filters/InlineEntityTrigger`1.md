# 🧩 InlineEntityTrigger\<E>

```csharp
public class InlineEntityTrigger<E> : IEntityTrigger<E>
    where E : IEntity
```

- **Description:** A flexible, **inline-configurable entity trigger** that allows you to define custom logic for
  **tracking** and **untracking** entities without creating a separate trigger class. This is useful for lightweight
  scenarios, quick prototyping, or when different entities require different tracking logic.

- **Type Parameter:** `E` — The entity type being tracked. Must implement [IEntity](../Entities/IEntity.md).
- **Inheritance:** [IEntityTrigger\<E>](IEntityTrigger%601.md)

---

## 🏗️ Constructor

```csharp
public InlineEntityTrigger(Action<E, Action<E>> track, Action<E, Action<E>> untrack)
```

- **Description:** Creates a new inline entity trigger with **custom tracking and untracking logic**.
- **Parameters:**
    - `track` — A delegate that defines how the entity should be tracked (subscribed, monitored, etc.).
    - `untrack` — A delegate that defines how the entity should be untracked (unsubscribed, ignored, etc.).
- **Exception:** `ArgumentNullException` — Thrown if either `track` or `untrack` is `null`.

---

## 🏹 Methods

#### `SetAction(Action<E>)`

```csharp
public void SetAction(Action<E> action);
```

- **Description:** Assigns the action that will be invoked whenever a tracked entity’s state changes (depending on your
  custom logic).
- **Parameter:** `action` — The callback delegate to invoke. Cannot be `null`.
- **Exception:** `ArgumentNullException` — Thrown if `action` is `null`.

#### `Track(E)`

```csharp
public void Track(E entity);
```

- **Description:** Starts tracking the specified entity using the custom `track` logic provided in the constructor.
- **Parameter:** `entity` — The entity to track.

#### `Untrack(E)`

```csharp
public void Untrack(E entity);
```

- **Description:** Stops tracking the specified entity using the custom `untrack` logic provided in the constructor.
- **Parameter:** `entity` — The entity to stop tracking.