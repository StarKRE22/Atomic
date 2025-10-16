# 🧩️ IEntityTick

Represents a behavior interface that executes logic during the regular update cycle of
an [IEntity](../Entities/IEntity.md).

---

## 📑 Table of Contents

- [Example of Usage](#-example-of-usage)
- [API Reference](#-api-reference)
    - [Type](#-type)
    - [Methods](#-methods)
        - [Tick(IEntity, float)](#tickientity-float)

---

## 🗂 Example of Usage

Update a move direction towards user input every frame for an entity:

```csharp
public class MoveController : IEntityTick
{
    public void Tick(IEntity entity, float deltaTime)
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
public interface IEntityTick : IEntityBehaviour
```

- **Inheritance:** implements [IEntityBehaviour](IEntityBehaviour.md)
- **Note:** It is automatically invoked once per frame by the entity’s `Tick`.
- **See also:** [IEntityTick&lt;E&gt;](IEntityTick%601.md)

---

### 🏹 Methods

#### `Tick(IEntity, float)`

```csharp
public void Tick(IEntity entity, float deltaTime);
```

- **Description:** Called during the main update phase of the frame.
- **Parameters:**
    - `entity` – The entity being updated.
    - `deltaTime` – Elapsed time since the last frame.
- **Remarks:** Automatically called once per frame by `IEntity.Tick()`.