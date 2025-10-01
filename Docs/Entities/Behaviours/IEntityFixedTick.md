# 🧩️ IEntityFixedTick

```csharp
public interface IEntityFixedTick : IEntityBehaviour
```

- **Description:** Represents a behavior interface that executes logic during the fixed update cycle of
  an [IEntity](../Entities/IEntity.md).
- **Inheritance:** implements [IEntityBehaviour](IEntityBehaviour.md)
- **Note:** It is automatically invoked at a fixed timestep by the entity’s `FixedTick`,
  typically used for physics updates.
- **See also:** [IEntityFixedTick&lt;E&gt;](IEntityFixedTick%601.md)

---

## 🏹 Methods

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

## 🗂 Example of Usage

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