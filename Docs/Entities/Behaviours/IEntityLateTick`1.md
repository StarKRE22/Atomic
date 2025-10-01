#  🧩 IEntityLateTick&lt;E&gt;

```csharp
public interface IEntityLateTick<in E> : IEntityLateTick where E : IEntity
```

- **Description:** Provides a strongly-typed version of `IEntityLateTick` for handling late update logic on a specific entity type.
- **Type Parameter:** `E` – The concrete entity type this behavior is associated with.
- **Inherits:** [IEntityLateTick](IEntityLateTick.md)
- **Remarks:** Automatically invoked by `IEntity.LateTick()` on entities of type `E`.

---

## 🏹 Methods

#### `LateTick(E, float)`

```csharp
public void LateTick(E entity, float deltaTime);
```

- **Description:** Called during the late update phase of the frame for the strongly-typed entity.
- **Parameters:**
    - `entity` – The strongly-typed entity being updated.
    - `deltaTime` – Elapsed time since the last frame.
- **Remarks:** Implements the base `IEntityLateTick.LateTick(IEntity, float)` explicitly by casting the entity to type `E`.

---

## 🗂 Example of Usage

Make a camera follow a `PlayerEntity` smoothly

```csharp
public class PlayerEntity : Entity
{
}
```

```csharp
public class CameraFollowBehaviour : IEntityLateTick<PlayerEntity>
{
    public void LateTick(PlayerEntity entity, float deltaTime)
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