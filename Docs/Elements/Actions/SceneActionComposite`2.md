
<details>
  <summary>
    <h2>🧩 SceneActionComposite&lt;T1, T2&gt;</h2>
    <br> Represents a composite scene action with <b>two parameters</b> that can be invoked.
  </summary>

<br>

```csharp
public class SceneActionComposite<T1, T2> : SceneActionAbstract<T1, T2>
```

- **Type parameters:**
    - `T1` — the first argument
    - `T2` — the second argument

---

### 🛠 Inspector Settings

| Parameter | Description                                                     |
|-----------|-----------------------------------------------------------------|
| `actions` | The array of actions to execute sequentially with two arguments |

---

### 🧱Fields

#### `actions`

```csharp
public SceneActionAbstract<T1, T2>[] actions;
```

- **Description:** The array of scene actions to invoke in order.
- **Access:** Read / Write

---

### 🏹 Methods

#### `Invoke(T1 arg1, T2 arg2)`

```csharp
public override void Invoke(T1 arg1, T2 arg2);
```

- **Description:** Executes each action sequentially with the provided arguments.
- **Parameters:**
    - `arg1` – The first argument
    - `arg2` – The second argument

</details>