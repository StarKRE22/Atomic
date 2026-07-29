# 🧩 MonoActionComposite&lt;T1, T2&gt;

Represents a composite scene action with <b>two parameters</b> that can be invoked.

---

## 📑 Table of Contents

- [API Reference](#-api-reference)
    - [Type](#-type)
    - [Inspector Settings](#-inspector-settings)
    - [Fields](#-fields)
        - [Actions](#actions)
    - [Methods](#-methods)
        - [Invoke(T1, T2)](#invoket1-t2)

---

## 🔍 API Reference

### 🏛️ Type <div id="-type"></div>

```csharp
public class MonoActionComposite<T1, T2> : MonoAction<T1, T2>
```

- **Description:** Represents a composite scene action with <b>two parameters</b> that can be invoked.
- **Inheritance:** [MonoAction&lt;T1, T2&gt;](MonoAction%602.md)
- **Type parameters:**
    - `T1` — the first argument
    - `T2` — the second argument
- **Notes:**
    - Supports Odin Inspector
    - Attach to a `GameObject`, assign a list of `MonoAction<T1, T2>` implementations in the Inspector, and
      they will be invoked sequentially.

---

### 🛠 Inspector Settings

| Parameter | Description                                                     |
|-----------|-----------------------------------------------------------------|
| `actions` | The array of actions to execute sequentially with two arguments |

---

### 🧱 Fields

#### `Actions`

```csharp
public MonoAction<T1, T2>[] actions;
```

- **Description:** The array of scene actions to invoke in order.
- **Access:** Read / Write

---

### 🏹 Methods

#### `Invoke(T1, T2)`

```csharp
public override void Invoke(T1 arg1, T2 arg2);
```

- **Description:** Executes each action sequentially with the provided arguments.
- **Parameters:**
    - `arg1` – The first argument
    - `arg2` – The second argument
