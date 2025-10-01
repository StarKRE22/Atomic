#  🧩 IEntityGizmos&lt;E&gt;

```csharp
public interface IEntityGizmos<in E> : IEntityGizmos where E : IEntity
```

- **Description:** Provides a strongly-typed version of `IEntityGizmos` for handling gizmo drawing on a specific entity type.
- **Type Parameter:** `E` – The concrete entity type this behavior is associated with.
- **Inherits:** [IEntityGizmos](IEntityGizmos.md)
- **Note:** Automatically invoked by Unity Editor gizmo methods on entities of type `E`.

---

## 🏹 Methods

#### `DrawGizmos(E)`

```csharp
public void DrawGizmos(E entity);
```

- **Description:** Draws gizmos for the strongly-typed entity.
- **Parameter:** `entity` – The strongly-typed entity.
- **Remarks:** Implements the base `IEntityGizmos.DrawGizmos(IEntity)` explicitly by casting to type `E`.

---

## 🗂 Example of Usage

Draw a debug sphere for a `UnitEntity`

```csharp
public class UnitEntity : Entity
{
}
```

```csharp
public class DrawSphereGizmo : IEntityGizmos<UnitEntity>
{
    public void DrawGizmos(UnitEntity entity)
    {
        Vector3 position = entity.GetValue<Vector3>("Position");
        float scale = entity.GetValue<float>("Scale");
        
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(position, 0.5f);
    }
}
```

> Note: Uses the strongly-typed `UnitEntity`, so no casting from `IEntity` is required.