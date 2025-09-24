# 🧩 SceneActionDefault&lt;T1, T2, T3&gt;

```csharp
public abstract class SceneActionDefault<T1, T2, T3> : SceneActionAbstract<T1, T2, T3>
```

- **Description:** Represents a scene-based composite action with <b>three parameters</b>.
- **Inheritance:** [SceneActionAbstract&lt;T1, T2, T3&gt;](SceneActionAbstract%603.md)
- **Type parameters:**
    - `T1` — the first argument
    - `T2` — the second argument
    - `T3` — the third argument
- **Notes:**
    - Supports Odin Inspector
    - Attach to a `GameObject`, assign a list of `IAction<T1, T2, T3>` implementations in the `Inspector`, and they will be
      invoked sequentially.

---

## 🛠 Inspector Settings

| Parameter | Description                              |
|-----------|------------------------------------------|
| `actions` | The array of actions to execute in order |

---

## 🧱Fields

#### `actions`

```csharp
public IAction<T1, T2, T3>[] actions;
```

- **Description:** The array of actions to invoke in order.
- **Access:** Read / Write

---

## 🏹 Methods

#### `Invoke(T1, T2, T3)`

```csharp
public override void Invoke(T1 arg1, T2 arg2, T3 arg3);
```

- **Description:** Executes each action sequentially with the provided arguments.
- **Parameters:**
    - `arg1` – The first argument
    - `arg2` – The second argument
    - `arg3` – The third argument