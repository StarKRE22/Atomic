# 🧩 IEntityFixedTick&lt;E&gt;

Provides a strongly-typed version of [IEntityFixedTick](IEntityFixedTick.md) for handling fixed update logic on a
specific entity type. Use this phase to perform physics calculations and update game mechanics with fixed timestamp.

---

## 📑 Table of Contents

- [Examples of Usage](#-examples-of-usage)
    - [Apply Physics](#ex1)
    - [Game Mechanics](#ex2)
- [API Reference](#-api-reference)
    - [Type](#-type)
    - [Methods](#-methods)
        - [FixedTick(E, float)](#fixedticke-float)

---

## 🗂 Examples of Usage

Assume we have a concrete entity type:

```csharp
public class UnitEntity : Entity
{
}
```

<div id="ex1"></div>

### 1️⃣ Apply Physics

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

<div id="ex2"></div>

### 2️⃣ Game Mechanics

Update the position of a `UnitEntity` every frame

```csharp
public class MoveBehaviour : IEntityFixedTick<UnitEntity>
{
    public void FixedTick(UnitEntity entity, float deltaTime)
    {
        float speed = entity.GetValue<float>("Speed");
        Vector3 position = entity.GetValue<Vector3>("Position");
        Vector3 direction = entity.GetValue<Vector3>("MoveDirection");
        
        Vector3 newPosition = position + direction * (speed * deltaTime);
        entity.SetValue("Position", newPosition);
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