# 🧩 SceneEntityProxy

```csharp
[AddComponentMenu("Atomic/Entities/Entity Proxy")]
[DisallowMultipleComponent]
public class SceneEntityProxy : SceneEntityProxy<SceneEntity>
```

- **Description:**  Represents non-generic proxy component for exposing and interacting with
  a [SceneEntity](SceneEntity.md) in the Unity scene.
- **Inheritance:** extends [SceneEntityProxy&lt;E&gt;](SceneEntityProxy%601.md)
- **Note:** Supports Odin Inspector

---

## 🛠 Inspector Settings

| Parameter | Description                                                        |
|-----------|--------------------------------------------------------------------|
| `source`  | Reference to the actual `SceneEntity` object that this proxy wraps |

---

## 🔑 Properties

#### `Source`

```csharp
public SceneEntity Source { get; }
```

- **Description:** The source entity that this proxy forwards calls to.