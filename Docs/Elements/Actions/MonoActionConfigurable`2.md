# 🧩 MonoActionConfigurable&lt;T1, T2&gt;

Represents a scene-based composite action with <b>two parameters</b>. Attach to a `GameObject`, assign a list of
[IAction\<T1, T2>](IAction%602.md) implementations in the Unity Inspector, and they will be
invoked sequentially. Supports Odin Inspector

---

## 📑 Table of Contents

- [Inspector Settings](#-inspector-settings)
- [API Reference](#-api-reference)
    - [Type](#-type)
    - [Fields](#-fields)
        - [Actions](#actions)
    - [Methods](#-methods)
        - [Invoke(T1, T2)](#invoket1-t2)

---

## 🛠 Inspector Settings

| Parameter | Description                              |
|-----------|------------------------------------------|
| `actions` | The array of actions to execute in order |

---

## 🔍 API Reference

### 🏛️ Type <div id="-type"></div>

```csharp
public abstract class MonoActionConfigurable<T1, T2> : MonoAction<T1, T2>
```

- **Description:** Represents a scene-based composite action with <b>two parameters</b>.
- **Inheritance:** [MonoAction&lt;T1, T2&gt;](MonoAction%602.md)
- **Type parameters:**
    - `T1` — the first argument
    - `T2` — the second argument

---

### 🧱 Fields

#### `Actions`

```csharp
public IAction<T1, T2>[] actions;
```

- **Description:** The array of actions to invoke in order.
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
