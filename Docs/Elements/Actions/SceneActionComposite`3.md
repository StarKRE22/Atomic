# 🧩 SceneActionComposite&lt;T1, T2, T3&gt;

Composite scene action with **three parameters**.

---

## 📑 Table of Contents

- [API Reference](#-api-reference)
  - [Type](#-type)
  - [Inspector Settings](#-inspector-settings)
  - [Fields](#-fields)
    - [Actions](#actions)
  - [Methods](#-methods)
    - [Invoke(T1, T2, T3)](#invoket1-t2-t3)

---

## 🔍 API Reference

### 🏛️ Type <div id="-type"></div>

```csharp
public class SceneActionComposite<T1, T2, T3> : SceneActionAbstract<T1, T2, T3>
```

- **Description:** Composite scene action with **three parameters**.
- **Inheritance:** [SceneActionAbstract&lt;T1, T2, T3&gt;](SceneActionAbstract%603.md)
- **Type parameters:**
    - `T1` — first argument
    - `T2` — second argument
    - `T3` — third argument
- **Notes:**
    - Supports Odin Inspector
    - Attach to a `GameObject`, assign a list of `SceneActionAbstract<T1, T2, T3>` implementations in the Inspector, and
      they will be invoked sequentially.

---

### 🛠 Inspector Settings

| Parameter | Description                                                       |
|-----------|-------------------------------------------------------------------|
| `actions` | The array of actions to execute sequentially with three arguments |

---

### 🧱 Fields

#### `Actions`

```csharp
public SceneActionComposite<T1, T2, T3>[] actions;
```

- **Description:** The array of scene actions to invoke in order.
- **Access:** Read / Write

---

### 🏹 Methods

#### `Invoke(T1, T2, T3)`

```csharp
public override void Invoke(T1 arg1, T2 arg2, T3 arg3);
```

- **Description:** Executes each action sequentially with the provided arguments.
- **Parameters:**
    - `arg1` – The first argument
    - `arg2` – The second argument
    - `arg3` – The third argument