# 🧩 SceneEntity Destruction

Provides methods of how to destroy entities at runtime.

---

## 🏹 Methods

#### `Destroy(IEntity, float)`

```csharp
public static void Destroy(IEntity entity, float t = 0)  
```

- **Description:** Destroys the associated `GameObject` of the specified `IEntity` if it can be cast to a `SceneEntity`.
- **Parameters:**
    - `entity` – The entity whose `GameObject` should be destroyed.
    - `t` – Optional delay in seconds before destruction. Defaults to `0`.
- **Note:** Internally casts the `IEntity` to `SceneEntity` before destroying.

#### `Destroy(SceneEntity, float)`

```csharp
public static void Destroy(SceneEntity entity, float t = 0)  
```

- **Description:** Destroys the specified `SceneEntity`'s `GameObject` after an optional delay.
- **Parameters:**
    - `entity` – The `SceneEntity` to destroy.
    - `t` – Optional delay in seconds before destruction. Defaults to `0`.
- **Note:** If `entity` is `null`, no action is taken.

---

## 🗂 Example of Usage

```csharp
// Destroys entity after 3 seconds
SceneEntity.Destroy(sceneEntity, 3f);
```