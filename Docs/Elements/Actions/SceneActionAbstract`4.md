# 🧩 SceneActionAbstract&lt;T1, T2, T3, T4&gt;

```csharp
public abstract class SceneActionAbstract<T1, T2, T3, T4> : MonoBehaviour, IAction<T1, T2, T3, T4>
```

- **Description:** Represents a scene action with **four parameters**.
- **Inheritance:** `MonoBehaviour`, [IAction&lt;T1, T2, T3, T4&gt;](IAction%604.md)
- **Type parameters:**
    - `T1` — the first argument
    - `T2` — the second argument
    - `T3` — the third argument
    - `T4` — the fourth argument
- **Note:** Attach to a GameObject and implement `Invoke(T1, T2, T3, T4)` to define custom behavior.

---

## 🏹 Methods

#### `Invoke(T1, T2, T3, T4)`

```csharp
public abstract void Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4);
```

- **Description:** Executes the action logic with the provided arguments.
- **Parameters:**
    - `arg1` – The first argument
    - `arg2` – The second argument
    - `arg3` – The third argument
    - `arg4` – The fourth argument