# 🧩 SceneEntityProxy&lt;E&gt;

```csharp
public abstract class SceneEntityProxy<E> : MonoBehaviour, IEntity
    where E : SceneEntity
```

- **Description:** Represents a generic proxy components that forwards calls to an underlying <code>E</code> source
  entity
- **Type Parameter:** `E` — The type of the source entity, must inherit from [SceneEntity](SceneEntity.md)
- **Inheritance:** `MonoBehaviour`, [IEntity](IEntity.md)
- **Note:** Supports Odin Inspector

---

## 🛠 Inspector Settings

| Parameter | Description                                              |
|-----------|----------------------------------------------------------|
| `source`  | Reference to the actual `E` object that this proxy wraps |

---

## 🔑 Properties

#### `Source`

```csharp
public E Source { get; }
```

- **Description:** The source entity that this proxy forwards calls to.