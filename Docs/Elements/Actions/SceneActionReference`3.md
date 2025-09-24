
<details>
  <summary>
    <h2>🧩 SceneActionReference&lt;T1, T2, T3&gt;</h2>
    <br> A reference wrapper for a <code>SceneActionAbstract&lt;T1, T2, T3&gt;</code> with <b>three parameters</b>.
  </summary>

<br>

```csharp
public sealed class SceneActionReference<T1, T2, T3> : IAction<T1, T2, T3>
```

- **Type parameters:** `T1`, `T2`, `T3` — the arguments.

---

### 🛠 Inspector Settings

| Parameter | Type                            | Description                           |
|-----------|---------------------------------|---------------------------------------|
| `action`  | `SceneActionAbstract<T1,T2,T3>` | The referenced scene action to invoke |

---

### 🏗️ Constructors

#### `SceneActionReference()`

```csharp
public SceneActionReference();
```

- **Description:** Default constructor, intended **only for use by the Unity Inspector**.
- **Usage:** Required for Unity to serialize the reference in the Inspector.

#### `SceneActionReference(SceneActionAbstract<T1, T2, T3> action)`

```csharp
public SceneActionReference(SceneActionAbstract<T1, T2, T3> action);
```

- **Description:** Creates a new reference wrapping the specified `SceneActionAbstract<T1, T2, T3>`.
- **Parameter:** `action` — The `SceneActionAbstract<T1, T2, T3>` to reference.

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

#### `Invoke(T1, T2, T3)`

```csharp
public void Invoke(T1 arg1, T2 arg2, T3 arg3);
```

- **Description:** Invokes the referenced scene action with the provided arguments.
- **Parameters:**
    - `arg1` – The first argument
    - `arg2` – The second argument
    - `arg3` – The third argument

</details>