# 🧩 SceneActionDefault&lt;T1, T2&gt;

```csharp
public abstract class SceneActionDefault<T1, T2> : SceneActionAbstract<T1, T2>
```

- **Description:** Represents a scene-based composite action with <b>two parameters</b>.
- **Inheritance:** [SceneActionAbstract&lt;T1, T2&gt;](SceneActionAbstract%602.md)
- **Type parameters:**
    - `T1` — the first argument
    - `T2` — the second argument
- **Notes:**
    - Supports Odin Inspector
    - Attach to a `GameObject`, assign a list of `IAction<T1, T2>` implementations in the `Inspector`, and they will be
      invoked sequentially.

---

## 🛠 Inspector Settings

| Parameter | Description                              |
|-----------|------------------------------------------|
| `actions` | The array of actions to execute in order |

---

## 🧱Fields

#### `Actions`

```csharp
public IAction<T1, T2>[] actions;
```

- **Description:** The array of actions to invoke in order.
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