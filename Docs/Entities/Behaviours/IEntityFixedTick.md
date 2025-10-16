# 🧩️ IEntityFixedTick

Represents a behavior interface that executes logic during the fixed update cycle of
an [IEntity](../Entities/IEntity.md). Use this phase to perform physics calculations and update game mechanics with fixed
timestamp.

---

## 📑 Table of Contents

- [Examples of Usage](#-examples-of-usage)
    - [Apply Physics](#ex1)
    - [Game Mechanics](#ex2)
- [API Reference](#-api-reference)
    - [Type](#-type)
    - [Methods](#-methods)
        - [FixedTick(IEntity, float)](#fixedtickientity-float)

---

## 🗂 Examples of Usage

<div id="ex1"></div>

### 1️⃣ Apply Physics

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

<div id="ex2"></div>

### 2️⃣ Game Mechanics

Update a `Transform` component towards move direction every frame for an entity

```csharp
public class MoveBehaviour : IEntityFixedTick
{
    public void FixedTick(IEntity entity, float deltaTime)
    {
        Transform transform = entity.GetValue<Transform>("Transform");
        float speed = entity.GetValue<float>("Speed");
        Vector3 direction = entity.GetValue<Vector3>("MoveDirection");
        transform.position += direction * (speed * deltaTime);
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