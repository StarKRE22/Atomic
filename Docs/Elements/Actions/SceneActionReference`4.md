# 🧩 SceneActionReference&lt;T1, T2, T3, T4&gt;

A reference wrapper for a [SceneActionAbstract&lt;T1, T2, T3, T4&gt;](SceneActionAbstract%604.md)
Assign a `SceneActionAbstract<T1, T2, T3, T4>` component in the Inspector and invoke it using `Invoke()`.

---

## 📑 Table of Contents

- [Inspector Settings](#-inspector-settings)
- [API Reference](#-api-reference)
    - [Type](#-type)
    - [Constructors](#-constructors)
        - [Constructor()](#sceneactionreference)
        - [Constructor(SceneActionAbstract\<T1, T2, T3, T4>)](#sceneactionreferencesceneactionabstractt1-t2-t3-t4)
    - [Fields](#-fields)
        - [Action](#action)
    - [Methods](#-methods)
        - [Invoke(T1, T2, T3, T4)](#invoket1-t2-t3-t4)

---

## 🛠 Inspector Settings

| Parameter | Description                           |
|-----------|---------------------------------------|
| `action`  | The referenced scene action to invoke |

---

## 🔍 API Reference

### 🏛️ Type <div id="-type"></div>

```csharp
public sealed class SceneActionReference<T1, T2, T3, T4> : IAction<T1, T2, T3, T4>
```

- **Description:** A reference wrapper for a [SceneActionAbstract&lt;T1, T2, T3, T4&gt;](SceneActionAbstract%604.md)
  with <b>four parameters</b>.
- **Inheritance:** [IAction&lt;T1, T2, T3, T4&gt;](IAction%604.md)
- **Type parameters:**
    - `T1` — first argument
    - `T2` — second argument
    - `T3` — third argument
    - `T4` — fourth argument
- **Notes:** Supports Unity serialization and Odin Inspector

---

### 🏗️ Constructors <div id="-constructors"></div>

#### `SceneActionReference()`

```csharp
public SceneActionReference();
```

- **Description:** Default constructor, intended **only for use by the Unity Inspector**.
- **Usage:** Required for Unity to serialize the reference in the Inspector.

#### `SceneActionReference(SceneActionAbstract<T1, T2, T3, T4>)`

```csharp
[Serializable]
public SceneActionReference(SceneActionAbstract<T1, T2, T3, T4> action);
```

- **Description:** Creates a new reference wrapping the specified `SceneActionAbstract<T1, T2, T3, T4>`.
- **Parameter:** `action` — The `SceneActionAbstract<T1, T2, T3, T4>` to reference.

---

### 🧱 Fields

#### `Action`

```csharp
public SceneActionAbstract<T1, T2, T3, T4> action;
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