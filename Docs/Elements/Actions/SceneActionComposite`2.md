# 🧩 SceneActionComposite&lt;T1, T2&gt;

```csharp
public class SceneActionComposite<T1, T2> : SceneActionAbstract<T1, T2>
```

- **Description:** Represents a composite scene action with <b>two parameters</b> that can be invoked.
- **Inheritance:** [SceneActionAbstract&lt;T1, T2&gt;](SceneActionAbstract%602.md)
- **Type parameters:**
    - `T1` — the first argument
    - `T2` — the second argument
- **Notes:**
    - Supports Odin Inspector
    - Attach to a `GameObject`, assign a list of `SceneActionAbstract<T1, T2>` implementations in the Inspector, and
      they will be invoked sequentially.

---

### 🛠 Inspector Settings

| Parameter | Description                                                     |
|-----------|-----------------------------------------------------------------|
| `actions` | The array of actions to execute sequentially with two arguments |

---

## 🧱 Fields

#### `actions`

```csharp
public SceneActionAbstract<T1, T2>[] actions;
```

- **Description:** The array of scene actions to invoke in order.
- **Access:** Read / Write

---

## 🏹 Methods

#### `Invoke(T1, T2)`

```csharp
public override void Invoke(T1 arg1, T2 arg2);
```

- **Description:** Executes each action sequentially with the provided arguments.
- **Parameters:**
    - `arg1` – The first argument
    - `arg2` – The second argument