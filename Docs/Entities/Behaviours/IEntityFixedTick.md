# 🧩️ IEntityFixedTick

Represents a behavior interface that executes logic during the fixed update cycle of
an [Entity](../Entities/Manual.md).

---

## 📑 Table of Contents

- [Example of Usage](#-example-of-usage)
- [API Reference](#-api-reference)
    - [Type](#-type)
    - [Methods](#-methods)
        - [FixedTick(IEntity, float)](#fixedtickientity-float)
      
---

## 🗂 Example of Usage

Apply a physics force to an entity:

```csharp
public class ApplyForceBehaviour : IEntityFixedTick
{
    public void FixedTick(IEntity entity, float deltaTime)
    {
        Rigidbody rb = entity.GetValue<Rigidbody>("Rigidbody");
        Vector3 force = entity.GetValue<Vector3>("Force");
        rb.AddForce(force * deltaTime);
    }
}
```

---

## 🔍 API Reference

### 🏛️ Type <div id="-type"></div>

```csharp
public interface IEntityFixedTick : IEntityBehaviour
```

- **Inheritance:** implements [IEntityBehaviour](IEntityBehaviour.md)
- **Note:** It is automatically invoked at a fixed timestep by the entity’s `FixedTick`,
  typically used for physics updates.
- **See also:** [IEntityFixedTick&lt;E&gt;](IEntityFixedTick%601.md)

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