# 🧩 MonoActionComposite&lt;T1, T2, T3, T4&gt;

Composite scene action with **four parameters**.

---

## 📑 Table of Contents

- [API Reference](#-api-reference)
  - [Type](#-type)
  - [Inspector Settings](#-inspector-settings)
  - [Fields](#-fields)
    - [Actions](#actions)
  - [Methods](#-methods)
    - [Invoke(T1, T2, T3, T4)](#invoket1-t2-t3-t4)

---

## 🔍 API Reference

### 🏛️ Type <div id="-type"></div>

```csharp
public class MonoActionComposite<T1, T2, T3, T4> : MonoAction<T1, T2, T3, T4>
```

- **Description:** Composite scene action with **four parameters**.
- **Inheritance:** [MonoAction&lt;T1, T2, T3, T4&gt;](MonoAction%604.md)
- **Type parameters:**
    - `T1` — first argument
    - `T2` — second argument
    - `T3` — third argument
    - `T4` — fourth argument
- **Notes:**
  - Supports Odin Inspector
  - Attach to a `GameObject`, assign a list of `MonoAction<T1, T2, T3, T4>` implementations in the Inspector, and
    they will be invoked sequentially.
---

### 🛠 Inspector Settings

| Parameter | Description                                                      |
|-----------|------------------------------------------------------------------|
| `actions` | The array of actions to execute sequentially with four arguments |

---

### 🧱 Fields

#### `Actions`

```csharp
public MonoActionComposite<T1, T2, T3, T4>[] actions;
```

- **Description:** The array of scene actions to invoke in order.
- **Access:** Read / Write

---

### 🏹 Methods

#### `Invoke(T1, T2, T3, T4)`

```csharp
public override void Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4);
```

- **Description:** Executes each action sequentially with the provided arguments.
- **Parameters:**
    - `arg1` – The first argument
    - `arg2` – The second argument
    - `arg3` – The third argument
    - `arg4` – The fourth argument
