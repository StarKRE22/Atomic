
<details>
  <summary>
    <h2 id="-scene-entity-proxy-t"> 🧩 SceneEntityProxy&lt;E&gt;</h2>
    <br> Represents a generic proxy components that forwards calls to an underlying <code>E</code> source entity
  </summary>

<br>

```csharp
public abstract class SceneEntityProxy<E> : MonoBehaviour, IEntity
    where E : SceneEntity
```

- **Type Parameter:** `E` — The type of the source entity, must inherit from [SceneEntity](SceneEntity.md)
- **Inheritance:** derived from `MonoBehaviour` and implemented [IEntity](IEntity.md)

---

### 🛠 Inspector Settings

| Parameter | Description                                              |
|-----------|----------------------------------------------------------|
| `source`  | Reference to the actual `E` object that this proxy wraps |

---

### 🔑 Properties

#### `Source`

```csharp
public E Source { get; }
```

- **Description:** The source entity that this proxy forwards calls to.

</details>

---