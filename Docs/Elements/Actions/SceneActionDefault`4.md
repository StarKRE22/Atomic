# 🧩 SceneActionDefault&lt;T1, T2, T3, T4&gt;

```csharp
public abstract class SceneActionDefault<T1, T2, T3, T4> : SceneActionAbstract<T1, T2, T3, T4>
```

- **Description:** Represents a scene-based composite action with <b>four parameters</b>.
- **Inheritance:** [SceneActionAbstract&lt;T1, T2, T3, T4&gt;](SceneActionAbstract%604.md)
- **Type parameters:**
    - `T1` — the first argument
    - `T2` — the second argument
    - `T3` — the third argument
    - `T4` — the fourth argument
- **Notes:**
  - Supports Odin Inspector
  - Attach to a `GameObject`, assign a list of `IAction` implementations in the `Inspector`, and they will be
    invoked sequentially.
---

## 🛠 Inspector Settings

| Parameter | Description                              |
|-----------|------------------------------------------|
| `actions` | The array of actions to execute in order |

---

## 🏹 Methods

#### `Invoke(T1, T2, T3, T4)`

```csharp
public override void Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4);
```

- **Description:** Executes each action sequentially with the provided arguments.
- **Parameters:**
    - `arg1` – The first argument
    - `arg2` – The second argument
    - `arg3` – The third argument
    - `arg4` – The fourth argument