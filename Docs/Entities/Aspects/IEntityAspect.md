# 🧩 IEntityAspect Classes

Represents a reusable piece of behavior or logic that can be **applied to entities and later discarded**. It comes in
two forms:

* **Non-generic** version (`IEntityAspect`) for working with `IEntity`
* **Generic** version (`IEntityAspect<E>`) for specific entity types

> [!TIP]
> Use `IEntityAspect` when you want modular, reusable behavior that can be dynamically applied or removed from entities
> at runtime.

---

<details>
  <summary>
    <h2 id="ienity-aspect"> 🧩 IEntityAspect</h2>
    <br>Non-generic interface for applying/discarding behavior on any <code>IEntity</code>.
  </summary>

<br>

```csharp
public interface IEntityAspect : IEntityAspect<IEntity>
```

- **Inheritance:** Implements [IEntityAspect&lt;IEntity&gt;](#ienity-aspect-e)
- **Description:** Applies or discards reusable behavior for any entity implementing `IEntity`.

---

### 🏹 Methods

#### `Apply(IEntity)`

```csharp
void Apply(IEntity entity);
```

- **Description:** Applies the aspect to the specified entity.
- **Parameters:** `entity` – The entity to which the aspect will be applied.

#### `Discard(IEntity)`

```csharp
void Discard(IEntity entity);
```

- **Description:** Reverses the effects of `Apply` on the specified entity.
- **Parameters:** `entity` – The entity from which the aspect should be removed.

---

### 🗂 Example of Usage

The `SpeedBuff` aspect temporarily multiplies an entity's speed and restores it when discarded.

```csharp
public sealed class SpeedBuff : ScriptableObject, IEntityAspect
{
    [SerializeField]
    private float _multiplier = 2;

    public void Apply(IEntity entity)
    {
        entity.GetValue<IVariable<float>>("Speed").Value *= _multiplier;
    }

    public void Discard(IEntity entity)
    {
        entity.GetValue<IVariable<float>>("Speed").Value /= _multiplier;
    }
}
```

!!!

</details>

---

<details>
  <summary>
    <h2 id="ienity-aspect-e"> 🧩 IEntityAspect&lt;E&gt;</h2>
    <br>Generic interface for applying/discarding behavior on a specific entity type.
  </summary>

<br>

```csharp
public interface IEntityAspect<in E> where E : IEntity
```

- **Type Parameter:** `E` – The specific entity type this aspect operates on.
- **Description:** Provides type-safe behavior application and discard for a specific entity type.

---

### 🏹 Methods

#### `Apply(E)`

```csharp
void Apply(E entity);
```

- **Description:** Applies the aspect to the strongly-typed entity.
- **Parameters:** `entity` – The entity of type `E` to which the aspect will be applied.

#### `Discard(E)`

```csharp
void Discard(E entity);
```

- **Description:** Reverses the effects of `Apply` on the strongly-typed entity.
- **Parameters:** `entity` – The entity of type `E` from which the aspect should be removed.

</details>

---

---

#### 2. JumpAspect (Generic)

The `JumpAspect` adds jump-related capabilities to entities implementing `IGameEntity`.

!!!
public sealed class JumpAspect : ScriptableObject, IEntityAspect<IGameEntity>
{
[SerializeField] private float _jumpForce = 2;

    public void Apply(IGameEntity entity)
    {
        entity.AddTag("Jumpable");
        entity.AddValue("JumpForce", _jumpForce);
        entity.AddBehaviour<JumpBehaviour>();
    }

    public void Discard(IGameEntity entity)
    {
        entity.DelTag("Jumpable");
        entity.DelValue("JumpForce");
        entity.DelBehaviour<JumpBehaviour>();
    }

}
!!!

> Note: Using the generic `IGameEntity` version allows type-safe access to entity-specific properties without casting.

---

## 📝 Notes

- **Reusable Logic** – Aspects encapsulate modular behavior that can be applied and discarded dynamically.
- **Generic Option** – `IEntityAspect<E>` allows compile-time type safety for specific entity types.
- **Non-Generic Option** – `IEntityAspect` works with any `IEntity` for convenience.
- **Implementation** – Can be implemented as `ScriptableObject` or `MonoBehaviour`.
- **Reusability** – The same aspect instance can be applied to multiple entities.

=============
=============

# 🧩 IEntityAspect

The `IEntityAspect` interface represents a reusable piece of behavior or logic that can be applied to entities and
discarded. It comes in two forms:

* **Non-generic** version (`IEntityAspect`) for working with `IEntity`
* **Generic** version (`IEntityAspect<E>`) for specific entity types

---

## Key Features

### Aspect Application

- Encapsulates reusable logic or behavior for entities
- Can be applied or discarded dynamically
- Supports generic and non-generic usage for flexibility

### Type Safety

- Generic interface allows compile-time type checking
- Non-generic interface provides convenience when type specificity is not required

### Reusability

- Can be implemented as `ScriptableObject` or `MonoBehaviour`
- Supports multiple entities using the same aspect instance

---

## IEntityAspect

**A shorthand interface for `IEntityAspect<IEntity>`.**

```csharp
public interface IEntityAspect : IEntityAspect<IEntity>
{
    void Apply(IEntity entity);
    void Discard(IEntity entity);
}
```

## IEntityAspect<E>

**A generic interface for applying or discarding behavior on a specific entity type.**

```csharp
public interface IEntityAspect<in E> where E : IEntity
{
    void Apply(E entity);
    void Discard(E entity);
}
```

---

## Methods

### Apply

```csharp
void Apply(E entity);
```

- **Purpose**: Applies the aspect to the specified entity
- **Parameter**: `entity` — The entity to which the aspect will be applied
- **Behavior**: Implements logic, changes, or effects on the entity

### Discard

```csharp
void Discard(E entity);
```

- **Purpose**: Reverses the effects of `Apply` on the specified entity
- **Parameter**: `entity` — The entity from which the aspect should be removed
- **Behavior**: Cleans up or removes applied logic from the entity

---

## Example Usage

### SpeedBuff

The `SpeedBuff` aspect temporarily increases the speed of any entity implementing `IEntity`. It multiplies the entity's
`Speed` value by a configurable factor when applied, and restores it when discarded.

**Key Points:**

- Works with non-generic `IEntityAspect`.
- Uses `GetValue<IVariable<float>>("Speed")` to access the speed variable.
- Safe to apply and discard multiple times as long as the same multiplier is used consistently.

```csharp
public sealed class SpeedBuff : ScriptableObject, IEntityAspect
{
    [SerializeField] private float _multiplier = 2;
    
    public void Apply(IEntity entity)
    {
        entity.GetValue<IVariable<float>>("Speed").Value *= _multiplier;
    }

    public void Discard(IEntity entity)
    {
        entity.GetValue<IVariable<float>>("Speed").Value /= _multiplier;
    }
}
```

### JumpAspect

The `JumpAspect` aspect adds jump-related capabilities to entities implementing `IGameEntity`. It applies tags, values,
and behaviours necessary for jumping and removes them when discarded.

**Key Points:**

- Works with generic `IEntityAspect<IGameEntity>`.
- Adds a "Jumpable" tag to mark the entity as able to jump.
- Adds a `JumpForce` value and attaches a `JumpBehaviour`.
- Discarding removes the tag, value, and behaviour, fully cleaning up the entity.

```csharp
public sealed class JumpAspect : ScriptableObject, IEntityAspect<IGameEntity>
{
    [SerializeField] private float _jumpForce = 2;
    
    public void Apply(IGameEntity entity)
    {
        entity.AddTag("Jumpable");
        entity.AddValue("JumpForce", _jumpForce);
        entity.AddBehaviour<JumpBehaviour>();
    }

    public void Discard(IGameEntity entity)
    {
        entity.DelTag("Jumpable");
        entity.DelValue("JumpForce");
        entity.DelBehaviour<JumpBehaviour>();
    }
}
```
