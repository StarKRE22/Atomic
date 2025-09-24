# 🧩 SceneActionReference&lt;T1, T2&gt;

```csharp
public sealed class SceneActionReference<T1, T2> : IAction<T1, T2>
```

- **Description:**  A reference wrapper for a [SceneActionAbstract&lt;T1, T2&gt;](SceneActionAbstract%602.md) with <b>two parameters</b>.
- **Inheritance:** [IAction&lt;T1, T2&gt;](IAction%602.md)
- **Type parameters:**
    - `T1` — first argument
    - `T2` — second argument
- **Notes:** Supports Unity serialization and Odin Inspector
- **Usage:** Assign a `SceneActionAbstract<T1, T2>` component in the Inspector and invoke it using `Invoke()`.

---

## 🛠 Inspector Settings

| Parameter | Description                           |
|-----------|---------------------------------------|
| `action`  | The referenced scene action to invoke |

---

## 🏗️ Constructors

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
- **Parameter:** `action` — The `SceneActionAbstract<T1, T2>` to reference.

---

## 🧱 Fields

#### `action`

```csharp
public SceneActionAbstract<T1, T2> action;
```

- **Description:** Reference to the scene action to invoke.
- **Access:** Read / Write

---

## 🏹 Methods

#### `Invoke(T1, T2)`

```csharp
public void Invoke(T1 arg1, T2 arg2);
```

- **Description:** Invokes the referenced scene action with the provided arguments.
- **Parameters:**
    - `arg1` – The first argument
    - `arg2` – The second argument