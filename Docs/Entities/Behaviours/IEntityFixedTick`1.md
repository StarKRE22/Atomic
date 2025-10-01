
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