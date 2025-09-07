# 🧩 IEntityGizmos

`IEntityGizmos` is a behavior interface that allows drawing gizmos for an `IEntity` during the **editor or debug rendering phase**.  
It is automatically invoked by `SceneEntity.OnDrawGizmos()` or `SceneEntity.OnDrawGizmosSelected()` in the Unity Editor, allowing visualization of entity data in the scene view.

---

## Key Features

- **Editor Visualization** – Draw gizmos to debug or visualize entity state.
- **Strongly-Typed Option** – `IEntityGizmos<E>` allows type-specific gizmo logic.
- **Automatic Invocation** – Called automatically by Unity editor methods.
- **Composable** – Can be combined with other behaviours to visualize multiple aspects of an entity.

---

## Interface: IEntityGizmos
```csharp
public interface IEntityGizmos : IEntityBehaviour
{
    void DrawGizmos(IEntity entity);
}
```
---

## Interface: IEntityGizmos&lt;E&gt;
```csharp
public interface IEntityGizmos<in E> : IEntityGizmos where E : IEntity
{
    void DrawGizmos(E entity);
}
```
- Implements `IEntityGizmos.DrawGizmos(IEntity)` automatically by casting to `E`.
- Ensures type-safe gizmo logic for specific entity types.

---

## Example Usage
Draw a debug sphere at the entity's position in the editor

### Example #1. Non-Generic (IEntity)
```csharp
public class DrawSphereGizmo : IEntityGizmos
{
    public void DrawGizmos(IEntity entity)
    {
        var position = entity.GetValue<Vector3>("Position");
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(position, 0.5f);
    }
}
```

> Note: Assumes the entity has a `Position` value set.

### Example #2. Generic with UnitEntity (strongly-typed)
```csharp
public class DrawSphereGizmo : IEntityGizmos<UnitEntity>
{
    public void DrawGizmos(UnitEntity entity)
    {
        var position = entity.GetValue<Vector3>("Position");
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(position, 0.5f);
    }
}
```

> Note: Uses the strongly-typed `UnitEntity`, so no casting from `IEntity` is required.

---

## Remarks

- `IEntityGizmos` is intended for editor visualization and debug purposes only.
- `IEntityGizmos<E>` is useful when the behavior is specific to a particular entity type.
- Works only in the Unity Editor (`#if UNITY_5_3_OR_NEWER`).
- Can be used alongside other gizmo behaviours to visualize multiple entity aspects simultaneously.  
