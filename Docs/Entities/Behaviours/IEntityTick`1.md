# 🧩 IEntityTick&lt;E&gt;

Provides a strongly-typed version of [IEntityTick](IEntityTick.md) for handling update logic on a specific entity
type.

---

## 📑 Table of Contents

- [Example of Usage](#-example-of-usage)
- [API Reference](#-api-reference)
    - [Type](#-type)
    - [Methods](#-methods)
        - [Tick(E, float)](#ticke-float)

---

## 🗂 Example of Usage

Assume we have a concrete entity type:

```csharp
public class PlayerEntity : Entity
{
}
```

Update a move direction towards user input every frame for an entity:

```csharp
public class MoveController : IEntityTick<PlayerEntity>
{
    public void Tick(PlayerEntity entity, float deltaTime)
    {
        float dx = Input.GetAxis("Horizontal");
        float dz = Input.GetAxis("Vertical");
        
        Vector3 moveDirection = new Vector3(dx, 0 , dz);
        entity.SetValue<Vector3>("MoveDirection", moveDirection);
    }
}
```

---

## 🔍 API Reference

### 🏛️ Type <div id="-type"></div>

```csharp
public interface IEntityTick<in E> : IEntityTick where E : IEntity
```

- **Type Parameter:** `E` – The concrete entity type this behavior is associated with.
- **Inherits:** [IEntityTick](IEntityTick.md)

---

### 🏹 Methods

#### `Tick(E, float)`

```csharp
public void Tick(E entity, float deltaTime);
```

- **Description:** Called during the main update phase of the frame for the strongly-typed entity.
- **Parameters:**
    - `entity` – The strongly-typed entity being updated.
    - `deltaTime` – Elapsed time since the last frame.
- **Remarks:** Implements the base `IEntityTick.Tick(IEntity, float)` explicitly by casting the entity to type `E`.