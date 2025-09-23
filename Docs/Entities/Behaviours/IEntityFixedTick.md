# 🧩️ IEntityFixedTick Interfaces

Represents a behavior interface that executes logic during the fixed update cycle of
an [IEntity](../Entities/IEntity.md). It is automatically invoked at a fixed timestep by the entity’s `FixedTick`,
typically used for physics updates.

<details>
  <summary>
    <h2 id="entity-fixed-tick"> 🧩 IEntityFixedTick</h2>
    <br>Defines a behavior that executes logic during the fixed update phase of an <code>IEntity</code>.
  </summary>

<br>

```csharp
public interface IEntityFixedTick : IEntityBehaviour
```

- **Inheritance:** implements [IEntityBehaviour](IEntityBehaviour.md)

---

### 🏹 Methods

#### `FixedTick(IEntity, float)`

```csharp
public void FixedTick(IEntity entity, float fixedDeltaTime);
```

- **Description:** Called during the fixed update phase of the frame.
- **Parameters:**
    - `entity` – The entity being updated.
    - `fixedDeltaTime` – The fixed time step elapsed since the last fixed update.
- **Remarks:** Automatically called once per fixed timestep by `IEntity.FixedTick()`.

---

### 🗂 Example of Usage

Apply physics forces to an entity

```csharp
public class ApplyPhysicsBehaviour : IEntityFixedTick
{
    public void FixedTick(IEntity entity, float fixedDeltaTime)
    {
        Rigidbody rb = entity.GetValue<Rigidbody>("Rigidbody");
        Vector3 force = entity.GetValue<Vector3>("Force");
        rb.AddForce(force * fixedDeltaTime);
    }
}
```

> Note: `GetValue<T>` assumes the entity has the relevant components already set.

</details>

---

<details>
  <summary>
    <h2 id="entity-fixed-tick-t"> 🧩 IEntityFixedTick&lt;E&gt;</h2>
    <br>Defines a strongly-typed behavior that executes fixed update logic on an <code>IEntity</code> of type <code>E</code>.
  </summary>

<br>

```csharp
public interface IEntityFixedTick<in E> : IEntityFixedTick where E : IEntity
```

- **Description:** Provides a strongly-typed version of `IEntityFixedTick` for handling fixed update logic on a specific
  entity type.
- **Type Parameter:** `E` – The concrete entity type this behavior is associated with.
- **Inherits:** [IEntityFixedTick](#entity-fixed-tick)
- **Remarks:** Automatically invoked by `IEntity.FixedTick()` on entities of type `E`.

---

## 🏹 Methods

#### `FixedTick(E, float)`

```csharp
public void FixedTick(E entity, float fixedDeltaTime);
```

- **Description:** Called during the fixed update phase of the frame for the strongly-typed entity.
- **Parameters:**
    - `entity` – The strongly-typed entity being updated.
    - `fixedDeltaTime` – The fixed time step elapsed since the last fixed update.
- **Remarks:** Implements the base `IEntityFixedTick.FixedTick(IEntity, float)` explicitly by casting the entity to type
  `E`.

---

### 🗂 Example of Usage

Apply physics forces to a `UnitEntity` every fixed update

```csharp
public class UnitEntity : Entity
{
}
```

```csharp
public class ApplyPhysicsBehaviour : IEntityFixedTick<UnitEntity>
{
    public void FixedTick(UnitEntity entity, float fixedDeltaTime)
    {
        Rigidbody rb = entity.GetValue<Rigidbody>("Rigidbody");
        Vector3 force = entity.GetValue<Vector3>("Force");
        rb.AddForce(force * fixedDeltaTime);
    }
}
```

> Note: Uses the strongly-typed `UnitEntity`, so no casting from `IEntity` is required.

</details>

---

## 📝 Notes

- **Fixed Update Logic** – Encapsulates routines executed at fixed timesteps.
- **Strongly-Typed Option** – `IEntityFixedTick<E>` allows type-specific fixed update logic.
- **Integration** – Called automatically by `IEntity.FixedTick()`.
- **Composable** – Can be combined with other behaviours for modular entity logic.

- `IEntityFixedTick` is intended for physics or deterministic updates.
- `IEntityFixedTick<E>` is useful when the behaviour is specific to a particular entity type.