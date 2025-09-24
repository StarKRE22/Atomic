
<details>
  <summary>
    <h2>🧩 SceneActionReference&lt;T1, T2, T3, T4&gt;</h2>
    <br> A reference wrapper for a <code>SceneActionAbstract&lt;T1, T2, T3, T4&gt;</code> with <b>four parameters</b>.
  </summary>

<br>

```csharp
public sealed class SceneActionReference<T1, T2, T3, T4> : IAction<T1, T2, T3, T4>
```

- **Description:** Reference wrapper for a scene action with **four parameters**.
- **Type parameters:** `T1`, `T2`, `T3`, `T4` — the arguments.

### 🛠 Inspector Settings

| Parameter | Type                               | Description                           |
|-----------|------------------------------------|---------------------------------------|
| `action`  | `SceneActionAbstract<T1,T2,T3,T4>` | The referenced scene action to invoke |

---

### 🏗️ Constructors

#### `SceneActionReference()`

```csharp
public SceneActionReference();
```

- **Description:** Default constructor, intended **only for use by the Unity Inspector**.
- **Usage:** Required for Unity to serialize the reference in the Inspector.

#### `SceneActionReference(SceneActionAbstract<T1, T2, T3, T4> action)`

```csharp
public SceneActionReference(SceneActionAbstract<T1, T2, T3, T4> action);
```

- **Description:** Creates a new reference wrapping the specified `SceneActionAbstract<T1, T2, T3, T4>`.
- **Parameter:** `action` — The `SceneActionAbstract<T1, T2, T3, T4>` to reference.

---

### 🧱 Fields

#### `action`

```csharp
public SceneActionAbstract<T1, T2, T3> action;
```

- **Description:** Reference to the scene action to invoke.
- **Access:** Read / Write

---

### 🏹 Methods

#### `Invoke(T1, T2, T3, T4)`

```csharp
public void Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4);
```

- **Description:** Invokes the referenced scene action with the provided arguments.
- **Parameters:**
    - `arg1` – The first argument
    - `arg2` – The second argument
    - `arg3` – The third argument
    - `arg4` – The fourth argument

</details>