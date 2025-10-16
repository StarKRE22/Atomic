# 🧩️ IEntityLateTick

Represents a behavior interface that executes logic during the **late update cycle** of
an [IEntity](../Entities/IEntity.md). This phase is useful for rendering including camera follow, animation corrections,
playing particles and sound effects.

---

## 📑 Table of Contents

- [Example of Usage](#-example-of-usage)
- [API Reference](#-api-reference)
    - [Type](#-type)
    - [Methods](#-methods)
        - [LateTick(IEntity, float)](#latetickientity-float)

---

## 🗂 Example of Usage

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

---

## 🔍 API Reference

### 🏛️ Type <div id="-type"></div>

```csharp
public interface IEntityLateTick : IEntityBehaviour
```

- **Inheritance:** implements [IEntityBehaviour](IEntityBehaviour.md)
- **Note:** It is automatically invoked once per frame by the entity’s `LateTick()`, after all
  regular updates have been processed.
- **See also:** [IEntityLateTick&lt;E&gt;](IEntityLateTick%601.md)

---

### 🏹 Methods

#### `LateTick(IEntity, float)`

```csharp
public void LateTick(IEntity entity, float deltaTime);
```

- **Description:** Called during the late update phase of the frame.
- **Parameters:**
    - `entity` – The entity being updated.
    - `deltaTime` – Elapsed time since the last frame.
- **Remarks:** Automatically called once per frame by `IEntity.LateTick()`.