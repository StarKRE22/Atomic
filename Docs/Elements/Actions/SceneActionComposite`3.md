
<details>
  <summary>
    <h2>🧩 SceneActionComposite&lt;T1, T2, T3&gt;</h2>
    <br> Represents a composite scene action with <b>three parameters</b> that can be invoked.
  </summary>

<br>

```csharp
public class SceneActionComposite<T1, T2, T3> : SceneActionAbstract<T1, T2, T3>
```

- **Description:** Composite scene action with **three parameters**.
- **Type parameters:**
    - `T1` — first argument
    - `T2` — second argument
    - `T3` — third argument

---

### 🛠 Inspector Settings

| Parameter | Description                                                       |
|-----------|-------------------------------------------------------------------|
| `actions` | The array of actions to execute sequentially with three arguments |

---

### 🧱Fields

#### `actions`

```csharp
public SceneActionComposite<T1, T2, T3>[] actions;
```

- **Description:** The array of scene actions to invoke in order.
- **Access:** Read / Write

---

### 🏹 Methods

#### `Invoke(T1 arg1, T2 arg2, T3 arg3)`

```csharp
public override void Invoke(T1 arg1, T2 arg2, T3 arg3);
```

- **Description:** Executes each action sequentially with the provided arguments.
- **Parameters:**
    - `arg1` – The first argument
    - `arg2` – The second argument
    - `arg3` – The third argument

</details>