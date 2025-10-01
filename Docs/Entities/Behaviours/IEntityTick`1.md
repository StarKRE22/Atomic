
<details>
  <summary>
    <h2 id="entity-tick-t"> 🧩 IEntityTick&lt;E&gt;</h2>
    <br>Defines a strongly-typed behavior that executes update logic on an <code>IEntity</code> of type <code>E</code>.
  </summary>

<br>

```csharp
public interface IEntityTick<in E> : IEntityTick where E : IEntity
```

- **Description:** Provides a strongly-typed version of `IEntityTick` for handling update logic on a specific entity
  type.
- **Type Parameter:** `E` – The concrete entity type this behavior is associated with.
- **Inherits:** [IEntityTick](#entity-tick)

---

## 🏹 Methods

#### `Tick(E, float)`

```csharp
public void Tick(E entity, float deltaTime);
```

- **Description:** Called during the main update phase of the frame for the strongly-typed entity.
- **Parameters:**
    - `entity` – The strongly-typed entity being updated.
    - `deltaTime` – Elapsed time since the last frame.
- **Remarks:** Implements the base `IEntityTick.Tick(IEntity, float)` explicitly by casting the entity to type `E`.

---

### 🗂 Example of Usage

Update the position of a `UnitEntity` every frame

```csharp
public class UnitEntity : Entity
{
}
```

```csharp
public class MoveBehaviour : IEntityTick<UnitEntity>
{
    public void Tick(UnitEntity entity, float deltaTime)
    {
        float speed = entity.GetValue<float>("Speed");
        Vector3 position = entity.GetValue<Vector3>("Position");
        Vector3 direction = entity.GetValue<Vector3>("MoveDirection");
        
        Vector3 newPosition = position + direction * (speed * deltaTime);
        entity.SetValue("Position", newPosition);
    }
}
```

> Note: Uses the strongly-typed `UnitEntity`, so no casting from `IEntity` is required.

</details>
