# 🧩 SceneActionComposite&lt;T1, T2, T3, T4&gt;

```csharp
public class SceneActionComposite<T1, T2, T3, T4> : SceneActionAbstract<T1, T2, T3, T4>
```

- **Description:** Composite scene action with **four parameters**.
- **Inheritance:** [SceneActionAbstract&lt;T1, T2, T3, T4&gt;](SceneActionAbstract%604.md)
- **Type parameters:**
    - `T1` — first argument
    - `T2` — second argument
    - `T3` — third argument
    - `T4` — fourth argument
- **Notes:**
  - Supports Odin Inspector
  - Attach to a `GameObject`, assign a list of `SceneActionAbstract<T1, T2, T3, T4>` implementations in the Inspector, and
    they will be invoked sequentially.
---

## 🛠 Inspector Settings

| Parameter | Description                                                      |
|-----------|------------------------------------------------------------------|
| `actions` | The array of actions to execute sequentially with four arguments |

---

## 🧱 Fields

#### `Actions`

```csharp
public SceneActionComposite<T1, T2, T3, T4>[] actions;
```

- **Description:** The array of scene actions to invoke in order.
- **Access:** Read / Write

---

### 🏹 Methods

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