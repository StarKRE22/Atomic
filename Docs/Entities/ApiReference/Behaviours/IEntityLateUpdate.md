# 🧩️  IEntityLateUpdate

`IEntityLateUpdate` is a behavior interface that executes logic during the **late update cycle** of an `IEntity`.  
It is automatically invoked by the entity’s `OnLateUpdate` method once per frame, after all regular updates have been processed.  
This phase is useful for things like camera follow, animation corrections, or logic that depends on the latest entity positions.

---

## Key Features

- **Post-Update Logic** – Executes routines after all standard updates in the frame.
- **Strongly-Typed Option** – `IEntityLateUpdate<E>` allows type-specific late update logic.
- **Automatic Invocation** – Called automatically by `IEntity.OnLateUpdate`.
- **Composable** – Can be combined with other behaviours for modular entity logic.

---

## Interface: IEntityLateUpdate

```csharp
public interface IEntityLateUpdate : IEntityBehaviour
{
    void LateUpdate(IEntity entity, float deltaTime);
}
```
---

## Interface: IEntityLateUpdate&lt;E&gt;

```csharp
public interface IEntityLateUpdate<in E> : IEntityLateUpdate where E : IEntity
{
    void LateUpdate(E entity, float deltaTime);
}
```

- Implements `IEntityLateUpdate.LateUpdate(IEntity)` automatically by casting to `E`.
- Ensures type-safe late update logic for specific entity types.

---

## Example Usage
Make a camera follow a `PlayerEntity` smoothly:

### Example #1. Non-Generic (IEntity)
```csharp
public class CameraFollowBehaviour : IEntityLateUpdate
{
    public void LateUpdate(IEntity entity, float deltaTime)
    {
        var camera = entity.GetValue<Camera>("Camera");
        var target = entity.GetValue<Transform>("Target");
        var smoothSpeed = entity.GetValue<float>("SmoothSpeed");
    
        camera.transform.position = Vector3.Lerp(
            camera.transform.position,
            target.position,
            smoothSpeed * deltaTime
        );
    }
}
```

> Note: Assumes `Camera` and `Target` are set on the entity.

### Example #2. Generic with PlayerEntity (strongly-typed)
```csharp
public class CameraFollowBehaviour : IEntityLateUpdate<PlayerEntity>
{
    public void LateUpdate(PlayerEntity entity, float deltaTime)
    {
        var camera = entity.GetValue<Camera>("Camera");
        var target = entity.GetValue<Transform>("Target");
        var smoothSpeed = entity.GetValue<float>("SmoothSpeed");
    
        camera.transform.position = Vector3.Lerp(
            camera.transform.position,
            target.position,
            smoothSpeed * deltaTime
        );
    }
}
```

> Note: Uses the strongly-typed `PlayerEntity`, so no casting from `IEntity` is required.

---

## Remarks

- `IEntityLateUpdate` is intended for logic that depends on the final positions or states of entities in the frame.
- `IEntityLateUpdate<E>` is useful when the behaviour is specific to a particular entity type.
- Behaviours can interact with other entity behaviours during the late update phase.
- Does not handle initialization, enabling, disabling, updating, fixed update, or disposal; separate interfaces exist for those phases (`IEntityInit`, `IEntityEnable`, `IEntityDisable`, `IEntityUpdate`, `IEntityFixedUpdate`, `IEntityDispose`).