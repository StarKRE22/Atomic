# 🧩 IEntityFixedTick&lt;E&gt;

Provides a strongly-typed version of [IEntityFixedTick](IEntityFixedTick.md) for handling fixed update logic on a specific
entity type.


---

## 📑 Table of Contents

- [Example of Usage](#-example-of-usage)
- [API Reference](#-api-reference)
    - [Type](#-type)
    - [Methods](#-methods)
        - [FixedTick(E, float)](#fixedticke-float)

---

## 🗂 Example of Usage

Assume we have a concrete entity type:

```csharp
public class UnitEntity : Entity
{
}
```

Apply physics forces to a `UnitEntity` every fixed update

```csharp
public class ApplyForceBehaviour : IEntityFixedTick<UnitEntity>
{
    public void FixedTick(UnitEntity entity, float fixedDeltaTime)
    {
        Rigidbody rb = entity.GetValue<Rigidbody>("Rigidbody");
        Vector3 force = entity.GetValue<Vector3>("Force");
        rb.AddForce(force * fixedDeltaTime);
    }
}
```

---

## 🔍 API Reference

### 🏛️ Type <div id="-type"></div>

```csharp
public interface IEntityFixedTick<in E> : IEntityFixedTick where E : IEntity
```

- **Description:**
- **Type Parameter:** `E` – The concrete entity type this behavior is associated with.
- **Inherits:** [IEntityFixedTick](IEntityFixedTick.md)
- **Remarks:** Automatically invoked by `IEntity.FixedTick()` on entities of type `E`.

---

### 🏹 Methods

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