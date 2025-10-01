# 🧩️ IEntityTick 

```csharp
public interface IEntityTick : IEntityBehaviour
```

- **Description:** Represents a behavior interface that executes logic during the regular update cycle of
an [IEntity](../Entities/IEntity.md). 
- **Inheritance:** implements [IEntityBehaviour](IEntityBehaviour.md)
- **Note:** It is automatically invoked once per frame by the entity’s `Tick`.
- **See also:** [IEntityTick&lt;E&gt;](IEntityTick%601.md)

---

## 🏹 Methods

#### `Tick(IEntity, float)`

```csharp
public void Tick(IEntity entity, float deltaTime);
```

- **Description:** Called during the main update phase of the frame.
- **Parameters:**
    - `entity` – The entity being updated.
    - `deltaTime` – Elapsed time since the last frame.
- **Remarks:** Automatically called once per frame by `IEntity.Tick()`.

---

## 🗂 Example of Usage

Update a `Transform` component towards move direction every frame for an entity

```csharp
public class MoveBehaviour : IEntityTick
{
    public void Tick(IEntity entity, float deltaTime)
    {
        Transform transform = entity.GetValue<Transform>("Transform");
        float speed = entity.GetValue<float>("Speed");
        Vector3 direction = entity.GetValue<Vector3>("MoveDirection");
        transform.position += direction * (speed * deltaTime);
    }
}
```

> Note: `GetValue<T>` assumes the entity has the relevant components and values already set.