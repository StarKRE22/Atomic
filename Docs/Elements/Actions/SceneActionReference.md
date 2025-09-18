# 🧩 SceneActionReference Classes

The `SceneActionReference` is **pointer** for [SceneActionAbstract](SceneActionAbstract.md). It is primarily used when a
game designer works with [SceneActionDefault](SceneActionDefault.md) and needs to reference or invoke another
`SceneActionDefault` from a different context. This wrapper implement the corresponding [IAction](IAction.md) interface
and can be used in **Inspector-driven workflows**.

> [!NOTE]  
> The reference only stores a pointer to a `SceneActionAbstract`. If the reference is null, invoking it does nothing.

---

<details>
  <summary>
    <h2>🧩 SceneActionReference</h2>
    <br> A parameterless reference wrapper for a <code>SceneActionAbstract</code>.
  </summary>

<br>

```csharp
public sealed class SceneActionReference : IAction
```

- **Usage:** Assign a `SceneActionAbstract` component in the Inspector and invoke it using `Invoke()`.

---

### 🛠 Inspector Settings

| Parameter | Description                             |
|-----------|-----------------------------------------|
| `action`  | Reference to the scene action to invoke |

---

### 🏗️ Constructors

#### `SceneActionReference()`

```csharp
public SceneActionReference();
```

- **Description:** Default constructor, intended **only for use by the Unity Inspector**.
- **Usage:** Required for Unity to serialize the reference in the Inspector.

#### `SceneActionReference(SceneActionAbstract)`

```csharp
public SceneActionReference(SceneActionAbstract action);
```

- **Description:** Creates a new reference wrapping the specified `SceneActionAbstract`.
- **Parameter:** `action` — The `SceneActionAbstract` to reference.

---

### 🧱 Fields

#### `action`

```csharp
public SceneActionAbstract action;
```

- **Description:** Reference to the scene action to invoke.
- **Access:** Read / Write

---

### 🏹 Methods

#### `Invoke()`

```csharp
public void Invoke();
```

- **Description:** Invokes the referenced scene action if it exists.

</details>

---

<details>
  <summary>
    <h2>🧩 SceneActionReference&lt;T&gt;</h2>
    <br> A reference wrapper for a <code>SceneActionAbstract&lt;T&gt;</code> with <b>one parameter</b>.
  </summary>

<br>

```csharp
public sealed class SceneActionReference<T> : IAction<T>
```

- **Type parameter:** `T` — the argument type.

---

### 🛠 Inspector Settings

| Parameter | Description                           |
|-----------|---------------------------------------|
| `action`  | The referenced scene action to invoke |

---

### 🏗️ Constructors

#### `SceneActionReference()`

```csharp
public SceneActionReference();
```

- **Description:** Default constructor, intended **only for use by the Unity Inspector**.
- **Usage:** Required for Unity to serialize the reference in the Inspector.

#### `SceneActionReference(SceneActionAbstract<T>)`

```csharp
public SceneActionReference(SceneActionAbstract<T> action);
```

- **Description:** Creates a new reference wrapping the specified `SceneActionAbstract<T>`.
- **Parameter:** `action` — The `SceneActionAbstract<T>` to reference.

---

### 🧱 Fields

#### `action`

```csharp
public SceneActionAbstract<T> action;
```

- **Description:** Reference to the scene action to invoke.
- **Access:** Read / Write

---

### 🏹 Methods

#### `Invoke(T)`

```csharp
public void Invoke(T arg);
```

- **Description:** Invokes the referenced scene action with the provided argument.
- **Parameter:** `arg` – The input argument.

</details>

---

<details>
  <summary>
    <h2>🧩 SceneActionReference&lt;T1, T2&gt;</h2>
    <br> A reference wrapper for a <code>SceneActionAbstract&lt;T1, T2&gt;</code> with <b>two parameters</b>.
  </summary>

<br>

```csharp
public sealed class SceneActionReference<T1, T2> : IAction<T1, T2>
```

- **Type parameters:**
    - `T1` — first argument
    - `T2` — second argument

---

### 🛠 Inspector Settings

| Parameter | Description                           |
|-----------|---------------------------------------|
| `action`  | The referenced scene action to invoke |

---

### 🏗️ Constructors

#### `SceneActionReference()`

```csharp
public SceneActionReference();
```

- **Description:** Default constructor, intended **only for use by the Unity Inspector**.
- **Usage:** Required for Unity to serialize the reference in the Inspector.

#### `SceneActionReference(SceneActionAbstract<T1, T2>)`

```csharp
public SceneActionReference(SceneActionAbstract<T1, T2> action);
```

- **Description:** Creates a new reference wrapping the specified `SceneActionAbstract<T1, T2>`.
- **Parameters:**
    - `action` — The `SceneActionAbstract<T1, T2>` to reference.

---

### 🧱 Fields

#### `action`

```csharp
public SceneActionAbstract<T1, T2> action;
```

- **Description:** Reference to the scene action to invoke.
- **Access:** Read / Write

---

### 🏹 Methods

#### `Invoke(T1, T2)`

```csharp
public void Invoke(T1 arg1, T2 arg2);
```

- **Description:** Invokes the referenced scene action with the provided arguments.
- **Parameters:**
    - `arg1` – The first argument
    - `arg2` – The second argument

</details>

---

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

---

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

---

## 🗂 Example of Usage

`SceneActionReference` is useful for creating a reference to another `SceneActionAbstract` via `[SerializeReference]`.

> [!WARNING]  
> Using `[SerializeReference]` should be considered a last resort. If possible, define actions through code for clarity
> and maintainability, as `[SerializeReference]` can be fragile during refactoring.

---

### 🔹 Non-generic Example

Below is an example of referencing a `SceneActionDefault` with a `HelloWorldSceneAction`.

<img src="../../Images/SceneActionReference.png" alt="SceneActionReference non-generic example" width="" height="128">

---

### 🔹 Generic Example

Below is an example of referencing a `DestroyGameObjectSceneAction` from the `GameObjectSceneActionDefault`.

<img src="../../Images/GameObjectSceneReference.png" alt="SceneActionReference generic example" width="" height="128">