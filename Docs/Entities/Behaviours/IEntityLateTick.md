# 🧩️ IEntityLateTick Interfaces

Represents a behavior interface that executes logic during the **late update cycle** of
an [IEntity](../Entities/IEntity.md). It is automatically invoked once per frame by the entity’s `LateTick()`, after all
regular updates have been processed. This phase is useful for camera follow, animation corrections, or logic that
depends on the latest entity positions.

<details>
  <summary>
    <h2 id="entity-late-tick"> 🧩 IEntityLateTick</h2>
    <br>Defines a behavior that executes logic during the late update phase of an <code>IEntity</code>.
  </summary>

<br>

```csharp
public interface IEntityLateTick : IEntityBehaviour
```

- **Inheritance:** implements [IEntityBehaviour](IEntityBehaviour.md)

---

### 🏹 Methods

#### `LateTick(IEntity, float)`

```csharp
void LateTick(IEntity entity, float deltaTime);
```

- **Description:** Called during the late update phase of the frame.
- **Parameters:**
    - `entity` – The entity being updated.
    - `deltaTime` – Elapsed time since the last frame.
- **Remarks:** Automatically called once per frame by `IEntity.LateTick()`.

---

### 🗂 Example of Usage

Make a camera follow an entity smoothly

```csharp
public class CameraFollowBehaviour : IEntityLateTick
{
    public void LateTick(IEntity entity, float deltaTime)
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

</details>

---

<details>
  <summary>
    <h2 id="entity-late-tick-t"> 🧩 IEntityLateTick&lt;E&gt;</h2>
    <br>Defines a strongly-typed behavior that executes late update logic on an <code>IEntity</code> of type <code>E</code>.
  </summary>

<br>

```csharp
public interface IEntityLateTick<in E> : IEntityLateTick where E : IEntity
```

- **Description:** Provides a strongly-typed version of `IEntityLateTick` for handling late update logic on a specific entity type.
- **Type Parameter:** `E` – The concrete entity type this behavior is associated with.
- **Inherits:** [IEntityLateTick](#entity-late-tick)
- **Remarks:** Automatically invoked by `IEntity.LateTick()` on entities of type `E`.

---

## 🏹 Methods

#### `LateTick(E, float)`

```csharp
void LateTick(E entity, float deltaTime);
```

- **Description:** Called during the late update phase of the frame for the strongly-typed entity.
- **Parameters:**
  - `entity` – The strongly-typed entity being updated.
  - `deltaTime` – Elapsed time since the last frame.
- **Remarks:** Implements the base `IEntityLateTick.LateTick(IEntity, float)` explicitly by casting the entity to type `E`.

---

### 🗂 Example of Usage

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

</details>

---

## 📝 Notes

- **Late Update Logic** – Encapsulates routines executed after all standard updates in the frame.
- **Strongly-Typed Option** – `IEntityLateTick<E>` allows type-specific late update logic.
- **Integration** – Called automatically by `IEntity.LateTick()`.
- **Composable** – Can be combined with other behaviours for modular entity logic.

- `IEntityLateTick` is intended for logic that depends on the final positions or states of entities in the frame.
- `IEntityLateTick<E>` is useful when the behaviour is specific to a particular entity type.
