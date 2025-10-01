# 🧩️ IEntityGizmos


```csharp
public interface IEntityGizmos : IEntityBehaviour
```

- **Description:** Represents a behavior interface that allows drawing gizmos for an [IEntity](../Entities/IEntity.md) during the **editor or debug rendering phase**.
- **Inheritance:** implements [IEntityBehaviour](IEntityBehaviour.md)
- **Note:** It is automatically invoked by `SceneEntity.OnDrawGizmos()` or `SceneEntity.OnDrawGizmosSelected()` in the Unity Editor, allowing visualization of entity data in the scene view.
- **See also:** [IEntityGizmos&lt;E&gt;](IEntityGizmos%601.md)

---

## 🏹 Methods

#### `DrawGizmos(IEntity)`

```csharp
public void DrawGizmos(IEntity entity);
```

- **Description:** Draws editor or debug gizmos for the entity.
- **Parameter:** `entity` – The entity to visualize.
- **Remarks:** Automatically called by `SceneEntity.OnDrawGizmos()` or `SceneEntity.OnDrawGizmosSelected()` in the Unity Editor.

---

## 🗂 Example of Usage

Draw a debug sphere at the entity’s position

```csharp
public class DrawSphereGizmo : IEntityGizmos
{
    public void DrawGizmos(IEntity entity)
    {
        Vector3 position = entity.GetValue<Vector3>("Position");
        float scale = entity.GetValue<float>("Scale");
        
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(position, scale);
    }
}
```

> Note: Assumes the entity has a `Position` value set.